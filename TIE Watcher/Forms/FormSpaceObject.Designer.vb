<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSpaceObject
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.grpSpaceObjectInfo = New System.Windows.Forms.GroupBox()
        Me.lblIFF = New System.Windows.Forms.Label()
        Me.lblIFF_Label = New System.Windows.Forms.Label()
        Me.lblPointer = New System.Windows.Forms.Label()
        Me.lblPointer_Label = New System.Windows.Forms.Label()
        Me.cmdMoreObjectInfo = New System.Windows.Forms.Button()
        Me.grpPosition = New System.Windows.Forms.GroupBox()
        Me.lblVelocity = New System.Windows.Forms.Label()
        Me.lblVelocity_Label = New System.Windows.Forms.Label()
        Me.lblRoll = New System.Windows.Forms.Label()
        Me.lblRoll_Label = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.lblPitch_Label = New System.Windows.Forms.Label()
        Me.lblYaw = New System.Windows.Forms.Label()
        Me.lblLastFrame_Label = New System.Windows.Forms.Label()
        Me.lblYaw_Label = New System.Windows.Forms.Label()
        Me.lblCurrent_Label = New System.Windows.Forms.Label()
        Me.lblZPositionLast = New System.Windows.Forms.Label()
        Me.lblYPositionLast = New System.Windows.Forms.Label()
        Me.lblXPositionLast = New System.Windows.Forms.Label()
        Me.lblZPosition = New System.Windows.Forms.Label()
        Me.lblZ_Label = New System.Windows.Forms.Label()
        Me.lblYPosition = New System.Windows.Forms.Label()
        Me.lblY_Label = New System.Windows.Forms.Label()
        Me.lblXPosition = New System.Windows.Forms.Label()
        Me.lblX_Label = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblType_Label = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblCategory_Label = New System.Windows.Forms.Label()
        Me.tvSpaceObjects = New System.Windows.Forms.TreeView()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.grpUnknownFields = New System.Windows.Forms.GroupBox()
        Me.dgvUnknownFields = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMatrixCellA = New System.Windows.Forms.Label()
        Me.lblMatrixCellB = New System.Windows.Forms.Label()
        Me.lblMatrixCellC = New System.Windows.Forms.Label()
        Me.lblMatrixCellP = New System.Windows.Forms.Label()
        Me.lblMatrixCellR = New System.Windows.Forms.Label()
        Me.lblMatrixCellQ = New System.Windows.Forms.Label()
        Me.lblMatrixCellW = New System.Windows.Forms.Label()
        Me.lblMatrixCellV = New System.Windows.Forms.Label()
        Me.lblMatrixCellU = New System.Windows.Forms.Label()
        Me.lblMatrixCell3 = New System.Windows.Forms.Label()
        Me.lblMatrixCell2 = New System.Windows.Forms.Label()
        Me.lblMatrixCell1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblCollisionDamage = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblParentIndex = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblParentType = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMarkings = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblFlightGroup = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblLifeTimer = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpSpaceObjectInfo.SuspendLayout()
        Me.grpPosition.SuspendLayout()
        Me.grpUnknownFields.SuspendLayout()
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(8, 8)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(64, 32)
        Me.cmdTest.TabIndex = 0
        Me.cmdTest.Text = "Test"
        Me.cmdTest.UseVisualStyleBackColor = True
        '
        'grpSpaceObjectInfo
        '
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblLifeTimer)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label8)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblFlightGroup)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label7)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblMarkings)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label5)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblParentType)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label6)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblParentIndex)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label4)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblAge)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label3)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblCollisionDamage)
        Me.grpSpaceObjectInfo.Controls.Add(Me.Label2)
        Me.grpSpaceObjectInfo.Controls.Add(Me.GroupBox1)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblIFF)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblIFF_Label)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblPointer)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblPointer_Label)
        Me.grpSpaceObjectInfo.Controls.Add(Me.cmdMoreObjectInfo)
        Me.grpSpaceObjectInfo.Controls.Add(Me.grpPosition)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblType)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblType_Label)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblCategory)
        Me.grpSpaceObjectInfo.Controls.Add(Me.lblCategory_Label)
        Me.grpSpaceObjectInfo.Location = New System.Drawing.Point(200, 8)
        Me.grpSpaceObjectInfo.Name = "grpSpaceObjectInfo"
        Me.grpSpaceObjectInfo.Size = New System.Drawing.Size(280, 472)
        Me.grpSpaceObjectInfo.TabIndex = 1
        Me.grpSpaceObjectInfo.TabStop = False
        Me.grpSpaceObjectInfo.Text = "Space Object Info"
        '
        'lblIFF
        '
        Me.lblIFF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIFF.Location = New System.Drawing.Point(40, 72)
        Me.lblIFF.Name = "lblIFF"
        Me.lblIFF.Size = New System.Drawing.Size(80, 16)
        Me.lblIFF.TabIndex = 11
        Me.lblIFF.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIFF_Label
        '
        Me.lblIFF_Label.AutoSize = True
        Me.lblIFF_Label.Location = New System.Drawing.Point(8, 72)
        Me.lblIFF_Label.Name = "lblIFF_Label"
        Me.lblIFF_Label.Size = New System.Drawing.Size(25, 13)
        Me.lblIFF_Label.TabIndex = 10
        Me.lblIFF_Label.Text = "IFF:"
        '
        'lblPointer
        '
        Me.lblPointer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPointer.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPointer.Location = New System.Drawing.Point(172, 48)
        Me.lblPointer.Name = "lblPointer"
        Me.lblPointer.Size = New System.Drawing.Size(72, 16)
        Me.lblPointer.TabIndex = 9
        Me.lblPointer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPointer_Label
        '
        Me.lblPointer_Label.AutoSize = True
        Me.lblPointer_Label.Location = New System.Drawing.Point(128, 48)
        Me.lblPointer_Label.Name = "lblPointer_Label"
        Me.lblPointer_Label.Size = New System.Drawing.Size(43, 13)
        Me.lblPointer_Label.TabIndex = 8
        Me.lblPointer_Label.Text = "Pointer:"
        '
        'cmdMoreObjectInfo
        '
        Me.cmdMoreObjectInfo.Location = New System.Drawing.Point(248, 44)
        Me.cmdMoreObjectInfo.Name = "cmdMoreObjectInfo"
        Me.cmdMoreObjectInfo.Size = New System.Drawing.Size(24, 24)
        Me.cmdMoreObjectInfo.TabIndex = 7
        Me.cmdMoreObjectInfo.Text = "->"
        Me.cmdMoreObjectInfo.UseVisualStyleBackColor = True
        '
        'grpPosition
        '
        Me.grpPosition.Controls.Add(Me.lblVelocity)
        Me.grpPosition.Controls.Add(Me.lblVelocity_Label)
        Me.grpPosition.Controls.Add(Me.lblRoll)
        Me.grpPosition.Controls.Add(Me.lblRoll_Label)
        Me.grpPosition.Controls.Add(Me.lblPitch)
        Me.grpPosition.Controls.Add(Me.lblPitch_Label)
        Me.grpPosition.Controls.Add(Me.lblYaw)
        Me.grpPosition.Controls.Add(Me.lblLastFrame_Label)
        Me.grpPosition.Controls.Add(Me.lblYaw_Label)
        Me.grpPosition.Controls.Add(Me.lblCurrent_Label)
        Me.grpPosition.Controls.Add(Me.lblZPositionLast)
        Me.grpPosition.Controls.Add(Me.lblYPositionLast)
        Me.grpPosition.Controls.Add(Me.lblXPositionLast)
        Me.grpPosition.Controls.Add(Me.lblZPosition)
        Me.grpPosition.Controls.Add(Me.lblZ_Label)
        Me.grpPosition.Controls.Add(Me.lblYPosition)
        Me.grpPosition.Controls.Add(Me.lblY_Label)
        Me.grpPosition.Controls.Add(Me.lblXPosition)
        Me.grpPosition.Controls.Add(Me.lblX_Label)
        Me.grpPosition.Location = New System.Drawing.Point(8, 104)
        Me.grpPosition.Name = "grpPosition"
        Me.grpPosition.Size = New System.Drawing.Size(256, 136)
        Me.grpPosition.TabIndex = 6
        Me.grpPosition.TabStop = False
        Me.grpPosition.Text = "Position"
        '
        'lblVelocity
        '
        Me.lblVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVelocity.Location = New System.Drawing.Point(56, 112)
        Me.lblVelocity.Name = "lblVelocity"
        Me.lblVelocity.Size = New System.Drawing.Size(40, 16)
        Me.lblVelocity.TabIndex = 19
        Me.lblVelocity.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVelocity_Label
        '
        Me.lblVelocity_Label.AutoSize = True
        Me.lblVelocity_Label.Location = New System.Drawing.Point(8, 112)
        Me.lblVelocity_Label.Name = "lblVelocity_Label"
        Me.lblVelocity_Label.Size = New System.Drawing.Size(47, 13)
        Me.lblVelocity_Label.TabIndex = 18
        Me.lblVelocity_Label.Text = "Velocity:"
        '
        'lblRoll
        '
        Me.lblRoll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRoll.Location = New System.Drawing.Point(200, 88)
        Me.lblRoll.Name = "lblRoll"
        Me.lblRoll.Size = New System.Drawing.Size(48, 16)
        Me.lblRoll.TabIndex = 17
        Me.lblRoll.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRoll_Label
        '
        Me.lblRoll_Label.AutoSize = True
        Me.lblRoll_Label.Location = New System.Drawing.Point(172, 88)
        Me.lblRoll_Label.Name = "lblRoll_Label"
        Me.lblRoll_Label.Size = New System.Drawing.Size(28, 13)
        Me.lblRoll_Label.TabIndex = 16
        Me.lblRoll_Label.Text = "Roll:"
        '
        'lblPitch
        '
        Me.lblPitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPitch.Location = New System.Drawing.Point(120, 88)
        Me.lblPitch.Name = "lblPitch"
        Me.lblPitch.Size = New System.Drawing.Size(48, 16)
        Me.lblPitch.TabIndex = 15
        Me.lblPitch.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPitch_Label
        '
        Me.lblPitch_Label.AutoSize = True
        Me.lblPitch_Label.Location = New System.Drawing.Point(88, 88)
        Me.lblPitch_Label.Name = "lblPitch_Label"
        Me.lblPitch_Label.Size = New System.Drawing.Size(34, 13)
        Me.lblPitch_Label.TabIndex = 14
        Me.lblPitch_Label.Text = "Pitch:"
        '
        'lblYaw
        '
        Me.lblYaw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYaw.Location = New System.Drawing.Point(40, 88)
        Me.lblYaw.Name = "lblYaw"
        Me.lblYaw.Size = New System.Drawing.Size(48, 16)
        Me.lblYaw.TabIndex = 8
        Me.lblYaw.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLastFrame_Label
        '
        Me.lblLastFrame_Label.Location = New System.Drawing.Point(136, 16)
        Me.lblLastFrame_Label.Name = "lblLastFrame_Label"
        Me.lblLastFrame_Label.Size = New System.Drawing.Size(112, 16)
        Me.lblLastFrame_Label.TabIndex = 13
        Me.lblLastFrame_Label.Text = "Last Frame"
        Me.lblLastFrame_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblYaw_Label
        '
        Me.lblYaw_Label.AutoSize = True
        Me.lblYaw_Label.Location = New System.Drawing.Point(8, 88)
        Me.lblYaw_Label.Name = "lblYaw_Label"
        Me.lblYaw_Label.Size = New System.Drawing.Size(31, 13)
        Me.lblYaw_Label.TabIndex = 7
        Me.lblYaw_Label.Text = "Yaw:"
        '
        'lblCurrent_Label
        '
        Me.lblCurrent_Label.Location = New System.Drawing.Point(24, 16)
        Me.lblCurrent_Label.Name = "lblCurrent_Label"
        Me.lblCurrent_Label.Size = New System.Drawing.Size(112, 16)
        Me.lblCurrent_Label.TabIndex = 7
        Me.lblCurrent_Label.Text = "Current"
        Me.lblCurrent_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblZPositionLast
        '
        Me.lblZPositionLast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblZPositionLast.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZPositionLast.Location = New System.Drawing.Point(136, 64)
        Me.lblZPositionLast.Name = "lblZPositionLast"
        Me.lblZPositionLast.Size = New System.Drawing.Size(112, 16)
        Me.lblZPositionLast.TabIndex = 12
        Me.lblZPositionLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblYPositionLast
        '
        Me.lblYPositionLast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYPositionLast.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYPositionLast.Location = New System.Drawing.Point(136, 48)
        Me.lblYPositionLast.Name = "lblYPositionLast"
        Me.lblYPositionLast.Size = New System.Drawing.Size(112, 16)
        Me.lblYPositionLast.TabIndex = 11
        Me.lblYPositionLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblXPositionLast
        '
        Me.lblXPositionLast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblXPositionLast.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXPositionLast.Location = New System.Drawing.Point(136, 32)
        Me.lblXPositionLast.Name = "lblXPositionLast"
        Me.lblXPositionLast.Size = New System.Drawing.Size(112, 16)
        Me.lblXPositionLast.TabIndex = 10
        Me.lblXPositionLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZPosition
        '
        Me.lblZPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblZPosition.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZPosition.Location = New System.Drawing.Point(24, 64)
        Me.lblZPosition.Name = "lblZPosition"
        Me.lblZPosition.Size = New System.Drawing.Size(112, 16)
        Me.lblZPosition.TabIndex = 9
        Me.lblZPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZ_Label
        '
        Me.lblZ_Label.AutoSize = True
        Me.lblZ_Label.Location = New System.Drawing.Point(8, 64)
        Me.lblZ_Label.Name = "lblZ_Label"
        Me.lblZ_Label.Size = New System.Drawing.Size(17, 13)
        Me.lblZ_Label.TabIndex = 8
        Me.lblZ_Label.Text = "Z:"
        '
        'lblYPosition
        '
        Me.lblYPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYPosition.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYPosition.Location = New System.Drawing.Point(24, 48)
        Me.lblYPosition.Name = "lblYPosition"
        Me.lblYPosition.Size = New System.Drawing.Size(112, 16)
        Me.lblYPosition.TabIndex = 7
        Me.lblYPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblY_Label
        '
        Me.lblY_Label.AutoSize = True
        Me.lblY_Label.Location = New System.Drawing.Point(8, 48)
        Me.lblY_Label.Name = "lblY_Label"
        Me.lblY_Label.Size = New System.Drawing.Size(17, 13)
        Me.lblY_Label.TabIndex = 6
        Me.lblY_Label.Text = "Y:"
        '
        'lblXPosition
        '
        Me.lblXPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblXPosition.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXPosition.Location = New System.Drawing.Point(24, 32)
        Me.lblXPosition.Name = "lblXPosition"
        Me.lblXPosition.Size = New System.Drawing.Size(112, 16)
        Me.lblXPosition.TabIndex = 5
        Me.lblXPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblX_Label
        '
        Me.lblX_Label.AutoSize = True
        Me.lblX_Label.Location = New System.Drawing.Point(8, 32)
        Me.lblX_Label.Name = "lblX_Label"
        Me.lblX_Label.Size = New System.Drawing.Size(17, 13)
        Me.lblX_Label.TabIndex = 4
        Me.lblX_Label.Text = "X:"
        '
        'lblType
        '
        Me.lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblType.Location = New System.Drawing.Point(44, 24)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(144, 16)
        Me.lblType.TabIndex = 3
        '
        'lblType_Label
        '
        Me.lblType_Label.AutoSize = True
        Me.lblType_Label.Location = New System.Drawing.Point(8, 24)
        Me.lblType_Label.Name = "lblType_Label"
        Me.lblType_Label.Size = New System.Drawing.Size(34, 13)
        Me.lblType_Label.TabIndex = 2
        Me.lblType_Label.Text = "Type:"
        '
        'lblCategory
        '
        Me.lblCategory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCategory.Location = New System.Drawing.Point(64, 48)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(56, 16)
        Me.lblCategory.TabIndex = 1
        Me.lblCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCategory_Label
        '
        Me.lblCategory_Label.AutoSize = True
        Me.lblCategory_Label.Location = New System.Drawing.Point(8, 48)
        Me.lblCategory_Label.Name = "lblCategory_Label"
        Me.lblCategory_Label.Size = New System.Drawing.Size(52, 13)
        Me.lblCategory_Label.TabIndex = 0
        Me.lblCategory_Label.Text = "Category:"
        '
        'tvSpaceObjects
        '
        Me.tvSpaceObjects.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.tvSpaceObjects.HideSelection = False
        Me.tvSpaceObjects.ItemHeight = 18
        Me.tvSpaceObjects.Location = New System.Drawing.Point(8, 48)
        Me.tvSpaceObjects.Name = "tvSpaceObjects"
        Me.tvSpaceObjects.Size = New System.Drawing.Size(184, 544)
        Me.tvSpaceObjects.TabIndex = 2
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 50
        '
        'grpUnknownFields
        '
        Me.grpUnknownFields.Controls.Add(Me.dgvUnknownFields)
        Me.grpUnknownFields.Location = New System.Drawing.Point(488, 8)
        Me.grpUnknownFields.Name = "grpUnknownFields"
        Me.grpUnknownFields.Size = New System.Drawing.Size(152, 592)
        Me.grpUnknownFields.TabIndex = 17
        Me.grpUnknownFields.TabStop = False
        Me.grpUnknownFields.Text = "Unknown Fields"
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
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnknownFields.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvUnknownFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnknownFields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.dgvUnknownFields.Location = New System.Drawing.Point(8, 16)
        Me.dgvUnknownFields.MultiSelect = False
        Me.dgvUnknownFields.Name = "dgvUnknownFields"
        Me.dgvUnknownFields.ReadOnly = True
        Me.dgvUnknownFields.RowHeadersVisible = False
        Me.dgvUnknownFields.RowTemplate.Height = 16
        Me.dgvUnknownFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvUnknownFields.Size = New System.Drawing.Size(136, 568)
        Me.dgvUnknownFields.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle17
        Me.DataGridViewTextBoxColumn1.HeaderText = "Offset"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 48
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle18
        Me.DataGridViewTextBoxColumn2.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCell3)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCell2)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCell1)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellW)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellV)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellU)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellP)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellR)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellC)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellB)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellQ)
        Me.GroupBox1.Controls.Add(Me.lblMatrixCellA)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 344)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 120)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transformation Matrix"
        '
        'lblMatrixCellA
        '
        Me.lblMatrixCellA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellA.Location = New System.Drawing.Point(8, 24)
        Me.lblMatrixCellA.Name = "lblMatrixCellA"
        Me.lblMatrixCellA.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellA.TabIndex = 12
        Me.lblMatrixCellA.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellB
        '
        Me.lblMatrixCellB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellB.Location = New System.Drawing.Point(88, 24)
        Me.lblMatrixCellB.Name = "lblMatrixCellB"
        Me.lblMatrixCellB.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellB.TabIndex = 13
        Me.lblMatrixCellB.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellC
        '
        Me.lblMatrixCellC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellC.Location = New System.Drawing.Point(168, 24)
        Me.lblMatrixCellC.Name = "lblMatrixCellC"
        Me.lblMatrixCellC.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellC.TabIndex = 14
        Me.lblMatrixCellC.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellP
        '
        Me.lblMatrixCellP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellP.Location = New System.Drawing.Point(8, 40)
        Me.lblMatrixCellP.Name = "lblMatrixCellP"
        Me.lblMatrixCellP.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellP.TabIndex = 17
        Me.lblMatrixCellP.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellR
        '
        Me.lblMatrixCellR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellR.Location = New System.Drawing.Point(168, 40)
        Me.lblMatrixCellR.Name = "lblMatrixCellR"
        Me.lblMatrixCellR.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellR.TabIndex = 16
        Me.lblMatrixCellR.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellQ
        '
        Me.lblMatrixCellQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellQ.Location = New System.Drawing.Point(88, 40)
        Me.lblMatrixCellQ.Name = "lblMatrixCellQ"
        Me.lblMatrixCellQ.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellQ.TabIndex = 15
        Me.lblMatrixCellQ.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellW
        '
        Me.lblMatrixCellW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellW.Location = New System.Drawing.Point(168, 56)
        Me.lblMatrixCellW.Name = "lblMatrixCellW"
        Me.lblMatrixCellW.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellW.TabIndex = 20
        Me.lblMatrixCellW.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellV
        '
        Me.lblMatrixCellV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellV.Location = New System.Drawing.Point(88, 56)
        Me.lblMatrixCellV.Name = "lblMatrixCellV"
        Me.lblMatrixCellV.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellV.TabIndex = 19
        Me.lblMatrixCellV.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCellU
        '
        Me.lblMatrixCellU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCellU.Location = New System.Drawing.Point(8, 56)
        Me.lblMatrixCellU.Name = "lblMatrixCellU"
        Me.lblMatrixCellU.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCellU.TabIndex = 18
        Me.lblMatrixCellU.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCell3
        '
        Me.lblMatrixCell3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCell3.Location = New System.Drawing.Point(168, 96)
        Me.lblMatrixCell3.Name = "lblMatrixCell3"
        Me.lblMatrixCell3.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCell3.TabIndex = 23
        Me.lblMatrixCell3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCell2
        '
        Me.lblMatrixCell2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCell2.Location = New System.Drawing.Point(88, 96)
        Me.lblMatrixCell2.Name = "lblMatrixCell2"
        Me.lblMatrixCell2.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCell2.TabIndex = 22
        Me.lblMatrixCell2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMatrixCell1
        '
        Me.lblMatrixCell1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMatrixCell1.Location = New System.Drawing.Point(8, 96)
        Me.lblMatrixCell1.Name = "lblMatrixCell1"
        Me.lblMatrixCell1.Size = New System.Drawing.Size(80, 16)
        Me.lblMatrixCell1.TabIndex = 21
        Me.lblMatrixCell1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(125, 13)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Yaw-Pitch Matrix Subset:"
        '
        'lblCollisionDamage
        '
        Me.lblCollisionDamage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollisionDamage.Location = New System.Drawing.Point(104, 248)
        Me.lblCollisionDamage.Name = "lblCollisionDamage"
        Me.lblCollisionDamage.Size = New System.Drawing.Size(56, 16)
        Me.lblCollisionDamage.TabIndex = 14
        Me.lblCollisionDamage.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 248)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Collision Damage:"
        '
        'lblAge
        '
        Me.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAge.Location = New System.Drawing.Point(208, 248)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(56, 16)
        Me.lblAge.TabIndex = 16
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(176, 248)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Age:"
        '
        'lblParentIndex
        '
        Me.lblParentIndex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblParentIndex.Location = New System.Drawing.Point(80, 296)
        Me.lblParentIndex.Name = "lblParentIndex"
        Me.lblParentIndex.Size = New System.Drawing.Size(40, 16)
        Me.lblParentIndex.TabIndex = 18
        Me.lblParentIndex.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 296)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Parent Index:"
        '
        'lblParentType
        '
        Me.lblParentType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblParentType.Location = New System.Drawing.Point(80, 320)
        Me.lblParentType.Name = "lblParentType"
        Me.lblParentType.Size = New System.Drawing.Size(144, 16)
        Me.lblParentType.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 320)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Parent Type:"
        '
        'lblMarkings
        '
        Me.lblMarkings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMarkings.Location = New System.Drawing.Point(64, 272)
        Me.lblMarkings.Name = "lblMarkings"
        Me.lblMarkings.Size = New System.Drawing.Size(56, 16)
        Me.lblMarkings.TabIndex = 22
        Me.lblMarkings.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 272)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Markings:"
        '
        'lblFlightGroup
        '
        Me.lblFlightGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFlightGroup.Location = New System.Drawing.Point(192, 296)
        Me.lblFlightGroup.Name = "lblFlightGroup"
        Me.lblFlightGroup.Size = New System.Drawing.Size(72, 16)
        Me.lblFlightGroup.TabIndex = 24
        Me.lblFlightGroup.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(124, 296)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Flight Group:"
        '
        'lblLifeTimer
        '
        Me.lblLifeTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLifeTimer.Location = New System.Drawing.Point(208, 272)
        Me.lblLifeTimer.Name = "lblLifeTimer"
        Me.lblLifeTimer.Size = New System.Drawing.Size(56, 16)
        Me.lblLifeTimer.TabIndex = 26
        Me.lblLifeTimer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(132, 272)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Lifetime Timer:"
        '
        'FormSpaceObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 609)
        Me.Controls.Add(Me.grpUnknownFields)
        Me.Controls.Add(Me.tvSpaceObjects)
        Me.Controls.Add(Me.grpSpaceObjectInfo)
        Me.Controls.Add(Me.cmdTest)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormSpaceObject"
        Me.Text = "FormSpaceObject"
        Me.grpSpaceObjectInfo.ResumeLayout(False)
        Me.grpSpaceObjectInfo.PerformLayout()
        Me.grpPosition.ResumeLayout(False)
        Me.grpPosition.PerformLayout()
        Me.grpUnknownFields.ResumeLayout(False)
        CType(Me.dgvUnknownFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdTest As Button
    Friend WithEvents grpSpaceObjectInfo As GroupBox
    Friend WithEvents tvSpaceObjects As TreeView
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblCategory_Label As Label
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents lblType As Label
    Friend WithEvents lblType_Label As Label
    Friend WithEvents grpPosition As GroupBox
    Friend WithEvents lblZPosition As Label
    Friend WithEvents lblZ_Label As Label
    Friend WithEvents lblYPosition As Label
    Friend WithEvents lblY_Label As Label
    Friend WithEvents lblXPosition As Label
    Friend WithEvents lblX_Label As Label
    Friend WithEvents lblLastFrame_Label As Label
    Friend WithEvents lblCurrent_Label As Label
    Friend WithEvents lblZPositionLast As Label
    Friend WithEvents lblYPositionLast As Label
    Friend WithEvents lblXPositionLast As Label
    Friend WithEvents lblVelocity As Label
    Friend WithEvents lblVelocity_Label As Label
    Friend WithEvents lblRoll As Label
    Friend WithEvents lblRoll_Label As Label
    Friend WithEvents lblPitch As Label
    Friend WithEvents lblPitch_Label As Label
    Friend WithEvents lblYaw As Label
    Friend WithEvents lblYaw_Label As Label
    Friend WithEvents cmdMoreObjectInfo As Button
    Friend WithEvents lblPointer As Label
    Friend WithEvents lblPointer_Label As Label
    Friend WithEvents lblIFF As Label
    Friend WithEvents lblIFF_Label As Label
    Friend WithEvents grpUnknownFields As GroupBox
    Friend WithEvents dgvUnknownFields As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents lblMatrixCell3 As Label
    Friend WithEvents lblMatrixCell2 As Label
    Friend WithEvents lblMatrixCell1 As Label
    Friend WithEvents lblMatrixCellW As Label
    Friend WithEvents lblMatrixCellV As Label
    Friend WithEvents lblMatrixCellU As Label
    Friend WithEvents lblMatrixCellP As Label
    Friend WithEvents lblMatrixCellR As Label
    Friend WithEvents lblMatrixCellQ As Label
    Friend WithEvents lblMatrixCellC As Label
    Friend WithEvents lblMatrixCellB As Label
    Friend WithEvents lblMatrixCellA As Label
    Friend WithEvents lblCollisionDamage As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblAge As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblParentType As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblParentIndex As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblFlightGroup As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblMarkings As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblLifeTimer As Label
    Friend WithEvents Label8 As Label
End Class
