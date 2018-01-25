<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCombatMap
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.picDisplay = New System.Windows.Forms.PictureBox()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.pnlControls = New System.Windows.Forms.Panel()
        Me.grpFollowObject = New System.Windows.Forms.GroupBox()
        Me.chkFollowObject = New System.Windows.Forms.CheckBox()
        Me.chkVectorLargeCraft = New System.Windows.Forms.CheckBox()
        Me.selSpaceObject = New TIE_Watcher.SpaceObjectSelector()
        Me.cmdFollowPlayer = New System.Windows.Forms.Button()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlControls.SuspendLayout()
        Me.grpFollowObject.SuspendLayout()
        Me.SuspendLayout()
        '
        'picDisplay
        '
        Me.picDisplay.Location = New System.Drawing.Point(8, 8)
        Me.picDisplay.MaximumSize = New System.Drawing.Size(1920, 1080)
        Me.picDisplay.MinimumSize = New System.Drawing.Size(64, 64)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.Size = New System.Drawing.Size(776, 528)
        Me.picDisplay.TabIndex = 0
        Me.picDisplay.TabStop = False
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 50
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.cmdFollowPlayer)
        Me.pnlControls.Controls.Add(Me.grpFollowObject)
        Me.pnlControls.Controls.Add(Me.chkVectorLargeCraft)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlControls.Location = New System.Drawing.Point(0, 544)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(791, 81)
        Me.pnlControls.TabIndex = 2
        '
        'grpFollowObject
        '
        Me.grpFollowObject.Controls.Add(Me.selSpaceObject)
        Me.grpFollowObject.Controls.Add(Me.chkFollowObject)
        Me.grpFollowObject.Location = New System.Drawing.Point(8, 8)
        Me.grpFollowObject.Name = "grpFollowObject"
        Me.grpFollowObject.Size = New System.Drawing.Size(200, 56)
        Me.grpFollowObject.TabIndex = 2
        Me.grpFollowObject.TabStop = False
        Me.grpFollowObject.Text = "Follow Object"
        '
        'chkFollowObject
        '
        Me.chkFollowObject.AutoSize = True
        Me.chkFollowObject.Checked = True
        Me.chkFollowObject.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFollowObject.Location = New System.Drawing.Point(8, 0)
        Me.chkFollowObject.Name = "chkFollowObject"
        Me.chkFollowObject.Size = New System.Drawing.Size(90, 17)
        Me.chkFollowObject.TabIndex = 1
        Me.chkFollowObject.Text = "Follow Object"
        Me.chkFollowObject.UseVisualStyleBackColor = True
        '
        'chkVectorLargeCraft
        '
        Me.chkVectorLargeCraft.AutoSize = True
        Me.chkVectorLargeCraft.Checked = True
        Me.chkVectorLargeCraft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVectorLargeCraft.Location = New System.Drawing.Point(376, 8)
        Me.chkVectorLargeCraft.Name = "chkVectorLargeCraft"
        Me.chkVectorLargeCraft.Size = New System.Drawing.Size(116, 17)
        Me.chkVectorLargeCraft.TabIndex = 0
        Me.chkVectorLargeCraft.Text = "Render Large Craft"
        Me.chkVectorLargeCraft.UseVisualStyleBackColor = True
        '
        'selSpaceObject
        '
        Me.selSpaceObject.DisableMouseWheelScrolling = True
        Me.selSpaceObject.Location = New System.Drawing.Point(8, 24)
        Me.selSpaceObject.MaximumSize = New System.Drawing.Size(1024, 21)
        Me.selSpaceObject.MinimumSize = New System.Drawing.Size(0, 21)
        Me.selSpaceObject.Name = "selSpaceObject"
        Me.selSpaceObject.Size = New System.Drawing.Size(184, 21)
        Me.selSpaceObject.TabIndex = 3
        '
        'cmdFollowPlayer
        '
        Me.cmdFollowPlayer.Location = New System.Drawing.Point(120, 4)
        Me.cmdFollowPlayer.Name = "cmdFollowPlayer"
        Me.cmdFollowPlayer.Size = New System.Drawing.Size(80, 24)
        Me.cmdFollowPlayer.TabIndex = 3
        Me.cmdFollowPlayer.Text = "Follow Player"
        Me.cmdFollowPlayer.UseVisualStyleBackColor = True
        '
        'FormCombatMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(791, 625)
        Me.Controls.Add(Me.pnlControls)
        Me.Controls.Add(Me.picDisplay)
        Me.Name = "FormCombatMap"
        Me.Text = "FormCombatMap"
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlControls.ResumeLayout(False)
        Me.pnlControls.PerformLayout()
        Me.grpFollowObject.ResumeLayout(False)
        Me.grpFollowObject.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picDisplay As PictureBox
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents pnlControls As Panel
    Friend WithEvents chkVectorLargeCraft As CheckBox
    Friend WithEvents grpFollowObject As GroupBox
    Friend WithEvents chkFollowObject As CheckBox
    Friend WithEvents selSpaceObject As SpaceObjectSelector
    Friend WithEvents cmdFollowPlayer As Button
End Class
