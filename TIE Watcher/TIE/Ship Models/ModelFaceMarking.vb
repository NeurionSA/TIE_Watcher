Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Model Face Marking.
    ''' </summary>
    Public Class ModelFaceMarking

#Region " --- Variables --- "

        ' color of the marking
        Friend _color As Byte
        ' number of vertices the marking has
        Friend _numVertices As Byte
        ' array of bytes for each vertex
        Friend _vertexByteArray()() As Byte

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Friend Sub New()
            ' nothing special
        End Sub

#End Region

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property Color As Byte
            Get
                Return _color
            End Get
        End Property

        Public ReadOnly Property VertexCount As Byte
            Get
                Return _numVertices
            End Get
        End Property

        Public ReadOnly Property VertexByteArray(index As Integer) As Byte()
            Get
                If index < 0 Or index >= _numVertices Then Throw New ArgumentOutOfRangeException("index")

                Return _vertexByteArray(index)
            End Get
        End Property

#End Region

    End Class

End Namespace