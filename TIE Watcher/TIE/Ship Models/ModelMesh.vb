Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Model Mesh.
    ''' <!--
    ''' NOTE: This is not a direct structural representation but rather an aggregation, as data for a mesh is
    ''' spread out and that would make things much too messy to maintain or design.
    ''' -->
    ''' </summary>
    Public Class ModelMesh

#Region " --- Variables --- "

        ' fields from SHIP_Mesh struct
        Private _mesh_field_0 As Byte
        Private _mesh_field_1 As Byte
        Private _numVertices As Byte
        Private _mesh_field_3 As Byte
        Private _numFaces As Byte
        ' array of ModelFace classes
        Private _faces() As ModelFace
        ' fields from SHIP_Mesh_Box struct
        Private _box_field_0 As Int16
        Private _box_field_2 As Int16
        Private _box_field_4 As Int16
        Private _box_field_6 As Int16
        Private _box_field_8 As Int16
        Private _box_field_A As Int16
        ' array of vertices
        Private _vertices() As ModelVertex
        ' TODO: Array of SHIP_5 structs, one for each face
        Private _numFacesWithMarkings As UInt16

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class.
        ''' </summary>
        Private Sub New()
            ' nothing special here
        End Sub

#End Region

        ''' <summary>
        ''' Creates a new instance of this class using the data in the specified stream.
        ''' The stream should already be at the position of a SHIP_Mesh struct before calling this method.
        ''' </summary>
        ''' <param name="stream"></param>
        ''' <returns></returns>
        Friend Shared Function CreateFromStream(stream As IO.Stream) As ModelMesh
            ' throw an exception if the arguments are incorrect
            If stream Is Nothing Then Throw New ArgumentNullException("stream")

            ' create the return instance
            Dim ret As New ModelMesh
            ' create a binary reader
            Dim bRead As New IO.BinaryReader(stream)

            ' operating with the instance to be returned
            With ret
                ' read the SHIP_Mesh struct's fields
                ._mesh_field_0 = bRead.ReadByte
                ._mesh_field_1 = bRead.ReadByte
                ._numVertices = bRead.ReadByte
                ._mesh_field_3 = bRead.ReadByte
                ._numFaces = bRead.ReadByte

                ' create the array of faces while also reading in the face colors
                ._faces = Array.CreateInstance(GetType(ModelFace), ret._numFaces)
                For i As Integer = 0 To ._numFaces - 1
                    ._faces(i) = New ModelFace
                    ._faces(i)._color = bRead.ReadByte()
                Next

                ' read in the fields from the SHIP_Mesh_Box struct
                ._box_field_0 = bRead.ReadInt16
                ._box_field_2 = bRead.ReadInt16
                ._box_field_4 = bRead.ReadInt16
                ._box_field_6 = bRead.ReadInt16
                ._box_field_8 = bRead.ReadInt16
                ._box_field_A = bRead.ReadInt16

                ' allocate the array of vertices
                ._vertices = Array.CreateInstance(GetType(ModelVertex), ret._numVertices)
                ' read in all the vertices
                For i As Integer = 0 To ._numVertices - 1
                    Dim x As Int16 = bRead.ReadInt16
                    Dim y As Int16 = bRead.ReadInt16
                    Dim z As Int16 = bRead.ReadInt16

                    ' handle vertex 'decoding' -- I have no idea why LucasArts did this
                    If x >> 8 = &H7F Then
                        x = ._vertices(i - ((x And &HFF) >> 1)).Point.X
                    End If
                    If y >> 8 = &H7F Then
                        y = ._vertices(i - ((y And &HFF) >> 1)).Point.Y
                    End If
                    If z >> 8 = &H7F Then
                        z = ._vertices(i - ((z And &HFF) >> 1)).Point.Z
                    End If

                    ' create the new vertex object
                    ._vertices(i) = New ModelVertex()
                    ' assign a new point3d to the vertex
                    ._vertices(i)._point = New Point3D(x, y, z)
                Next

                ' read in all the vertex normals
                For i As Integer = 0 To ._numVertices - 1
                    Dim x As Single = bRead.ReadInt16 / 32678
                    Dim y As Single = bRead.ReadInt16 / 32768
                    Dim z As Single = bRead.ReadInt16 / 32768

                    ' assign a new normal to the vertex
                    ._vertices(i)._normal = New Point3D(x, y, z)
                Next

                ' read in all the face normals, while also reading in the face edge data
                For faceIndex As Integer = 0 To ._numFaces - 1
                    ' remember offset of this struct
                    Dim structOffset As Long = stream.Position
                    Dim returnOffset As Long

                    ' working with this face...
                    With ._faces(faceIndex)

                        ' read in the face normal
                        Dim x As Single = bRead.ReadInt16 / 32768
                        Dim y As Single = bRead.ReadInt16 / 32768
                        Dim z As Single = bRead.ReadInt16 / 32768

                        ' assign it to the face object
                        ._normal = New Point3D(x, y, z)

                        ' read the relative offset of the face edge
                        Dim edgeOffset As UInt16 = bRead.ReadUInt16()

                        ' remember where to seek back to for the next SHIP_Face_Normal struct
                        returnOffset = stream.Position

                        ' seek to the face edge data
                        stream.Seek(structOffset + edgeOffset, IO.SeekOrigin.Begin)

                        ' for now just read in the data as an array of bytes -- I'll have to analyze it later
                        ' read the face flags/edge count byte
                        Dim faceFlags As Byte = bRead.ReadByte

                        ' set the face's vertex count
                        ._vertexCount = faceFlags And &H3F

                        ' set the face's flags
                        ._flags = faceFlags And &HC0

                        ' seek backwards 1 so we read that byte, too when we read the raw face edge information
                        stream.Seek(-1, IO.SeekOrigin.Current)

                        ' read the raw edge bytes
                        ._edgeBytes = bRead.ReadBytes(4 + (._vertexCount << 1))

                        ' set the face's type based on the vertex count and populate its vertex list
                        ._vertexIndices = Array.CreateInstance(GetType(Byte), ._vertexCount)
                        If ._vertexCount = 2 Then
                            ' line
                            ._type = ModelFaceType.Line
                            ' set the line width
                            ._lineWidth = BitConverter.ToUInt16(._edgeBytes, 1)
                            ' set the vertex indices (bytes 3 and 4)
                            ._vertexIndices(0) = ._edgeBytes(3)
                            ._vertexIndices(1) = ._edgeBytes(4)
                        Else
                            ' polygon
                            ._type = ModelFaceType.Polygon
                            ' set the vertex indices
                            For i As Integer = 0 To ._vertexCount - 1
                                ._vertexIndices(i) = ._edgeBytes((i << 1) + 1)
                            Next
                        End If

                        ' calculate the face's centroid using its vertices
                        x = 0
                        y = 0
                        z = 0
                        For i As Integer = 0 To ._vertexCount - 1
                            x += ret._vertices(._vertexIndices(i)).Point.X
                            y += ret._vertices(._vertexIndices(i)).Point.Y
                            z += ret._vertices(._vertexIndices(i)).Point.Z
                        Next

                        ' create the centroid as the average of the vertices' coordinates
                        ' (hopefully this will work correctly)
                        ._centroid = New Point3D(x / ._vertexCount, y / ._vertexCount, z / ._vertexCount)
                    End With

                    ' seek back to the next SHIP_Face_Normal struct if there's more face edge data to read in
                    If faceIndex < ._numFaces - 1 Then stream.Seek(returnOffset, IO.SeekOrigin.Begin)
                Next

                ' NOTE: I still don't have enough information on Markings to load them in yet
                ' This has been seen with:
                ' Container A, at 0x230A7 in SPECIES.LFD
                ' Freighter, at ???? in SPECIES.LFD

                '' now we'd read the SHIP_5 structs, 1 for each face, but for now, skip them
                'stream.Seek(._numFaces * 3, IO.SeekOrigin.Current)



                '' read the number of sub-face markings on this mesh
                '._numFacesWithMarkings = bRead.ReadUInt16

                '' force it to 0 for now
                '._numFacesWithMarkings = 0

                '' iterate through the marking collections and read those in
                'For i As Integer = 0 To ._numFacesWithMarkings - 1
                '    ' read which face is getting markings
                '    Dim faceIndex As Integer = bRead.ReadByte
                '    ' read the relative offset to jump to
                '    Dim jumpOffset As UInt16 = bRead.ReadUInt16

                '    ' remember offset to return to for the next entry
                '    Dim returnOffset As Long = stream.Position

                '    ' jump to the relative offset
                '    stream.Seek(jumpOffset - 3, IO.SeekOrigin.Current)

                '    ' working with the face
                '    With ._faces(faceIndex)
                '        ' read the number of markings the face has
                '        ._numMarkings = bRead.ReadByte

                '        ' create the array of markings
                '        ._markings = Array.CreateInstance(GetType(ModelFaceMarking), ._numMarkings)

                '        ' create each marking while reading in their colors
                '        For markingIndex As Integer = 0 To ._numMarkings - 1
                '            ._markings(markingIndex) = New ModelFaceMarking()
                '            ' read in the marking's color
                '            ._markings(markingIndex)._color = bRead.ReadByte
                '        Next

                '        ' read in each marking's data
                '        For markingIndex As Integer = 0 To ._numMarkings - 1
                '            ' working with the marking
                '            With ._markings(markingIndex)
                '                ' read the number of vertices the marking has
                '                ._numVertices = bRead.ReadByte
                '                ._vertexByteArray = Array.CreateInstance(GetType(Byte()), ._numVertices)

                '                ' read in the data on each vertex
                '                For v As Integer = 0 To ._numVertices - 1

                '                    ._vertexByteArray(v) = bRead.ReadBytes(3)
                '                Next
                '            End With

                '        Next

                '    End With

                '    ' restore the offset for the next entry
                '    stream.Seek(returnOffset, IO.SeekOrigin.Begin)
                'Next

            End With

            ' return the new instance
            Return ret
        End Function

#End Region
#Region " --- Properties --- "

#Region " Fields from SHIP_Mesh struct "

        Public ReadOnly Property Mesh_Field_0 As Byte
            Get
                Return _mesh_field_0
            End Get
        End Property

        Public ReadOnly Property Mesh_Field_1 As Byte
            Get
                Return _mesh_field_1
            End Get
        End Property

        Public ReadOnly Property VertexCount As Byte
            Get
                Return _numVertices
            End Get
        End Property

        Public ReadOnly Property Mesh_Field_3 As Byte
            Get
                Return _mesh_field_3
            End Get
        End Property

        Public ReadOnly Property FaceCount As Byte
            Get
                Return _numFaces
            End Get
        End Property

#End Region

        Public ReadOnly Property Faces(index) As ModelFace
            Get
                If index < 0 Or index > _numFaces Then Throw New ArgumentOutOfRangeException("index")

                Return _faces(index)
            End Get
        End Property

#Region " Fields from SHIP_Mesh_Box struct "

        Public ReadOnly Property Box_Field_0 As Int16
            Get
                Return _box_field_0
            End Get
        End Property

        Public ReadOnly Property Box_Field_2 As Int16
            Get
                Return _box_field_2
            End Get
        End Property

        Public ReadOnly Property Box_Field_4 As Int16
            Get
                Return _box_field_4
            End Get
        End Property

        Public ReadOnly Property Box_Field_6 As Int16
            Get
                Return _box_field_6
            End Get
        End Property

        Public ReadOnly Property Box_Field_8 As Int16
            Get
                Return _box_field_8
            End Get
        End Property

        Public ReadOnly Property Box_Field_A As Int16
            Get
                Return _box_field_A
            End Get
        End Property

#End Region

        Public ReadOnly Property Vertex(index As Integer) As ModelVertex
            Get
                If index < 0 Or index > _numVertices Then Throw New ArgumentOutOfRangeException("index")

                Return _vertices(index)
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of faces that have markings on them.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property MarkedFacesCount As UInt16
            Get
                Return _numFacesWithMarkings
            End Get
        End Property

#End Region

    End Class

End Namespace