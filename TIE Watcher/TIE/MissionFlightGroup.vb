Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission Flight Group struct
    ''' </summary>
    Public Class MissionFlightGroup
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 292

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _name As String
        Private _pilot As String
        Private _cargo As String
        Private _specialCargo As String
        Private _specialCargoCraft As Byte
        Private _randomSpecialCargoCraft As Byte
        Private _craftType As Byte
        Private _numCraft As Byte
        Private _status As MissionFlightGroupStatus
        Private _warhead As MissionFlightGroupWarhead
        Private _beam As MissionFlightGroupBeam
        Private _IFF As Byte
        Private _groupAI As MissionFlightGroupAI
        Private _markings As MissionFlightGroupMarkings
        Private _obeyPlayer As Byte
        Private _field_3B As Byte
        Private _formation As MissionFlightGroupFormation
        Private _formationSpacing As Byte
        Private _globalGroup As Byte
        Private _leaderSpacing As Byte
        Private _numWaves As Byte
        Private _field_41 As Byte
        Private _playerCraft As Byte
        Private _yaw As Byte
        Private _pitch As Byte
        Private _roll As Byte
        Private _field_46 As Byte
        Private _field_47 As Byte
        Private _field_48 As Byte
        Private _arrivalDifficulty As MissionFlightGroupArrivalDifficulty
        Private _trig_arrival() As MissionTrigger
        Private _arrival_1or2 As Byte
        Private _field_53 As Byte
        Private _arrivalDelayMin As Byte
        Private _arrivalDelaySec As Byte
        Private _trig_departure As MissionTrigger
        Private _departDelayMin As Byte
        Private _departDelaySec As Byte
        Private _abort As Byte
        Private _field_5D As Byte
        Private _field_5E As Byte
        Private _field_5F As Byte
        Private _arrivalMothership As Byte
        Private _arriveViaMothership As Byte
        Private _departureMothership As Byte
        Private _departViaMothership As Byte
        Private _altArrivalMothership As Byte
        Private _altArriveViaMothership As Byte
        Private _altDepartureMothership As Byte
        Private _altDepartViaMothership As Byte
        Private _order() As MissionOrder
        Private _goal() As MissionFlightGroupGoal
        Private _bonusPoints As SByte
        Private _waypoints() As MissionWaypoint
        Private _field_120 As UInt16
        Private _field_122 As Byte
        Private _field_123 As Byte

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class.
        ''' </summary>
        Public Sub New()
            ' create arrays of the appropriate lengths for members
            Me._trig_arrival = Array.CreateInstance(GetType(MissionTrigger), 2)
            For i As Integer = 0 To 1
                Me._trig_arrival(i) = New MissionTrigger
            Next
            Me._order = Array.CreateInstance(GetType(MissionOrder), 3)
            For i As Integer = 0 To 2
                Me._order(i) = New MissionOrder
            Next
            Me._goal = Array.CreateInstance(GetType(MissionFlightGroupGoal), 4)
            For i As Integer = 0 To 3
                Me._goal(i) = New MissionFlightGroupGoal
            Next
            Me._waypoints = Array.CreateInstance(GetType(MissionWaypoint), 15)
            For i As Integer = 0 To 14
                Me._waypoints(i) = New MissionWaypoint(0, 0, 0, 0)
            Next
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionFlightGroup = DirectCast(Me.MemberwiseClone, MissionFlightGroup)

            ' since this class has contains object references, we need to make deep copies of those, too
            ret._trig_arrival = Array.CreateInstance(GetType(MissionTrigger), 2)
            For i As Integer = 0 To 1
                ret._trig_arrival(i) = Me._trig_arrival(i).Clone
            Next
            ret._order = Array.CreateInstance(GetType(MissionOrder), 3)
            For i As Integer = 0 To 2
                ret._order(i) = Me._order(i).Clone
            Next
            ret._goal = Array.CreateInstance(GetType(MissionFlightGroupGoal), 4)
            For i As Integer = 0 To 3
                ret._goal(i) = Me._goal(i).Clone
            Next
            ret._waypoints = Array.CreateInstance(GetType(MissionWaypoint), 15)
            For i As Integer = 0 To 14
                ret._waypoints(i) = Me._waypoints(i).Clone
            Next

            Return ret
        End Function

#End Region

        ' Updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._name = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, 0, 12))
            Me._pilot = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &HC, 12))
            Me._cargo = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &H18, 12))
            Me._specialCargo = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &H24, 12))
            Me._specialCargoCraft = bytes(&H30)
            Me._randomSpecialCargoCraft = bytes(&H31)
            Me._craftType = bytes(&H32)
            Me._numCraft = bytes(&H33)
            Me._status = bytes(&H34)
            Me._warhead = bytes(&H35)
            Me._beam = bytes(&H36)
            Me._IFF = bytes(&H37)
            Me._groupAI = bytes(&H38)
            Me._markings = bytes(&H39)
            Me._obeyPlayer = bytes(&H3A)
            Me._field_3B = bytes(&H3B)
            Me._formation = bytes(&H3C)
            Me._formationSpacing = bytes(&H3D)
            Me._globalGroup = bytes(&H3E)
            Me._leaderSpacing = bytes(&H3F)
            Me._numWaves = bytes(&H40)
            Me._field_41 = bytes(&H41)
            Me._playerCraft = bytes(&H42)
            Me._yaw = bytes(&H43)
            Me._pitch = bytes(&H44)
            Me._roll = bytes(&H45)
            Me._field_46 = bytes(&H46)
            Me._field_47 = bytes(&H47)
            Me._field_48 = bytes(&H48)
            Me._arrivalDifficulty = bytes(&H49)
            For i = 0 To 1
                Dim subBytes(3) As Byte
                Array.Copy(bytes, &H4A + i * 4, subBytes, 0, 4)
                If Me._trig_arrival(i) Is Nothing Then
                    Me._trig_arrival(i) = MissionTrigger.FromBytes(subBytes)
                Else
                    Me._trig_arrival(i).UpdateBytes(subBytes)
                End If
            Next 'i
            Me._arrival_1or2 = bytes(&H52)
            Me._field_53 = bytes(&H53)
            Me._arrivalDelayMin = bytes(&H54)
            Me._arrivalDelaySec = bytes(&H55)
            If Me._trig_departure Is Nothing Then
                Dim subBytes(3) As Byte
                Array.Copy(bytes, &H56, subBytes, 0, 4)
                Me._trig_departure = MissionTrigger.FromBytes(subBytes)
            Else
                Dim subBytes(3) As Byte
                Array.Copy(bytes, &H56, subBytes, 0, 4)
                Me._trig_departure.UpdateBytes(subBytes)
            End If
            Me._departDelayMin = bytes(&H5A)
            Me._departDelaySec = bytes(&H5B)
            Me._abort = bytes(&H5C)
            Me._field_5D = bytes(&H5D)
            Me._field_5E = bytes(&H5E)
            Me._field_5F = bytes(&H5F)
            Me._arrivalMothership = bytes(&H60)
            Me._arriveViaMothership = bytes(&H61)
            Me._departureMothership = bytes(&H62)
            Me._departViaMothership = bytes(&H63)
            Me._altArrivalMothership = bytes(&H64)
            Me._altArriveViaMothership = bytes(&H65)
            Me._altDepartureMothership = bytes(&H66)
            Me._altDepartViaMothership = bytes(&H67)
            For i = 0 To 2
                Dim subBytes(17) As Byte
                Array.Copy(bytes, &H68 + i * 18, subBytes, 0, 18)
                If Me._order(i) Is Nothing Then
                    Me._order(i) = MissionOrder.FromBytes(subBytes)
                Else
                    Me._order(i).UpdateBytes(subBytes)
                End If
            Next 'i
            For i = 0 To 3
                Dim subBytes(1) As Byte
                Array.Copy(bytes, &H9E + i * 2, subBytes, 0, 2)
                If Me._goal(i) Is Nothing Then
                    Me._goal(i) = MissionFlightGroupGoal.FromBytes(subBytes)
                Else
                    Me._goal(i).UpdateBytes(subBytes)
                End If
            Next 'i
            ' bit of a kludge but whatever
            Me._bonusPoints = IIf(bytes(&HA6) >= 128, bytes(&HA6) - 256, bytes(&HA6))
            ' handle the Waypoints, which are arranged strangely in the original struct layout, but more sensibly here
            For i = 0 To 14
                If Me._waypoints(i) Is Nothing Then
                    ' make a new waypoint
                    Me._waypoints(i) = New MissionWaypoint(BitConverter.ToInt16(bytes, &HA8 + i * 2),
                                                       BitConverter.ToInt16(bytes, &HC6 + i * 2),
                                                       BitConverter.ToInt16(bytes, &HE4 + i * 2),
                                                       BitConverter.ToInt16(bytes, &H102 + i * 2))
                Else
                    ' update the existing one's properties
                    Me._waypoints(i).X = BitConverter.ToInt16(bytes, &HA8 + i * 2)
                    Me._waypoints(i).Y = BitConverter.ToInt16(bytes, &HC6 + i * 2)
                    Me._waypoints(i).Z = BitConverter.ToInt16(bytes, &HE4 + i * 2)
                    Me._waypoints(i).Valid = BitConverter.ToInt16(bytes, &H102 + i * 2)
                End If
            Next 'i
            Me._field_120 = BitConverter.ToUInt16(bytes, &H120)
            Me._field_122 = bytes(&H122)
            Me._field_123 = bytes(&H123)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionFlightGroup
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New MissionFlightGroup
            ' update all the fields using the supplied bytes
            ret.updateFromBytes(bytes)
            ' return the new instance
            Return ret
        End Function

        ''' <summary>
        ''' Updates all of the class' fields using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to update the structure with.</param>
        Public Sub UpdateBytes(bytes() As Byte)
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            Me.updateFromBytes(bytes)
        End Sub

#End Region
#Region " --- Properties --- "

        ''' <summary>
        ''' Gets size, in bytes, of the struct the class represents.
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property SizeOf As Integer
            Get
                Return SIZE_OF
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the Flight Group.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property Name As String
            Get
                Return Me._name
            End Get
        End Property

        ''' <summary>
        ''' Gets the Flight Group Pilot string.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property Pilot As String
            Get
                Return Me._pilot
            End Get
        End Property

        Public ReadOnly Property Cargo As String
            Get
                Return Me._cargo
            End Get
        End Property

        Public ReadOnly Property SpecialCargo As String
            Get
                Return Me._specialCargo
            End Get
        End Property

        Public ReadOnly Property SpecialCargoCraft As Byte
            Get
                Return Me._specialCargoCraft
            End Get
        End Property

        Public ReadOnly Property RandomSpecialCargoCraft As Byte
            Get
                Return Me._randomSpecialCargoCraft
            End Get
        End Property

        Public ReadOnly Property CraftType As SpaceObjectType
            Get
                Return Me._craftType
            End Get
        End Property

        Public ReadOnly Property NumCraft As Byte
            Get
                Return Me._numCraft
            End Get
        End Property

        Public ReadOnly Property Status As MissionFlightGroupStatus
            Get
                Return Me._status
            End Get
        End Property

        Public ReadOnly Property Warhead As MissionFlightGroupWarhead
            Get
                Return Me._warhead
            End Get
        End Property

        Public ReadOnly Property Beam As MissionFlightGroupBeam
            Get
                Return Me._beam
            End Get
        End Property

        Public ReadOnly Property IFF As Byte
            Get
                Return Me._IFF
            End Get
        End Property

        Public ReadOnly Property GroupAI As MissionFlightGroupAI
            Get
                Return Me._groupAI
            End Get
        End Property

        Public ReadOnly Property Markings As MissionFlightGroupMarkings
            Get
                Return Me._markings
            End Get
        End Property

        Public ReadOnly Property ObeyPlayer As Byte
            Get
                Return Me._obeyPlayer
            End Get
        End Property

        'Public ReadOnly Property field_3B As Byte
        '    Get
        '        Return Me._field_3B
        '    End Get
        'End Property

        Public ReadOnly Property Formation As MissionFlightGroupFormation
            Get
                Return Me._formation
            End Get
        End Property

        Public ReadOnly Property FormationSpacing As Byte
            Get
                Return Me._formationSpacing
            End Get
        End Property

        Public ReadOnly Property GlobalGroup As Byte
            Get
                Return Me._globalGroup
            End Get
        End Property

        Public ReadOnly Property LeaderSpacing As Byte
            Get
                Return Me._leaderSpacing
            End Get
        End Property

        Public ReadOnly Property NumWaves As Byte
            Get
                Return Me._numWaves
            End Get
        End Property

        Public ReadOnly Property PlayerCraft As Byte
            Get
                Return Me._playerCraft
            End Get
        End Property

        Public ReadOnly Property Yaw As Byte
            Get
                Return Me._yaw
            End Get
        End Property

        Public ReadOnly Property Pitch As Byte
            Get
                Return Me._pitch
            End Get
        End Property

        Public ReadOnly Property Roll As Byte
            Get
                Return Me._roll
            End Get
        End Property

        Public ReadOnly Property ArrivalDifficulty As MissionFlightGroupArrivalDifficulty
            Get
                Return Me._arrivalDifficulty
            End Get
        End Property

        Public ReadOnly Property ArrivalTrigger(index As Integer) As MissionTrigger
            Get
                If index < 0 Or index > 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._trig_arrival(index)
            End Get
        End Property

        Public ReadOnly Property ArrivalBoolean As Byte
            Get
                Return Me._arrival_1or2
            End Get
        End Property

        Public ReadOnly Property ArrivalDelayMinutes As Byte
            Get
                Return Me._arrivalDelayMin
            End Get
        End Property

        Public ReadOnly Property ArrivalDelaySeconds As Byte
            Get
                Return Me._arrivalDelaySec
            End Get
        End Property

        Public ReadOnly Property DepartureTrigger As MissionTrigger
            Get
                Return Me._trig_departure
            End Get
        End Property

        Public ReadOnly Property DepatureDelayMinutes As Byte
            Get
                Return Me._departDelayMin
            End Get
        End Property

        Public ReadOnly Property DepatureDelaySeconds As Byte
            Get
                Return Me._departDelaySec
            End Get
        End Property

        Public ReadOnly Property Abort As MissionAbortTrigger
            Get
                Return Me._abort
            End Get
        End Property

        Public ReadOnly Property ArrivalMothership As Byte
            Get
                Return Me._arrivalMothership
            End Get
        End Property

        Public ReadOnly Property ArriveViaMothership As Byte
            Get
                Return Me._arriveViaMothership
            End Get
        End Property

        Public ReadOnly Property DepartureMothership As Byte
            Get
                Return Me._departureMothership
            End Get
        End Property

        Public ReadOnly Property DepartViaMothership As Byte
            Get
                Return Me._departViaMothership
            End Get
        End Property

        Public ReadOnly Property AltArrivalMothership As Byte
            Get
                Return Me._altArrivalMothership
            End Get
        End Property

        Public ReadOnly Property AltArriveViaMothership As Byte
            Get
                Return Me._altArriveViaMothership
            End Get
        End Property

        Public ReadOnly Property AltDepartureMothership As Byte
            Get
                Return Me._altDepartureMothership
            End Get
        End Property

        Public ReadOnly Property AltDepartViaMothership As Byte
            Get
                Return Me._altDepartViaMothership
            End Get
        End Property

        Public ReadOnly Property Orders(index As Integer) As MissionOrder
            Get
                If index < 0 Or index > 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._order(index)
            End Get
        End Property

        Public ReadOnly Property Goals(index As Integer) As MissionFlightGroupGoal
            Get
                If index < 0 Or index > 3 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._goal(index)
            End Get
        End Property

        Public ReadOnly Property BonusPoints As SByte
            Get
                Return Me._bonusPoints
            End Get
        End Property

        Public ReadOnly Property Waypoints(index As Integer) As MissionWaypoint
            Get
                If index < 0 Or index > 14 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._waypoints(index)
            End Get
        End Property
#End Region

    End Class

End Namespace