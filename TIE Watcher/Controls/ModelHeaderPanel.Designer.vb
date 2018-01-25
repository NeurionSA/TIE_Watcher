<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModelHeaderPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dgvUnknownFields = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPartCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblLODTreeCount = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblBoxZ2 = New System.Windows.Forms.Label()
        Me.lblBoxY2 = New System.Windows.Forms.Label()
        Me.lblBoxX2 = New System.Windows.Forms.Label()
        Me.lblBoxZ1 = New System.Windows.Forms.Label()
        Me.lblBoxY1 = New System.Windows.Forms.Label()
        Me.lblBoxX1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dgvUnknownFields)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(168, 400)
        Me.GroupBox5.TabIndex = 16
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
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnknownFields.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUnknownFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnknownFields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.dgvUnknownFields.Location = New System.Drawing.Point(8, 16)
        Me.dgvUnknownFields.MultiSelect = False
        Me.dgvUnknownFields.Name = "dgvUnknownFields"
        Me.dgvUnknownFields.ReadOnly = True
        Me.dgvUnknownFields.RowHeadersVisible = False
        Me.dgvUnknownFields.RowTemplate.Height = 16
        Me.dgvUnknownFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvUnknownFields.Size = New System.Drawing.Size(152, 376)
        Me.dgvUnknownFields.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn1.HeaderText = "Offset"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 48
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn2.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(176, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Part Count:"
        '
        'lblPartCount
        '
        Me.lblPartCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPartCount.Location = New System.Drawing.Point(240, 0)
        Me.lblPartCount.Name = "lblPartCount"
        Me.lblPartCount.Size = New System.Drawing.Size(40, 16)
        Me.lblPartCount.TabIndex = 18
        Me.lblPartCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "LOD Tree Count:"
        '
        'lblLODTreeCount
        '
        Me.lblLODTreeCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLODTreeCount.Location = New System.Drawing.Point(264, 24)
        Me.lblLODTreeCount.Name = "lblLODTreeCount"
        Me.lblLODTreeCount.Size = New System.Drawing.Size(40, 16)
        Me.lblLODTreeCount.TabIndex = 20
        Me.lblLODTreeCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblBoxZ2)
        Me.GroupBox1.Controls.Add(Me.lblBoxY2)
        Me.GroupBox1.Controls.Add(Me.lblBoxX2)
        Me.GroupBox1.Controls.Add(Me.lblBoxZ1)
        Me.GroupBox1.Controls.Add(Me.lblBoxY1)
        Me.GroupBox1.Controls.Add(Me.lblBoxX1)
        Me.GroupBox1.Location = New System.Drawing.Point(176, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(184, 80)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bounding Box"
        '
        'lblBoxZ2
        '
        Me.lblBoxZ2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxZ2.Location = New System.Drawing.Point(120, 56)
        Me.lblBoxZ2.Name = "lblBoxZ2"
        Me.lblBoxZ2.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxZ2.TabIndex = 28
        Me.lblBoxZ2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxY2
        '
        Me.lblBoxY2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxY2.Location = New System.Drawing.Point(64, 56)
        Me.lblBoxY2.Name = "lblBoxY2"
        Me.lblBoxY2.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxY2.TabIndex = 27
        Me.lblBoxY2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxX2
        '
        Me.lblBoxX2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxX2.Location = New System.Drawing.Point(8, 56)
        Me.lblBoxX2.Name = "lblBoxX2"
        Me.lblBoxX2.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxX2.TabIndex = 26
        Me.lblBoxX2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxZ1
        '
        Me.lblBoxZ1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxZ1.Location = New System.Drawing.Point(120, 40)
        Me.lblBoxZ1.Name = "lblBoxZ1"
        Me.lblBoxZ1.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxZ1.TabIndex = 25
        Me.lblBoxZ1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxY1
        '
        Me.lblBoxY1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxY1.Location = New System.Drawing.Point(64, 40)
        Me.lblBoxY1.Name = "lblBoxY1"
        Me.lblBoxY1.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxY1.TabIndex = 24
        Me.lblBoxY1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBoxX1
        '
        Me.lblBoxX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBoxX1.Location = New System.Drawing.Point(8, 40)
        Me.lblBoxX1.Name = "lblBoxX1"
        Me.lblBoxX1.Size = New System.Drawing.Size(56, 16)
        Me.lblBoxX1.TabIndex = 23
        Me.lblBoxX1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "X"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(64, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Y"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(120, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Z"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ModelHeaderPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblLODTreeCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPartCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Name = "ModelHeaderPanel"
        Me.Size = New System.Drawing.Size(494, 409)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents dgvUnknownFields As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPartCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblLODTreeCount As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblBoxZ2 As Label
    Friend WithEvents lblBoxY2 As Label
    Friend WithEvents lblBoxX2 As Label
    Friend WithEvents lblBoxZ1 As Label
    Friend WithEvents lblBoxY1 As Label
    Friend WithEvents lblBoxX1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
End Class
