<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModelPartHeaderPanel
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dgvUnknownFields = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblWeaponCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPartType = New System.Windows.Forms.Label()
        Me.lblFlags = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblWeaponArrayOffset = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMeshDataOffset = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dgvUnknownFields)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(168, 400)
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
        Me.dgvUnknownFields.Size = New System.Drawing.Size(152, 376)
        Me.dgvUnknownFields.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn1.HeaderText = "Offset"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 48
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
        'lblWeaponCount
        '
        Me.lblWeaponCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeaponCount.Location = New System.Drawing.Point(256, 48)
        Me.lblWeaponCount.Name = "lblWeaponCount"
        Me.lblWeaponCount.Size = New System.Drawing.Size(40, 16)
        Me.lblWeaponCount.TabIndex = 22
        Me.lblWeaponCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Weapon Count:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(176, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Part Type:"
        '
        'lblPartType
        '
        Me.lblPartType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPartType.Location = New System.Drawing.Point(232, 0)
        Me.lblPartType.Name = "lblPartType"
        Me.lblPartType.Size = New System.Drawing.Size(128, 16)
        Me.lblPartType.TabIndex = 24
        Me.lblPartType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFlags
        '
        Me.lblFlags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFlags.Location = New System.Drawing.Point(216, 24)
        Me.lblFlags.Name = "lblFlags"
        Me.lblFlags.Size = New System.Drawing.Size(80, 16)
        Me.lblFlags.TabIndex = 26
        Me.lblFlags.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(176, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Flags:"
        '
        'lblWeaponArrayOffset
        '
        Me.lblWeaponArrayOffset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeaponArrayOffset.Location = New System.Drawing.Point(288, 72)
        Me.lblWeaponArrayOffset.Name = "lblWeaponArrayOffset"
        Me.lblWeaponArrayOffset.Size = New System.Drawing.Size(72, 16)
        Me.lblWeaponArrayOffset.TabIndex = 28
        Me.lblWeaponArrayOffset.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(176, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Weapon Array Offset:"
        '
        'lblMeshDataOffset
        '
        Me.lblMeshDataOffset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeshDataOffset.Location = New System.Drawing.Point(288, 96)
        Me.lblMeshDataOffset.Name = "lblMeshDataOffset"
        Me.lblMeshDataOffset.Size = New System.Drawing.Size(72, 16)
        Me.lblMeshDataOffset.TabIndex = 30
        Me.lblMeshDataOffset.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(176, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Mesh Data Offset:"
        '
        'ModelPartHeaderPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.lblMeshDataOffset)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblWeaponArrayOffset)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblFlags)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblPartType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblWeaponCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Name = "ModelPartHeaderPanel"
        Me.Size = New System.Drawing.Size(364, 411)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents dgvUnknownFields As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents lblWeaponCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPartType As Label
    Friend WithEvents lblFlags As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblWeaponArrayOffset As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblMeshDataOffset As Label
    Friend WithEvents Label6 As Label
End Class
