<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTrainer
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkNoPanelDamage = New System.Windows.Forms.CheckBox()
        Me.chkResetHullDamage = New System.Windows.Forms.CheckBox()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkNoPanelDamage)
        Me.GroupBox1.Controls.Add(Me.chkResetHullDamage)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(304, 168)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Player's Craft"
        '
        'chkNoPanelDamage
        '
        Me.chkNoPanelDamage.AutoSize = True
        Me.chkNoPanelDamage.Location = New System.Drawing.Point(8, 48)
        Me.chkNoPanelDamage.Name = "chkNoPanelDamage"
        Me.chkNoPanelDamage.Size = New System.Drawing.Size(146, 17)
        Me.chkNoPanelDamage.TabIndex = 2
        Me.chkNoPanelDamage.Text = "Take no Panel Damage *"
        Me.chkNoPanelDamage.UseVisualStyleBackColor = True
        '
        'chkResetHullDamage
        '
        Me.chkResetHullDamage.AutoSize = True
        Me.chkResetHullDamage.ForeColor = System.Drawing.Color.Red
        Me.chkResetHullDamage.Location = New System.Drawing.Point(8, 24)
        Me.chkResetHullDamage.Name = "chkResetHullDamage"
        Me.chkResetHullDamage.Size = New System.Drawing.Size(118, 17)
        Me.chkResetHullDamage.TabIndex = 1
        Me.chkResetHullDamage.Text = "Reset Hull Damage"
        Me.chkResetHullDamage.UseVisualStyleBackColor = True
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Options marked with * involve code injection/modification."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(315, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Options in Red text will cause desync of in-flight game recordings."
        '
        'FormTrainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 351)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormTrainer"
        Me.Text = "FormTrainer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents chkNoPanelDamage As CheckBox
    Friend WithEvents chkResetHullDamage As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
