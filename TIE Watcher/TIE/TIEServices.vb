Namespace TIE

    ''' <summary>
    ''' Class responsible for all TIE Fighter-related services, serving as the intermediary between the main program and the DosBoxServices class.
    ''' </summary>
    Public Class TIEServices

#Region " --- Constants --- "

        ''' <summary>
        ''' String constant used for throwing exceptions when a process is not open, cannot be reached, or DOSBox is not running TIE.EXE
        ''' </summary>
        Private Const NO_OPEN_PROCESS As String = "Data cannot be read from the DOSBox process because the target process does not exist, cannot be reached, or is not running TIE.EXE."

        ' Square dimensions of CraftType icons
        Private Const CRAFT_ICON_SIZE As Integer = 16

#Region " DOS Memory Offsets "

        ' Offset of MISS_FlightGroup array for current mission
        Private Const DOS_OFFSET_MISS_FG_ARRAY = &H29F5D8
        ' Offset of MISS_Header struct for current mission
        Private Const DOS_OFFSET_MISS_HEADER = &H2A343E
        ' Offset of Space_Object array for current mission
        Private Const DOS_OFFSET_SPACE_OBJECT_ARRAY = &H2A37C8
        ' Offset of Weapon_Object array for current mission
        Private Const DOS_OFFSET_WEAPON_OBJECT_ARRAY = &H2AA5E0
        ' Offset of Space Object Definition array
        Private Const DOS_OFFSET_SPACE_OBJECT_DEFINITION_ARRAY = &H28BAF8
        ' Offset of MISS_GlobalGoal array for current mission
        Private Const DOS_OFFSET_MISS_GLOBAL_GOAL_ARRAY = &H2A33D0
        ' Offset of current Player's Difficulty setting
        Private Const DOS_OFFSET_PLAYER_DIFFICULTY_SETTING = &H2AB48B
        ' Offset of FlightGroupStatus array for current mission
        Private Const DOS_OFFSET_FLIGHT_GROUP_STATUS_ARRAY = &H29ECD8
        ' Offset of Primary, Secondary, Bonus Mission Status Array
        Private Const DOS_OFFSET_MISSION_STATUS_ARRAY = &H2AB3F2
        ' Offset of Player's SpaceObject index
        Private Const DOS_OFFSET_PLAYER_OBJECT_INDEX = &H2AB06C
        ' Offset of Player's FlightGroup index
        Private Const DOS_OFFSET_PLAYER_FLIGHT_GROUP_INDEX = &H2AB06E
        ' Offset of Player's Craft pointer
        Private Const DOS_OFFSET_PLAYER_CRAFT_POINTER = &H2AB068
        ' Offset of Player's SpaceObject pointer
        Private Const DOS_OFFSET_PLAYER_OBJECT_POINTER = &H2AB064
        ' Offset of Player's CMD Target Index
        Private Const DOS_OFFSET_PLAYER_CMD_TARGET_INDEX = &H2AB076

#End Region

#End Region
#Region " --- Variables --- "

#Region " Copies from Process "

        ' MissionHeader struct from a loaded mission
        Private Shared _missionHeader As MissionHeader
        ' MissionFlightGroup structs from a loaded mission
        Private Shared _missionFlightGroup(47) As MissionFlightGroup
        ' SpaceObject structs from a loaded mission
        Private Shared _spaceObject(119) As SpaceObject
        ' MissionGlobalGoal structs
        Private Shared _missionGlobalGoal(2) As MissionGlobalGoal
        ' FlightGroupStatus structs
        Private Shared _flightGroupStatus(47) As FlightGroupStatus
        ' WeaponObject structs
        Private Shared _weaponObject(63) As WeaponObject
        ' SpaceObjectDefinition structs
        Private Shared _spaceObjectDefinition(160) As SpaceObjectDefinition

#End Region
#Region " Manufactured Stuff "

        ' 16 x 16 bitmaps used for displaying craft types
        Private Shared craftIcons() As Bitmap

        ' craft Icon outline boxes, like those used in TIE Fighter briefings
        Private Shared craftIconBoxes() As Bitmap

#End Region
#Region " Code Injection/Assembly Tracking "

        ' whether or not the hack to disable Craft Panel damage has been injected
        Private Shared injectedCraftPanelDamageHack As Boolean

#End Region

        ' DOSBox instance
        Private Shared WithEvents DOS As DosBoxServices

        ' Whether or not a DOSBox process is available
        Private Shared processAvailable As Boolean = False

        ' whether or not the icons have been initialized
        Private Shared initedIcons As Boolean = False

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ' Invoked the first time TIEServices is used in code -- which may or may not actually be at the time of application launch
        ' To ensure this happens as early as possible, this class should be referenced ASAP
        Shared Sub New()
            ' create the initial internal structs
            _missionHeader = New MissionHeader
            For i As Integer = 0 To 47
                _missionFlightGroup(i) = New MissionFlightGroup
                _flightGroupStatus(i) = New FlightGroupStatus
            Next
            For i As Integer = 0 To 119
                _spaceObject(i) = New SpaceObject
            Next
            For i As Integer = 0 To 2
                _missionGlobalGoal(i) = New MissionGlobalGoal
            Next
            For i As Integer = 0 To 63
                _weaponObject(i) = New WeaponObject
            Next
            For i As Integer = 0 To 160
                _spaceObjectDefinition(i) = New SpaceObjectDefinition
            Next
        End Sub

#End Region

#Region " Private Methods "

#Region " Initialization "

        ''' <summary>
        ''' Initializes the craft icons
        ''' </summary>
        Private Shared Sub initIcons()
            If initedIcons Then Exit Sub

            ' create the bitmaps/icons used for displaying craft types, like the icons displayed during briefings
            'Dim lfd As New LFD("resources\player.lfd")
            ' use the embedded resource
            Dim lfd As New LFD(ResourceHelper.GetEmbeddedResourceStream("PLAYER.LFD"))
            'Dim lfd As New LFD(My.Application.)

            Dim palRange As PaletteResource = lfd.LoadPalette("range")
            Dim anim(3) As AnimationResource
            anim(0) = lfd.LoadAnimation("iconsgrn")
            anim(1) = lfd.LoadAnimation("iconsred")
            anim(2) = lfd.LoadAnimation("iconsblu")
            anim(3) = lfd.LoadAnimation("iconspur")
            '' create the color matrix for the drop shadow
            'Dim colorMatrix As New Imaging.ColorMatrix
            'colorMatrix.Matrix00 = 0.2
            'colorMatrix.Matrix11 = 0.2
            'colorMatrix.Matrix22 = 0.2
            'colorMatrix.Matrix33 = 1
            '' create the image attributes object for the drop shadow
            'Dim imageAttributes As New Imaging.ImageAttributes
            'imageAttributes.SetColorMatrix(colorMatrix, Imaging.ColorMatrixFlag.Default, Imaging.ColorAdjustType.Bitmap)

            craftIcons = Array.CreateInstance(GetType(Bitmap), anim(0).FrameCount * 4)
            For i As Integer = 0 To anim(0).FrameCount - 1
                For j As Integer = 0 To 3
                    ' create the new bitmap and a graphics object to draw to it
                    craftIcons(i * 4 + j) = New Bitmap(CRAFT_ICON_SIZE, CRAFT_ICON_SIZE)
                    Dim g As Graphics = Graphics.FromImage(craftIcons(i * 4 + j))
                    ' determine which source image to use
                    Dim src As Bitmap
                    src = anim(j).Frames(i, palRange, True, True)

                    ' clear the target with a transparent background
                    g.Clear(Color.Transparent)
                    '' draw the source image's outline
                    Dim x As Integer = (CRAFT_ICON_SIZE - 20) >> 1
                    Dim y As Integer = (CRAFT_ICON_SIZE - 26) >> 1
                    ''g.DrawImage(src, New Rectangle(x, y + 1, 20, 26), 0, 0, 20, 26, GraphicsUnit.Pixel, imageAttributes)
                    ''g.DrawImage(src, New Rectangle(x, y - 1, 20, 26), 0, 0, 20, 26, GraphicsUnit.Pixel, imageAttributes)
                    ''g.DrawImage(src, New Rectangle(x + 1, y, 20, 26), 0, 0, 20, 26, GraphicsUnit.Pixel, imageAttributes)
                    ''g.DrawImage(src, New Rectangle(x - 1, y, 20, 26), 0, 0, 20, 26, GraphicsUnit.Pixel, imageAttributes)
                    ' draw the source 20 x 26 image centered within the graphics view
                    g.DrawImage(src, x, y)
                    ' dispose of the graphics object
                    g.Dispose()
                Next 'j
            Next 'i

            ' Create the craft icon boxes
            craftIconBoxes = Array.CreateInstance(GetType(Bitmap), 4)
            For i As Integer = 0 To 3
                craftIconBoxes(i) = New Bitmap(CRAFT_ICON_SIZE, CRAFT_ICON_SIZE)
                Dim g As Graphics = Graphics.FromImage(craftIconBoxes(i))

                ' clear the image with the appropriate background color
                g.Clear(palRange.Colors(&HE0 + i * 8))

                ' draw the outline with the appropriate color
                g.DrawRectangle(New Pen(palRange.Colors(&HE7 + i * 8)), 0, 0, CRAFT_ICON_SIZE - 1, CRAFT_ICON_SIZE - 1)
                ' dispose of the graphics object
                g.Dispose()
            Next 'i

            initedIcons = True
        End Sub

#End Region
#Region " Local Struct Updating Methods "

        Private Shared Sub updateMissionHeader()
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_MISS_HEADER, MissionHeader.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _missionHeader.UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateMissionFlightGroup(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_MISS_FG_ARRAY + MissionFlightGroup.SizeOf * index, MissionFlightGroup.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _missionFlightGroup(index).UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateSpaceObject(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_SPACE_OBJECT_ARRAY + SpaceObject.SizeOf * index, SpaceObject.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _spaceObject(index).UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateMissionGlobalGoal(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_MISS_GLOBAL_GOAL_ARRAY + MissionGlobalGoal.SizeOf * index, MissionGlobalGoal.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _missionGlobalGoal(index).UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateFlightGroupStatus(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_FLIGHT_GROUP_STATUS_ARRAY + TIE.FlightGroupStatus.SizeOf * index, TIE.FlightGroupStatus.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _flightGroupStatus(index).UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateWeaponObject(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_WEAPON_OBJECT_ARRAY + WeaponObject.SizeOf * index, WeaponObject.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _weaponObject(index).UpdateBytes(bytes)
        End Sub

        Private Shared Sub updateSpaceObjectDefinition(index As Integer)
            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(DOS_OFFSET_SPACE_OBJECT_DEFINITION_ARRAY + SpaceObjectDefinition.SizeOf * index, SpaceObjectDefinition.SizeOf)
            ' TODO: Handle read errors
            If Not bytes Is Nothing Then _spaceObjectDefinition(index).UpdateBytes(bytes)
        End Sub

#End Region
#Region " Methods for Reading Basic Data Types "

        Private Shared Function readPlayerDifficultySetting() As Byte
            Return DOS.ReadDosMemoryByte(DOS_OFFSET_PLAYER_DIFFICULTY_SETTING)
        End Function

        Private Shared Function readPlayerObjectIndex() As UInt16
            Return DOS.ReadDosMemoryUInt16(DOS_OFFSET_PLAYER_OBJECT_INDEX)
        End Function

        Private Shared Function readPlayerFlightGroupIndex() As Byte
            Return DOS.ReadDosMemoryByte(DOS_OFFSET_PLAYER_FLIGHT_GROUP_INDEX)
        End Function

        Private Shared Function readPlayerCraftPointer() As UInt32
            Return DOS.ReadDosMemoryUInt32(DOS_OFFSET_PLAYER_CRAFT_POINTER)
        End Function

        Private Shared Function readPlayerSpaceObjectPointer() As UInt32
            Return DOS.ReadDosMemoryUInt32(DOS_OFFSET_PLAYER_OBJECT_POINTER)
        End Function

        Private Shared Function readPlayerCMDTargetIndex() As UInt16
            Return DOS.ReadDosMemoryUInt16(DOS_OFFSET_PLAYER_CMD_TARGET_INDEX)
        End Function

#End Region

        ' Generates a basic time string given a number of seconds
        Private Shared Function basicTimeString(sec As Integer) As String
            Dim m As Integer = Math.Floor(sec / 60)
            Dim s As Integer = sec Mod 60

            Return String.Format("{0:D}:{1:D02}", m, s)
        End Function

        ' returns whether or not a DOSBox process can be reached and is running TIE.EXE
        Private Shared Function processIsRunningTIE() As Boolean
            ' if process is not available return false
            If Not processAvailable Then Return False

            ' check if DOSBox is running TIE.EXE
            If Not DOS.CurrentProgramName.Equals("TIE") Then Return False

            ' all other checksh have passed, return true
            Return True
        End Function

        ' Generates a human-readable string describing a MissionTriggerAmount
        Private Shared Function triggerAmountDescription(value As MissionTriggerAmount, Optional ByRef isPlural As Boolean = Nothing) As String
            Select Case value
                Case MissionTriggerAmount.AllButOne
                    isPlural = True
                    Return "All but one"
                Case MissionTriggerAmount.AllNonPlayerCraft
                    isPlural = True
                    Return "All non-player craft"
                Case MissionTriggerAmount.AllNonSpecialCraft
                    isPlural = True
                    Return "All non-special craft"
                Case MissionTriggerAmount.AtLeastOne
                    isPlural = False
                    Return "At least one"
                Case MissionTriggerAmount.FirstWave100Percent
                    isPlural = True
                    Return "100% of First Wave"
                Case MissionTriggerAmount.FirstWave25Percent
                    isPlural = True
                    Return "25% of First Wave"
                Case MissionTriggerAmount.FirstWave50Percent
                    isPlural = True
                    Return "50% of First Wave"
                Case MissionTriggerAmount.FirstWave75Percent
                    isPlural = True
                    Return "75% of First Wave"
                Case MissionTriggerAmount.FirstWaveAllButOne
                    isPlural = True
                    Return "All but one of First Wave"
                Case MissionTriggerAmount.FirstWaveAtLeastOne
                    isPlural = False
                    Return "At least one of First Wave"
                Case MissionTriggerAmount.Percent100
                    isPlural = True
                    Return "100%"
                Case MissionTriggerAmount.Percent25
                    isPlural = True
                    Return "25%"
                Case MissionTriggerAmount.Percent50
                    isPlural = True
                    Return "50%"
                Case MissionTriggerAmount.Percent75
                    isPlural = True
                    Return "75%"
                Case MissionTriggerAmount.PlayerCraft
                    isPlural = False
                    Return "Player's craft"
                Case MissionTriggerAmount.SpecialCraft
                    isPlural = False
                    Return "Special craft"
                Case Else
                    Return String.Format("UNHANDLED: {0}", value)
            End Select
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing the variable of a MissionTrigger.
        ''' </summary>
        ''' <param name="trigger"></param>
        ''' <param name="includeAmount">Whether or not the trugger amount should be included.</param>
        ''' <param name="isPlural">Boolean variable to receive whether or not the string should be treated as plural.</param>
        ''' <returns></returns>
        Private Shared Function triggerVarDescription(trigger As MissionTrigger, includeAmount As Boolean, ByRef isPlural As Boolean) As String
            ' NOTE: I guess with primitive data types passed by reference it's not required to check if it's Nothing before it is asigned
            Dim sb As New Text.StringBuilder

            Select Case trigger.VariableType
                Case MissionVariableType.CraftType
                    If includeAmount Then
                        sb.AppendFormat("{0} of all ", triggerAmountDescription(trigger.TriggerAmount, isPlural))
                    End If
                    sb.AppendFormat("{0}", SpaceObjectTypeInfo.Types(trigger.Variable + 1).NamePlural)

                Case MissionVariableType.CraftWhen
                    Select Case trigger.Variable
                        Case MissionCraftWhen.PlayerCraft
                            sb.Append("Player's Craft")

                        Case Else
                            sb.AppendFormat("UNHANDLED CRAFT_WHEN {0}", trigger.Variable)

                    End Select

                Case MissionVariableType.FlightGroup
                    ' force an update of the flight group information
                    updateMissionFlightGroup(trigger.Variable)

                    Dim fg As MissionFlightGroup = _missionFlightGroup(trigger.Variable)

                    ' handle cases where the flight group has only 1 member
                    If fg.NumCraft = 1 Then
                        ' write it as a singular
                        sb.AppendFormat("[{0} {1}]",
                                    SpaceObjectTypeInfo.Types(fg.CraftType).CMD,
                                    fg.Name)
                    Else
                        If includeAmount Then
                            sb.AppendFormat("{0} of ", triggerAmountDescription(trigger.TriggerAmount, isPlural))
                        End If
                        ' write it as a flight group
                        sb.AppendFormat("Flight Group [{0} {1}]",
                                    SpaceObjectTypeInfo.Types(fg.CraftType).CMD,
                                    fg.Name)
                    End If

                    ' force non-plural
                    isPlural = False

                Case MissionVariableType.GlobalGroup
                    ' perhaps in the future I could also generate the list of all FlightGroups that fall into this global group,
                    ' but that might make the string too long
                    If includeAmount Then
                        sb.AppendFormat("{0} of ", triggerAmountDescription(trigger.TriggerAmount, isPlural))
                    End If
                    sb.AppendFormat("Global Group [{0}]", trigger.Variable)
                    ' force non-plural
                    isPlural = False

                Case MissionVariableType.IFF
                    ' force an update of the mission header information
                    updateMissionHeader()
                    ' write the variable amount
                    If includeAmount Then
                        sb.AppendFormat("{0} of all ", triggerAmountDescription(trigger.TriggerAmount, isPlural))
                    End If
                    ' write the IFF
                    sb.AppendFormat("{0} craft", IFFName(trigger.Variable))

                Case Else
                    sb.AppendFormat("UNHANDLED: {0} {1}", trigger.VariableType, trigger.Variable)

            End Select
            Return sb.ToString
        End Function

        ' Internal method for generating mission trigger description
        Private Shared Function generateTriggerDescription(trigger As MissionTrigger) As String
            Dim sb As New Text.StringBuilder
            Dim isPlural As Boolean

            Select Case trigger.Condition
                Case MissionTriggerCondition.Always
                    sb.Append("Always")

                Case MissionTriggerCondition.Arrived
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have Arrived")
                    Else
                        sb.Append(" has Arrived")
                    End If

                Case MissionTriggerCondition.Attacked
                    ' NOTE: So far I have only seen quantities of 100%
                    ' I may need to delve into the TIE Fighter code again to see whether trigger amounts have an effect on this type
                    sb.Append(triggerVarDescription(trigger, False, isPlural))
                    If isPlural Then
                        sb.Append(" have been Attacked")
                    Else
                        sb.Append(" has been Attacked")
                    End If

                Case MissionTriggerCondition.Boarded
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have been Boarded")
                    Else
                        sb.Append(" has been Boarded")
                    End If

                Case MissionTriggerCondition.Captured
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have been Captured")
                    Else
                        sb.Append(" has been Captured")
                    End If

                Case MissionTriggerCondition.CompletedMission
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have Completed Mission")
                    Else
                        sb.Append(" has Completed Mission")
                    End If

                Case MissionTriggerCondition.CompletedBonusGoals
                    sb.Append("Completed Bonus Goals")

                Case MissionTriggerCondition.CompletedPrimaryGoals
                    sb.Append("Completed Primary Goals")

                Case MissionTriggerCondition.CompletedSecondaryGoals
                    sb.Append("Completed Seconday Goals")

                Case MissionTriggerCondition.Created
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have Arrived")
                    Else
                        sb.Append(" has Arrived")
                    End If

                Case MissionTriggerCondition.Destroyed
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have been Destroyed")
                    Else
                        sb.Append(" has been Destroyed")
                    End If

                Case MissionTriggerCondition.Disabled
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have been Disabled")
                    Else
                        sb.Append(" has been Disabled")
                    End If

                Case MissionTriggerCondition.Docked
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have Docked")
                    Else
                        sb.Append(" has Docked")
                    End If

                Case MissionTriggerCondition.DroppedOff
                    ' NOTE: This one's kinda weird, so the sentence syntax will be fucked up
                    sb.Append("Dropped Off by ")
                    sb.Append(triggerVarDescription(trigger, False, isPlural))
                'sb.Append(triggerVarDescription(trigger))
                'If isPlural Then
                '    sb.Append(" have been Dropped Off")
                'Else
                '    sb.Append(" has been Dropped Off")
                'End If

                Case MissionTriggerCondition.FailedBonusGoals
                    sb.Append("Failed Bonus Goals")

                Case MissionTriggerCondition.FailedPrimaryGoals
                    sb.Append("Failed Primary Goals")

                Case MissionTriggerCondition.FailedSecondaryGoals
                    sb.Append("Failed Secondary Goals")

                Case MissionTriggerCondition.HullDamage
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have < 50% Hull")
                    Else
                        sb.Append(" has < 50% Hull")
                    End If

                Case MissionTriggerCondition.Inspected
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have been Inspected")
                    Else
                        sb.Append(" has been Inspected")
                    End If

                Case MissionTriggerCondition.NoShields
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have 0% Shields")
                    Else
                        sb.Append(" has 0% Shields")
                    End If

                Case MissionTriggerCondition.OutOfWarheads
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" are out of Warheads")
                    Else
                        sb.Append(" is out of Warheads")
                    End If

                Case MissionTriggerCondition.Reinforced
                    ' NOTE: Presumably this is when the player calls for Reinforcements.
                    ' TODO: Check if trigger variables are used or not -- for now I assume they are not
                    sb.Append("Reinforcements called")
                'sb.Append(triggerVarDescription(trigger))
                'sb.Append(" has been reinforced")

                Case MissionTriggerCondition.None
                    sb.Append("Never")

                Case MissionTriggerCondition.Survived
                    sb.Append(triggerVarDescription(trigger, True, isPlural))
                    If isPlural Then
                        sb.Append(" have Survived")
                    Else
                        sb.Append(" has Survived")
                    End If

                Case Else
                    ' unhandled
                    sb.AppendFormat("UNHANDLED: {0}", trigger.Condition)
            End Select

            Return sb.ToString
        End Function

        ' Internal method for generating order target description
        ' TODO: Make things a bit more elegant, for example if target_type(0) is IFF and target_type(1) is CraftCategory,
        ' have it print "Rebel Starfighters", rather than "Rebel craft AND Starfighters"
        Private Shared Function generateOrderTargetDescription(order As MissionOrder) As String
            Dim sb As New Text.StringBuilder

            ' kind of a crude way to do this, but loop logic simplifies some stuff
            ' outer loop is for target pair 1, 2 then 3, 4
            ' inner loop is for target within the pair
            For j As Integer = 0 To 1
                For i As Integer = 0 To 1
                    Dim target As Byte
                    Dim target_type As MissionVariableType
                    Dim booleanOp As Int16

                    ' determine the target, target_type and boolean for this part of the loop
                    target = order.Target(j * 2 + i)
                    target_type = order.TargetType(j * 2 + i)
                    If j = 0 Then
                        booleanOp = order.Target1Or2
                    Else
                        booleanOp = order.Target3or4
                    End If

                    ' if we're on the 1st item of the 1st pair and the target type is none, print out something at least
                    If j = 0 And i = 0 And target_type = MissionVariableType.None Then
                        sb.Append("No targets")
                    End If

                    ' skip this target type if it's None
                    If target_type = MissionVariableType.None Then Continue For

                    ' if we're on the 1st item of the 2nd pair, print a separator of some sort
                    If j = 1 And i = 0 Then
                        sb.Append(", also ")
                    End If

                    ' if we're on the 2nd item of the pair, handle the boolean
                    If i = 1 Then
                        If booleanOp = 0 Then
                            ' AND operator
                            sb.Append(" AND ")
                        Else
                            ' OR operator
                            sb.Append(" OR ")
                        End If
                    End If

                    Select Case target_type
                        Case MissionVariableType.CraftCategory
                            Select Case target
                                Case MissionCraftCategory.Freighters
                                    sb.Append("Freighters")
                                Case MissionCraftCategory.Mines
                                    sb.Append("Mines")
                                Case MissionCraftCategory.Platforms
                                    sb.Append("Platforms")
                                Case MissionCraftCategory.Starfighters
                                    sb.Append("Starfighters")
                                Case MissionCraftCategory.Starships
                                    sb.Append("Starships")
                                Case MissionCraftCategory.Transports
                                    sb.Append("Transports")
                                Case MissionCraftCategory.Utility
                                    sb.Append("Utility Craft")
                            End Select

                        Case MissionVariableType.CraftType
                            sb.AppendFormat("{0}", SpaceObjectTypeInfo.Types(target + 1).NamePlural)

                        Case MissionVariableType.CraftWhen
                            Select Case target
                                Case MissionCraftWhen.Boarded
                                    sb.Append("Boarded craft")
                                Case MissionCraftWhen.Boarding
                                    sb.Append("Boarding craft")
                                Case MissionCraftWhen.Defence
                                    sb.Append("Defense craft")
                                Case MissionCraftWhen.Disabled
                                    sb.Append("Disabled craft")
                                Case MissionCraftWhen.NonPlayerCraft
                                    sb.Append("Non-Player craft")
                                Case MissionCraftWhen.NonSpecialCraft
                                    sb.Append("Non-Special craft")
                                Case MissionCraftWhen.PlayerCraft
                                    sb.Append("Player craft")
                                Case MissionCraftWhen.SpecialCraft
                                    sb.Append("Special craft")
                                Case Else
                                    sb.AppendFormat("UNHANDLED CRAFT_WHEN {0}", target)
                            End Select

                        Case MissionVariableType.FlightGroup
                            ' force an update of the flight group information
                            updateMissionFlightGroup(target)
                            Dim fg As MissionFlightGroup = _missionFlightGroup(target)
                            ' handle cases where the FG contains only 1 craft
                            If fg.NumCraft = 1 Then
                                sb.AppendFormat("[{0} {1}]", SpaceObjectTypeInfo.Types(fg.CraftType).CMD, fg.Name)
                            Else
                                sb.AppendFormat("Flight Group [{0} {1}]", SpaceObjectTypeInfo.Types(fg.CraftType).CMD, fg.Name)
                            End If

                        Case MissionVariableType.GlobalGroup
                            sb.AppendFormat("Global Group [{0}]", target)

                        Case MissionVariableType.IFF
                            ' force an update of the mission header information
                            updateMissionHeader()
                            Select Case target
                                Case 0
                                    sb.Append("Rebel craft")
                                Case 1
                                    sb.Append("Imperial craft")
                                Case Else
                                    sb.AppendFormat("{0} craft", IFFName(target))
                            End Select

                        Case Else
                            sb.AppendFormat("UNHANDLED: {0} {1}", target_type, target)

                    End Select

                Next 'i

            Next 'j

            Return sb.ToString
        End Function

        ' Internal method for generating goal amount description
        Private Shared Function generateGoalAmountDescription(amount As MissionGoalAmount) As String
            Select Case amount
                Case MissionGoalAmount.AllButOne
                    Return "All but one"
                Case MissionGoalAmount.AtLeastOne
                    Return "At least one"
                Case MissionGoalAmount.Percent100
                    Return "100%"
                Case MissionGoalAmount.Percent50
                    Return "50%"
                Case MissionGoalAmount.SpecialCraft
                    Return "Special craft"
                Case Else
                    Return String.Format("UNHANDLED {0}", amount)
            End Select
        End Function

#End Region
#Region " Public Methods "

        ''' <summary>
        ''' Opens the specified Process, ideally a running instance of DOSBox, for reading TIE Fighter data from.
        ''' </summary>
        ''' <param name="proc">Process representing a running instance of DOSBox.</param>
        Public Shared Sub OpenProcess(proc As Process)
            ' handle argument exceptions
            If proc Is Nothing Then Throw New ArgumentNullException("proc")

            ' initialize any sub-services that have yet to init
            initIcons()

            ' for now just go ahead with this
            DOS = New DosBoxServices(proc)

            ' TODO: Handle cases when TIE.EXE is not being run in DOSBox
            processAvailable = True
        End Sub

        ''' <summary>
        ''' Closes the underlying process.
        ''' </summary>
        Public Shared Sub CloseProcess()
            If Not processAvailable Then Throw New InvalidOperationException("Cannot close a process that is not open.")

            DOS.Close()
            DOS = Nothing
            processAvailable = False
        End Sub

        ''' <summary>
        ''' Returns a pointer into DOS memory based off a TIE Fighter XMEM Handle.
        ''' </summary>
        ''' <param name="handle"></param>
        ''' <returns></returns>
        Public Shared Function GetXMEMPointer(handle As UInt16) As UInt32
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)
            If handle = 0 Then Throw New ArgumentOutOfRangeException("handle", "Handle must not be zero.")

            ' TODO: Maybe tidy this up so I'm not just reading selected bits and pieces
            handle -= 1
            Dim heapIndex As Integer = handle >> &HC
            Dim handleIndex As Integer = handle And &HFFF

            ' read the pointer to the XMEM_Core struct
            Dim pXMEM_Core As UInt32 = DOS.ReadDosMemoryUInt32(&H292E90)
            Dim pHandleList As UInt32 = DOS.ReadDosMemoryUInt32(pXMEM_Core + &H102)
            Dim handleOffset As UInt32 = DOS.ReadDosMemoryUInt32(pHandleList + handleIndex * 8)

            Return DOS.ReadDosMemoryUInt32(pXMEM_Core + 16 * heapIndex) + handleOffset
        End Function

        ''' <summary>
        ''' Checks a MissionTrigger based on the game's current state and returns whether the trigger's condition is Incomplete, Complete, or Failed.
        ''' GoalState.None will be returned if the MissionTrigger is not a valid goal trigger.
        ''' This requires an open DOSBox process, so an exception will be thrown if a connection is not available.
        ''' </summary>
        ''' <param name="trigger"></param>
        ''' <returns></returns>
        Public Shared Function CheckTriggerGoalState(trigger As MissionTrigger) As GoalState
            ' handle arguments
            If trigger Is Nothing Then Throw New ArgumentNullException("trigger")
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

            Dim totalCraft As Integer = 0
            Dim totalSpecialCraft As Integer = 0
            Dim totalComplete As Integer = 0
            Dim totalFailed As Integer = 0
            Dim totalSpecialComplete As Integer = 0
            Dim totalSpecialFailed As Integer = 0

            ' loop through all of the flight groups and start counting things
            updateMissionHeader()

            For fgIndex As Integer = 0 To _missionHeader.NumFlightGroups - 1
                ' update the flight group mission info and the flight group status
                updateMissionFlightGroup(fgIndex)
                updateFlightGroupStatus(fgIndex)

                Dim fg As MissionFlightGroup = _missionFlightGroup(fgIndex)
                Dim fgStatus As FlightGroupStatus = _flightGroupStatus(fgIndex)

                ' handle the trigger variable type
                Select Case trigger.VariableType
                    Case MissionVariableType.CraftType
                        ' skip this flight group if the craft type doesn't match
                        If fg.CraftType <> trigger.Variable + 1 Then Continue For

                    Case MissionVariableType.FlightGroup
                        ' easy, if the index doesn't match, skip
                        If fgIndex <> trigger.Variable Then Continue For

                    Case MissionVariableType.IFF
                        ' skip this FG if the IFF doesn't match
                        If fg.IFF <> trigger.Variable Then Continue For

                    Case MissionVariableType.Order
                        ' skip this FG if it has no orders of the matching type
                        Dim hasOrder As Boolean = False
                        For i As Integer = 0 To 2
                            If fg.Orders(i).Order = trigger.Variable Then
                                hasOrder = True
                                Exit For
                            End If
                        Next i
                        If hasOrder = False Then Continue For

                    Case MissionVariableType.CraftCategory
                        Dim soDef As SpaceObjectDefinition = TIEServices.SpaceObjectDefinitions(fg.CraftType)
                        ' skip this FG if the craft type doesn't meet the category type
                        'If soDef.Category <> trigger.Variable Then Continue For
                        Select Case trigger.Variable
                            Case MissionCraftCategory.Starfighters
                                If soDef.Category <> SpaceObjectCategory.Fighter Then Continue For
                            Case MissionCraftCategory.Freighters
                                If soDef.Category <> SpaceObjectCategory.Freight Then Continue For
                            Case MissionCraftCategory.Mines
                                If soDef.Category <> SpaceObjectCategory.Mine Then Continue For
                            Case MissionCraftCategory.Platforms
                                If soDef.Category <> SpaceObjectCategory.Platform Then Continue For
                            Case MissionCraftCategory.Starships
                                If soDef.Category <> SpaceObjectCategory.Capital Then Continue For
                            Case MissionCraftCategory.Transports
                                If soDef.Category <> SpaceObjectCategory.Transport Then Continue For
                            Case MissionCraftCategory.Utility
                                If soDef.Category <> SpaceObjectCategory.Utility Then Continue For
                            Case Else
                                ' unhandled variable, throw exception
                                Throw New ArgumentException(String.Format("MissionVariableType.CraftCategory : MissionTrigger.Variable UNHANDLED: {0}", trigger.Variable))
                        End Select

                    Case Else
                        ' unhandled variable type, throw exception
                        Throw New ArgumentException(String.Format("MissionTrigger.VariableType UNHANDLED: {0}", trigger.VariableType))
                End Select

                ' the flight group meets the trigger's criteria
                ' add the flight group's total size to the craft tally
                totalCraft += fgStatus.TotalCraft
                totalSpecialCraft += fgStatus.TotalSpecialCraft
                ' total things up based on the trigger condition
                Select Case trigger.Condition
                    Case MissionTriggerCondition.Created
                        totalComplete += fgStatus.TotalCreated
                        totalSpecialComplete += fgStatus.TotalSpecialCreated
                    Case MissionTriggerCondition.Destroyed
                        totalComplete += fgStatus.TotalDestroyed
                        totalSpecialComplete += fgStatus.TotalSpecialDestroyed
                        totalFailed += fgStatus.Field_B + fgStatus.Field_1D + fgStatus.Field_C + fgStatus.Field_1E
                        totalSpecialFailed += fgStatus.Field_B + fgStatus.Field_1D + fgStatus.Field_C + fgStatus.Field_1E
                    Case MissionTriggerCondition.Attacked
                        totalComplete += fgStatus.TotalAttackedComplete
                        totalSpecialFailed += fgStatus.TotalSpecialAttackedComplete
                        totalFailed += fgStatus.TotalAttackedFailed
                        totalSpecialFailed += fgStatus.TotalSpecialAttackedFailed
                    Case MissionTriggerCondition.Captured
                        totalComplete += fgStatus.TotalCapturedComplete
                        totalSpecialFailed += fgStatus.TotalSpecialCapturedComplete
                        totalFailed += fgStatus.TotalCapturedFailed
                        totalSpecialFailed += fgStatus.TotalSpecialCapturedFailed
                    Case MissionTriggerCondition.Inspected
                        totalComplete += fgStatus.TotalInspectedComplete
                        totalSpecialFailed += fgStatus.TotalSpecialInspectedComplete
                        totalFailed += fgStatus.TotalInspectedFailed
                        totalSpecialFailed += fgStatus.TotalSpecialInspectedFailed
                    Case MissionTriggerCondition.Boarded
                        totalComplete += fgStatus.TotalBoardedComplete
                        totalSpecialFailed += fgStatus.TotalSpecialBoardedComplete
                        totalFailed += fgStatus.TotalBoardedFailed
                        totalSpecialFailed += fgStatus.TotalSpecialBoardedFailed
                    Case MissionTriggerCondition.Docked
                        totalComplete += fgStatus.TotalDockedComplete
                        totalSpecialFailed += fgStatus.TotalSpecialDockedComplete
                        totalFailed += fgStatus.TotalDockedFailed
                        totalSpecialFailed += fgStatus.TotalSpecialDockedFailed
                    Case MissionTriggerCondition.Disabled
                        totalComplete += fgStatus.TotalDisabledComplete
                        totalSpecialFailed += fgStatus.TotalSpecialDisabledComplete
                        totalFailed += fgStatus.TotalDisabledFailed
                        totalSpecialFailed += fgStatus.TotalSpecialDisabledFailed
                    Case MissionTriggerCondition.Survived
                        totalComplete += fgStatus.TotalCreated - fgStatus.TotalDestroyed
                        totalSpecialFailed += fgStatus.TotalSpecialCreated - fgStatus.TotalSpecialDestroyed
                        totalFailed += fgStatus.TotalDestroyed
                        totalSpecialFailed += fgStatus.TotalSpecialDestroyed
                    Case MissionTriggerCondition.CompletedMission
                        totalComplete += fgStatus.Field_B + fgStatus.Field_C
                        totalSpecialFailed += fgStatus.Field_1D + fgStatus.Field_1E
                        totalFailed += fgStatus.TotalDestroyed
                        totalSpecialFailed += fgStatus.TotalSpecialDestroyed
                    Case Else
                        ' unhandled condition type, throw exception
                        Throw New ArgumentException(String.Format("MissionTrigger.Condition UNHANDLED: {0}", trigger.Condition))
                End Select
            Next 'i

            ' now that the totals are complete, handle the trigger amount
            Select Case trigger.TriggerAmount
                Case MissionTriggerAmount.Percent100
                    If totalComplete = totalCraft Then
                        Return GoalState.Complete
                    ElseIf totalFailed > 0 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.Percent75
                    If totalComplete / totalCraft >= 0.75 Then
                        Return GoalState.Complete
                    ElseIf totalFailed / totalCraft > 0.25 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.Percent50
                    If totalComplete / totalCraft >= 0.5 Then
                        Return GoalState.Complete
                    ElseIf totalFailed / totalCraft > 0.5 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.Percent25
                    If totalComplete / totalCraft >= 0.25 Then
                        Return GoalState.Complete
                    ElseIf totalFailed / totalCraft > 0.75 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.AtLeastOne, MissionTriggerAmount.FirstWaveAtLeastOne
                    If totalComplete > 0 Then
                        Return GoalState.Complete
                    ElseIf totalFailed = totalCraft Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.AllButOne
                    If totalComplete = totalCraft - 1 Then
                        Return GoalState.Complete
                    ElseIf totalFailed > 1 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.SpecialCraft
                    If totalSpecialCraft = 0 Then Return GoalState.Incomplete
                    If totalSpecialComplete = totalSpecialCraft Then
                        Return GoalState.Complete
                    ElseIf totalSpecialFailed > 0 Then
                        Return GoalState.Failed
                    End If

                Case MissionTriggerAmount.AllNonSpecialCraft
                    ' This looks messy because I copied its layout from the game's disassembly without thinking too hard
                    If totalCraft - totalSpecialCraft <> totalComplete Then
                        If totalFailed = 0 OrElse totalSpecialComplete = 0 Then
                            If totalFailed <= 1 OrElse totalSpecialComplete = 0 Then
                                Return GoalState.Incomplete
                            Else
                                Return GoalState.Failed
                            End If
                        Else
                            Return GoalState.Failed
                        End If
                    Else
                        Return GoalState.Complete
                    End If

                Case Else
                    ' unhandled trigger amount, throw an exception
                    Throw New ArgumentException(String.Format("MissionTrigger.TriggerAmount UNHANDLED: {0}", trigger.TriggerAmount))
            End Select

            ' goal was neither completed nor failed
            Return GoalState.Incomplete
        End Function

#Region " Description String Generating Methods "


        ''' <summary>
        ''' Generates a human-readable string describing the criteria for a MissionTrigger.
        ''' This requires an open DOSBox process, so an exception will be thrown if a connection is not available.
        ''' </summary>
        ''' <param name="trigger"></param>
        ''' <returns></returns>
        Public Overloads Shared Function MissionTriggerDescription(trigger As MissionTrigger) As String
            ' handle arguments
            If trigger Is Nothing Then Throw New ArgumentNullException("trigger")
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

            Dim sb As New Text.StringBuilder

            sb.Append(generateTriggerDescription(trigger))
            sb.Append(".")

            Return sb.ToString
        End Function

        Public Overloads Shared Function MissionTriggerDescription(trigger1 As MissionTrigger, trigger2 As MissionTrigger, booleanOp As Int16) As String
            ' handle arguments
            If trigger1 Is Nothing Then Throw New ArgumentNullException("trigger1")
            If trigger2 Is Nothing Then Throw New ArgumentNullException("trigger2")

            Dim sb As New Text.StringBuilder

            ' kind of a crude way to do this, but I need loop logic
            For i As Integer = 0 To 1
                Dim trigger As MissionTrigger

                If i = 0 Then
                    ' Always print the first trigger condition
                    trigger = trigger1
                Else
                    trigger = trigger2
                    ' only print the 2nd trigger condition if it would make sense, based on the 2nd trigger and boolean operator

                    ' do not print the 2nd trigger if it's Always
                    If trigger.Condition = MissionTriggerCondition.Always Then Continue For
                    ' do not print the 2nd trigger if boolean is OR and 2nd trigger is Never
                    If booleanOp <> 0 And trigger.Condition = MissionTriggerCondition.None Then Continue For

                    If booleanOp = 0 Then
                        ' AND operator
                        sb.Append(" AND ")
                    Else
                        ' OR operator
                        sb.Append(" OR ")
                    End If

                End If

                sb.Append(generateTriggerDescription(trigger))
            Next 'i

            ' append a period
            sb.Append(".")
            ' return the built string
            Return sb.ToString
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a MissionOrder.
        ''' This requires an open DOSBox process, so an exception will be thrown if a connection is not available.
        ''' </summary>
        ''' <param name="order"></param>
        ''' <returns></returns>
        Public Shared Function MissionOrderDescription(order As MissionOrder) As String
            ' handle arguments
            If order Is Nothing Then Throw New ArgumentNullException("order")
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

            Dim sb As New Text.StringBuilder

            Select Case order.Order
                Case MissionOrderType.AwaitBoarding
                    sb.AppendFormat("Await Boarding, {0} time{1}",
                                order.Variable1,
                                IIf(order.Variable1 = 1, "", "s"))

                Case MissionOrderType.Attack
                    sb.AppendFormat("Attack, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.AttackEscorts
                    sb.AppendFormat("Attack Escorts, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardAndCaptureCargo
                    sb.AppendFormat("Board and Capture Cargo, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 > 1, "s", ""),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardAndDestroyCargo
                    sb.AppendFormat("Board and Destroy Cargo, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 > 1, "s", ""),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardAndExchangeCargo
                    sb.AppendFormat("Board and Exchange Cargo, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 > 1, "s", ""),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardAndGiveCargo
                    sb.AppendFormat("Board and Give Cargo, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardAndTakeCargo
                    sb.AppendFormat("Board and Take Cargo, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.BoardToRepair
                    sb.AppendFormat("Board to Repair, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.Circle
                    sb.AppendFormat("Circle {0} time{1}, at {2}% throttle",
                                order.Variable1,
                                IIf(order.Variable1 = 1, "", "s"),
                                order.Throttle * 10)
                ' TODO: Determine if this has target criteria -- from a cursory inspection of orders scripts it does not appear to

                Case MissionOrderType.CircleAndEvade
                    sb.AppendFormat("Circle and Evade {0} time{1}, at {2}% throttle",
                                order.Variable1,
                                IIf(order.Variable1 = 1, "", "s"),
                                order.Throttle * 10)
                ' TODO: Determine if this has target criteria -- from a cursory inspection of orders scripts it does not appear to

                Case MissionOrderType.Disable
                    sb.AppendFormat("Disable, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.Disabled
                    sb.AppendFormat("Disabled, until Boarded {0} time{1}",
                                order.Variable1,
                                IIf(order.Variable1 = 1, "", "s"))

                Case MissionOrderType.DropOff
                    sb.AppendFormat("Drop Off, at {0}% throttle, Flight Group [{1} {2}]",
                                order.Throttle * 10,
                                SpaceObjectTypeInfo.Types(_missionFlightGroup(order.Variable2 - 1).CraftType).CMD,
                                _missionFlightGroup(order.Variable2 - 1).Name)

                Case MissionOrderType.Escort
                    sb.AppendFormat("Escort, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.GoHome
                    sb.AppendFormat("Go Home, at {0}% throttle", order.Throttle * 10)

                Case MissionOrderType.HoldStation
                    ' TODO: Determine if this an indefinite thing or it can be disrupted by Abort or Departure triggers
                    sb.AppendFormat("Hold Station")

                Case MissionOrderType.PickUp
                    sb.AppendFormat("Pick Up, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.Protect
                    sb.AppendFormat("Protect, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.Rendezvous
                    sb.AppendFormat("Rendezvous, at {0}% throttle, docking {1} time{2}, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"))

                Case MissionOrderType.SS_AwaitReturn
                    ' NOTE: In spite of there being Throttle values, the craft appears to remain stationary
                    sb.AppendFormat("SS Await Return, ")
                    ' This does indeed have target criteria
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_Board
                    sb.AppendFormat("SS Board, at {0}% throttle, docking {1} time{2} for {3} seconds, ",
                                order.Throttle * 10,
                                order.Variable2,
                                IIf(order.Variable2 = 1, "", "s"),
                                order.Variable1 * 5)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_HoldStation
                    sb.Append("SS Hold Station")

                Case MissionOrderType.SS_GoHome
                    sb.AppendFormat("SS Go Home, at {0}% throttle", order.Throttle * 10)

                Case MissionOrderType.SS_Launch
                    sb.AppendFormat("SS Launch, at {0}% throttle", order.Throttle * 10)
                'sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_PatrolAndAttack
                    sb.AppendFormat("SS Patrol and Attack, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_PatrolAndDisable
                    sb.AppendFormat("SS Patrol and Disable, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_PatrolLoop
                    sb.AppendFormat("SS Patrol Loop {0} time{1}, at {2}% throttle, ",
                                order.Variable1,
                                IIf(order.Variable1 = 1, "", "s"),
                                order.Throttle * 10)
                    ' TODO: Determine if this has target criteria
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_Protect
                    sb.AppendFormat("SS Protect, at {0}% throttle, ", order.Throttle * 10)
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.SS_Wait
                    sb.AppendFormat("SS Wait, for {0}", basicTimeString(order.Variable1 * 5))

                Case MissionOrderType.SS_Wait2
                    sb.AppendFormat("SS Wait 2, for {0}", basicTimeString(order.Variable1 * 5))

                Case MissionOrderType.SS_WaitAndProtect
                    sb.AppendFormat("SS Wait and Protect, ")
                    sb.Append(generateOrderTargetDescription(order))

                Case MissionOrderType.Wait
                    sb.AppendFormat("Wait, for {0}", basicTimeString(order.Variable1 * 5))

                Case Else
                    sb.AppendFormat("UNHANDLED: {0}", order.Order)

            End Select

            sb.Append(".")
            Return sb.ToString
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a MissionAbortTrigger.
        ''' </summary>
        ''' <param name="abort"></param>
        ''' <returns></returns>
        Public Shared Function AbortTriggerDescription(abort As MissionAbortTrigger) As String
            Select Case abort
                Case MissionAbortTrigger.Attacked
                    Return "Attacked"
                Case MissionAbortTrigger.HullDamage
                    Return "Less than 50% Hull"
                Case MissionAbortTrigger.None
                    Return "None"
                Case MissionAbortTrigger.NoShields
                    Return "Shields Down"
                Case MissionAbortTrigger.OutOfWarheads
                    Return "Out of Warheads"
                Case MissionAbortTrigger.Systems75
                    Return "Systems 75% of Less"
                Case Else
                    ' throw an exception
                    Throw New ArgumentOutOfRangeException("abort")
            End Select
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a MissionFlightGroupGoal.
        ''' </summary>
        ''' <param name="goal"></param>
        ''' <returns></returns>
        Public Shared Function FlightGroupGoalDescription(goal As MissionFlightGroupGoal) As String
            ' handle arguments
            If goal Is Nothing Then Throw New ArgumentNullException("goal")

            Dim sb As New Text.StringBuilder

            Select Case goal.Condition
                Case MissionTriggerCondition.Attacked
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Attacked")

                Case MissionTriggerCondition.Boarded
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Boarded")

                Case MissionTriggerCondition.Captured
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Captured")

                Case MissionTriggerCondition.CompletedMission
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must have Completed Mission")

                Case MissionTriggerCondition.Created
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must have Arrived")

                Case MissionTriggerCondition.Destroyed
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Destroyed")

                Case MissionTriggerCondition.Disabled
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Disabled")

                Case MissionTriggerCondition.Docked
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must have finished Docking")

                Case MissionTriggerCondition.Inspected
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must be Inspected")

                Case MissionTriggerCondition.None
                    sb.Append("None")

                Case MissionTriggerCondition.Survived
                    sb.Append(generateGoalAmountDescription(goal.Amount))
                    sb.Append(" must have Survived until other goals completed")

                Case Else
                    sb.AppendFormat("UNHANDLED: {0}", goal.Condition)

            End Select

            sb.Append(".")
            Return sb.ToString
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a MissionTrigger as if it were a global goal, in a specific state of completion.
        ''' </summary>
        ''' <param name="trigger"></param>
        ''' <param name="state"></param>
        ''' <returns></returns>
        Public Shared Function TriggerGoalDescriptionByState(trigger As MissionTrigger, state As GoalState) As String
            If trigger Is Nothing Then Throw New ArgumentNullException("trigger")
            ' the various state phrases
            Dim stateTextTarget As String   ' actions that must be performed to a target
            Dim stateTextSelf As String     ' actions the target must do itself

            Select Case state
                Case GoalState.Complete
                    stateTextTarget = "were"
                    stateTextSelf = "have"
                Case GoalState.Failed
                    stateTextTarget = "were not"
                    stateTextSelf = "have not"
                Case GoalState.None, GoalState.Incomplete
                    stateTextTarget = "must be"
                    stateTextSelf = "must have"
                Case Else
                    Throw New ArgumentException("Argument has invalid value.", "state")
            End Select

            Dim sb As New Text.StringBuilder

            Select Case trigger.VariableType
                Case MissionVariableType.FlightGroup
                    ' force an update of the flight group
                    updateMissionFlightGroup(trigger.Variable)
                    Dim fg As MissionFlightGroup = _missionFlightGroup(trigger.Variable)
                    If fg.NumCraft > 1 Or (fg.NumCraft * fg.NumWaves) > 1 Then
                        sb.AppendFormat("{0} of {1} group {2} ",
                                    triggerAmountDescription(trigger.TriggerAmount),
                                    SpaceObjectTypeInfo.Types(fg.CraftType).Name,
                                    fg.Name)

                    Else
                        sb.AppendFormat("{0} {1} ",
                                    SpaceObjectTypeInfo.Types(fg.CraftType).Name,
                                    fg.Name)
                    End If

                Case MissionVariableType.CraftType
                    sb.AppendFormat("{0} of all {1} ",
                                triggerAmountDescription(trigger.TriggerAmount),
                                SpaceObjectTypeInfo.Types(trigger.Variable + 1).NamePlural)

                Case MissionVariableType.IFF
                    sb.AppendFormat("{0} of all {1} craft ",
                                triggerAmountDescription(trigger.TriggerAmount),
                                IFFName(trigger.Variable))

                Case MissionVariableType.CraftCategory
                    sb.AppendFormat("{0} of all ", triggerAmountDescription(trigger.TriggerAmount))
                    Select Case trigger.Variable
                        Case MissionCraftCategory.Freighters
                            sb.Append("freighter craft ")
                        Case MissionCraftCategory.Mines
                            sb.Append("mines ")
                        Case MissionCraftCategory.Platforms
                            sb.Append("platforms ")
                        Case MissionCraftCategory.Starfighters
                            sb.Append("starfighters ")
                        Case MissionCraftCategory.Transports
                            sb.Append("transport craft ")
                        Case MissionCraftCategory.Starships
                            sb.Append("starships ")
                        Case MissionCraftCategory.Utility
                            sb.Append("utility craft ")
                        Case Else
                            sb.AppendFormat("UNHANDLED {0} ", trigger.VariableType.ToString)
                    End Select

                Case MissionVariableType.Order
                    sb.AppendFormat("{0} of all ", triggerAmountDescription(trigger.TriggerAmount))
                    Select Case trigger.Variable
                        Case MissionOrderType.HoldStation
                            sb.Append("stationary craft ")
                        Case MissionOrderType.GoHome
                            sb.Append("craft returning to base ")
                        Case MissionOrderType.Circle
                            sb.Append("non-evading craft ")
                        Case MissionOrderType.CircleAndEvade
                            sb.Append("craft flying in formation ")
                        Case MissionOrderType.Rendezvous
                            sb.Append("rendezvous craft ")
                        Case MissionOrderType.Disabled
                            sb.Append("disabled craft ")
                        Case MissionOrderType.AwaitBoarding
                            sb.Append("craft awaiting boarding ")
                        Case MissionOrderType.Attack
                            sb.Append("craft on free patrol ")
                        Case MissionOrderType.AttackEscorts
                            sb.Append("craft attacking escorts ")
                        Case MissionOrderType.Protect
                            sb.Append("craft attacking in response ")
                        Case MissionOrderType.Escort
                            sb.Append("escorting fighters ")
                        Case MissionOrderType.Disable
                            sb.Append("craft attempting to disable ")
                        Case MissionOrderType.BoardAndGiveCargo
                            sb.Append("craft delivering cargo ")
                        Case MissionOrderType.BoardAndTakeCargo
                            sb.Append("craft seizing cargo ")
                        Case MissionOrderType.BoardAndExchangeCargo
                            sb.Append("craft exchanging cargo ")
                        Case MissionOrderType.BoardAndCaptureCargo
                            sb.Append("capturing craft ")
                        Case MissionOrderType.BoardAndDestroyCargo
                            sb.Append("craft destroying cargo ")
                        Case MissionOrderType.PickUp
                            sb.Append("craft picking-up cargo ")
                        Case MissionOrderType.DropOff
                            sb.Append("craft dropping-off cargo ")
                        Case MissionOrderType.Wait
                            sb.Append("waiting fighters ")
                        Case MissionOrderType.SS_Wait
                            sb.Append("waiting starships ")
                        Case MissionOrderType.SS_PatrolLoop
                            sb.Append("patrolling starships ")
                        Case MissionOrderType.SS_AwaitReturn
                            sb.Append("starships waiting for all returning craft ")
                        Case MissionOrderType.SS_Launch
                            sb.Append("starships waiting to dispatch all craft ")
                        Case MissionOrderType.SS_Protect
                            sb.Append("starships waiting for boarding craft ")
                        Case MissionOrderType.SS_WaitAndProtect
                            sb.Append("starships waiting for boarding craft to appear ")
                        Case MissionOrderType.SS_PatrolAndAttack
                            sb.Append("craft on free patrol ")
                        Case MissionOrderType.SS_PatrolAndDisable
                            sb.Append("craft attempting to disable ")
                        Case MissionOrderType.SS_HoldSteady
                            sb.Append("craft attempting to disable ")
                        Case MissionOrderType.SS_GoHome
                            sb.Append("starships returning to base ")
                        Case Else
                            sb.AppendFormat("UNHANDLED {0} ", trigger.Variable)
                    End Select

                Case Else
                    sb.AppendFormat("UNHANDLED {0} ", trigger.VariableType.ToString)
            End Select

            Select Case trigger.Condition
                Case MissionTriggerCondition.Attacked
                    sb.AppendFormat("{0} attacked.", stateTextTarget)
                Case MissionTriggerCondition.Boarded
                    sb.AppendFormat("{0} boarded.", stateTextTarget)
                Case MissionTriggerCondition.Captured
                    sb.AppendFormat("{0} captured.", stateTextTarget)
                Case MissionTriggerCondition.CompletedMission
                    sb.AppendFormat("{0} completed mission.", stateTextSelf)
                Case MissionTriggerCondition.Created
                    sb.AppendFormat("{0} arrived.", stateTextSelf)
                Case MissionTriggerCondition.Destroyed
                    sb.AppendFormat("{0} destroyed.", stateTextTarget)
                Case MissionTriggerCondition.Disabled
                    sb.AppendFormat("{0} disabled.", stateTextTarget)
                Case MissionTriggerCondition.Docked
                    sb.AppendFormat("{0} finished docking.", stateTextSelf)
                Case MissionTriggerCondition.Inspected
                    sb.AppendFormat("{0} inspected.", stateTextTarget)
                Case MissionTriggerCondition.Survived
                    sb.AppendFormat("{0} survived until other goals completed.", stateTextSelf)
                Case Else
                    sb.AppendFormat("UNHANDLED {0}", trigger.Condition.ToString)
            End Select

            Return sb.ToString
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing one of a Flight Group's goal conditions in a specific state.
        ''' </summary>
        ''' <param name="flightGroup"></param>
        ''' <param name="goalType">Type of goal.</param>
        ''' <param name="state">Completion state.</param>
        ''' <param name="penalty">Optional. Whether or not to display the goal as a penalty to be avoided. Defaults to false.</param>
        ''' <returns></returns>
        Public Shared Function FlightGroupGoalDescriptionByState(flightGroup As MissionFlightGroup, goalType As GoalType, state As GoalState, Optional penalty As Boolean = False) As String
            ' handle argument exceptions
            If flightGroup Is Nothing Then Throw New ArgumentNullException("flightGroup")
            Select Case goalType
                Case GoalType.Bonus, GoalType.Primary, GoalType.Secondary
                    ' a-ok
                Case Else
                    ' exception
                    Throw New ArgumentException("Argument has invalid value.", "goalType")
            End Select
            ' the various state phrases
            Dim stateTextTarget As String   ' actions that must be performed to a target
            Dim stateTextSelf As String     ' actions the target must do itself

            Select Case state
                Case GoalState.Complete
                    stateTextTarget = "was"
                    stateTextSelf = "has"
                Case GoalState.Failed
                    stateTextTarget = "was not"
                    stateTextSelf = "has not"
                Case GoalState.Incomplete, GoalState.None
                    ' NOTE: For now we'll just treat GoalState.None as Incomplete
                    If Not penalty Then
                        stateTextTarget = "must be"
                        stateTextSelf = "must have"
                    Else
                        stateTextTarget = "must not be"
                        stateTextSelf = "must not have"
                    End If
                Case Else
                    Throw New ArgumentException("Argument has invalid value.", "state")
            End Select

            Dim sb As New Text.StringBuilder
            Dim goal As MissionFlightGroupGoal = flightGroup.Goals(goalType)
            Dim craftTypeName As String = SpaceObjectTypeInfo.Types(flightGroup.CraftType).Name

            ' start with the goal amount
            If flightGroup.NumCraft > 1 Or (flightGroup.NumCraft * flightGroup.NumWaves) > 1 Then
                Select Case goal.Amount
                    Case MissionGoalAmount.AllButOne
                        sb.AppendFormat("All but one of {0} group {1} ",
                                    craftTypeName,
                                    flightGroup.Name)
                    Case MissionGoalAmount.AtLeastOne
                        sb.AppendFormat("At least one of {0} group {1} ",
                                    craftTypeName,
                                    flightGroup.Name)
                    Case MissionGoalAmount.Percent100
                        sb.AppendFormat("100% of {0} group {1} ",
                                    craftTypeName,
                                    flightGroup.Name)
                    Case MissionGoalAmount.Percent50
                        sb.AppendFormat("50% of {0} group {1} ",
                                    craftTypeName,
                                    flightGroup.Name)
                    Case MissionGoalAmount.SpecialCraft
                        sb.AppendFormat("{0} {1} {2} ",
                                    craftTypeName,
                                    flightGroup.Name,
                                    flightGroup.SpecialCargoCraft + 1)
                End Select
            Else
                ' just the one craft
                sb.AppendFormat("{0} {1} ",
                            craftTypeName,
                            flightGroup.Name)
            End If

            Select Case goal.Condition
                Case MissionTriggerCondition.Attacked
                    sb.AppendFormat("{0} attacked.", stateTextTarget)
                Case MissionTriggerCondition.Boarded
                    sb.AppendFormat("{0} boarded.", stateTextTarget)
                Case MissionTriggerCondition.Captured
                    sb.AppendFormat("{0} captured.", stateTextTarget)
                Case MissionTriggerCondition.CompletedMission
                    sb.AppendFormat("{0} completed mission.", stateTextSelf)
                Case MissionTriggerCondition.Created
                    sb.AppendFormat("{0} arrived.", stateTextSelf)
                Case MissionTriggerCondition.Destroyed
                    sb.AppendFormat("{0} destroyed.", stateTextTarget)
                Case MissionTriggerCondition.Disabled
                    sb.AppendFormat("{0} disabled.", stateTextTarget)
                Case MissionTriggerCondition.Docked
                    sb.AppendFormat("{0} finished docking.", stateTextSelf)
                Case MissionTriggerCondition.Inspected
                    sb.AppendFormat("{0} inspected.", stateTextTarget)
                Case MissionTriggerCondition.Survived
                    sb.AppendFormat("{0} survived until all other goals completed.", stateTextSelf)
                Case Else
                    sb.AppendFormat("UNHANDLED: {0}", goal.Condition)
            End Select

            Return sb.ToString
        End Function

        ''' <summary>
        ''' Generates a human-readable IFF string from an IFF code.
        ''' This is done using the currently loaded mission data.
        ''' </summary>
        ''' <param name="IFF"></param>
        ''' <returns></returns>
        Public Shared Function IFFName(IFF As Integer) As String
            If IFF < 0 Or IFF > 5 Then Throw New ArgumentOutOfRangeException("IFF")

            Select Case IFF
                Case 0
                    Return "Rebel"
                Case 1
                    Return "Imperial"
            End Select

            Dim ret As String = _missionHeader.IFFNames(IFF - 2)
            ' handle empty strings
            If ret.Equals("") Then
                ' IFF 4 is often used for Traitors
                If IFF = 4 Then Return "Traitor"
                ' other cases
                Return String.Format("IFF {0}", IFF)
            End If
            ' if the first character is recognized as the 'hostile' indicator, do not include it in the return
            If ret.Chars(0) = CChar("1") Then
                Return Strings.Right(ret, ret.Length - 1)
            Else
                Return ret.Clone
            End If
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a MissionFlightGroup, using the CMD Code of the Flight Group's craft and the Flight Group's name.
        ''' </summary>
        ''' <param name="flightGroup"></param>
        ''' <returns></returns>
        Public Shared Function FlightGroupDescription(flightGroup As MissionFlightGroup) As String
            ' handle argument exceptions
            If flightGroup Is Nothing Then Throw New ArgumentNullException("flightGroup")

            Return String.Format("{0} {1}",
                             SpaceObjectTypeInfo.Types(flightGroup.CraftType).CMD,
                             flightGroup.Name)
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a SpaceObject by index.
        ''' This requires an open DOSBox process, so an exception will be thrown if a connection is not available.
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Shared Function SpaceObjectDescription(index As Integer) As String
            ' handle arguments
            If index < 0 Or index >= 120 Then Throw New ArgumentOutOfRangeException("index")
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

            ' force an update on the space object
            _spaceObject(index).UpdateBytes(DOS.ReadDosMemoryBytes(DOS_OFFSET_SPACE_OBJECT_ARRAY + SpaceObject.SizeOf * index, SpaceObject.SizeOf))
            Dim obj As SpaceObject = _spaceObject(index)
            Dim sb As New Text.StringBuilder

            ' branch based on the object category
            Select Case obj.Category
                Case SpaceObjectCategory.Fighter,
                 SpaceObjectCategory.Transport,
                 SpaceObjectCategory.Utility,
                 SpaceObjectCategory.Freight,
                 SpaceObjectCategory.Capital,
                 SpaceObjectCategory.Platform

                    ' check the object type
                    If obj.ObjectType = 0 Then
                        ' the object technically doesn't exist, so the pointer is dangling
                        ' return an appropriate string
                        sb.Append("n/a")

                    Else
                        ' start with the CMD code
                        sb.AppendFormat("{0} ", SpaceObjectTypeInfo.Types(obj.ObjectType).CMD)
                        ' force an update on the craft's flight group

                        _missionFlightGroup(obj.FlightGroupIndex).UpdateBytes(DOS.ReadDosMemoryBytes(DOS_OFFSET_MISS_FG_ARRAY + MissionFlightGroup.SizeOf * obj.FlightGroupIndex, MissionFlightGroup.SizeOf))
                        Dim fg As MissionFlightGroup = _missionFlightGroup(obj.FlightGroupIndex)
                        ' append the flight group name
                        sb.AppendFormat("{0}", fg.Name)
                        ' if the flight group's got more than 1 craft in it, append the craft number
                        If fg.NumCraft > 1 Then
                            ' do not bother forcing a full craft update, just read the appropriate field
                            Dim offset As UInt32 = obj.ObjectPointer + &H90
                            Dim bytes() As Byte = DOS.ReadDosMemoryBytes(offset, 1)
                            sb.AppendFormat(" {0}", bytes(0) + 1)
                        End If

                    End If

                Case Else
                    sb.AppendFormat("CATEGORY {0}",
                                obj.Category)
            End Select

            Return sb.ToString
        End Function

        ''' <summary>
        ''' Returns the name of a Weapon Type.
        ''' </summary>
        ''' <param name="weaponType"></param>
        ''' <returns></returns>
        Public Shared Function WeaponTypeDescription(weaponType As Byte) As String
            Select Case weaponType
                Case 0
                    Return "None"
                Case &H89, &H8A
                    Return "Laser Cannon (Red)"
                Case &H8B, &H8C
                    Return "Laser Cannon (Green)"
                Case &H8D, &H8E
                    Return "Ion Cannon"
                Case &H8F
                    Return "Proton Torpedo"
                Case &H90
                    Return "Concussion Missile"
                Case &H94
                    Return "Adv. Proton Torpedo"
                Case &H95
                    Return "Adv. Concussion Missile"
                Case &H96
                    Return "Space Bomb"
                Case &H97
                    Return "Heavy Rocket"
                Case &H98
                    Return "Mag Pulse Warhead"
                Case Else
                    Return String.Format("WEAPON_TYPE 0x{0:X2}", weaponType)
            End Select
        End Function

        ''' <summary>
        ''' Generates a human-readable string describing a target by it's code.
        ''' This requires an open DOSBox process, so an exception will be thrown if a connection is not available.
        ''' </summary>
        ''' <param name="target">Target code to use for the description.</param>
        ''' <returns></returns>
        Public Shared Function TargetDescription(target As UInt16) As String
            If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

            Dim sb As New Text.StringBuilder

            Dim targetIndex As UInt16 = target And &HFF

            ' handle the target code -- the upper byte determines the type of target
            Select Case (target >> 8)
                Case 0
                    ' SpaceObject index of a Craft
                    ' This may sometimes be 0xFF, in which case it should be indicated to be N/A
                    If targetIndex = &HFF Then
                        sb.Append("N/A")
                    Else
                        sb.Append(SpaceObjectDescription(targetIndex))
                    End If

                Case &H80
                    ' Waypoint
                    Select Case targetIndex
                        Case 0 To 3
                            ' Starting Point
                            sb.AppendFormat("Starting Point {0}", targetIndex)
                        Case 4 To 11
                            ' Flight Plan Waypoint
                            sb.AppendFormat("Flight Waypoint {0}", targetIndex - 4)
                        Case 12
                            ' Rendezvous Point
                            sb.Append("Rendezvous Point")
                        Case 13
                            ' Hyperspace Point
                            sb.Append("Hyperspace Point")
                        Case 14
                            ' Briefing Point (unlikely to occur, but why not?)
                            sb.Append("Briefing Point")
                        Case Else
                            ' Unhandled/Unknown
                            sb.AppendFormat("UNHANDLED 0x{0:X4}", target)
                    End Select

                Case &HFF
                    ' Nothing, I think
                    If targetIndex = &HFF Then
                        sb.Append("None")
                    Else
                        ' Unhandled/Unknown
                        sb.AppendFormat("UNHANDLED 0x{0:X4}", target)
                    End If
                Case Else
                    ' Unhandled/Unknown
                    sb.AppendFormat("UNHANDLED 0x{0:X4}", target)

            End Select
            Return sb.ToString
        End Function

#End Region

#End Region
#Region " Event-Handlers "

        Private Shared Sub DOS_ProcessExited(sender As Object, e As EventArgs) Handles DOS.ProcessExited
            ' process is no longer available
            processAvailable = False

            ' pass the event along
            RaiseEvent ProcessExited(sender, e)
        End Sub

#End Region

#End Region
#Region " --- Properties --- "

#Region " Process-Accessing "

#Region " Pertaining to the Player "

        Public Shared ReadOnly Property PlayerSpaceObjectIndex As UInt16
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerObjectIndex()
            End Get
        End Property

        Public Shared ReadOnly Property PlayerFlightGroupIndex As Byte
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerFlightGroupIndex()
            End Get
        End Property

        Public Shared ReadOnly Property PlayerCraftPointer As UInt32
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerCraftPointer()
            End Get
        End Property

        Public Shared ReadOnly Property PlayerSpaceObjectPointer As UInt32
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerSpaceObjectPointer()
            End Get
        End Property

        Public Shared ReadOnly Property PlayerCMDTargetIndex As UInt16
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerCMDTargetIndex()
            End Get
        End Property

        Public Shared Property PlayerCraftPanelDamageFlags As UInt16
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' read the player's craft pointer
                Dim pCraft As UInt32 = readPlayerCraftPointer()
                ' read the appropriate field
                Return DOS.ReadDosMemoryUInt16(pCraft + &HAA)
            End Get
            Set(value As UInt16)
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' read the player's craft pointer
                Dim pCraft As UInt32 = readPlayerCraftPointer()
                ' proceed only if the pointer is valid
                ' TODO: Maybe throw an exception, but for now fail silently
                If pCraft = 0 Then Exit Property

                ' write to the appropriate field
                DOS.WriteDosMemoryUInt16(pCraft + &HAA, value)
            End Set
        End Property

        Public Shared Property PlayerCraftDamage As UInt16
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' read the player's craft pointer
                Dim pCraft As UInt32 = readPlayerCraftPointer()
                ' read the appropriate field
                Return DOS.ReadDosMemoryUInt16(pCraft + &HA2)
            End Get
            Set(value As UInt16)
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' read the player's craft pointer
                Dim pCraft As UInt32 = readPlayerCraftPointer()
                ' proceed only if the pointer is valid
                ' TODO: Maybe throw an exception, but for now fail silently
                If pCraft = 0 Then Exit Property

                ' write to the appropriate field
                DOS.WriteDosMemoryUInt16(pCraft + &HA2, value)
            End Set
        End Property
#End Region
#Region " Code Injection/Reassembly "

        Public Shared Property HackCraftPanelDamage As Boolean
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return injectedCraftPanelDamageHack
            End Get
            Set(value As Boolean)
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' do nothing if the user assigns the same state it currently is
                If value = injectedCraftPanelDamageHack Then Exit Property

                ' TODO: Perform some extra checks maybe, to keep the injection from occuring at a bad time

                ' address for damaging craft's panel 0x1C004D
                ' original bytes: 66 21 97 AA 00 00 00
                ' and     [edi+0AAh], dx
                ' replaced bytes: 90 90 90 90 90 90 90 (NOPs)
                If value = True Then
                    ' enable the hack
                    DOS.WriteDosMemoryBytes(&H1C004D, {&H90, &H90, &H90, &H90, &H90, &H90, &H90})
                    injectedCraftPanelDamageHack = True
                Else
                    ' restore the original behavior
                    DOS.WriteDosMemoryBytes(&H1C004D, {&H66, &H21, &H97, &HAA, &H0, &H0, &H0})
                    injectedCraftPanelDamageHack = False
                End If
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Gets the MissionHeader of a loaded mission in TIE Fighter.
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property MissonHeader As MissionHeader
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateMissionHeader()
                ' return a clone
                Return _missionHeader.Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a MissionFlightGroup from a loaded mission in TIE Fighter.
        ''' </summary>
        ''' <param name="index">Index of the MissionFlightGroup to get.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property MissionFlightGroups(index As Integer) As MissionFlightGroup
            Get
                If index < 0 Or index >= 48 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateMissionFlightGroup(index)
                ' return a clone
                Return _missionFlightGroup(index).Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a FlightGroupStatus struct fronm a loaded mission in TIE Fighter.
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Shared ReadOnly Property FlightGroupStatus(index As Integer) As FlightGroupStatus
            Get
                If index < 0 Or index >= 48 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateFlightGroupStatus(index)
                ' return a clone
                Return _flightGroupStatus(index).Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a SpaceObject from a loaded mission in TIE Fighter.
        ''' </summary>
        ''' <param name="index">Index of the SpaceObject to get.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property SpaceObjects(index As Integer) As SpaceObject
            Get
                If index < 0 Or index >= 120 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateSpaceObject(index)
                ' return a clone
                Return _spaceObject(index).Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a SpaceObjectDefinition from TIE Fighter.
        ''' </summary>
        ''' <param name="index">Index of the SpaceObjectDefinition to get.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property SpaceObjectDefinitions(index As SpaceObjectType) As SpaceObjectDefinition
            Get
                If index < 0 Or index >= 161 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateSpaceObjectDefinition(index)
                ' return a clone
                Return _spaceObjectDefinition(index).Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a WeaponObject from a loaded mission in TIE Fighter.
        ''' </summary>
        ''' <param name="index">Index of the WeaponObject to get.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property WeaponObjects(index As Integer) As WeaponObject
            Get
                If index < 0 Or index >= 64 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateWeaponObject(index)
                ' return a clone
                Return _weaponObject(index).Clone
            End Get
        End Property

        ''' <summary>
        ''' Gets a CraftObject from memory pointed to by a SpaceObject.
        ''' If the SpaceObject does not point to a CraftObject an exception will be thrown.
        ''' </summary>
        ''' <param name="spaceObject">The SpaceObject to derive the CraftObject from.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property CraftObject(spaceObject As SpaceObject) As CraftObject
            Get
                ' handle argument exceptions
                If spaceObject Is Nothing Then Throw New ArgumentNullException("spaceObject")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)
                ' TODO: Sanity check the passed SpaceObject

                ' return the new CraftObject
                Return CraftObject.FromBytes(DOS.ReadDosMemoryBytes(spaceObject.ObjectPointer, CraftObject.SizeOf))
            End Get
        End Property

        ''' <summary>
        ''' Gets the current player's Difficulty Setting
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property DifficultySetting As DifficultySetting
            Get
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                Return readPlayerDifficultySetting()
            End Get
        End Property

        ''' <summary>
        ''' Gets the overall status of the current mission's goals by type.
        ''' </summary>
        ''' <param name="goalType"></param>
        ''' <returns></returns>
        Public Shared ReadOnly Property MissionOverallStatus(goalType As GoalType) As GoalState
            Get
                Dim i As Integer
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)
                Select Case goalType
                    Case GoalType.Primary
                        i = 0
                    Case GoalType.Secondary
                        i = 1
                    Case GoalType.Bonus
                        i = 2
                    Case Else
                        Throw New ArgumentException("Invalid GoalType.")
                End Select

                Dim bytes(2) As Byte
                bytes = DOS.ReadDosMemoryBytes(DOS_OFFSET_MISSION_STATUS_ARRAY, 3)
                Select Case bytes(i)
                    Case 0
                        Return GoalState.Incomplete
                    Case 1
                        Return GoalState.Complete
                    Case 2
                        Return GoalState.Failed
                End Select

                Return GoalState.None
            End Get
        End Property

        ''' <summary>
        ''' Gets MissionGlobalGoals
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Shared ReadOnly Property MissionGlobalGoals(index As Integer) As MissionGlobalGoal
            Get
                If index < 0 Or index >= 3 Then Throw New ArgumentOutOfRangeException("index")
                If Not processIsRunningTIE() Then Throw New InvalidOperationException(NO_OPEN_PROCESS)

                ' update the internal copy
                updateMissionGlobalGoal(index)
                ' return a clone
                Return _missionGlobalGoal(index).Clone
            End Get
        End Property

#End Region
#Region " General "

        ''' <summary>
        ''' Gets the Process that's currently being used as a data source.
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property Process As Process
            Get
                If DOS Is Nothing Then Return Nothing
                Return DOS.Process
            End Get
        End Property

        ''' <summary>
        ''' Gets a bitmap icon representing a Craft Type, colored to match an IFF.
        ''' </summary>
        ''' <param name="index">CraftType index.</param>
        ''' <param name="IFF">IFF code.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property CraftIcon(index As Integer, IFF As Integer) As Bitmap
            Get
                ' handle arguments
                If index < 0 Or index > SpaceObjectTypeInfo.Count Then Throw New ArgumentOutOfRangeException("index")
                If IFF < 0 Or IFF > 5 Then Throw New ArgumentOutOfRangeException("IFF")
                Select Case IFF
                    Case 0
                        Return craftIcons(SpaceObjectTypeInfo.Types(index).IconIndex * 4)
                    Case 1, 4
                        Return craftIcons(SpaceObjectTypeInfo.Types(index).IconIndex * 4 + 1)
                    Case 2
                        Return craftIcons(SpaceObjectTypeInfo.Types(index).IconIndex * 4 + 2)
                End Select
                ' Case 3, 5
                Return craftIcons(SpaceObjectTypeInfo.Types(index).IconIndex * 4 + 3)
            End Get
        End Property

        ''' <summary>
        ''' Gets a bitmap to draw a craft icon within, colored to match an IFF.
        ''' </summary>
        ''' <param name="IFF">IFF code.</param>
        ''' <returns></returns>
        Public Shared ReadOnly Property CraftIconBox(IFF As Integer) As Bitmap
            Get
                If IFF < 0 Or IFF > 5 Then Throw New ArgumentOutOfRangeException("IFF")
                ' Colors were ordered differently in the RANGE palette, so return the proper color
                Select Case IFF
                    Case 0
                        ' Green
                        Return craftIconBoxes(0)
                    Case 1, 4
                        ' Red
                        Return craftIconBoxes(1)
                    Case 2
                        ' Purple
                        Return craftIconBoxes(3)
                End Select
                ' Case 3, 5
                ' Blue
                Return craftIconBoxes(2)
            End Get
        End Property

#End Region

#End Region
#Region " --- Events --- "

        Public Shared Event ProcessExited(sender As Object, e As EventArgs)

#End Region

    End Class

End Namespace
