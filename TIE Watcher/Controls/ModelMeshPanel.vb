Imports TIE_Watcher.TIE

''' <summary>
''' Panel for display ModelMesh info.
''' </summary>
Public Class ModelMeshPanel

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "0x{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "0x{0:X4}"
    Private Const UNKNOWN_DWORD_FORMAT As String = "0x{0:X8}"

#End Region
#Region " --- Variables --- "

    Private mesh As ModelMesh

    ' unknown fields
    Private cell_Mesh_Field_0,
        cell_Mesh_Field_1,
        cell_Mesh_Field_3 As DataGridViewCell

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cell_Mesh_Field_0 = addUnknownField("Mesh_0")
        cell_Mesh_Field_1 = addUnknownField("Mesh_1")
        cell_Mesh_Field_3 = addUnknownField("Mesh_3")
    End Sub

#End Region
#Region " Event-Handlers "

#End Region

    ' adds a new row with a label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Private Function byteArrayString(bytes() As Byte) As String
        Dim sb As New Text.StringBuilder

        For i As Integer = 0 To bytes.Length - 1
            sb.AppendFormat("{0:X2} ", bytes(i))
        Next

        Return sb.ToString
    End Function

    Public Sub UpdateDisplay(mesh As ModelMesh)
        Me.mesh = mesh

        ' handle known fields
        lblVertexCount.Text = mesh.VertexCount
        lblFaceCount.Text = mesh.FaceCount

        lblBoxX1.Text = mesh.Box_Field_0
        lblBoxY1.Text = mesh.Box_Field_2
        lblBoxZ1.Text = mesh.Box_Field_4
        lblBoxX2.Text = mesh.Box_Field_6
        lblBoxY2.Text = mesh.Box_Field_8
        lblBoxZ2.Text = mesh.Box_Field_A

        lblNumMarkedFaces.Text = mesh.MarkedFacesCount

        ' handle unknown fields
        cell_Mesh_Field_0.Value = String.Format(UNKNOWN_BYTE_FORMAT, mesh.Mesh_Field_0)
        cell_Mesh_Field_1.Value = String.Format(UNKNOWN_BYTE_FORMAT, mesh.Mesh_Field_1)
        cell_Mesh_Field_3.Value = String.Format(UNKNOWN_BYTE_FORMAT, mesh.Mesh_Field_3)

        ' loop through the faces and put the relevant info in the faces table
        dgvFaces.Rows.Clear()
        For i As Integer = 0 To mesh.FaceCount - 1
            Dim row As DataGridViewRow = dgvFaces.Rows.Item(dgvFaces.Rows.Add())
            ' face index
            row.Cells.Item(0).Value = i
            ' face color
            row.Cells.Item(1).Value = mesh.Faces(i).Color
            ' normal x
            row.Cells.Item(2).Value = String.Format("{0:0.0000}", mesh.Faces(i).Normal.X)
            ' normal y
            row.Cells.Item(3).Value = String.Format("{0:0.0000}", mesh.Faces(i).Normal.Y)
            ' normal z
            row.Cells.Item(4).Value = String.Format("{0:0.0000}", mesh.Faces(i).Normal.Z)
            ' face edge bytes
            row.Cells.Item(5).Value = byteArrayString(mesh.Faces(i).EdgeBytes)
        Next
    End Sub

#End Region
#Region " --- Properties --- "



#End Region

End Class
