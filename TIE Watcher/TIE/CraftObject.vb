Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Craft_Object struct
    ''' </summary>
    Public Class CraftObject
        Implements ICloneable

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = &H21A

#End Region
#Region " --- Variables --- "

        Private _craftType As Byte      ' IDA: Craft's Type, derived from field_13 of the struct_28BAF8 and used for the Craft_Stats array
        Private _field_1 As Byte
        Private _field_2 As UInt16
        Private _field_4 As UInt16
        Private _pitch As Int16
        Private _yaw As Int16
        Private _field_A As UInt16
        Private _field_C As UInt16
        Private _field_E As Int32       ' IDA: x-Position of Craft relative to Camera's view? (left/right)
        Private _field_12 As Int32      ' IDA: y-Position of Craft relative to Camera's view? (up/down)
        Private _field_16 As Int32      ' IDA: z-Position of Craft relative to Camera's view? (depth)
        Private _field_1A As Byte
        Private _field_1B As Byte
        Private _orderMissionIndex As Byte      ' IDA: Orders - Craft's Current MISS_FG Order index
        Private _array_1D() As Byte     ' IDA: ? (starts as 0) Set to 2 on Order completion
        Private _array_20() As Byte     ' IDA: Orders - Current Variable 1 count for each Mission Order
        Private _scriptCurrent As Byte  ' IDA: Orders - Active Order (i.e. actions involved with the overall order)
        Private _scriptOverall As Byte  ' IDA: Orders - Overall Order
        Private _scriptWaypoint As Byte  ' IDA: Orders - Current Waypoint index
        Private _field_26 As UInt16
        Private _scriptTimerReload As Int16    ' IDA: Orders - Script Timer Reload value (time between execution of Orders Scripts)
        Private _scriptTimer As Int16          ' IDA: Orders - Script Timer
        Private _scriptTarget As UInt16          ' IDA: Orders - Current Target (Space_Object or Waypoint)
        Private _field_2E As UInt16
        Private _scriptTargetXPosition As Int32  ' IDA: Orders - x-Position of Target
        Private _scriptTargetYPosition As Int32  ' IDA: Orders - y-Position of Target
        Private _scriptTargetZPosition As Int32  ' IDA: Orders - z-Position of Target
        Private _field_3C As UInt16
        Private _field_3E As UInt16
        Private _field_40 As Byte
        Private _field_41 As Byte
        Private _lastAttacker As UInt16     ' IDA: Space_Object index of last Attacker (set on hit from projectile, reset when Orders change)
        Private _field_44 As UInt16
        Private _field_46 As Byte
        Private _field_47 As Byte
        Private _field_48 As Byte
        Private _field_49 As Byte
        Private _field_4A As Byte       ' IDA: Count of valid entries in array_4C
        Private _field_4B As Byte
        Private _array_4C() As UInt16     ' 10 elements
        Private _field_60 As Byte
        Private _field_61 As Byte
        Private _field_62 As Byte
        Private _field_63 As Byte
        Private _orderTimer As UInt32   ' IDA: Countdown Timer for current Order (i.e. docking or exiting hangar)
        Private _field_68 As UInt16
        Private _baseSpeed As UInt16    ' IDA: Base Speed (max velocity at normal engine power)
        Private _field_6C As UInt16
        Private _field_6E As Byte
        Private _field_6F As Byte
        Private _rotVertical As UInt16  ' IDA: Base Vertical Pitch (turning rate?)
        Private _field_72 As UInt16
        Private _field_74 As Byte
        Private _field_75 As Byte
        Private _field_76 As Int16
        Private _field_78 As UInt16
        Private _rotHorizontal As UInt16    ' IDA: Base Horizontal Pitch (turning rate?)
        Private _field_7C As UInt16
        Private _field_7E As Byte
        Private _field_7F As Byte
        Private _field_80 As UInt16
        Private _field_82 As UInt16
        Private _field_84 As UInt16     ' IDA: ? (starts as Craft_Stats.field_20) -- may be Roll turning rate
        Private _field_86 As UInt16
        Private _field_88 As Byte
        Private _field_89 As Byte
        Private _field_8A As Int16
        Private _field_8C As UInt16
        Private _flightGroupFormation As Byte
        Private _flightGroupSpacing As Byte
        Private _flightGroupMemberIndex As Byte
        Private _field_91 As Byte
        Private _field_92 As Int32      ' IDA: x-Coordinate of Docking-related vector
        Private _field_96 As Int32      ' IDA: y-Coordinate of Docking-related vector
        Private _field_9A As Int32      ' IDA: z-Coordinate of Docking-related vector
        Private _throttle As UInt16
        Private _field_A0 As UInt16
        Private _damage As UInt16
        Private _field_A4 As UInt16
        Private _maxDamage As UInt16
        Private _panelFlags As UInt16
        Private _panelDamageFlags As UInt16
        Private _featureFlags As UInt16
        Private _featureDamageFlags As UInt16
        Private _field_B0 As UInt16
        Private _field_B2 As UInt16
        Private _field_B4 As Byte
        Private _field_B5 As Byte
        Private _field_B6 As Byte
        Private _field_B7 As Byte
        Private _inspected As Byte
        Private _cargoStatus As Byte
        Private _cargoString As String
        Private _shieldChargeFore As UInt16
        Private _shieldChargeRear As UInt16
        Private _shieldChargeRate As Byte
        Private _shieldBalance As Byte
        Private _cannonBanks As Byte        ' IDA: Cannon System - Number of Cannon Banks (Laser, Ion)
        Private _cannonChargeRate As Byte   ' IDA: Cannon System - Charge Rate
        Private _cannonCount As Byte        ' IDA: Cannon System - Total Cannons (i.e. 6 for T/D)
        Private _cannonType() As Byte       ' IDA: Cannon System - Bank's Weapon Type
        Private _cannonLink() As Byte       ' IDA: Cannon System - Bank's Link State
        Private _array_D7() As Byte
        Private _cannonNext() As Byte       ' IDA: Cannon System - Next Weapon Index to Fire
        Private _field_DB As Byte
        Private _cannonTimer() As UInt16    ' IDA: Cannon System - Cooldown Timer
        Private _warheadBanks As Byte       ' IDA: Warhead System - Number of Warhead Banks
        Private _warheadType() As Byte      ' IDA: Warhead System - Weapon Type
        Private _warheadNext() As Byte      ' IDA: Warhead System - Next Weapon Index to Fire
        Private _field_E5 As Byte
        Private _warheadTimer() As UInt16   ' IDA: Warhead System - Cooldown Timer
        Private _warheadLock As UInt16      ' IDA: Warhead System - Lock Timer
        Private _field_EC As Byte
        Private _beamChargeRate As Byte     ' IDA: Beam Weapon - Charge Rate
        Private _beamCharge As UInt16       ' IDA: Beam Weapon - Charge
        Private _lasersFired As UInt16      ' IDA: Number of Laser shots fired
        Private _lasersHit As UInt16        ' IDA: Number of Laser shots hit
        Private _ionsFired As UInt16        ' IDA: Number of Ion shots fired
        Private _ionsHit As UInt16          ' IDA: Number of Ion shots hit
        Private _warheadsFired As Byte      ' IDA: Number of Warheads fired
        Private _warheadsHit As Byte        ' IDA: Number of Warheads hit
        Private _kills() As Byte            ' IDA: Seems to be a kill counter for each of the other craft types, using their 'stats index'
        Private _field_13F As Byte
        Private _field_140 As UInt16
        Private _weapons() As CraftObjectWeapon
        Private _array_1A2() As Byte        ' IDA: ? (related to targettable components)
        Private _array_1CA() As Byte        ' IDA: ? (starts as 0)
        Private _partHP() As Byte           ' IDA: HP of craft's Components/Parts

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class.
        ''' </summary>
        Public Sub New()
            ' create all the arrays used in this class
            Me._array_1D = Array.CreateInstance(GetType(Byte), 3)
            Me._array_20 = Array.CreateInstance(GetType(Byte), 3)
            Me._array_4C = Array.CreateInstance(GetType(UInt16), 10)
            Me._cannonType = Array.CreateInstance(GetType(Byte), 2)
            Me._cannonLink = Array.CreateInstance(GetType(Byte), 2)
            Me._array_D7 = Array.CreateInstance(GetType(Byte), 2)
            Me._cannonNext = Array.CreateInstance(GetType(Byte), 2)
            Me._cannonTimer = Array.CreateInstance(GetType(UInt16), 2)
            Me._warheadType = Array.CreateInstance(GetType(Byte), 2)
            Me._warheadNext = Array.CreateInstance(GetType(Byte), 2)
            Me._warheadTimer = Array.CreateInstance(GetType(UInt16), 2)
            Me._kills = Array.CreateInstance(GetType(Byte), 69)
            Me._weapons = Array.CreateInstance(GetType(CraftObjectWeapon), 16)
            For i As Integer = 0 To 15
                Me._weapons(i) = New CraftObjectWeapon
            Next
            Me._array_1A2 = Array.CreateInstance(GetType(Byte), 40)
            Me._array_1CA = Array.CreateInstance(GetType(Byte), 40)
            Me._partHP = Array.CreateInstance(GetType(Byte), 40)
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As CraftObject = DirectCast(Me.MemberwiseClone, CraftObject)

            ' since this class has contains object references, we need to make deep copies of those, too
            ret._array_1D = Array.CreateInstance(GetType(Byte), 3)
            ' arrays of basic types (i.e. bytes, intergers) can be safely and quickly copied thusly
            Array.Copy(Me._array_1D, ret._array_1D, 3)
            ret._array_20 = Array.CreateInstance(GetType(Byte), 3)
            Array.Copy(Me._array_20, ret._array_20, 3)
            ret._array_4C = Array.CreateInstance(GetType(UInt16), 10)
            Array.Copy(Me._array_4C, ret._array_4C, 10)
            ret._cannonType = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._cannonType, ret._cannonType, 2)
            ret._cannonLink = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._cannonLink, ret._cannonLink, 2)
            ret._array_D7 = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._array_D7, ret._array_D7, 2)
            ret._cannonNext = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._cannonNext, ret._cannonNext, 2)
            ret._cannonTimer = Array.CreateInstance(GetType(UInt16), 2)
            Array.Copy(Me._cannonTimer, ret._cannonTimer, 2)
            ret._warheadType = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._warheadType, ret._warheadType, 2)
            ret._warheadNext = Array.CreateInstance(GetType(Byte), 2)
            Array.Copy(Me._warheadNext, ret._warheadNext, 2)
            ret._warheadTimer = Array.CreateInstance(GetType(UInt16), 2)
            Array.Copy(Me._warheadTimer, ret._warheadTimer, 2)
            ret._kills = Array.CreateInstance(GetType(Byte), 69)
            Array.Copy(Me._kills, ret._kills, 69)
            ret._weapons = Array.CreateInstance(GetType(CraftObjectWeapon), 16)
            For i As Integer = 0 To 15
                ret._weapons(i) = Me._weapons(i).Clone
            Next
            ret._array_1A2 = Array.CreateInstance(GetType(Byte), 40)
            Array.Copy(Me._array_1A2, ret._array_1A2, 40)
            ret._array_1CA = Array.CreateInstance(GetType(Byte), 40)
            Array.Copy(Me._array_1CA, ret._array_1CA, 40)
            ret._partHP = Array.CreateInstance(GetType(Byte), 40)
            Array.Copy(Me._partHP, ret._partHP, 40)

            Return ret
        End Function

#End Region

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._craftType = bytes(0)
            Me._field_1 = bytes(1)
            Me._field_2 = BitConverter.ToUInt16(bytes, &H2)
            Me._field_4 = BitConverter.ToUInt16(bytes, &H4)
            Me._pitch = BitConverter.ToInt16(bytes, &H6)
            Me._yaw = BitConverter.ToInt16(bytes, &H8)
            Me._field_A = BitConverter.ToUInt16(bytes, &HA)
            Me._field_C = BitConverter.ToUInt16(bytes, &HC)
            Me._field_E = BitConverter.ToInt32(bytes, &HE)
            Me._field_12 = BitConverter.ToInt32(bytes, &H12)
            Me._field_16 = BitConverter.ToInt32(bytes, &H16)
            Me._field_1A = bytes(&H1A)
            Me._field_1B = bytes(&H1B)
            Me._orderMissionIndex = bytes(&H1C)
            ' all arrays of length 2
            For i As Integer = 0 To 1
                Me._cannonType(i) = bytes(&HD3 + i)
                Me._cannonLink(i) = bytes(&HD5 + i)
                Me._array_D7(i) = bytes(&HD7 + i)
                Me._cannonNext(i) = bytes(&HD5 + i)
                Me._cannonTimer(i) = BitConverter.ToUInt16(bytes, &HDC + i * 2)
                Me._warheadType(i) = bytes(&HE1 + i)
                Me._warheadNext(i) = bytes(&HE3 + i)
                Me._warheadTimer(i) = BitConverter.ToUInt16(bytes, &HE6 + i * 2)
            Next
            ' all arrays of length 3
            For i As Integer = 0 To 2
                Me._array_1D(i) = bytes(&H1D + i)
                Me._array_20(i) = bytes(&H20 + i)
            Next
            Me._scriptCurrent = bytes(&H23)
            Me._scriptOverall = bytes(&H24)
            Me._scriptWaypoint = bytes(&H25)
            Me._field_26 = BitConverter.ToUInt16(bytes, &H26)
            Me._scriptTimerReload = BitConverter.ToInt16(bytes, &H28)
            Me._scriptTimer = BitConverter.ToInt16(bytes, &H2A)
            Me._scriptTarget = BitConverter.ToUInt16(bytes, &H2C)
            Me._field_2E = BitConverter.ToUInt16(bytes, &H2E)
            Me._scriptTargetXPosition = BitConverter.ToInt32(bytes, &H30)
            Me._scriptTargetYPosition = BitConverter.ToInt32(bytes, &H34)
            Me._scriptTargetZPosition = BitConverter.ToInt32(bytes, &H38)
            Me._field_3C = BitConverter.ToUInt16(bytes, &H3C)
            Me._field_3E = BitConverter.ToUInt16(bytes, &H3E)
            Me._field_40 = bytes(&H40)
            Me._field_41 = bytes(&H41)
            Me._lastAttacker = BitConverter.ToUInt16(bytes, &H42)
            Me._field_44 = BitConverter.ToUInt16(bytes, &H44)
            Me._field_46 = bytes(&H46)
            Me._field_47 = bytes(&H47)
            Me._field_48 = bytes(&H48)
            Me._field_49 = bytes(&H49)
            Me._field_4A = bytes(&H4A)
            Me._field_4B = bytes(&H4B)
            For i As Integer = 0 To 9
                Me._array_4C(i) = BitConverter.ToUInt16(bytes, &H4C + i * 2)
            Next
            Me._field_60 = bytes(&H60)
            Me._field_61 = bytes(&H61)
            Me._field_62 = bytes(&H62)
            Me._field_63 = bytes(&H63)
            Me._orderTimer = BitConverter.ToUInt32(bytes, &H64)
            Me._field_68 = BitConverter.ToUInt16(bytes, &H68)
            Me._baseSpeed = BitConverter.ToUInt16(bytes, &H6A)
            Me._field_6C = BitConverter.ToUInt16(bytes, &H6C)
            Me._field_6E = bytes(&H6E)
            Me._field_6F = bytes(&H6F)
            Me._rotVertical = BitConverter.ToUInt16(bytes, &H70)
            Me._field_72 = BitConverter.ToUInt16(bytes, &H72)
            Me._field_74 = bytes(&H74)
            Me._field_75 = bytes(&H75)
            Me._field_76 = BitConverter.ToInt16(bytes, &H76)
            Me._field_78 = BitConverter.ToUInt16(bytes, &H78)
            Me._rotHorizontal = BitConverter.ToUInt16(bytes, &H7A)
            Me._field_7C = BitConverter.ToUInt16(bytes, &H7C)
            Me._field_7E = bytes(&H7E)
            Me._field_7F = bytes(&H7F)
            Me._field_80 = BitConverter.ToUInt16(bytes, &H80)
            Me._field_82 = BitConverter.ToUInt16(bytes, &H82)
            Me._field_84 = BitConverter.ToUInt16(bytes, &H84)
            Me._field_86 = BitConverter.ToUInt16(bytes, &H86)
            Me._field_88 = bytes(&H88)
            Me._field_89 = bytes(&H89)
            Me._field_8A = BitConverter.ToInt16(bytes, &H8A)
            Me._field_8C = BitConverter.ToUInt16(bytes, &H8C)
            Me._flightGroupFormation = bytes(&H8E)
            Me._flightGroupSpacing = bytes(&H8F)
            Me._flightGroupMemberIndex = bytes(&H90)
            Me._field_91 = bytes(&H91)
            Me._field_92 = BitConverter.ToInt32(bytes, &H92)
            Me._field_96 = BitConverter.ToInt32(bytes, &H96)
            Me._field_9A = BitConverter.ToInt32(bytes, &H9A)
            Me._throttle = BitConverter.ToUInt16(bytes, &H9E)
            Me._field_A0 = BitConverter.ToUInt16(bytes, &HA0)
            Me._damage = BitConverter.ToUInt16(bytes, &HA2)
            Me._field_A4 = BitConverter.ToUInt16(bytes, &HA4)
            Me._maxDamage = BitConverter.ToUInt16(bytes, &HA6)
            Me._panelFlags = BitConverter.ToUInt16(bytes, &HA8)
            Me._panelDamageFlags = BitConverter.ToUInt16(bytes, &HAA)
            Me._featureFlags = BitConverter.ToUInt16(bytes, &HAC)
            Me._featureDamageFlags = BitConverter.ToUInt16(bytes, &HAE)
            Me._field_B0 = BitConverter.ToUInt16(bytes, &HB0)
            Me._field_B2 = BitConverter.ToUInt16(bytes, &HB2)
            Me._field_B4 = bytes(&HB4)
            Me._field_B5 = bytes(&HB5)
            Me._field_B6 = bytes(&HB6)
            Me._field_B7 = bytes(&HB7)
            Me._inspected = bytes(&HB8)
            Me._cargoStatus = bytes(&HB9)
            Me._cargoString = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &HBA, 16))
            Me._shieldChargeFore = BitConverter.ToUInt16(bytes, &HCA)
            Me._shieldChargeRear = BitConverter.ToUInt16(bytes, &HCC)
            Me._shieldChargeRate = bytes(&HCE)
            Me._shieldBalance = bytes(&HCF)
            Me._cannonBanks = bytes(&HD0)
            Me._cannonChargeRate = bytes(&HD1)
            Me._cannonCount = bytes(&HD2)
            Me._field_DB = bytes(&HDB)
            Me._warheadBanks = bytes(&HE0)
            Me._field_E5 = bytes(&HE5)
            Me._warheadLock = BitConverter.ToUInt16(bytes, &HEA)
            Me._field_EC = bytes(&HEC)
            Me._beamChargeRate = bytes(&HED)
            Me._beamCharge = BitConverter.ToUInt16(bytes, &HEE)
            Me._lasersFired = BitConverter.ToUInt16(bytes, &HF0)
            Me._lasersHit = BitConverter.ToUInt16(bytes, &HF2)
            Me._ionsFired = BitConverter.ToUInt16(bytes, &HF4)
            Me._ionsHit = BitConverter.ToUInt16(bytes, &HF6)
            Me._warheadsFired = bytes(&HF8)
            Me._warheadsHit = bytes(&HF9)
            For i As Integer = 0 To 68
                Me._kills(i) = bytes(&HFA + i)
            Next
            Me._field_13F = bytes(&H13F)
            Me._field_140 = BitConverter.ToUInt16(bytes, &H140)
            For i As Integer = 0 To 15
                Dim subBytes(CraftObjectWeapon.SizeOf - 1) As Byte
                Array.Copy(bytes, &H142 + CraftObjectWeapon.SizeOf * i, subBytes, 0, CraftObjectWeapon.SizeOf)
                Me._weapons(i).UpdateBytes(subBytes)
            Next
            For i As Integer = 0 To 39
                Me._array_1A2(i) = bytes(&H1A2 + i)
                Me._array_1CA(i) = bytes(&H1CA + i)
                Me._partHP(i) = bytes(&H1F2 + i)
            Next
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As CraftObject
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New CraftObject
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

        Public ReadOnly Property CraftType As Byte
            Get
                Return Me._craftType
            End Get
        End Property

        Public ReadOnly Property Field_1 As Byte
            Get
                Return Me._field_1
            End Get
        End Property

        Public ReadOnly Property Field_2 As UInt16
            Get
                Return Me._field_2
            End Get
        End Property

        Public ReadOnly Property Field_4 As UInt16
            Get
                Return Me._field_4
            End Get
        End Property

        Public ReadOnly Property Pitch As Int16
            Get
                Return Me._pitch
            End Get
        End Property

        Public ReadOnly Property Yaw As Int16
            Get
                Return Me._yaw
            End Get
        End Property

        Public ReadOnly Property Field_A As UInt16
            Get
                Return Me._field_A
            End Get
        End Property

        Public ReadOnly Property Field_C As UInt16
            Get
                Return Me._field_C
            End Get
        End Property

        Public ReadOnly Property Field_E As Int32
            Get
                Return Me._field_E
            End Get
        End Property

        Public ReadOnly Property Field_12 As Int32
            Get
                Return Me._field_12
            End Get
        End Property

        Public ReadOnly Property Field_16 As Int32
            Get
                Return Me._field_16
            End Get
        End Property

        Public ReadOnly Property Field_1A As Byte
            Get
                Return Me._field_1A
            End Get
        End Property

        Public ReadOnly Property Field_1B As Byte
            Get
                Return Me._field_1B
            End Get
        End Property

        Public ReadOnly Property OrderMissionIndex As Byte
            Get
                Return Me._orderMissionIndex
            End Get
        End Property

        Public ReadOnly Property Array_1D(index As Integer) As Byte
            Get
                If index < 0 Or index >= 3 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._array_1D(index)
            End Get
        End Property

        Public ReadOnly Property Array_20(index As Integer) As Byte
            Get
                If index < 0 Or index >= 3 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._array_20(index)
            End Get
        End Property

        Public ReadOnly Property ScriptCurrent As Byte
            Get
                Return Me._scriptCurrent
            End Get
        End Property

        Public ReadOnly Property ScriptOverall As Byte
            Get
                Return Me._scriptOverall
            End Get
        End Property

        Public ReadOnly Property ScriptWaypoint As Byte
            Get
                Return Me._scriptWaypoint
            End Get
        End Property

        Public ReadOnly Property Field_26 As UInt16
            Get
                Return Me._field_26
            End Get
        End Property

        Public ReadOnly Property ScriptTimerReload As Int16
            Get
                Return Me._scriptTimerReload
            End Get
        End Property

        Public ReadOnly Property ScriptTimer As Int16
            Get
                Return Me._scriptTimer
            End Get
        End Property

        Public ReadOnly Property ScriptTarget As UInt16
            Get
                Return Me._scriptTarget
            End Get
        End Property

        Public ReadOnly Property Field_2E As UInt16
            Get
                Return Me._field_2E
            End Get
        End Property

        Public ReadOnly Property ScriptTargetXPosition As Int32
            Get
                Return Me._scriptTargetXPosition
            End Get
        End Property

        Public ReadOnly Property ScriptTargetYPosition As Int32
            Get
                Return Me._scriptTargetYPosition
            End Get
        End Property

        Public ReadOnly Property ScriptTargetZPosition As Int32
            Get
                Return Me._scriptTargetZPosition
            End Get
        End Property

        Public ReadOnly Property Field_3C As UInt16
            Get
                Return Me._field_3C
            End Get
        End Property

        Public ReadOnly Property Field_3E As UInt16
            Get
                Return Me._field_3E
            End Get
        End Property

        Public ReadOnly Property Field_40 As Byte
            Get
                Return Me._field_40
            End Get
        End Property

        Public ReadOnly Property Field_41 As Byte
            Get
                Return Me._field_41
            End Get
        End Property

        Public ReadOnly Property LastAttacker As UInt16
            Get
                Return Me._lastAttacker
            End Get
        End Property

        Public ReadOnly Property Field_44 As UInt16
            Get
                Return Me._field_44
            End Get
        End Property

        Public ReadOnly Property Field_46 As Byte
            Get
                Return Me._field_46
            End Get
        End Property

        Public ReadOnly Property Field_47 As Byte
            Get
                Return Me._field_47
            End Get
        End Property

        Public ReadOnly Property Field_48 As Byte
            Get
                Return Me._field_48
            End Get
        End Property

        Public ReadOnly Property Field_49 As Byte
            Get
                Return Me._field_49
            End Get
        End Property

        Public ReadOnly Property Field_4A As Byte
            Get
                Return Me._field_4A
            End Get
        End Property

        Public ReadOnly Property Field_4B As Byte
            Get
                Return Me._field_4B
            End Get
        End Property

        ' TODO: Array_4C

        Public ReadOnly Property Field_60 As Byte
            Get
                Return Me._field_60
            End Get
        End Property

        Public ReadOnly Property Field_61 As Byte
            Get
                Return Me._field_61
            End Get
        End Property

        Public ReadOnly Property Field_62 As Byte
            Get
                Return Me._field_62
            End Get
        End Property

        Public ReadOnly Property Field_63 As Byte
            Get
                Return Me._field_63
            End Get
        End Property

        Public ReadOnly Property OrderTimer As UInt32
            Get
                Return Me._orderTimer
            End Get
        End Property

        Public ReadOnly Property Field_68 As UInt16
            Get
                Return Me._field_68
            End Get
        End Property

        Public ReadOnly Property BaseSpeed As UInt16
            Get
                Return Me._baseSpeed
            End Get
        End Property

        Public ReadOnly Property Field_6C As UInt16
            Get
                Return Me._field_6C
            End Get
        End Property

        Public ReadOnly Property Field_6E As Byte
            Get
                Return Me._field_6E
            End Get
        End Property

        Public ReadOnly Property Field_6F As Byte
            Get
                Return Me._field_6F
            End Get
        End Property

        Public ReadOnly Property RotVertical As UInt16
            Get
                Return Me._rotVertical
            End Get
        End Property

        Public ReadOnly Property Field_72 As UInt16
            Get
                Return Me._field_72
            End Get
        End Property

        Public ReadOnly Property Field_74 As Byte
            Get
                Return Me._field_74
            End Get
        End Property

        Public ReadOnly Property Field_75 As Byte
            Get
                Return Me._field_75
            End Get
        End Property

        Public ReadOnly Property Field_76 As UInt16
            Get
                Return Me._field_76
            End Get
        End Property

        Public ReadOnly Property Field_78 As UInt16
            Get
                Return Me._field_78
            End Get
        End Property

        Public ReadOnly Property RotHorizontal As UInt16
            Get
                Return Me._rotHorizontal
            End Get
        End Property

        Public ReadOnly Property Field_7C As UInt16
            Get
                Return Me._field_7C
            End Get
        End Property

        Public ReadOnly Property Field_7E As Byte
            Get
                Return Me._field_7E
            End Get
        End Property

        Public ReadOnly Property Field_7F As Byte
            Get
                Return Me._field_7F
            End Get
        End Property

        Public ReadOnly Property Field_80 As UInt16
            Get
                Return Me._field_80
            End Get
        End Property

        Public ReadOnly Property Field_82 As UInt16
            Get
                Return Me._field_82
            End Get
        End Property

        Public ReadOnly Property Field_84 As UInt16
            Get
                Return Me._field_84
            End Get
        End Property

        Public ReadOnly Property Field_86 As UInt16
            Get
                Return Me._field_86
            End Get
        End Property

        Public ReadOnly Property Field_88 As Byte
            Get
                Return Me._field_88
            End Get
        End Property

        Public ReadOnly Property Field_89 As Byte
            Get
                Return Me._field_89
            End Get
        End Property

        Public ReadOnly Property Field_8A As Int16
            Get
                Return Me._field_8A
            End Get
        End Property

        Public ReadOnly Property Field_8C As UInt16
            Get
                Return Me._field_8C
            End Get
        End Property

        Public ReadOnly Property FlightGroupFormation As Byte
            Get
                Return Me._flightGroupFormation
            End Get
        End Property

        Public ReadOnly Property FlightGroupSpacing As Byte
            Get
                Return Me._flightGroupSpacing
            End Get
        End Property

        Public ReadOnly Property FlightGroupMemberIndex As Byte
            Get
                Return Me._flightGroupMemberIndex
            End Get
        End Property

        Public ReadOnly Property Field_91 As Byte
            Get
                Return Me._field_89
            End Get
        End Property

        Public ReadOnly Property Field_92 As Int32
            Get
                Return Me._field_92
            End Get
        End Property

        Public ReadOnly Property Field_96 As Int32
            Get
                Return Me._field_96
            End Get
        End Property

        Public ReadOnly Property Field_9A As Int32
            Get
                Return Me._field_9A
            End Get
        End Property

        Public ReadOnly Property Throttle As UInt16
            Get
                Return Me._throttle
            End Get
        End Property

        Public ReadOnly Property Field_A0 As UInt16
            Get
                Return Me._field_A0
            End Get
        End Property

        Public ReadOnly Property Damage As UInt16
            Get
                Return Me._damage
            End Get
        End Property

        Public ReadOnly Property Field_A4 As UInt16
            Get
                Return Me._field_A4
            End Get
        End Property

        Public ReadOnly Property MaxDamage As UInt16
            Get
                Return Me._maxDamage
            End Get
        End Property

        Public ReadOnly Property PanelFlags As UInt16
            Get
                Return Me._panelFlags
            End Get
        End Property

        Public ReadOnly Property PanelDamageFlags As UInt16
            Get
                Return Me._panelDamageFlags
            End Get
        End Property

        Public ReadOnly Property FeatureFlags As UInt16
            Get
                Return Me._featureFlags
            End Get
        End Property

        Public ReadOnly Property FeatureDamageFlags As UInt16
            Get
                Return Me._featureDamageFlags
            End Get
        End Property

        Public ReadOnly Property Field_B0 As UInt16
            Get
                Return Me._field_B0
            End Get
        End Property

        Public ReadOnly Property Field_B2 As UInt16
            Get
                Return Me._field_B2
            End Get
        End Property

        Public ReadOnly Property Field_B4 As Byte
            Get
                Return Me._field_B4
            End Get
        End Property

        Public ReadOnly Property Field_B5 As Byte
            Get
                Return Me._field_B5
            End Get
        End Property

        Public ReadOnly Property Field_B6 As Byte
            Get
                Return Me._field_B6
            End Get
        End Property

        Public ReadOnly Property Field_B7 As Byte
            Get
                Return Me._field_B7
            End Get
        End Property

        Public ReadOnly Property Inspected As Byte
            Get
                Return Me._inspected
            End Get
        End Property

        Public ReadOnly Property CargoStatus As Byte
            Get
                Return Me._cargoStatus
            End Get
        End Property

        Public ReadOnly Property CargoString As String
            Get
                Return Me._cargoString
            End Get
        End Property

        Public ReadOnly Property ShieldChargeFore As UInt16
            Get
                Return Me._shieldChargeFore
            End Get
        End Property

        Public ReadOnly Property ShieldChargeRear As UInt16
            Get
                Return Me._shieldChargeRear
            End Get
        End Property

        Public ReadOnly Property ShieldChargeRate As Byte
            Get
                Return Me._shieldChargeRate
            End Get
        End Property

        Public ReadOnly Property ShieldBalance As Byte
            Get
                Return Me._shieldBalance
            End Get
        End Property

        Public ReadOnly Property CannonBanks As Byte
            Get
                Return Me._cannonBanks
            End Get
        End Property

        Public ReadOnly Property CannonChargeRate As Byte
            Get
                Return Me._cannonChargeRate
            End Get
        End Property

        Public ReadOnly Property CannonCount As Byte
            Get
                Return Me._cannonCount
            End Get
        End Property

        Public ReadOnly Property CannonType(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._cannonType(index)
            End Get
        End Property

        Public ReadOnly Property CannonLink(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._cannonLink(index)
            End Get
        End Property

        Public ReadOnly Property Array_D7(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._array_D7(index)
            End Get
        End Property

        Public ReadOnly Property CannonNext(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._cannonNext(index)
            End Get
        End Property

        Public ReadOnly Property Field_DB As Byte
            Get
                Return Me._field_DB
            End Get
        End Property

        Public ReadOnly Property CannonTimer(index As Integer) As UInt16
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._cannonTimer(index)
            End Get
        End Property

        Public ReadOnly Property WarheadBanks As Byte
            Get
                Return Me._warheadBanks
            End Get
        End Property

        Public ReadOnly Property WarheadType(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._warheadType(index)
            End Get
        End Property

        Public ReadOnly Property WarheadNext(index As Integer) As Byte
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._warheadNext(index)
            End Get
        End Property

        Public ReadOnly Property Field_E5 As Byte
            Get
                Return Me._field_E5
            End Get
        End Property

        Public ReadOnly Property WarheadTimer(index As Integer) As UInt16
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._warheadTimer(index)
            End Get
        End Property

        Public ReadOnly Property WarheadLock As UInt16
            Get
                Return Me._warheadLock
            End Get
        End Property

        Public ReadOnly Property Field_EC As Byte
            Get
                Return Me._field_EC
            End Get
        End Property

        Public ReadOnly Property BeamChargeRate As Byte
            Get
                Return Me._beamChargeRate
            End Get
        End Property

        Public ReadOnly Property BeamCharge As UInt16
            Get
                Return Me._beamCharge
            End Get
        End Property

        Public ReadOnly Property LasersFired As UInt16
            Get
                Return Me._lasersFired
            End Get
        End Property

        Public ReadOnly Property LasersHit As UInt16
            Get
                Return Me._lasersHit
            End Get
        End Property

        Public ReadOnly Property IonsFired As UInt16
            Get
                Return Me._ionsFired
            End Get
        End Property

        Public ReadOnly Property IonsHit As UInt16
            Get
                Return Me._ionsHit
            End Get
        End Property

        Public ReadOnly Property WarheadsFired As Byte
            Get
                Return Me._warheadsFired
            End Get
        End Property

        Public ReadOnly Property WarheadsHit As Byte
            Get
                Return Me._warheadsHit
            End Get
        End Property

        Public ReadOnly Property Kills(index As Integer) As Byte
            Get
                If index < 0 Or index >= 69 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._kills(index)
            End Get
        End Property

        Public ReadOnly Property Field_13F As Byte
            Get
                Return Me._field_13F
            End Get
        End Property

        Public ReadOnly Property Field_140 As UInt16
            Get
                Return Me._field_140
            End Get
        End Property

        Public ReadOnly Property Weapons(index As Integer) As CraftObjectWeapon
            Get
                If index < 0 Or index >= 16 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._weapons(index)
            End Get
        End Property

        Public ReadOnly Property Array_1A2(index As Integer) As Byte
            Get
                If index < 0 Or index >= 40 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._array_1A2(index)
            End Get
        End Property

        Public ReadOnly Property Array_1CA(index As Integer) As Byte
            Get
                If index < 0 Or index >= 40 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._array_1CA(index)
            End Get
        End Property

        Public ReadOnly Property PartHP(index As Integer) As Byte
            Get
                If index < 0 Or index >= 40 Then Throw New ArgumentOutOfRangeException("index")
                Return Me._partHP(index)
            End Get
        End Property

#End Region

    End Class

End Namespace
