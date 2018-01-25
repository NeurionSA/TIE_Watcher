<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormTest
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.cmdTestFlightGroup = New System.Windows.Forms.Button()
        Me.cmdTestSpaceObjects = New System.Windows.Forms.Button()
        Me.cmdTestCombatMap = New System.Windows.Forms.Button()
        Me.cmdTestMissionGoals = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdTrainer = New System.Windows.Forms.Button()
        Me.cmdModelBrowser = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdTestFlightGroup
        '
        Me.cmdTestFlightGroup.Location = New System.Drawing.Point(8, 8)
        Me.cmdTestFlightGroup.Name = "cmdTestFlightGroup"
        Me.cmdTestFlightGroup.Size = New System.Drawing.Size(88, 40)
        Me.cmdTestFlightGroup.TabIndex = 0
        Me.cmdTestFlightGroup.Text = "Flight Groups"
        Me.cmdTestFlightGroup.UseVisualStyleBackColor = True
        '
        'cmdTestSpaceObjects
        '
        Me.cmdTestSpaceObjects.Location = New System.Drawing.Point(104, 8)
        Me.cmdTestSpaceObjects.Name = "cmdTestSpaceObjects"
        Me.cmdTestSpaceObjects.Size = New System.Drawing.Size(88, 40)
        Me.cmdTestSpaceObjects.TabIndex = 1
        Me.cmdTestSpaceObjects.Text = "Space Objects"
        Me.cmdTestSpaceObjects.UseVisualStyleBackColor = True
        '
        'cmdTestCombatMap
        '
        Me.cmdTestCombatMap.Location = New System.Drawing.Point(8, 56)
        Me.cmdTestCombatMap.Name = "cmdTestCombatMap"
        Me.cmdTestCombatMap.Size = New System.Drawing.Size(88, 40)
        Me.cmdTestCombatMap.TabIndex = 2
        Me.cmdTestCombatMap.Text = "Combat Map"
        Me.cmdTestCombatMap.UseVisualStyleBackColor = True
        '
        'cmdTestMissionGoals
        '
        Me.cmdTestMissionGoals.Location = New System.Drawing.Point(104, 56)
        Me.cmdTestMissionGoals.Name = "cmdTestMissionGoals"
        Me.cmdTestMissionGoals.Size = New System.Drawing.Size(88, 40)
        Me.cmdTestMissionGoals.TabIndex = 3
        Me.cmdTestMissionGoals.Text = "Mission Goals"
        Me.cmdTestMissionGoals.UseVisualStyleBackColor = True
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(8, 152)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(88, 40)
        Me.cmdTest.TabIndex = 4
        Me.cmdTest.Text = "Test"
        Me.cmdTest.UseVisualStyleBackColor = True
        '
        'cmdTrainer
        '
        Me.cmdTrainer.Location = New System.Drawing.Point(8, 104)
        Me.cmdTrainer.Name = "cmdTrainer"
        Me.cmdTrainer.Size = New System.Drawing.Size(88, 40)
        Me.cmdTrainer.TabIndex = 5
        Me.cmdTrainer.Text = "Trainer"
        Me.cmdTrainer.UseVisualStyleBackColor = True
        '
        'cmdModelBrowser
        '
        Me.cmdModelBrowser.Location = New System.Drawing.Point(104, 104)
        Me.cmdModelBrowser.Name = "cmdModelBrowser"
        Me.cmdModelBrowser.Size = New System.Drawing.Size(88, 40)
        Me.cmdModelBrowser.TabIndex = 6
        Me.cmdModelBrowser.Text = "Model Browser"
        Me.cmdModelBrowser.UseVisualStyleBackColor = True
        '
        'FormTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(200, 199)
        Me.Controls.Add(Me.cmdModelBrowser)
        Me.Controls.Add(Me.cmdTrainer)
        Me.Controls.Add(Me.cmdTest)
        Me.Controls.Add(Me.cmdTestMissionGoals)
        Me.Controls.Add(Me.cmdTestCombatMap)
        Me.Controls.Add(Me.cmdTestSpaceObjects)
        Me.Controls.Add(Me.cmdTestFlightGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormTest"
        Me.Text = "Tests"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdTestFlightGroup As Button
    Friend WithEvents cmdTestSpaceObjects As Button
    Friend WithEvents cmdTestCombatMap As Button
    Friend WithEvents cmdTestMissionGoals As Button
    Friend WithEvents cmdTest As Button
    Friend WithEvents cmdTrainer As Button
    Friend WithEvents cmdModelBrowser As Button
End Class
