<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormGoalWatcher
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.picGoals = New System.Windows.Forms.PictureBox()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picGoals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picGoals
        '
        Me.picGoals.Location = New System.Drawing.Point(8, 8)
        Me.picGoals.Name = "picGoals"
        Me.picGoals.Size = New System.Drawing.Size(696, 616)
        Me.picGoals.TabIndex = 1
        Me.picGoals.TabStop = False
        '
        'tmrUpdate
        '
        '
        'FormGoalWatcher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 631)
        Me.Controls.Add(Me.picGoals)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 1080)
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "FormGoalWatcher"
        Me.Text = "FormGoalWatcher"
        CType(Me.picGoals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picGoals As PictureBox
    Friend WithEvents tmrUpdate As Timer
End Class
