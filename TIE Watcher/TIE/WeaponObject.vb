Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter WeaponObject struct (i.e. mines and probes)
    ''' </summary>
    Public Class WeaponObject
        Implements ICloneable

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = 18

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _field_0 As UInt16
        Private _objectCategory As Byte
        Private _objectType As Byte
        Private _xPosition As Int16
        Private _yPosition As Int16
        Private _zPosition As Int16
        Private _yaw As Byte
        Private _pitch As Byte
        Private _roll As Byte
        Private _flightGroupIndex As Byte
        Private _featureDamageFlags As UInt16
        Private _field_10 As Byte
        Private _scriptTimer As Byte

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
#Region " Interface Suport "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>

        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As WeaponObject = DirectCast(Me.MemberwiseClone, WeaponObject)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._field_0 = BitConverter.ToUInt16(bytes, 0)
            Me._objectCategory = bytes(2)
            Me._objectType = bytes(3)
            Me._xPosition = BitConverter.ToInt16(bytes, 4)
            Me._yPosition = BitConverter.ToInt16(bytes, 6)
            Me._zPosition = BitConverter.ToInt16(bytes, 8)
            Me._yaw = bytes(&HA)
            Me._pitch = bytes(&HB)
            Me._roll = bytes(&HC)
            Me._flightGroupIndex = bytes(&HD)
            Me._featureDamageFlags = BitConverter.ToUInt16(bytes, &HE)
            Me._field_10 = bytes(&H10)
            Me._scriptTimer = bytes(&H11)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As WeaponObject
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New WeaponObject
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

        Public ReadOnly Property Category As SpaceObjectCategory
            Get
                Return Me._objectCategory
            End Get
        End Property

        Public ReadOnly Property ObjectType As SpaceObjectType
            Get
                Return Me._objectType
            End Get
        End Property

        Public ReadOnly Property XPosition As Int16
            Get
                Return Me._xPosition
            End Get
        End Property

        Public ReadOnly Property YPosition As Int16
            Get
                Return Me._yPosition
            End Get
        End Property

        Public ReadOnly Property ZPosition As Int16
            Get
                Return Me._zPosition
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

        Public ReadOnly Property FlightGroupIndex As Byte
            Get
                Return Me._flightGroupIndex
            End Get
        End Property

        Public ReadOnly Property FeatureDamageFlags As UInt16
            Get
                Return Me._featureDamageFlags
            End Get
        End Property

        Public ReadOnly Property Field_10 As Byte
            Get
                Return Me._field_10
            End Get
        End Property

        Public ReadOnly Property ScriptTimer As Byte
            Get
                Return Me._scriptTimer
            End Get
        End Property

#End Region

    End Class

End Namespace