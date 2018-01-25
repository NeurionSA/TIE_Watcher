Namespace TIE

    ''' <summary>
    ''' Class that encapsulates a TIE Fighter 3D Model (SHIP) Resource.
    ''' </summary>
    Public Class ModelResource

#Region " --- Constants --- "



#End Region
#Region " --- Variables --- "

        ' model header
        Private _header As ModelHeader

        ' model's parts
        Private _parts() As ModelPart

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class using the data in the provided stream.
        ''' Only accessible from within this assembly, specifically meant to be accessed by an instance of the LFD class.
        ''' </summary>
        ''' <param name="srcStream"></param>
        Friend Sub New(srcStream As IO.Stream)
            ' check arguments
            If srcStream Is Nothing Then Throw New ArgumentNullException("srcStream")

            ' remember the starting position of the stream, we'll need it later
            Dim startPosition As Long = srcStream.Position

            ' create the binary reader
            Dim bRead As New IO.BinaryReader(srcStream)

            ' read the header
            _header = ModelHeader.FromBytes(bRead.ReadBytes(ModelHeader.SizeOf))

            ' TODO: Read in SHIP_0 structs and the LOD trees they define

            ' allocate the array for the Part Headers
            _parts = Array.CreateInstance(GetType(ModelPart), _header.PartCount)

            ' read in the Model Parts
            For partIndex As Integer = 0 To Header.PartCount - 1
                ' seek to the appropriate SHIP_Part struct
                srcStream.Seek(startPosition + &H20 + _header.LODTreeCount * 6 + partIndex * &H40, IO.SeekOrigin.Begin)
                ' read it in from the stream
                _parts(partIndex) = ModelPart.CreateFromStream(srcStream)
            Next

        End Sub

#End Region


#End Region
#Region " --- Properties --- "

        Public ReadOnly Property Header As ModelHeader
            Get
                Return _header
            End Get
        End Property

        Public ReadOnly Property Parts(index As Integer) As ModelPart
            Get
                If index < 0 Or index >= _header.PartCount Then Throw New ArgumentOutOfRangeException("index")

                Return _parts(index)
            End Get
        End Property

#End Region

    End Class

End Namespace