<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormModelVectorize
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvFaces = New System.Windows.Forms.DataGridView()
        Me.colDrawCheck = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colPartName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFaceIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFaceType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.picRender = New System.Windows.Forms.PictureBox()
        Me.cmdMoveFront = New System.Windows.Forms.Button()
        Me.cmdMoveBack = New System.Windows.Forms.Button()
        Me.cmdSaveAs = New System.Windows.Forms.Button()
        Me.sdlgSaveAs = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvFaces, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRender, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvFaces)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 552)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Face Draw Order"
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
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFaces.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvFaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvFaces.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDrawCheck, Me.colPartName, Me.colFaceIndex, Me.colFaceType})
        Me.dgvFaces.Location = New System.Drawing.Point(8, 16)
        Me.dgvFaces.Name = "dgvFaces"
        Me.dgvFaces.RowHeadersVisible = False
        Me.dgvFaces.RowTemplate.Height = 20
        Me.dgvFaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFaces.Size = New System.Drawing.Size(360, 528)
        Me.dgvFaces.TabIndex = 3
        '
        'colDrawCheck
        '
        Me.colDrawCheck.FalseValue = "False"
        Me.colDrawCheck.HeaderText = "Draw"
        Me.colDrawCheck.Name = "colDrawCheck"
        Me.colDrawCheck.TrueValue = "True"
        Me.colDrawCheck.Width = 38
        '
        'colPartName
        '
        Me.colPartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colPartName.HeaderText = "Part"
        Me.colPartName.Name = "colPartName"
        Me.colPartName.ReadOnly = True
        Me.colPartName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPartName.Width = 150
        '
        'colFaceIndex
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colFaceIndex.DefaultCellStyle = DataGridViewCellStyle4
        Me.colFaceIndex.HeaderText = "Face"
        Me.colFaceIndex.Name = "colFaceIndex"
        Me.colFaceIndex.ReadOnly = True
        Me.colFaceIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colFaceIndex.Width = 48
        '
        'colFaceType
        '
        Me.colFaceType.HeaderText = "Type"
        Me.colFaceType.Name = "colFaceType"
        Me.colFaceType.ReadOnly = True
        Me.colFaceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'picRender
        '
        Me.picRender.Location = New System.Drawing.Point(392, 8)
        Me.picRender.Name = "picRender"
        Me.picRender.Size = New System.Drawing.Size(696, 704)
        Me.picRender.TabIndex = 1
        Me.picRender.TabStop = False
        '
        'cmdMoveFront
        '
        Me.cmdMoveFront.Location = New System.Drawing.Point(104, 568)
        Me.cmdMoveFront.Name = "cmdMoveFront"
        Me.cmdMoveFront.Size = New System.Drawing.Size(88, 40)
        Me.cmdMoveFront.TabIndex = 2
        Me.cmdMoveFront.Text = "Bring to Front"
        Me.cmdMoveFront.UseVisualStyleBackColor = True
        '
        'cmdMoveBack
        '
        Me.cmdMoveBack.Location = New System.Drawing.Point(8, 568)
        Me.cmdMoveBack.Name = "cmdMoveBack"
        Me.cmdMoveBack.Size = New System.Drawing.Size(88, 40)
        Me.cmdMoveBack.TabIndex = 3
        Me.cmdMoveBack.Text = "Send to Back"
        Me.cmdMoveBack.UseVisualStyleBackColor = True
        '
        'cmdSaveAs
        '
        Me.cmdSaveAs.Location = New System.Drawing.Point(288, 568)
        Me.cmdSaveAs.Name = "cmdSaveAs"
        Me.cmdSaveAs.Size = New System.Drawing.Size(88, 40)
        Me.cmdSaveAs.TabIndex = 4
        Me.cmdSaveAs.Text = "Save As..."
        Me.cmdSaveAs.UseVisualStyleBackColor = True
        '
        'sdlgSaveAs
        '
        Me.sdlgSaveAs.DefaultExt = "vec"
        Me.sdlgSaveAs.Filter = "SHIP Vector files|*.vec|All files|*.*"
        Me.sdlgSaveAs.Title = "Save Vector as..."
        '
        'FormModelVectorize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 721)
        Me.Controls.Add(Me.cmdSaveAs)
        Me.Controls.Add(Me.cmdMoveBack)
        Me.Controls.Add(Me.cmdMoveFront)
        Me.Controls.Add(Me.picRender)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormModelVectorize"
        Me.Text = "FormModelVectorize"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvFaces, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRender, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvFaces As DataGridView
    Friend WithEvents picRender As PictureBox
    Friend WithEvents colDrawCheck As DataGridViewCheckBoxColumn
    Friend WithEvents colPartName As DataGridViewTextBoxColumn
    Friend WithEvents colFaceIndex As DataGridViewTextBoxColumn
    Friend WithEvents colFaceType As DataGridViewTextBoxColumn
    Friend WithEvents cmdMoveFront As Button
    Friend WithEvents cmdMoveBack As Button
    Friend WithEvents cmdSaveAs As Button
    Friend WithEvents sdlgSaveAs As SaveFileDialog
End Class
