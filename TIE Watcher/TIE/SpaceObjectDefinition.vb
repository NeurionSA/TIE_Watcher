Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Space Object Definition struct.
    ''' </summary>
    Public Class SpaceObjectDefinition
        Implements ICloneable

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = 22

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _field_0 As Byte
        Private _field_1 As Byte
        Private _objectCategory As Byte
        Private _craftCategory As Byte
        Private _field_4 As UInt16
        Private _field_6 As UInt16
        Private _hResource As UInt16
        Private _field_A As UInt32
        Private _field_E As UInt32
        Private _field_12 As Byte
        Private _statsIndex As Byte         ' IDA: Index into Object_Stats array
        Private _speciesIndex As Byte       ' IDA: Which SPECIES.LFD file the ship's model is in
        Private _resourceIndex As Byte      ' IDA: Resource Index in the SPECIES#.LFD file

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
            Dim ret As SpaceObjectDefinition = DirectCast(Me.MemberwiseClone, SpaceObjectDefinition)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._field_0 = bytes(0)
            Me._field_1 = bytes(1)
            Me._objectCategory = bytes(2)
            Me._craftCategory = bytes(3)
            Me._field_4 = BitConverter.ToUInt16(bytes, 4)
            Me._field_6 = BitConverter.ToUInt16(bytes, 6)
            Me._hResource = BitConverter.ToUInt16(bytes, 8)
            Me._field_A = BitConverter.ToUInt32(bytes, &HA)
            Me._field_E = BitConverter.ToUInt32(bytes, &HE)
            Me._field_12 = bytes(&H12)
            Me._statsIndex = bytes(&H13)
            Me._speciesIndex = bytes(&H14)
            Me._resourceIndex = bytes(&H15)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As SpaceObjectDefinition
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New SpaceObjectDefinition
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

        Public ReadOnly Property Field_0 As Byte
            Get
                Return Me._field_0
            End Get
        End Property

        Public ReadOnly Property Field_1 As Byte
            Get
                Return Me._field_1
            End Get
        End Property

        Public ReadOnly Property ObjectCategory As Byte
            Get
                Return Me._objectCategory
            End Get
        End Property

        Public ReadOnly Property Category As SpaceObjectCategory
            Get
                Return Me._craftCategory
            End Get
        End Property

        Public ReadOnly Property Field_4 As UInt16
            Get
                Return Me._field_4
            End Get
        End Property

        Public ReadOnly Property Field_6 As UInt16
            Get
                Return Me._field_6
            End Get
        End Property

        Public ReadOnly Property ResourceHandle As UInt16
            Get
                Return Me._hResource
            End Get
        End Property

        Public ReadOnly Property Field_A As UInt32
            Get
                Return Me._field_A
            End Get
        End Property

        Public ReadOnly Property Field_E As UInt32
            Get
                Return Me._field_E
            End Get
        End Property

        Public ReadOnly Property Field_12 As Byte
            Get
                Return Me._field_12
            End Get
        End Property

        Public ReadOnly Property StatsIndex As Byte
            Get
                Return Me._statsIndex
            End Get
        End Property

        Public ReadOnly Property SpeciesIndex As Byte
            Get
                Return Me._speciesIndex
            End Get
        End Property

        Public ReadOnly Property ResourceIndex As Byte
            Get
                Return Me._resourceIndex
            End Get
        End Property

#End Region

    End Class

End Namespace