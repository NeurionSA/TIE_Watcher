Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter SHIP_Head struct.
    ''' </summary>
    Public Class ModelHeader

#Region " --- Constants --- "

        ' size of the struct this class represents, in bytes
        Private Const SIZE_OF = 32

#End Region
#Region " --- Variables --- "

        Private _field_0 As UInt16
        Private _field_2 As UInt16
        Private _field_4 As UInt16      ' IDA: Ship Dimensions (width?)
        Private _field_6 As UInt16      ' IDA: Ship Dimensions (length?)
        Private _field_8 As UInt16      ' IDA: Ship Dimensions (height?)
        Private _field_A As UInt16      ' IDA: Largest Dimensions?
        Private _field_C As UInt32      ' IDA: Scaling related?
        Private _bound_x_min As Int16
        Private _bound_y_min As Int16
        Private _bound_z_min As Int16
        Private _bound_x_max As Int16
        Private _bound_y_max As Int16
        Private _bound_z_max As Int16
        Private _numParts As Byte       ' IDA: Number of Parts in the model (fuselage, wing, etc.)
        Private _numLODTrees As Byte    ' IDA: Number of SHIP_0 structs
        Private _field_1E As Byte       ' IDA: ? 0 for most craft, but 2 for the largest capitals
        Private _field_1F As Byte

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

        ' updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            _field_0 = BitConverter.ToUInt16(bytes, 0)
            _field_2 = BitConverter.ToUInt16(bytes, 2)
            _field_4 = BitConverter.ToUInt16(bytes, 4)
            _field_6 = BitConverter.ToUInt16(bytes, 6)
            _field_8 = BitConverter.ToUInt16(bytes, 8)
            _field_A = BitConverter.ToUInt16(bytes, &HA)
            _field_C = BitConverter.ToUInt32(bytes, &HC)
            _bound_x_min = BitConverter.ToInt16(bytes, &H10)
            _bound_y_min = BitConverter.ToInt16(bytes, &H12)
            _bound_z_min = BitConverter.ToInt16(bytes, &H14)
            _bound_x_max = BitConverter.ToInt16(bytes, &H16)
            _bound_y_max = BitConverter.ToInt16(bytes, &H18)
            _bound_z_max = BitConverter.ToInt16(bytes, &H1A)
            _numParts = bytes(&H1C)
            _numLODTrees = bytes(&H1D)
            _field_1E = bytes(&H1E)
            _field_1F = bytes(&H1F)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromBytes(bytes() As Byte) As ModelHeader
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New ModelHeader
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
                Return _field_0
            End Get
        End Property

        Public ReadOnly Property Field_2 As UInt16
            Get
                Return _field_2
            End Get
        End Property

        Public ReadOnly Property Field_4 As UInt16
            Get
                Return _field_4
            End Get
        End Property

        Public ReadOnly Property Field_6 As UInt16
            Get
                Return _field_6
            End Get
        End Property

        Public ReadOnly Property Field_8 As UInt16
            Get
                Return _field_8
            End Get
        End Property

        Public ReadOnly Property Field_A As UInt16
            Get
                Return _field_A
            End Get
        End Property

        Public ReadOnly Property Field_C As UInt32
            Get
                Return _field_C
            End Get
        End Property

        Public ReadOnly Property BoundsXMinimum As Int16
            Get
                Return _bound_x_min
            End Get
        End Property

        Public ReadOnly Property BoundsYMinimum As Int16
            Get
                Return _bound_y_min
            End Get
        End Property

        Public ReadOnly Property BoundsZMinimum As Int16
            Get
                Return _bound_z_min
            End Get
        End Property

        Public ReadOnly Property BoundsXMaximum As Int16
            Get
                Return _bound_x_max
            End Get
        End Property

        Public ReadOnly Property BoundsYMaximum As Int16
            Get
                Return _bound_y_max
            End Get
        End Property

        Public ReadOnly Property BoundsZMaximum As Int16
            Get
                Return _bound_z_max
            End Get
        End Property

        Public ReadOnly Property PartCount As Byte
            Get
                Return _numParts
            End Get
        End Property

        Public ReadOnly Property LODTreeCount As Byte
            Get
                Return _numLODTrees
            End Get
        End Property

        Public ReadOnly Property Field_1E As Byte
            Get
                Return _field_1E
            End Get
        End Property

        Public ReadOnly Property Field_1F As Byte
            Get
                Return _field_1F
            End Get
        End Property

#End Region

    End Class

End Namespace
