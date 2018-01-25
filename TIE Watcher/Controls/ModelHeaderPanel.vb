Imports TIE_Watcher.TIE

''' <summary>
''' Panel for displaying ModelHeader info.
''' </summary>
Public Class ModelHeaderPanel

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "0x{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "0x{0:X4}"
    Private Const UNKNOWN_DWORD_FORMAT As String = "0x{0:X8}"

#End Region
#Region " --- Variables --- "

    'Private header As ModelHeader

    ' unknown fields
    Private cell_Field_0,
        cell_Field_2,
        cell_Field_4,
        cell_Field_6,
        cell_Field_8,
        cell_Field_A,
        cell_Field_C,
        cell_Field_1E,
        cell_Field_1F As DataGridViewCell

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cell_Field_0 = addUnknownField("0")
        cell_Field_2 = addUnknownField("2")
        cell_Field_4 = addUnknownField("4")
        cell_Field_6 = addUnknownField("6")
        cell_Field_8 = addUnknownField("8")
        cell_Field_A = addUnknownField("A")
        cell_Field_C = addUnknownField("C")
        cell_Field_1E = addUnknownField("1E")
        cell_Field_1F = addUnknownField("1F")
    End Sub

#End Region

    ' adds a new row with an offset label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Public Sub UpdateDisplay(header As ModelHeader)
        'Me.header = header

        ' handle known fields
        lblPartCount.Text = header.PartCount
        lblLODTreeCount.Text = header.LODTreeCount
        lblBoxX1.Text = header.BoundsXMinimum
        lblBoxX2.Text = header.BoundsXMaximum
        lblBoxY1.Text = header.BoundsYMinimum
        lblBoxY2.Text = header.BoundsYMaximum
        lblBoxZ1.Text = header.BoundsZMinimum
        lblBoxZ2.Text = header.BoundsZMaximum

        ' handle the unknown fields
        cell_Field_0.Value = String.Format(UNKNOWN_WORD_FORMAT, header.Field_0)
        cell_Field_2.Value = String.Format(UNKNOWN_WORD_FORMAT, header.Field_2)
        cell_Field_4.Value = String.Format("{0}", header.Field_4)
        cell_Field_6.Value = String.Format("{0}", header.Field_6)
        cell_Field_8.Value = String.Format("{0}", header.Field_8)
        cell_Field_A.Value = String.Format("{0}", header.Field_A)
        cell_Field_C.Value = String.Format("{0}", header.Field_C)
        cell_Field_1E.Value = String.Format(UNKNOWN_BYTE_FORMAT, header.Field_1E)
        cell_Field_1F.Value = String.Format(UNKNOWN_BYTE_FORMAT, header.Field_1F)
    End Sub

#End Region
#Region " --- Properties --- "



#End Region

End Class
