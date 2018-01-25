Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter CraftWeapon struct
    ''' </summary>
    Public Class CraftObjectWeapon
        Implements ICloneable

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = 6

#End Region
#Region " --- Variables --- "

        Private _weaponType As Byte
        Private _cannonCharge As Byte
        Private _warheadAmmo As Byte
        Private _field_3 As Byte
        Private _turretTarget As UInt16

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
            Dim ret As CraftObjectWeapon = DirectCast(Me.MemberwiseClone, CraftObjectWeapon)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._weaponType = bytes(0)
            Me._cannonCharge = bytes(1)
            Me._warheadAmmo = bytes(2)
            Me._field_3 = bytes(3)
            Me._turretTarget = BitConverter.ToUInt16(bytes, &H4)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As CraftObjectWeapon
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New CraftObjectWeapon
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

        Public ReadOnly Property WeaponType As Byte
            Get
                Return Me._weaponType
            End Get
        End Property

        Public ReadOnly Property CannonCharge As Byte
            Get
                Return Me._cannonCharge
            End Get
        End Property

        Public ReadOnly Property WarheadAmmo As Byte
            Get
                Return Me._warheadAmmo
            End Get
        End Property

        Public ReadOnly Property TurretTarget As UInt16
            Get
                Return Me._turretTarget
            End Get
        End Property

#End Region

    End Class

End Namespace