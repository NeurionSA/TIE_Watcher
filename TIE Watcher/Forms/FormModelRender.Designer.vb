<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormModelRender
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
        Me.components = New System.ComponentModel.Container()
        Me.pnlControls = New System.Windows.Forms.Panel()
        Me.picDisplay = New System.Windows.Forms.PictureBox()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlControls
        '
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlControls.Location = New System.Drawing.Point(8, 510)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(844, 81)
        Me.pnlControls.TabIndex = 3
        '
        'picDisplay
        '
        Me.picDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picDisplay.Location = New System.Drawing.Point(8, 8)
        Me.picDisplay.MaximumSize = New System.Drawing.Size(1920, 1080)
        Me.picDisplay.MinimumSize = New System.Drawing.Size(64, 64)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.Size = New System.Drawing.Size(844, 502)
        Me.picDisplay.TabIndex = 4
        Me.picDisplay.TabStop = False
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 50
        '
        'FormModelRender
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 599)
        Me.Controls.Add(Me.picDisplay)
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "FormModelRender"
        Me.Padding = New System.Windows.Forms.Padding(8)
        Me.Text = "FormModelRender"
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlControls As Panel
    Friend WithEvents picDisplay As PictureBox
    Friend WithEvents tmrUpdate As Timer
End Class
