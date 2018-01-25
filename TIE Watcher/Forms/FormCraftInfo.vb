Imports System.ComponentModel
Imports TIE_Watcher.TIE

Public Class FormCraftInfo

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "{0:X4}"

#End Region
#Region " --- Variables --- "

    ' SpaceObject and CraftObject data we used LAST frame, to avoid unnecessary load when updating information
    Private lastSpaceObject As SpaceObject
    Private lastCraftObject As CraftObject

    ' SpaceObject and CraftObject we base the information off of
    Private spaceObject As SpaceObject
    Private craftObject As CraftObject

    ' control arrays
    Private lblCannonType() As Label
    Private lblCannonLink() As Label
    Private lblCannon_D7() As Label
    Private lblCannonNext() As Label
    Private lblCannonTimer() As Label
    Private lblWarheadType() As Label
    Private lblWarheadNext() As Label
    Private lblWarheadTimer() As Label

    ' data grid view cells for unknown fields
    Private cell_Field_1,
        cell_Field_2,
        cell_Field_4,
        cell_Field_A,
        cell_Field_C,
        cell_Field_E,
        cell_Field_12,
        cell_Field_16,
        cell_Field_1A,
        cell_Field_1B,
        cell_Field_3C,
        cell_Field_3E,
        cell_Field_40,
        cell_Field_41,
        cell_Field_44,
        cell_Field_46,
        cell_Field_47,
        cell_Field_48,
        cell_Field_49,
        cell_Field_4A,
        cell_Field_4B,
        cell_Field_60,
        cell_Field_61,
        cell_Field_62,
        cell_Field_63,
        cell_Field_68,
        cell_Field_6C,
        cell_Field_6E,
        cell_Field_6F,
        cell_Field_72,
        cell_Field_74,
        cell_Field_75,
        cell_Field_76,
        cell_Field_78,
        cell_Field_7C,
        cell_Field_7E,
        cell_Field_7F,
        cell_Field_80,
        cell_Field_82,
        cell_Field_84,
        cell_Field_86,
        cell_Field_88,
        cell_Field_89,
        cell_Field_8A,
        cell_Field_8C,
        cell_Field_91,
        cell_Field_92,
        cell_Field_96,
        cell_Field_9A,
        cell_Field_A0,
        cell_Field_A4,
        cell_Field_B0,
        cell_Field_B2,
        cell_Field_B4,
        cell_Field_B5,
        cell_Field_B6,
        cell_Field_B7,
        cell_Field_EC,
        cell_Field_13F,
        cell_Field_140 As DataGridViewCell

#End Region
#Region " --- Methods --- "

    ' adds a new row with an offset label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' add 16 emtpy rows to the Weapons data grid view
        Me.dgvWeapons.Rows.Add(16)
        ' add 40 empty rows to the Parts data grid view
        Me.dgvComponents.Rows.Add(40)
        ' populate the index fields
        For i As Integer = 0 To 39
            Dim row As DataGridViewRow = dgvComponents.Rows.Item(i)
            row.Cells.Item(0).Value = i
        Next

        ' add rows to the Unknown Fields data grid view
        cell_Field_1 = addUnknownField("1")
        cell_Field_2 = addUnknownField("2")
        cell_Field_4 = addUnknownField("4")
        cell_Field_A = addUnknownField("A")
        cell_Field_C = addUnknownField("C")
        cell_Field_E = addUnknownField("E")
        cell_Field_12 = addUnknownField("12")
        cell_Field_16 = addUnknownField("16")
        cell_Field_1A = addUnknownField("1A")
        cell_Field_1B = addUnknownField("1B")
        cell_Field_3C = addUnknownField("3C")
        cell_Field_3E = addUnknownField("3E")
        cell_Field_40 = addUnknownField("40")
        cell_Field_41 = addUnknownField("41")
        cell_Field_44 = addUnknownField("44")
        cell_Field_46 = addUnknownField("46")
        cell_Field_47 = addUnknownField("47")
        cell_Field_48 = addUnknownField("48")
        cell_Field_49 = addUnknownField("49")
        cell_Field_4A = addUnknownField("4A")
        cell_Field_4B = addUnknownField("4B")
        cell_Field_60 = addUnknownField("60")
        cell_Field_61 = addUnknownField("61")
        cell_Field_62 = addUnknownField("62")
        cell_Field_63 = addUnknownField("63")
        cell_Field_68 = addUnknownField("68")
        cell_Field_6C = addUnknownField("6C")
        cell_Field_6E = addUnknownField("6E")
        cell_Field_6F = addUnknownField("6F")
        cell_Field_72 = addUnknownField("72")
        cell_Field_74 = addUnknownField("74")
        cell_Field_75 = addUnknownField("75")
        cell_Field_76 = addUnknownField("76")
        cell_Field_78 = addUnknownField("78")
        cell_Field_7C = addUnknownField("7C")
        cell_Field_7E = addUnknownField("7E")
        cell_Field_7F = addUnknownField("7F")
        cell_Field_80 = addUnknownField("80")
        cell_Field_82 = addUnknownField("82")
        cell_Field_84 = addUnknownField("84")
        cell_Field_86 = addUnknownField("86")
        cell_Field_88 = addUnknownField("88")
        cell_Field_89 = addUnknownField("89")
        cell_Field_8A = addUnknownField("8A")
        cell_Field_8C = addUnknownField("8C")
        cell_Field_91 = addUnknownField("91")
        cell_Field_92 = addUnknownField("92")
        cell_Field_96 = addUnknownField("96")
        cell_Field_9A = addUnknownField("9A")
        cell_Field_A0 = addUnknownField("A0")
        cell_Field_A4 = addUnknownField("A4")
        cell_Field_B0 = addUnknownField("B0")
        cell_Field_B2 = addUnknownField("B2")
        cell_Field_B4 = addUnknownField("B4")
        cell_Field_B5 = addUnknownField("B5")
        cell_Field_B6 = addUnknownField("B6")
        cell_Field_B7 = addUnknownField("B7")
        cell_Field_EC = addUnknownField("EC")
        cell_Field_13F = addUnknownField("13F")
        cell_Field_140 = addUnknownField("140")

        ' define the label arrays
        Me.lblCannonType = {lblCannon0Type, lblCannon1Type}
        Me.lblCannonLink = {lblCannon0Link, lblCannon1Link}
        Me.lblCannon_D7 = {lblCannon0_D7, lblCannon1_D7}
        Me.lblCannonNext = {lblCannon0Next, lblCannon1Next}
        Me.lblCannonTimer = {lblCannon0Timer, lblCannon1Timer}
        Me.lblWarheadType = {lblWarhead0Type, lblWarhead1Type}
        Me.lblWarheadNext = {lblWarhead0Next, lblWarhead1Next}
        Me.lblWarheadTimer = {lblWarhead0Timer, lblWarhead1Timer}
    End Sub

    ' updates the colors of a single craft system feature label to reflect its state
    Private Sub updateFeatureSystem(lbl As Label, feature As Boolean, notDamaged As Boolean)
        ' colorize the label based on the flag states
        If feature Then
            ' feature is installed
            If notDamaged Then
                ' feature is not damaged
                lbl.ForeColor = Color.Black
                lbl.BackColor = Color.Lime
            Else
                ' feature is damaged
                lbl.ForeColor = Color.White
                lbl.BackColor = Color.Red
            End If
        Else
            ' feature is not installed
            lbl.ForeColor = Color.LightGray
            lbl.BackColor = Color.Black
        End If
    End Sub

    Private Sub updateFields()
        ' whether or not to ignore the differences between old and new data
        Dim ignoreDiff As Boolean = False
        ' if the previous objects don't exist, force a full update
        If lastSpaceObject Is Nothing Or lastCraftObject Is Nothing Then
            ignoreDiff = True
        End If

        ' only update displays when data has changed, to reduce load and potential display flickering

        ' SpaceObject.Type
        If ignoreDiff OrElse Me.spaceObject.ObjectType <> Me.lastSpaceObject.ObjectType Then
            Select Case Me.spaceObject.Category
                Case SpaceObjectCategory.Capital,
                     SpaceObjectCategory.Fighter,
                     SpaceObjectCategory.Freight,
                     SpaceObjectCategory.Platform,
                     SpaceObjectCategory.Transport,
                     SpaceObjectCategory.Utility
                    lblType.Text = SpaceObjectTypeInfo.Types(Me.spaceObject.ObjectType).Name
                Case Else
                    lblType.Text = "? " & Me.spaceObject.Category.ToString
            End Select

        End If
        ' CraftObject.Pitch
        If ignoreDiff OrElse Me.craftObject.Pitch <> Me.lastCraftObject.Pitch Then
            lblPitch.Text = String.Format("{0:0.00}", Me.craftObject.Pitch / 182.044)
        End If
        ' CraftObject.Yaw
        If ignoreDiff OrElse Me.craftObject.Yaw <> Me.lastCraftObject.Yaw Then
            lblYaw.Text = String.Format("{0:0.00}", Me.craftObject.Yaw / 182.044)
        End If
        ' CraftObject.Throttle
        If ignoreDiff OrElse Me.craftObject.Throttle <> Me.lastCraftObject.Throttle Then
            lblThrottle.Text = String.Format("{0:0.0}%", Me.craftObject.Throttle / 655.35)
        End If

        ' Damage
        If ignoreDiff OrElse Me.craftObject.Damage <> Me.lastCraftObject.Damage Then
            lblDamage.Text = String.Format("{0}", Me.craftObject.Damage)
        End If
        ' MaxDamage
        If ignoreDiff OrElse Me.craftObject.MaxDamage <> Me.lastCraftObject.MaxDamage Then
            lblMaxDamage.Text = String.Format("{0}", Me.craftObject.MaxDamage)
        End If
        ' Hull %
        If ignoreDiff OrElse
            (Me.craftObject.Damage <> Me.lastCraftObject.Damage) OrElse
            (Me.craftObject.MaxDamage <> Me.lastCraftObject.MaxDamage) Then

            If Me.craftObject.Damage > Me.craftObject.MaxDamage Then
                lblDamagePercent.Text = "0%"
            Else
                lblDamagePercent.Text = String.Format("{0:0.0}%", (Me.craftObject.MaxDamage - Me.craftObject.Damage) / (Me.craftObject.MaxDamage) * 100)
            End If

        End If
        ' Shield Charge Rate
        If ignoreDiff OrElse Me.craftObject.ShieldChargeRate <> Me.lastCraftObject.ShieldChargeRate Then
            lblShieldChargeRate.Text = String.Format("{0}%", Me.craftObject.ShieldChargeRate * 50)
        End If
        ' Shield Balance
        If ignoreDiff OrElse Me.craftObject.ShieldBalance <> Me.lastCraftObject.ShieldBalance Then
            Dim t As String
            Select Case Me.craftObject.ShieldBalance
                Case 0
                    t = "Fore"
                Case 1
                    t = "Even"
                Case 2
                    t = "Rear"
                Case Else
                    t = String.Format("{0}", Me.craftObject.ShieldBalance)
            End Select
            lblShieldBalance.Text = t
        End If
        ' Shield Fore
        If ignoreDiff OrElse Me.craftObject.ShieldChargeFore <> Me.lastCraftObject.ShieldChargeFore Then
            lblShieldChargeFore.Text = String.Format("{0}", Me.craftObject.ShieldChargeFore)
        End If
        ' Shield Rear
        If ignoreDiff OrElse Me.craftObject.ShieldChargeRear <> Me.lastCraftObject.ShieldChargeRear Then
            lblShieldChargeRear.Text = String.Format("{0}", Me.craftObject.ShieldChargeRear)
        End If

        ' Current Order
        ' Checks CraftObject.OrderMissionIndex and SpaceObject.FlightGroupIndex
        If ignoreDiff OrElse
            (Me.craftObject.OrderMissionIndex <> Me.lastCraftObject.OrderMissionIndex) OrElse
            (Me.spaceObject.FlightGroupIndex <> Me.lastSpaceObject.FlightGroupIndex) Then

            Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(Me.spaceObject.FlightGroupIndex)

            lblCurrentOrder.Text = TIEServices.MissionOrderDescription(fg.Orders(Me.craftObject.OrderMissionIndex))
        End If
        ' CraftObject.ScriptOverall
        If ignoreDiff OrElse Me.craftObject.ScriptOverall <> Me.lastCraftObject.ScriptOverall Then
            lblScriptOverall.Text = String.Format("0x{0:X2}", Me.craftObject.ScriptOverall)
        End If
        ' CraftObject.ScriptCurrent
        If ignoreDiff OrElse Me.craftObject.ScriptCurrent <> Me.lastCraftObject.ScriptCurrent Then
            lblScriptCurrent.Text = String.Format("0x{0:X2}", Me.craftObject.ScriptCurrent)
        End If
        ' CraftObject.ScriptWaypoint
        If ignoreDiff OrElse Me.craftObject.ScriptWaypoint <> Me.lastCraftObject.ScriptWaypoint Then
            lblScriptWaypoint.Text = String.Format("0x{0:X2}", Me.craftObject.ScriptWaypoint)
        End If
        ' CraftObject.ScriptTimer
        If ignoreDiff OrElse Me.craftObject.ScriptTimer <> Me.lastCraftObject.ScriptTimer Then
            lblScriptTimer.Text = Me.craftObject.ScriptTimer
        End If
        ' CraftObject.ScriptTarget
        ' NOTE: Update behavior may need to be overriden if I change it to something other than the raw value, like the target craft's name
        If ignoreDiff OrElse Me.craftObject.ScriptTarget <> Me.lastCraftObject.ScriptTarget Then
            'lblScriptTarget.Text = String.Format("0x{0:X4}", Me.craftObject.ScriptTarget)
            lblScriptTarget.Text = TIEServices.TargetDescription(Me.craftObject.ScriptTarget)
        End If
        ' CraftObject.LastAttacker
        If ignoreDiff OrElse Me.craftObject.LastAttacker <> Me.lastCraftObject.LastAttacker Then
            lblLastAttacker.Text = TIEServices.TargetDescription(Me.craftObject.LastAttacker)
        End If
        ' CraftObject.ScriptTargetXPosition
        If ignoreDiff OrElse Me.craftObject.ScriptTargetXPosition <> Me.lastCraftObject.ScriptTargetXPosition Then
            lblTargetXPosition.Text = String.Format("{0:#,0}", Me.craftObject.ScriptTargetXPosition)
        End If
        ' CraftObject.ScriptTargetYPosition
        If ignoreDiff OrElse Me.craftObject.ScriptTargetYPosition <> Me.lastCraftObject.ScriptTargetYPosition Then
            lblTargetYPosition.Text = String.Format("{0:#,0}", Me.craftObject.ScriptTargetYPosition)
        End If
        ' CraftObject.ScriptTargetZPosition
        If ignoreDiff OrElse Me.craftObject.ScriptTargetZPosition <> Me.lastCraftObject.ScriptTargetZPosition Then
            lblTargetZPosition.Text = String.Format("{0:#,0}", Me.craftObject.ScriptTargetZPosition)
        End If
        ' CraftObject.Array1D
        lblArray1D.Text = String.Format("0x{0:X2} 0x{1:X2} 0x{2:X2}",
                                        Me.craftObject.Array_1D(0),
                                        Me.craftObject.Array_1D(1),
                                        Me.craftObject.Array_1D(2))
        ' CraftObject.Array20
        lblArray20.Text = String.Format("0x{0:X2} 0x{1:X2} 0x{2:X2}",
                                        Me.craftObject.Array_20(0),
                                        Me.craftObject.Array_20(1),
                                        Me.craftObject.Array_20(2))
        ' Field_26
        lblField_26.Text = String.Format("0x{0:X4}", Me.craftObject.Field_26)
        ' Field_2E
        lblField_2E.Text = String.Format("0x{0:X4}", Me.craftObject.Field_2E)

        ' Craft's Feature Flags and Feature Damage Flags
        If ignoreDiff OrElse
            (Me.craftObject.FeatureFlags <> Me.lastCraftObject.FeatureFlags) OrElse
            (Me.craftObject.FeatureDamageFlags <> Me.lastCraftObject.FeatureDamageFlags) Then

            updateFeatureSystem(lblShieldSystem, Me.craftObject.FeatureFlags And &H1, Me.craftObject.FeatureDamageFlags And &H1)
            updateFeatureSystem(lblEjectionSystem, Me.craftObject.FeatureFlags And &H2, Me.craftObject.FeatureDamageFlags And &H2)
            updateFeatureSystem(lblTargetingSystem, Me.craftObject.FeatureFlags And &H4, Me.craftObject.FeatureDamageFlags And &H4)
            updateFeatureSystem(lblWarheadSystem, Me.craftObject.FeatureFlags And &H8, Me.craftObject.FeatureDamageFlags And &H8)
            updateFeatureSystem(lblCannonSystem, Me.craftObject.FeatureFlags And &H10, Me.craftObject.FeatureDamageFlags And &H10)
            updateFeatureSystem(lblFlightControlSystem, Me.craftObject.FeatureFlags And &H20, Me.craftObject.FeatureDamageFlags And &H20)
            updateFeatureSystem(lblEngineSystem, Me.craftObject.FeatureFlags And &H40, Me.craftObject.FeatureDamageFlags And &H40)
            updateFeatureSystem(lblHyperdriveSystem, Me.craftObject.FeatureFlags And &H80, Me.craftObject.FeatureDamageFlags And &H80)
            updateFeatureSystem(lblBeamWeaponSystem, Me.craftObject.FeatureFlags And &H100, Me.craftObject.FeatureDamageFlags And &H100)
            updateFeatureSystem(lblSystemFlag0200, Me.craftObject.FeatureFlags And &H200, Me.craftObject.FeatureDamageFlags And &H200)
        End If

        ' CraftObject.CannonChargeRate
        If ignoreDiff OrElse Me.craftObject.CannonChargeRate <> Me.lastCraftObject.CannonChargeRate Then
            lblCannonChargeRate.Text = String.Format("{0}%", Me.craftObject.CannonChargeRate * 50)
        End If
        ' CraftObject.CannonBanks
        If ignoreDiff OrElse Me.craftObject.CannonBanks <> Me.lastCraftObject.CannonBanks Then
            lblCannonBanks.Text = String.Format("{0}", Me.craftObject.CannonBanks)
        End If
        ' CraftObject.CannonCount
        If ignoreDiff OrElse Me.craftObject.CannonCount <> Me.lastCraftObject.CannonCount Then
            lblCannonCount.Text = String.Format("{0}", Me.craftObject.CannonCount)
        End If
        ' Craft's Cannon Systems
        For i As Integer = 0 To 1
            If ignoreDiff OrElse Me.craftObject.CannonType(i) <> Me.lastCraftObject.CannonType(i) Then
                lblCannonType(i).Text = TIEServices.WeaponTypeDescription(Me.craftObject.CannonType(i))
            End If
            If ignoreDiff OrElse Me.craftObject.CannonLink(i) <> Me.lastCraftObject.CannonLink(i) Then
                lblCannonLink(i).Text = String.Format("{0}", Me.craftObject.CannonLink(i))
            End If
            If ignoreDiff OrElse Me.craftObject.CannonNext(i) <> Me.lastCraftObject.CannonNext(i) Then
                lblCannonNext(i).Text = String.Format("{0}", Me.craftObject.CannonNext(i))
            End If
            If ignoreDiff OrElse Me.craftObject.CannonTimer(i) <> Me.lastCraftObject.CannonTimer(i) Then
                lblCannonTimer(i).Text = String.Format("{0}", Me.craftObject.CannonTimer(i))
            End If
            If ignoreDiff OrElse Me.craftObject.Array_D7(i) <> Me.lastCraftObject.Array_D7(i) Then
                lblCannon_D7(i).Text = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Array_D7(i))
            End If
        Next
        ' CraftObject.WarheadBanks
        If ignoreDiff OrElse Me.craftObject.WarheadBanks <> Me.lastCraftObject.WarheadBanks Then
            lblWarheadBanks.Text = String.Format("{0}", Me.craftObject.WarheadBanks)
        End If
        ' CraftObject.WarheadLock
        If ignoreDiff OrElse Me.craftObject.WarheadLock <> Me.lastCraftObject.WarheadLock Then
            lblWarheadLock.Text = String.Format("{0}", Me.craftObject.WarheadLock)
        End If
        ' field_E5
        If ignoreDiff OrElse Me.craftObject.Field_E5 <> Me.lastCraftObject.Field_E5 Then
            lblField_E5.Text = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_E5)
        End If
        ' Craft's Warhead System
        For i As Integer = 0 To 1
            If ignoreDiff OrElse Me.craftObject.WarheadType(i) <> Me.lastCraftObject.WarheadType(i) Then
                lblWarheadType(i).Text = TIEServices.WeaponTypeDescription(Me.craftObject.WarheadType(i))
            End If
            If ignoreDiff OrElse Me.craftObject.WarheadNext(i) <> Me.lastCraftObject.WarheadNext(i) Then
                lblWarheadNext(i).Text = String.Format("{0}", Me.craftObject.CannonNext(i))
            End If
            If ignoreDiff OrElse Me.craftObject.WarheadTimer(i) <> Me.lastCraftObject.WarheadTimer(i) Then
                lblWarheadTimer(i).Text = String.Format("{0}", Me.craftObject.WarheadTimer(i))
            End If
        Next

        ' Time and Distance to Target
        If ignoreDiff OrElse
                (Me.spaceObject.XPosition <> Me.lastSpaceObject.XPosition) OrElse
                (Me.spaceObject.YPosition <> Me.lastSpaceObject.YPosition) OrElse
                (Me.spaceObject.ZPosition <> Me.lastSpaceObject.ZPosition) OrElse
                (Me.craftObject.ScriptTargetXPosition <> Me.lastCraftObject.ScriptTargetXPosition) OrElse
                (Me.craftObject.ScriptTargetYPosition <> Me.lastCraftObject.ScriptTargetYPosition) OrElse
                (Me.craftObject.ScriptTargetZPosition <> Me.lastCraftObject.ScriptTargetZPosition) Then
            ' calculate distance to target
            Dim xDiff As Long = Me.craftObject.ScriptTargetXPosition - Me.spaceObject.XPosition
            Dim yDiff As Long = Me.craftObject.ScriptTargetYPosition - Me.spaceObject.YPosition
            Dim zDiff As Long = Me.craftObject.ScriptTargetZPosition - Me.spaceObject.ZPosition
            Dim distance As Long = Math.Sqrt(xDiff * xDiff + yDiff * yDiff + zDiff * zDiff)

            lblDistanceToTarget.Text = String.Format("{0:#,0}", distance)

            ' calculate time to target
            If Me.spaceObject.Velocity = 0 Then
                lblTimeToTarget.Text = "N/A"
            Else
                Dim seconds As Integer
                Dim minutes As Integer

                seconds = distance / (Me.spaceObject.Velocity * 18)
                minutes = Math.Floor(seconds / 60)
                seconds = seconds Mod 60
                lblTimeToTarget.Text = String.Format("{0:0}:{1:00}", minutes, seconds)
            End If


        End If

        ' Handle Elements of the Weapon datagridview
        Dim numWeapons As Integer = craftObject.CannonCount + craftObject.WarheadBanks * 2
        For i As Integer = 0 To 15
            Dim row As DataGridViewRow = dgvWeapons.Rows.Item(i)

            ' set the row's color based on if the weapon is valid or not (the craft has enough)
            If i < numWeapons Then
                row.DefaultCellStyle.BackColor = SystemColors.Window
                row.DefaultCellStyle.ForeColor = SystemColors.ControlText
            Else
                row.DefaultCellStyle.BackColor = SystemColors.Control
                row.DefaultCellStyle.ForeColor = SystemColors.GrayText
            End If

            ' first cell is weapon type
            If ignoreDiff OrElse Me.craftObject.Weapons(i).WeaponType <> Me.lastCraftObject.Weapons(i).WeaponType Then
                row.Cells.Item(0).Value = TIEServices.WeaponTypeDescription(Me.craftObject.Weapons(i).WeaponType)
            End If
            ' 2nd cell is charge
            If ignoreDiff OrElse Me.craftObject.Weapons(i).CannonCharge <> Me.lastCraftObject.Weapons(i).CannonCharge Then
                row.Cells.Item(1).Value = String.Format("{0:0.0}%", Me.craftObject.Weapons(i).CannonCharge / 1.27)
            End If
            ' 3rd cell is warhead ammo
            If ignoreDiff OrElse Me.craftObject.Weapons(i).WarheadAmmo <> Me.lastCraftObject.Weapons(i).WarheadAmmo Then
                row.Cells.Item(2).Value = Me.craftObject.Weapons(i).WarheadAmmo
            End If
            ' 4th cell is target
            ' NOTE: Update behavior may need to be overriden if I change it to something other than the raw value, like the target craft's name
            If ignoreDiff OrElse Me.craftObject.Weapons(i).TurretTarget <> Me.lastCraftObject.Weapons(i).TurretTarget Then
                'row.Cells.Item(3).Value = String.Format("0x{0:X4}", Me.craftObject.Weapons(i).TurretTarget)
                row.Cells.Item(3).Value = TIEServices.TargetDescription(Me.craftObject.Weapons(i).TurretTarget)
            End If
        Next

        ' handle elements of the Parts arrays
        For i As Integer = 0 To 39
            Dim row As DataGridViewRow = dgvComponents.Rows.Item(i)
            ' 2nd cell is array_1A2
            If ignoreDiff OrElse Me.craftObject.Array_1A2(i) <> Me.lastCraftObject.Array_1A2(i) Then
                row.Cells.Item(1).Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Array_1A2(i))
            End If
            ' 3rd cell is array_1CA
            If ignoreDiff OrElse Me.craftObject.Array_1CA(i) <> Me.lastCraftObject.Array_1CA(i) Then
                row.Cells.Item(2).Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Array_1CA(i))
            End If
            ' 4th cell is array_PartHP
            If ignoreDiff OrElse Me.craftObject.PartHP(i) <> Me.lastCraftObject.PartHP(i) Then
                row.Cells.Item(3).Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.PartHP(i))
            End If
        Next

        ' Unknown Fields
        If ignoreDiff OrElse Me.craftObject.Field_1 <> Me.lastCraftObject.Field_1 Then
            cell_Field_1.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_1)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_2 <> Me.lastCraftObject.Field_2 Then
            cell_Field_2.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_2)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_4 <> Me.lastCraftObject.Field_4 Then
            cell_Field_4.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_4)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_A <> Me.lastCraftObject.Field_A Then
            cell_Field_A.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_A)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_C <> Me.lastCraftObject.Field_C Then
            cell_Field_C.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_C)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_E <> Me.lastCraftObject.Field_E Then
            cell_Field_E.Value = String.Format("{0}", Me.craftObject.Field_E)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_12 <> Me.lastCraftObject.Field_12 Then
            cell_Field_12.Value = String.Format("{0}", Me.craftObject.Field_12)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_16 <> Me.lastCraftObject.Field_16 Then
            cell_Field_16.Value = String.Format("{0}", Me.craftObject.Field_16)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_1A <> Me.lastCraftObject.Field_1A Then
            cell_Field_1A.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_1A)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_1B <> Me.lastCraftObject.Field_1B Then
            cell_Field_1B.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_1B)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_3C <> Me.lastCraftObject.Field_3C Then
            cell_Field_3C.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_3C)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_3E <> Me.lastCraftObject.Field_3E Then
            cell_Field_3E.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_3E)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_40 <> Me.lastCraftObject.Field_40 Then
            cell_Field_40.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_40)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_41 <> Me.lastCraftObject.Field_41 Then
            cell_Field_41.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_41)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_44 <> Me.lastCraftObject.Field_44 Then
            cell_Field_44.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_44)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_46 <> Me.lastCraftObject.Field_46 Then
            cell_Field_46.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_46)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_47 <> Me.lastCraftObject.Field_47 Then
            cell_Field_47.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_47)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_48 <> Me.lastCraftObject.Field_48 Then
            cell_Field_48.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_48)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_49 <> Me.lastCraftObject.Field_49 Then
            cell_Field_49.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_49)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_4A <> Me.lastCraftObject.Field_4A Then
            cell_Field_4A.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_4A)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_4B <> Me.lastCraftObject.Field_4B Then
            cell_Field_4B.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_4B)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_60 <> Me.lastCraftObject.Field_60 Then
            cell_Field_60.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_60)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_61 <> Me.lastCraftObject.Field_61 Then
            cell_Field_61.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_61)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_62 <> Me.lastCraftObject.Field_62 Then
            cell_Field_62.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_62)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_63 <> Me.lastCraftObject.Field_63 Then
            cell_Field_63.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_63)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_68 <> Me.lastCraftObject.Field_68 Then
            cell_Field_68.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_68)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_6C <> Me.lastCraftObject.Field_6C Then
            cell_Field_6C.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_6C)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_6E <> Me.lastCraftObject.Field_6E Then
            cell_Field_6E.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_6E)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_6F <> Me.lastCraftObject.Field_6F Then
            cell_Field_6F.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_6F)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_72 <> Me.lastCraftObject.Field_72 Then
            cell_Field_72.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_72)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_74 <> Me.lastCraftObject.Field_74 Then
            cell_Field_74.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_74)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_75 <> Me.lastCraftObject.Field_75 Then
            cell_Field_75.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_75)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_76 <> Me.lastCraftObject.Field_76 Then
            cell_Field_76.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_76)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_78 <> Me.lastCraftObject.Field_78 Then
            cell_Field_78.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_78)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_7C <> Me.lastCraftObject.Field_7C Then
            cell_Field_7C.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_7C)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_7E <> Me.lastCraftObject.Field_7E Then
            cell_Field_7E.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_7E)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_7F <> Me.lastCraftObject.Field_7F Then
            cell_Field_7F.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_7F)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_80 <> Me.lastCraftObject.Field_80 Then
            cell_Field_80.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_80)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_82 <> Me.lastCraftObject.Field_82 Then
            cell_Field_82.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_82)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_84 <> Me.lastCraftObject.Field_84 Then
            cell_Field_84.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_84)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_86 <> Me.lastCraftObject.Field_86 Then
            cell_Field_86.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_86)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_88 <> Me.lastCraftObject.Field_88 Then
            cell_Field_88.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_88)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_89 <> Me.lastCraftObject.Field_89 Then
            cell_Field_89.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_89)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_8A <> Me.lastCraftObject.Field_8A Then
            cell_Field_8A.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_8A)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_8C <> Me.lastCraftObject.Field_8C Then
            cell_Field_8C.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_8C)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_91 <> Me.lastCraftObject.Field_91 Then
            cell_Field_91.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_91)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_92 <> Me.lastCraftObject.Field_92 Then
            cell_Field_92.Value = String.Format("{0}", Me.craftObject.Field_92)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_96 <> Me.lastCraftObject.Field_96 Then
            cell_Field_96.Value = String.Format("{0}", Me.craftObject.Field_96)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_9A <> Me.lastCraftObject.Field_9A Then
            cell_Field_9A.Value = String.Format("{0}", Me.craftObject.Field_9A)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_A0 <> Me.lastCraftObject.Field_A0 Then
            cell_Field_A0.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_A0)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_A4 <> Me.lastCraftObject.Field_A4 Then
            cell_Field_A4.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_A4)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B0 <> Me.lastCraftObject.Field_B0 Then
            cell_Field_B0.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_B0)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B2 <> Me.lastCraftObject.Field_B2 Then
            cell_Field_B2.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_B2)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B4 <> Me.lastCraftObject.Field_B4 Then
            cell_Field_B4.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_B4)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B5 <> Me.lastCraftObject.Field_B5 Then
            cell_Field_B5.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_B5)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B6 <> Me.lastCraftObject.Field_B6 Then
            cell_Field_B6.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_B6)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_B7 <> Me.lastCraftObject.Field_B7 Then
            cell_Field_B7.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_B7)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_EC <> Me.lastCraftObject.Field_EC Then
            cell_Field_EC.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_EC)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_13F <> Me.lastCraftObject.Field_13F Then
            cell_Field_13F.Value = String.Format(UNKNOWN_BYTE_FORMAT, Me.craftObject.Field_13F)
        End If
        If ignoreDiff OrElse Me.craftObject.Field_140 <> Me.lastCraftObject.Field_140 Then
            cell_Field_140.Value = String.Format(UNKNOWN_WORD_FORMAT, Me.craftObject.Field_140)
        End If
    End Sub

    ''' <summary>
    ''' Updates the form's displays using the provided SpaceObject and CraftObject data.
    ''' </summary>
    ''' <param name="spaceObject"></param>
    ''' <param name="craftObject"></param>
    Public Sub UpdateDisplay(spaceObject As SpaceObject, craftObject As CraftObject)
        ' handle argument exceptions
        If spaceObject Is Nothing Then Throw New ArgumentNullException("spaceObject")
        If craftObject Is Nothing Then Throw New ArgumentNullException("craftObject")

        ' clone the data
        Me.spaceObject = spaceObject.Clone
        Me.craftObject = craftObject.Clone
        updateFields()
        Me.lastSpaceObject = Me.spaceObject
        Me.lastCraftObject = Me.craftObject
    End Sub

#Region " Event-Handlers "

    Private Sub FormCraftInfo_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' close, don't dispose!
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub dgvWeapons_SelectionChanged(sender As Object, e As EventArgs) Handles dgvWeapons.SelectionChanged
        ' you're not allowed to select things!
        dgvWeapons.ClearSelection()
    End Sub

    Private Sub dgvUnknownFields_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUnknownFields.SelectionChanged
        ' you're not allowed to select things!
        dgvUnknownFields.ClearSelection()
    End Sub

#End Region

#End Region
#Region " --- Properties --- "


#End Region

End Class