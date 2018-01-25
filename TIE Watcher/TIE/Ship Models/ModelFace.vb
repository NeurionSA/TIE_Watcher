Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Model Face.
    ''' <!--
    ''' NOTE: This is not a direct structural representation but rather an aggregation, as data for a face is
    ''' spread out and that would make things much too messy to maintain or design.
    ''' -->
    ''' </summary>
    Public Class ModelFace

#Region " --- Variables --- "

        ' variables are exposed as Friend so other objects in the assembly can access them
        ' this may change if I learn of a better way to do this, but for this quick and dirty run, this should be fine I suppose

        ' data directly read from the 3D Model
        ' color of the face
        Friend _color As Byte
        ' normal vector for the face
        Friend _normal As Point3D
        ' raw edge-defining bytes of the face
        Friend _edgeBytes() As Byte
        ' number of markings this face has
        Friend _numMarkings As Byte
        ' array of markings the face has
        Friend _markings() As ModelFaceMarking

        ' derived data to aid in rendering
        ' the face's type
        Friend _type As ModelFaceType
        ' the face's flags
        Friend _flags As ModelFaceFlags
        ' the number of vertices the face uses
        Friend _vertexCount As Byte
        ' the array of vertex indices the face uses
        Friend _vertexIndices() As Byte
        ' thickness of the face's line, if it is indeed a line
        Friend _lineWidth As Integer
        ' the centroid of the face
        Friend _centroid As Point3D

#End Region
#Region " --- Methods --- "

#Region " Constructors "

#End Region
        ' exposed as Friend so other objects in the assembly (ideally just the TIE namespace) can create a new instance
        Friend Sub New()
            ' nothing special here
        End Sub

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property Color As Byte
            Get
                Return _color
            End Get
        End Property

        Public ReadOnly Property Normal As Point3D
            Get
                Return _normal.Clone
            End Get
        End Property

        Public ReadOnly Property EdgeBytes As Byte()
            Get
                Return _edgeBytes
            End Get
        End Property

        Public ReadOnly Property MarkingCount As Byte
            Get
                Return _numMarkings
            End Get
        End Property

        Public ReadOnly Property Markings(index As Integer) As ModelFaceMarking
            Get
                If index < 0 Or index >= _numMarkings Then Throw New ArgumentOutOfRangeException("index")

                Return _markings(index)
            End Get
        End Property

        Public ReadOnly Property FaceType As ModelFaceType
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property Flags As ModelFaceFlags
            Get
                Return _flags
            End Get
        End Property

        Public ReadOnly Property VertexCount As Byte
            Get
                Return _vertexCount
            End Get
        End Property

        Public ReadOnly Property VertexIndices(index As Integer) As Byte
            Get
                If index < 0 Or index >= _vertexCount Then Throw New ArgumentOutOfRangeException("index")

                Return _vertexIndices(index)
            End Get
        End Property

        Public ReadOnly Property LineWidth As Integer
            Get
                ' NOTE: I'm not sure whether or not I should throw an exception of the face is not in fact a line type
                Return _lineWidth
            End Get
        End Property

        Public ReadOnly Property Centroid As Point3D
            Get
                ' return a clone
                Return _centroid.Clone
            End Get
        End Property

#End Region

    End Class

    ''' <summary>
    ''' ModelFace types.
    ''' </summary>
    Public Enum ModelFaceType As Integer
        ''' <summary>
        ''' The face is a line with a given thickness.
        ''' </summary>
        Line
        ''' <summary>
        ''' The face is a polygon.
        ''' </summary>
        Polygon
    End Enum

    ''' <summary>
    ''' Flags for ModelFaces.
    ''' </summary>
    Public Enum ModelFaceFlags As Byte
        ''' <summary>
        ''' Indicates the face should be rendered with smooth rather than flat shading.
        ''' </summary>
        SmoothShading = &H40
        ''' <summary>
        ''' Indicates the face should be exempt from backface culling.
        ''' </summary>
        NoCulling = &H80
    End Enum

End Namespace