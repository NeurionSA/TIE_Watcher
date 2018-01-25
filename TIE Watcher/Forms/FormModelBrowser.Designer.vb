<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormModelBrowser
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbModelSelect = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tvModelElements = New System.Windows.Forms.TreeView()
        Me.grpElementInfo = New System.Windows.Forms.GroupBox()
        Me.cmdTestRender = New System.Windows.Forms.Button()
        Me.cmdVector = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbModelSelect)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(192, 48)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Model Selector"
        '
        'cmbModelSelect
        '
        Me.cmbModelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbModelSelect.FormattingEnabled = True
        Me.cmbModelSelect.Location = New System.Drawing.Point(8, 16)
        Me.cmbModelSelect.Name = "cmbModelSelect"
        Me.cmbModelSelect.Size = New System.Drawing.Size(176, 21)
        Me.cmbModelSelect.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tvModelElements)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(248, 456)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Model Elements"
        '
        'tvModelElements
        '
        Me.tvModelElements.HideSelection = False
        Me.tvModelElements.Location = New System.Drawing.Point(8, 16)
        Me.tvModelElements.Name = "tvModelElements"
        Me.tvModelElements.Size = New System.Drawing.Size(232, 432)
        Me.tvModelElements.TabIndex = 2
        '
        'grpElementInfo
        '
        Me.grpElementInfo.Location = New System.Drawing.Point(264, 8)
        Me.grpElementInfo.Name = "grpElementInfo"
        Me.grpElementInfo.Padding = New System.Windows.Forms.Padding(8)
        Me.grpElementInfo.Size = New System.Drawing.Size(584, 512)
        Me.grpElementInfo.TabIndex = 3
        Me.grpElementInfo.TabStop = False
        Me.grpElementInfo.Text = "Element Information"
        '
        'cmdTestRender
        '
        Me.cmdTestRender.Location = New System.Drawing.Point(208, 16)
        Me.cmdTestRender.Name = "cmdTestRender"
        Me.cmdTestRender.Size = New System.Drawing.Size(48, 32)
        Me.cmdTestRender.TabIndex = 0
        Me.cmdTestRender.Text = "Draw"
        Me.cmdTestRender.UseVisualStyleBackColor = True
        '
        'cmdVector
        '
        Me.cmdVector.Location = New System.Drawing.Point(8, 528)
        Me.cmdVector.Name = "cmdVector"
        Me.cmdVector.Size = New System.Drawing.Size(80, 32)
        Me.cmdVector.TabIndex = 4
        Me.cmdVector.Text = "Vectorize"
        Me.cmdVector.UseVisualStyleBackColor = True
        '
        'FormModelBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 571)
        Me.Controls.Add(Me.cmdVector)
        Me.Controls.Add(Me.cmdTestRender)
        Me.Controls.Add(Me.grpElementInfo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormModelBrowser"
        Me.Text = "FormModelBrowser"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbModelSelect As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tvModelElements As TreeView
    Friend WithEvents grpElementInfo As GroupBox
    Friend WithEvents cmdTestRender As Button
    Friend WithEvents cmdVector As Button
End Class
