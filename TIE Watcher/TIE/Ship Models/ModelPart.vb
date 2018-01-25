Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Model Part.
    ''' <!--
    ''' NOTE: This is not a direct structural representation but rather an aggregation, as data for a part is
    ''' spread out and that would make things much too messy to maintain or design.
    ''' -->
    ''' </summary>
    Public Class ModelPart

#Region " --- Variables --- "

        ' fields from the SHIP_Part struct
        Private _partType As UInt16
        Private _flags As UInt16
        Private _field_4 As UInt16
        Private _field_6 As UInt16
        Private _field_8 As UInt16
        Private _field_A As UInt16
        Private _field_C As UInt32
        Private _field_10 As Int16
        Private _field_12 As Int16
        Private _field_14 As Int16
        Private _field_16 As Int16
        Private _field_18 As Int16
        Private _field_1A As Int16
        Private _field_1C As Int16
        Private _field_1E As Int16
        Private _field_20 As Int16
        Private _field_22 As Int16
        Private _field_24 As UInt32
        Private _field_28 As UInt16
        Private _field_2A As Byte
        Private _numWeapons As Byte     ' IDA: Number of weapon points on this part (i.e. missile launchers, laser cannons) -- these are only used to override hard-coded weapon points
        Private _meshDataOffset As UInt16     ' IDA: Offset to SHIP_3 list, relative to this struct
        Private _weaponOffset As UInt16     ' IDA: Offset to array of struct SHIP_Weapon, relative to this struct
        Private _field_30 As UInt16
        Private _field_32 As UInt16     ' IDA: Offset to ? data, relative to this struct...
        ' Only seen on parts of type Wing_2, LaserTurret_2, BeamSystem_2, CommSystem, CommSystem_3, Comm_System4
        Private _field_34 As UInt16
        Private _field_36 As UInt16
        Private _field_38 As UInt16
        Private _field_3A As UInt16
        Private _field_3C As UInt16
        Private _field_3E As UInt16

        ' mesh data
        Private _numMeshes As Integer           ' the number of mesh LODs the part has
        Private _meshes() As ModelMesh          ' array of ModelMesh objects for each mesh LOD
        Private _meshLODThreshold() As Int32    ' LOD thresholds for each Mesh

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
        ''' The stream should already be at the position of a SHIP_Part struct before calling this method.
        ''' The stream's position is not restored in any way upon returning.
        ''' </summary>
        ''' <param name="stream"></param>
        ''' <returns></returns>
        Friend Shared Function CreateFromStream(stream As IO.Stream) As ModelPart
            ' throw an exception if the arguments are incorrect
            If stream Is Nothing Then Throw New ArgumentNullException("stream")

            ' create the return instance
            Dim ret As New ModelPart
            ' create a binary reader
            Dim bRead As New IO.BinaryReader(stream)

            ' remember where the SHIP_Part struct is
            Dim startOffset As Long = stream.Position

            ' read in the fields from the SHIP_Part struct
            ret._partType = bRead.ReadUInt16
            ret._flags = bRead.ReadUInt16
            ret._field_4 = bRead.ReadUInt16
            ret._field_6 = bRead.ReadUInt16
            ret._field_8 = bRead.ReadUInt16
            ret._field_A = bRead.ReadUInt16
            ret._field_C = bRead.ReadUInt32
            ret._field_10 = bRead.ReadInt16
            ret._field_12 = bRead.ReadInt16
            ret._field_14 = bRead.ReadInt16
            ret._field_16 = bRead.ReadInt16
            ret._field_18 = bRead.ReadInt16
            ret._field_1A = bRead.ReadInt16
            ret._field_1C = bRead.ReadInt16
            ret._field_1E = bRead.ReadInt16
            ret._field_20 = bRead.ReadInt16
            ret._field_22 = bRead.ReadInt16
            ret._field_24 = bRead.ReadUInt32
            ret._field_28 = bRead.ReadUInt16
            ret._field_2A = bRead.ReadByte
            ret._numWeapons = bRead.ReadByte
            ret._meshDataOffset = bRead.ReadUInt16
            ret._weaponOffset = bRead.ReadUInt16
            ret._field_30 = bRead.ReadUInt16
            ret._field_32 = bRead.ReadUInt16
            ret._field_34 = bRead.ReadUInt16
            ret._field_36 = bRead.ReadUInt16
            ret._field_38 = bRead.ReadUInt16
            ret._field_3A = bRead.ReadUInt16
            ret._field_3C = bRead.ReadUInt16
            ret._field_3E = bRead.ReadUInt16

            ' TODO: Load in the part's weapons

            ' load in the part's different LOD meshes
            ' first, seek to the LOD array, whose length is undefined
            stream.Seek(startOffset + ret._meshDataOffset, IO.SeekOrigin.Begin)
            ' now we're going to count how many LODs/Meshes there are by reading the SHIP_3 structs until
            ' we get one with a threshold of 0x7FFFFFFF
            Dim lodCounter As Integer = 1
            Do Until bRead.ReadUInt32 = &H7FFFFFFF
                lodCounter += 1
                ' read past the offset, don't need it right now
                bRead.ReadUInt16()
            Loop

            ' now that we have the number of meshes, allocate the arrays
            ret._numMeshes = lodCounter
            ret._meshes = Array.CreateInstance(GetType(ModelMesh), lodCounter)
            ret._meshLODThreshold = Array.CreateInstance(GetType(Int32), lodCounter)

            ' now load each mesh in
            For i As Integer = 0 To lodCounter - 1
                ' seek to the correct position in the SHIP_3 array
                stream.Seek(startOffset + ret._meshDataOffset + i * 6, IO.SeekOrigin.Begin)
                ' read the relative offset of the mesh
                ret._meshLODThreshold(i) = bRead.ReadInt32
                Dim meshOffset As UInt16 = bRead.ReadUInt16

                ' seek relative to where we are now to the mesh (less the size of the SHIP_3 struct)
                stream.Seek(meshOffset - 6, IO.SeekOrigin.Current)

                ' create the mesh object from the stream
                ret._meshes(i) = ModelMesh.CreateFromStream(stream)
            Next

            ' that should be all we need to do here

            Return ret
        End Function

#End Region
#Region " --- Properties --- "

#Region " SHIP_Part struct fields "

        Public ReadOnly Property PartType As ModelPartType
            Get
                Return _partType
            End Get
        End Property

        Public ReadOnly Property Flags As UInt16
            Get
                Return _flags
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

        Public ReadOnly Property Field_10 As Int16
            Get
                Return _field_10
            End Get
        End Property

        Public ReadOnly Property Field_12 As Int16
            Get
                Return _field_12
            End Get
        End Property

        Public ReadOnly Property Field_14 As Int16
            Get
                Return _field_14
            End Get
        End Property

        Public ReadOnly Property Field_16 As Int16
            Get
                Return _field_16
            End Get
        End Property

        Public ReadOnly Property Field_18 As Int16
            Get
                Return _field_18
            End Get
        End Property

        Public ReadOnly Property Field_1A As Int16
            Get
                Return _field_1A
            End Get
        End Property

        Public ReadOnly Property Field_1C As Int16
            Get
                Return _field_1C
            End Get
        End Property

        Public ReadOnly Property Field_1E As Int16
            Get
                Return _field_1E
            End Get
        End Property

        Public ReadOnly Property Field_20 As Int16
            Get
                Return _field_20
            End Get
        End Property

        Public ReadOnly Property Field_22 As Int16
            Get
                Return _field_22
            End Get
        End Property

        Public ReadOnly Property Field_24 As UInt32
            Get
                Return _field_24
            End Get
        End Property

        Public ReadOnly Property Field_28 As UInt16
            Get
                Return _field_28
            End Get
        End Property

        Public ReadOnly Property Field_2A As Byte
            Get
                Return _field_2A
            End Get
        End Property

        Public ReadOnly Property WeaponCount As Byte
            Get
                Return _numWeapons
            End Get
        End Property

        Public ReadOnly Property MeshDataOffset As UInt16
            Get
                Return _meshDataOffset
            End Get
        End Property

        Public ReadOnly Property WeaponArrayOffset As UInt16
            Get
                Return _weaponOffset
            End Get
        End Property

        Public ReadOnly Property Field_30 As UInt16
            Get
                Return _field_30
            End Get
        End Property

        Public ReadOnly Property Field_32 As UInt16
            Get
                Return _field_32
            End Get
        End Property

        Public ReadOnly Property Field_34 As UInt16
            Get
                Return _field_34
            End Get
        End Property

        Public ReadOnly Property Field_36 As UInt16
            Get
                Return _field_36
            End Get
        End Property

        Public ReadOnly Property Field_38 As UInt16
            Get
                Return _field_38
            End Get
        End Property

        Public ReadOnly Property Field_3A As UInt16
            Get
                Return _field_3A
            End Get
        End Property

        Public ReadOnly Property Field_3C As UInt16
            Get
                Return _field_3C
            End Get
        End Property

        Public ReadOnly Property Field_3E As UInt16
            Get
                Return _field_3E
            End Get
        End Property

#End Region

        Public ReadOnly Property MeshCount As Integer
            Get
                Return _numMeshes
            End Get
        End Property

        Public ReadOnly Property Meshes(index As Integer) As ModelMesh
            Get
                If index < 0 Or index >= _numMeshes Then Throw New ArgumentOutOfRangeException("index")

                Return _meshes(index)
            End Get
        End Property

        Public ReadOnly Property MeshLODThreshold(index As Integer) As Int32
            Get
                If index < 0 Or index >= _numMeshes Then Throw New ArgumentOutOfRangeException("index")

                Return _meshLODThreshold(index)
            End Get
        End Property

#End Region

    End Class

End Namespace
