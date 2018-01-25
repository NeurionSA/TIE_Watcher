Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Space_Object struct
    ''' </summary>
    Public Class SpaceObject
        Implements ICloneable

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = 88

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _field_0 As UInt16
        Private _field_2 As Byte
        Private _category As SpaceObjectCategory
        Private _objectType As Byte
        Private _field_5 As Byte
        Private _xPosition As Int32
        Private _yPosition As Int32
        Private _zPosition As Int32
        Private _xPositionLast As Int32
        Private _yPositionLast As Int32
        Private _zPositionLast As Int32
        Private _yaw As Int16
        Private _pitch As Int16
        Private _roll As Int16
        Private _field_24 As UInt16
        Private _velocity As Int16
        Private _field_28 As UInt16
        Private _collisionDamage As UInt16
        Private _destructionTimer As UInt16
        Private _age As UInt16
        Private _parentIndex As UInt16
        Private _parentType As Byte
        Private _IFF As Byte
        Private _markings As Byte
        Private _flightGroupIndex As Byte
        Private _field_36 As UInt16
        Private _matrix1Flag As Byte
        Private _field_39 As Byte
        Private _field_3A As Int16  ' IDA: Trans. Matrix Cell 'p', from mY * mP = sin( pitch ) * sin( yaw )
        Private _field_3C As Int16  ' IDA: Trans. Matrix Cell 'a', from mY * mP = sin( pitch ) * cos( yaw )
        Private _field_3E As Int16  ' IDA: Trans. Matrix Cell 'u', from mY * mP = cos( pitch )
        Private _matrix2Flag As Byte
        Private _field_41 As Byte
        Private _field_42 As Int16  ' IDA: Trans. Matrix Cell 'p'
        Private _field_44 As Int16  ' IDA: Trans. Matrix Cell 'a'
        Private _field_46 As Int16  ' IDA: Trans. Matrix Cell 'u'
        Private _field_48 As Int16  ' IDA: Trans. Matrix Cell 'q'
        Private _field_4A As Int16  ' IDA: Trans. Matrix Cell 'b'
        Private _field_4C As Int16  ' IDA: Trans. Matrix Cell 'v'
        Private _field_4E As Int16  ' IDA: Trans. Matrix Cell 'r'
        Private _field_50 As Int16  ' IDA: Trans. Matrix Cell 'c'
        Private _field_52 As Int16  ' IDA: Trans. Matrix Cell 'w'
        Private _pObject As UInt32

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class.
        ''' </summary>
        Public Sub New()
            ' nothing special here
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As SpaceObject = DirectCast(Me.MemberwiseClone, SpaceObject)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._field_0 = BitConverter.ToUInt16(bytes, 0)
            Me._field_2 = bytes(2)
            Me._category = bytes(3)
            Me._objectType = bytes(4)
            Me._field_5 = bytes(5)
            Me._xPosition = BitConverter.ToInt32(bytes, &H6)
            Me._yPosition = BitConverter.ToInt32(bytes, &HA)
            Me._zPosition = BitConverter.ToInt32(bytes, &HE)
            Me._xPositionLast = BitConverter.ToInt32(bytes, &H12)
            Me._yPositionLast = BitConverter.ToInt32(bytes, &H16)
            Me._zPositionLast = BitConverter.ToInt32(bytes, &H1A)
            Me._yaw = BitConverter.ToInt16(bytes, &H1E)
            Me._pitch = BitConverter.ToInt16(bytes, &H20)
            Me._roll = BitConverter.ToInt16(bytes, &H22)
            Me._field_24 = BitConverter.ToUInt16(bytes, &H24)
            Me._velocity = BitConverter.ToInt16(bytes, &H26)
            Me._field_28 = BitConverter.ToUInt16(bytes, &H28)
            Me._collisionDamage = BitConverter.ToUInt16(bytes, &H2A)
            Me._destructionTimer = BitConverter.ToUInt16(bytes, &H2C)
            Me._age = BitConverter.ToUInt16(bytes, &H2E)
            Me._parentIndex = BitConverter.ToUInt16(bytes, &H30)
            Me._parentType = bytes(&H32)
            Me._IFF = bytes(&H33)
            Me._markings = bytes(&H34)
            Me._flightGroupIndex = bytes(&H35)
            Me._field_36 = BitConverter.ToUInt16(bytes, &H36)
            Me._matrix1Flag = bytes(&H38)
            Me._field_39 = bytes(&H39)
            Me._field_3A = BitConverter.ToInt16(bytes, &H3A)
            Me._field_3C = BitConverter.ToInt16(bytes, &H3C)
            Me._field_3E = BitConverter.ToInt16(bytes, &H3E)
            Me._matrix2Flag = bytes(&H40)
            Me._field_41 = bytes(&H41)
            Me._field_42 = BitConverter.ToInt16(bytes, &H42)
            Me._field_44 = BitConverter.ToInt16(bytes, &H44)
            Me._field_46 = BitConverter.ToInt16(bytes, &H46)
            Me._field_48 = BitConverter.ToInt16(bytes, &H48)
            Me._field_4A = BitConverter.ToInt16(bytes, &H4A)
            Me._field_4C = BitConverter.ToInt16(bytes, &H4C)
            Me._field_4E = BitConverter.ToInt16(bytes, &H4E)
            Me._field_50 = BitConverter.ToInt16(bytes, &H50)
            Me._field_52 = BitConverter.ToInt16(bytes, &H52)
            Me._pObject = BitConverter.ToUInt32(bytes, &H54)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As SpaceObject
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New SpaceObject
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

        Public ReadOnly Property Field_0 As UInt16
            Get
                Return Me._field_0
            End Get
        End Property

        Public ReadOnly Property Field_2 As Byte
            Get
                Return Me._field_2
            End Get
        End Property

        Public ReadOnly Property Category As SpaceObjectCategory
            Get
                Return Me._category
            End Get
        End Property

        Public ReadOnly Property ObjectType As SpaceObjectType
            Get
                Return Me._objectType
            End Get
        End Property

        Public ReadOnly Property Field_5 As Byte
            Get
                Return Me._field_5
            End Get
        End Property

        Public ReadOnly Property XPosition As Int32
            Get
                Return Me._xPosition
            End Get
        End Property

        Public ReadOnly Property YPosition As Int32
            Get
                Return Me._yPosition
            End Get
        End Property

        Public ReadOnly Property ZPosition As Int32
            Get
                Return Me._zPosition
            End Get
        End Property

        Public ReadOnly Property XPositionLast As Int32
            Get
                Return Me._xPositionLast
            End Get
        End Property

        Public ReadOnly Property YPositionLast As Int32
            Get
                Return Me._yPositionLast
            End Get
        End Property

        Public ReadOnly Property ZPositionLast As Int32
            Get
                Return Me._zPositionLast
            End Get
        End Property

        Public ReadOnly Property Yaw As Int16
            Get
                Return Me._yaw
            End Get
        End Property

        Public ReadOnly Property Pitch As Int16
            Get
                Return Me._pitch
            End Get
        End Property

        Public ReadOnly Property Roll As Int16
            Get
                Return Me._roll
            End Get
        End Property

        Public ReadOnly Property Field_24 As UInt16
            Get
                Return Me._field_24
            End Get
        End Property

        Public ReadOnly Property Velocity As Int16
            Get
                Return Me._velocity
            End Get
        End Property

        Public ReadOnly Property Field_28 As UInt16
            Get
                Return Me._field_28
            End Get
        End Property

        Public ReadOnly Property CollisionDamage As UInt16
            Get
                Return Me._collisionDamage
            End Get
        End Property

        Public ReadOnly Property DestructionTimer As UInt16
            Get
                Return Me._destructionTimer
            End Get
        End Property

        Public ReadOnly Property Age As UInt16
            Get
                Return Me._age
            End Get
        End Property

        Public ReadOnly Property ParentIndex As UInt16
            Get
                Return Me._parentIndex
            End Get
        End Property

        Public ReadOnly Property ParentType As SpaceObjectType
            Get
                Return Me._parentType
            End Get
        End Property

        Public ReadOnly Property IFF As Byte
            Get
                Return Me._IFF
            End Get
        End Property

        Public ReadOnly Property Markings As Byte
            Get
                Return Me._markings
            End Get
        End Property

        Public ReadOnly Property FlightGroupIndex As Byte
            Get
                Return Me._flightGroupIndex
            End Get
        End Property

        Public ReadOnly Property Field_36 As UInt16
            Get
                Return Me._field_36
            End Get
        End Property

        Public ReadOnly Property Matrix1Flag As Byte
            Get
                Return Me._matrix1Flag
            End Get
        End Property

        Public ReadOnly Property Field_39 As Byte
            Get
                Return Me._field_39
            End Get
        End Property

        Public ReadOnly Property Field_3A As Int16
            Get
                Return Me._field_3A
            End Get
        End Property

        Public ReadOnly Property Field_3C As Int16
            Get
                Return Me._field_3C
            End Get
        End Property

        Public ReadOnly Property Field_3E As Int16
            Get
                Return Me._field_3E
            End Get
        End Property

        Public ReadOnly Property Matrix2Flag As Byte
            Get
                Return Me._matrix2Flag
            End Get
        End Property

        Public ReadOnly Property Field_41 As Byte
            Get
                Return Me._field_41
            End Get
        End Property

        Public ReadOnly Property Field_42 As Int16
            Get
                Return Me._field_42
            End Get
        End Property

        Public ReadOnly Property Field_44 As Int16
            Get
                Return Me._field_44
            End Get
        End Property

        Public ReadOnly Property Field_46 As Int16
            Get
                Return Me._field_46
            End Get
        End Property

        Public ReadOnly Property Field_48 As Int16
            Get
                Return Me._field_48
            End Get
        End Property

        Public ReadOnly Property Field_4A As Int16
            Get
                Return Me._field_4A
            End Get
        End Property

        Public ReadOnly Property Field_4C As Int16
            Get
                Return Me._field_4C
            End Get
        End Property

        Public ReadOnly Property Field_4E As Int16
            Get
                Return Me._field_4E
            End Get
        End Property

        Public ReadOnly Property Field_50 As Int16
            Get
                Return Me._field_50
            End Get
        End Property

        Public ReadOnly Property Field_52 As Int16
            Get
                Return Me._field_52
            End Get
        End Property

        Public ReadOnly Property ObjectPointer As UInt32
            Get
                Return Me._pObject
            End Get
        End Property

#End Region

    End Class

End Namespace