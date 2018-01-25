Imports TIE_Watcher.TIE

''' <summary>
''' Panel for displaying ModelPartHeader info.
''' </summary>
Public Class ModelPartHeaderPanel

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "0x{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "0x{0:X4}"
    Private Const UNKNOWN_DWORD_FORMAT As String = "0x{0:X8}"

#End Region
#Region " --- Variables --- "

    ' unknown fields
    Private cell_Field_4,
        cell_Field_6,
        cell_Field_8,
        cell_Field_A,
        cell_Field_C,
        cell_Field_10,
        cell_Field_12,
        cell_Field_14,
        cell_Field_16,
        cell_Field_18,
        cell_Field_1A,
        cell_Field_1C,
        cell_Field_1E,
        cell_Field_20,
        cell_Field_22,
        cell_Field_24,
        cell_Field_28,
        cell_Field_2A,
        cell_Field_30,
        cell_Field_32,
        cell_Field_34,
        cell_Field_36,
        cell_Field_38,
        cell_Field_3A,
        cell_Field_3C,
        cell_Field_3E As DataGridViewCell

#End Region
#Region " --- Methods --- "
#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cell_Field_4 = addUnknownField("4")
        cell_Field_6 = addUnknownField("6")
        cell_Field_8 = addUnknownField("8")
        cell_Field_A = addUnknownField("A")
        cell_Field_C = addUnknownField("C")
        cell_Field_10 = addUnknownField("10")
        cell_Field_12 = addUnknownField("12")
        cell_Field_14 = addUnknownField("14")
        cell_Field_16 = addUnknownField("16")
        cell_Field_18 = addUnknownField("18")
        cell_Field_1A = addUnknownField("1A")
        cell_Field_1C = addUnknownField("1C")
        cell_Field_1E = addUnknownField("1E")
        cell_Field_20 = addUnknownField("20")
        cell_Field_22 = addUnknownField("22")
        cell_Field_24 = addUnknownField("24")
        cell_Field_28 = addUnknownField("28")
        cell_Field_2A = addUnknownField("2A")
        cell_Field_30 = addUnknownField("30")
        cell_Field_32 = addUnknownField("32")
        cell_Field_34 = addUnknownField("34")
        cell_Field_36 = addUnknownField("36")
        cell_Field_38 = addUnknownField("38")
        cell_Field_3A = addUnknownField("3A")
        cell_Field_3C = addUnknownField("3C")
        cell_Field_3E = addUnknownField("3E")
    End Sub

#End Region

    ' adds a new row with an offset label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Public Sub UpdateDisplay(partHeader As ModelPart)
        ' handle known fields
        lblPartType.Text = partHeader.PartType.ToString
        lblFlags.Text = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Flags)
        lblWeaponCount.Text = partHeader.WeaponCount
        lblWeaponArrayOffset.Text = String.Format(UNKNOWN_WORD_FORMAT, partHeader.WeaponArrayOffset)
        lblMeshDataOffset.Text = String.Format(UNKNOWN_WORD_FORMAT, partHeader.MeshDataOffset)

        ' handle unknown fields
        cell_Field_4.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_4)
        cell_Field_6.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_6)
        cell_Field_8.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_8)
        cell_Field_A.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_A)
        cell_Field_C.Value = String.Format(UNKNOWN_DWORD_FORMAT, partHeader.Field_C)
        cell_Field_10.Value = String.Format("{0}", partHeader.Field_10)
        cell_Field_12.Value = String.Format("{0}", partHeader.Field_12)
        cell_Field_14.Value = String.Format("{0}", partHeader.Field_14)
        cell_Field_16.Value = String.Format("{0}", partHeader.Field_16)
        cell_Field_18.Value = String.Format("{0}", partHeader.Field_18)
        cell_Field_1A.Value = String.Format("{0}", partHeader.Field_1A)
        cell_Field_1C.Value = String.Format("{0}", partHeader.Field_1C)
        cell_Field_1E.Value = String.Format("{0}", partHeader.Field_1E)
        cell_Field_20.Value = String.Format("{0}", partHeader.Field_20)
        cell_Field_22.Value = String.Format("{0}", partHeader.Field_22)
        cell_Field_24.Value = String.Format(UNKNOWN_DWORD_FORMAT, partHeader.Field_24)
        cell_Field_28.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_28)
        cell_Field_2A.Value = String.Format(UNKNOWN_BYTE_FORMAT, partHeader.Field_2A)
        cell_Field_30.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_30)
        cell_Field_32.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_32)
        cell_Field_34.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_34)
        cell_Field_36.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_36)
        cell_Field_38.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_38)
        cell_Field_3A.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_3A)
        cell_Field_3C.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_3C)
        cell_Field_3E.Value = String.Format(UNKNOWN_WORD_FORMAT, partHeader.Field_3E)
    End Sub

#End Region

End Class
