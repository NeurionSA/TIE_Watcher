<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpaceObjectSelector
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
        Me.cmbSpaceObject = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cmbSpaceObject
        '
        Me.cmbSpaceObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSpaceObject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSpaceObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpaceObject.FormattingEnabled = True
        Me.cmbSpaceObject.ItemHeight = 17
        Me.cmbSpaceObject.Location = New System.Drawing.Point(0, 0)
        Me.cmbSpaceObject.Name = "cmbSpaceObject"
        Me.cmbSpaceObject.Size = New System.Drawing.Size(150, 23)
        Me.cmbSpaceObject.TabIndex = 0
        '
        'SpaceObjectSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmbSpaceObject)
        Me.MaximumSize = New System.Drawing.Size(1024, 23)
        Me.MinimumSize = New System.Drawing.Size(0, 23)
        Me.Name = "SpaceObjectSelector"
        Me.Size = New System.Drawing.Size(150, 23)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents cmbSpaceObject As ComboBox
End Class
