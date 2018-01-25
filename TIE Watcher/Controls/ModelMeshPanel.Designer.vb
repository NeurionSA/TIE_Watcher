<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModelMeshPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dgvUnknownFields = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblVertexCount = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFaceCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblBoxZ2 = New System.Windows.Forms.Label()
        Me.lblBoxY2 = New System.Windows.Forms.Label()
        Me.lblBoxX2 = New System.Windows.Forms.Label()
        Me.lblBoxZ1 = New System.Windows.Forms.Label()
        Me.lblBoxY1 = New System.Windows.Forms.Label()
        Me.lblBoxX1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvFaces = New System.Windows.Forms.DataGridView()
        Me.lblNumMarkedFaces = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvFaces, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dgvUnknownFields)
        Me.GroupBox5.Location = New System.Drawing.Point(240, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(176, 96)
        Me.GroupBox5.TabIndex = 17
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Unknown Fields"
        '
        'dgvUnknownFields
        '
        Me.dgvUnknownFields.AllowUserToAddRows = False
        Me.dgvUnknownFields.AllowUserToDeleteRows = False
        Me.dgvUnknownFields.AllowUserToResizeColumns = False
        Me.dgvUnknownFields.AllowUserToResizeRows = False
        Me.dgvUnknownFields.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvUnknownFields.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvUnknownFields.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        Me.dgvUnknownFields.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnknownFields.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUnknownFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnknownFields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.dgvUnknownFields.Location = New System.Drawing.Point(8, 16)
        Me.dgvUnknownFields.MultiSelect = False
        Me.dgvUnknownFields.Name = "dgvUnknownFields"
        Me.dgvUnknownFields.ReadOnly = True
        Me.dgvUnknownFields.RowHeadersVisible = False
        Me.dgvUnknownFields.RowTemplate.Height = 16
        Me.dgvUnknownFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvUnknownFields.Size = New System.Drawing.Size(160, 72)
        Me.dgvUnknownFields.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 64
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn2.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'lblVertexCount
        '
        Me.lblVertexCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVertexCount.Location = New System.Drawing.Point(72, 0)
        Me.lblVertexCount.Name = "lblVertexCount"
        Me.lblVertexCount.Size = New System.Drawing.Size(40, 16)
        Me.lblVertexCount.TabIndex = 20
        Me.lblVertexCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Vertex Count:"
        '
        'lblFaceCount
        '
        Me.lblFaceCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFaceCount.Location = New System.Drawing.Point(184, 0)
        Me.lblFaceCount.Name = "lblFaceCount"
        Me.lblFaceCount.Size = New System.Drawing.Size(40, 16)
        Me.lblFaceCount.TabIndex = 22
        Me.lblFaceCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(120, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Face Count:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblBoxZ2)
        Me.GroupBox1.Controls.Add(Me.lblBoxY2)
        Me.GroupBox1.Controls.Add(Me.lblBoxX2)
        Me.GroupBox1.Controls.Add(Me.lblBoxZ1)
        Me.GroupBox1.Controls.Add(Me.lblBoxY1)
        Me.GroupBox1.Controls.Add(Me.lblBoxX1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 64)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bounding Box?"
        '
        'lblBoxZ2
        '
        Me.lblBoxZ2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxZ2.Location = New System.Drawing.Point(152, 40)
        Me.lblBoxZ2.Name = "lblBoxZ2"
        Me.lblBoxZ2.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxZ2.TabIndex = 28
        Me.lblBoxZ2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxY2
        '
        Me.lblBoxY2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxY2.Location = New System.Drawing.Point(80, 40)
        Me.lblBoxY2.Name = "lblBoxY2"
        Me.lblBoxY2.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxY2.TabIndex = 27
        Me.lblBoxY2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxX2
        '
        Me.lblBoxX2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxX2.Location = New System.Drawing.Point(8, 40)
        Me.lblBoxX2.Name = "lblBoxX2"
        Me.lblBoxX2.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxX2.TabIndex = 26
        Me.lblBoxX2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxZ1
        '
        Me.lblBoxZ1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxZ1.Location = New System.Drawing.Point(152, 24)
        Me.lblBoxZ1.Name = "lblBoxZ1"
        Me.lblBoxZ1.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxZ1.TabIndex = 25
        Me.lblBoxZ1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxY1
        '
        Me.lblBoxY1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxY1.Location = New System.Drawing.Point(80, 24)
        Me.lblBoxY1.Name = "lblBoxY1"
        Me.lblBoxY1.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxY1.TabIndex = 24
        Me.lblBoxY1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxX1
        '
        Me.lblBoxX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxX1.Location = New System.Drawing.Point(8, 24)
        Me.lblBoxX1.Name = "lblBoxX1"
        Me.lblBoxX1.Size = New System.Drawing.Size(64, 16)
        Me.lblBoxX1.TabIndex = 23
        Me.lblBoxX1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvFaces)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(520, 240)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Faces"
        '
        'dgvFaces
        '
        Me.dgvFaces.AllowUserToAddRows = False
        Me.dgvFaces.AllowUserToDeleteRows = False
        Me.dgvFaces.AllowUserToResizeColumns = False
        Me.dgvFaces.AllowUserToResizeRows = False
        Me.dgvFaces.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvFaces.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvFaces.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        Me.dgvFaces.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFaces.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvFaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvFaces.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.Column4, Me.DataGridViewTextBoxColumn4, Me.Column1, Me.Column2, Me.Column3})
        Me.dgvFaces.Location = New System.Drawing.Point(8, 16)
        Me.dgvFaces.MultiSelect = False
        Me.dgvFaces.Name = "dgvFaces"
        Me.dgvFaces.ReadOnly = True
        Me.dgvFaces.RowHeadersVisible = False
        Me.dgvFaces.RowTemplate.Height = 16
        Me.dgvFaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvFaces.Size = New System.Drawing.Size(504, 216)
        Me.dgvFaces.TabIndex = 2
        '
        'lblNumMarkedFaces
        '
        Me.lblNumMarkedFaces.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumMarkedFaces.Location = New System.Drawing.Point(112, 24)
        Me.lblNumMarkedFaces.Name = "lblNumMarkedFaces"
        Me.lblNumMarkedFaces.Size = New System.Drawing.Size(40, 16)
        Me.lblNumMarkedFaces.TabIndex = 26
        Me.lblNumMarkedFaces.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Marked Faces Count:"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn3.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'Column4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column4.HeaderText = "Color"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 40
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn4.HeaderText = "Normal X"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 56
        '
        'Column1
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column1.HeaderText = "Normal Y"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 56
        '
        'Column2
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column2.HeaderText = "Normal Z"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 56
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column3.HeaderText = "Edge Bytes"
        Me.Column3.MinimumWidth = 200
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'ModelMeshPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblNumMarkedFaces)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblFaceCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblVertexCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Name = "ModelMeshPanel"
        Me.Size = New System.Drawing.Size(555, 410)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvFaces, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents dgvUnknownFields As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents lblVertexCount As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblFaceCount As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblBoxZ2 As Label
    Friend WithEvents lblBoxY2 As Label
    Friend WithEvents lblBoxX2 As Label
    Friend WithEvents lblBoxZ1 As Label
    Friend WithEvents lblBoxY1 As Label
    Friend WithEvents lblBoxX1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dgvFaces As DataGridView
    Friend WithEvents lblNumMarkedFaces As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
End Class
