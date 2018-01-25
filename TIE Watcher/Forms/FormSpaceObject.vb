Imports TIE_Watcher.TIE

Public Class FormSpaceObject

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "{0:X4}"

#End Region
#Region " --- Variables --- "

    ' Craft info form we refer to when viewing a craft's information
    Private frmCraftInfo As New FormCraftInfo

    ' data grid view cells for unknown fields
    Private cell_Field_0,
        cell_Field_2,
        cell_Field_5,
        cell_Field_24,
        cell_Field_28,
        cell_Field_36,
        cell_Field_39,
        cell_Field_41 As DataGridViewCell

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cell_Field_0 = addUnknownField("0")
        cell_Field_2 = addUnknownField("2")
        cell_Field_5 = addUnknownField("5")
        cell_Field_24 = addUnknownField("24")
        cell_Field_28 = addUnknownField("28")
        cell_Field_36 = addUnknownField("36")
        cell_Field_39 = addUnknownField("39")
        cell_Field_41 = addUnknownField("41")
    End Sub

#End Region

    ' adds a new row with an offset label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Private Function matrixCellString(value As Int16) As String
        If value > 0 Then
            Return String.Format("{0:0.00000}", value / 32767)
        Else
            Return String.Format("{0:0.00000}", value / 32768)
        End If
    End Function

    ' updates all of the form elements for the specified SpaceObject index
    Private Sub updateSpaceObject(index As Integer)
        Dim so As SpaceObject = TIEServices.SpaceObjects(index)

        Select Case so.Category
            Case SpaceObjectCategory.Capital,
                 SpaceObjectCategory.Fighter,
                 SpaceObjectCategory.Freight,
                 SpaceObjectCategory.Mine,
                 SpaceObjectCategory.Platform,
                 SpaceObjectCategory.Probe,
                 SpaceObjectCategory.Transport,
                 SpaceObjectCategory.Utility
                lblType.Text = SpaceObjectTypeInfo.Types(so.ObjectType).Name
            Case Else
                lblType.Text = "?"
        End Select

        lblCategory.Text = so.Category.ToString
        lblXPosition.Text = String.Format("{0:#,0}", so.XPosition)
        lblYPosition.Text = String.Format("{0:#,0}", so.YPosition)
        lblZPosition.Text = String.Format("{0:#,0}", so.ZPosition)
        lblXPositionLast.Text = String.Format("{0:#,0}", so.XPositionLast)
        lblYPositionLast.Text = String.Format("{0:#,0}", so.YPositionLast)
        lblZPositionLast.Text = String.Format("{0:#,0}", so.ZPositionLast)
        lblYaw.Text = String.Format("{0:0.00}", so.Yaw / 182.044)
        lblPitch.Text = String.Format("{0:0.00}", so.Pitch / 182.044)
        lblRoll.Text = String.Format("{0:0.00}", so.Roll / 182.044)
        lblVelocity.Text = so.Velocity
        lblPointer.Text = String.Format("0x{0:X}", so.ObjectPointer)
        ' IFFs in-game can be bad sometimes, so handle that situation
        If so.IFF < 6 Then
            lblIFF.Text = TIEServices.IFFName(so.IFF)
        Else
            lblIFF.Text = so.IFF
        End If

        lblCollisionDamage.Text = so.CollisionDamage
        lblLifeTimer.Text = so.DestructionTimer
        lblAge.Text = so.Age
        lblParentIndex.Text = String.Format(UNKNOWN_WORD_FORMAT, so.ParentIndex)
        lblParentType.Text = SpaceObjectTypeInfo.Types(so.ParentType).Name
        lblMarkings.Text = so.Markings
        lblFlightGroup.Text = so.FlightGroupIndex

        ' update transformation matrix
        lblMatrixCell1.Text = matrixCellString(so.Field_3C)
        lblMatrixCell2.Text = matrixCellString(so.Field_3A)
        lblMatrixCell3.Text = matrixCellString(so.Field_3E)

        lblMatrixCellA.Text = matrixCellString(so.Field_44)
        lblMatrixCellB.Text = matrixCellString(so.Field_4A)
        lblMatrixCellC.Text = matrixCellString(so.Field_50)
        lblMatrixCellP.Text = matrixCellString(so.Field_42)
        lblMatrixCellQ.Text = matrixCellString(so.Field_48)
        lblMatrixCellR.Text = matrixCellString(so.Field_4E)
        lblMatrixCellU.Text = matrixCellString(so.Field_46)
        lblMatrixCellV.Text = matrixCellString(so.Field_4C)
        lblMatrixCellW.Text = matrixCellString(so.Field_52)

        ' handle unknown fields
        cell_Field_0.Value = String.Format(UNKNOWN_WORD_FORMAT, so.Field_0)
        cell_Field_2.Value = String.Format(UNKNOWN_BYTE_FORMAT, so.Field_2)
        cell_Field_5.Value = String.Format(UNKNOWN_BYTE_FORMAT, so.Field_5)
        cell_Field_24.Value = String.Format(UNKNOWN_WORD_FORMAT, so.Field_24)
        cell_Field_28.Value = String.Format(UNKNOWN_WORD_FORMAT, so.Field_28)
        cell_Field_36.Value = String.Format(UNKNOWN_WORD_FORMAT, so.Field_36)
        cell_Field_39.Value = String.Format(UNKNOWN_BYTE_FORMAT, so.Field_39)
        cell_Field_41.Value = String.Format(UNKNOWN_BYTE_FORMAT, so.Field_41)

        ' update the corresponding sub-forms if they're visible
        If frmCraftInfo.Visible Then
            ' only update the Craft form if we're viewing a craft
            Select Case so.Category
                Case SpaceObjectCategory.Capital,
                     SpaceObjectCategory.Fighter,
                     SpaceObjectCategory.Freight,
                     SpaceObjectCategory.Platform,
                     SpaceObjectCategory.Transport,
                     SpaceObjectCategory.Utility

                    ' and only if the craft is not null
                    If so.ObjectType <> 0 Then frmCraftInfo.UpdateDisplay(so, TIEServices.CraftObject(so))

                Case Else
                    ' do nothing
            End Select
        End If

    End Sub

    ' updates an individual tree node, recursively
    Private Sub updateTreeNode(node As TreeNode)
        Dim tag As SpaceObjectTreeNodeTag = node.Tag

        ' update the node's information as necessary
        Select Case tag.NodeType
            Case SpaceObjectTreeNodeType.Item
                Dim redrawNode As Boolean = False   ' whether or not the node will need to be redrawn
                Dim so As SpaceObject = TIEServices.SpaceObjects(tag.Index)

                ' update node information as necessary, comparing old data with new data
                If tag.IFF <> so.IFF Then
                    tag.IFF = so.IFF
                    redrawNode = True
                End If
                If tag.Category <> so.Category Then
                    tag.Category = so.Category
                    redrawNode = True
                End If
                If tag.ObjectType <> so.ObjectType Then
                    tag.ObjectType = so.ObjectType
                    ' object type change necessitates changing the text
                    tag.Text = TIEServices.SpaceObjectDescription(tag.Index)
                    redrawNode = True
                End If

                ' invalidate the node if it needs to be redrawn
                'If redrawNode Then node.TreeView.Invalidate(Rectangle.Inflate(node.Bounds, -4, -4))
                If redrawNode Then
                    ' invalidate a region that is the bounds of the node, extended 'all the way' to the right
                    Dim bounds As Rectangle = node.Bounds
                    bounds.Width += 1000
                    node.TreeView.Invalidate(bounds)
                End If
        End Select

        ' update all the children nodes, if there are any
        For Each child As TreeNode In node.Nodes
            updateTreeNode(child)
        Next
    End Sub

    ' updates the treeview's node information, yeowch
    Private Sub updateAllTreeNodes()
        ' iterate through all the nodes in the tree, updating each one

        For Each node As TreeNode In tvSpaceObjects.Nodes
            updateTreeNode(node)
        Next
    End Sub

#Region " Event-Handlers "

#Region " SpaceObjects TreeView "

    ' returns the bounds of the specified node, including the region occupied by extra
    ' tag-related data (i.e. icons, etc)
    Private Function NodeBounds(node As TreeNode) As Rectangle
        Dim bounds As Rectangle = node.Bounds
        Dim tag As SpaceObjectTreeNodeTag = node.Tag

        If tag IsNot Nothing Then
            ' expand the bounds to include the text
            Dim g As Graphics = node.TreeView.CreateGraphics

            bounds.Width += CInt(g.MeasureString(tag.Text, node.TreeView.Font).Width)

            ' dispose of the graphics object
            g.Dispose()
            ' handle the node type
            Select Case tag.NodeType
                Case SpaceObjectTreeNodeType.Item
                    ' expand to include an icon
                    bounds.Width += 19
            End Select
        End If

        Return bounds
    End Function

    Private Sub tvSpaceObjects_DrawNode(sender As Object, e As DrawTreeNodeEventArgs) Handles tvSpaceObjects.DrawNode

        Dim tv As TreeView = DirectCast(sender, TreeView)

        ' abort drawing if the node's bounds would render it invisible
        If e.Bounds.Width <= 0 Or e.Bounds.Height <= 0 Then Exit Sub

        Dim bounds As Rectangle = NodeBounds(e.Node)
        Dim hPosition As Integer = bounds.Left  ' horizontal position to draw things; changes as we draw items
        Dim vCenter As Integer = (bounds.Height >> 1) + e.Bounds.Top
        Dim textSize As SizeF
        Dim textBrush As Brush
        ' get the tag from the node
        Dim tag As SpaceObjectTreeNodeTag = DirectCast(e.Node.Tag, SpaceObjectTreeNodeTag)

        ' override the base highlight behaviour if the node is selected
        If ((e.State And TreeNodeStates.Selected) <> 0) Then
            ' node is selected, draw the highlight rectangle
            e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds)
            ' set the string's brush color
            textBrush = SystemBrushes.HighlightText
        Else
            ' node is not selected, draw the control background
            Dim bgBrush As New SolidBrush(tv.BackColor)
            e.Graphics.FillRectangle(bgBrush, bounds)
            bgBrush.Dispose()
            textBrush = SystemBrushes.ControlText
        End If

        ' calculate the size of the text
        textSize = e.Graphics.MeasureString(tag.Text, tv.Font)

        ' handle the node type
        Select Case tag.NodeType
            Case SpaceObjectTreeNodeType.Item
                ' handle if the object exists or not
                If tag.ObjectType = 0 Then
                    ' it doesn't exist, draw something generic
                    ' draw the icon box
                    Dim icon As Bitmap = TIEServices.CraftIconBox(2)
                    hPosition += 1

                    e.Graphics.DrawImage(icon, hPosition, vCenter - (icon.Height >> 1))
                    ' draw a 'blank' craft icon
                    icon = TIEServices.CraftIcon(0, 2)
                    e.Graphics.DrawImage(icon, hPosition, vCenter - (icon.Height >> 1))

                Else
                    ' handle the SpaceObject category of the node
                    Select Case tag.Category
                        Case SpaceObjectCategory.Fighter,
                             SpaceObjectCategory.Capital,
                             SpaceObjectCategory.Freight,
                             SpaceObjectCategory.Platform,
                             SpaceObjectCategory.Transport,
                             SpaceObjectCategory.Utility

                            ' draw the icon box
                            Dim icon As Bitmap = TIEServices.CraftIconBox(tag.IFF)
                            hPosition += 1

                            e.Graphics.DrawImage(icon, hPosition, vCenter - (icon.Height >> 1))
                            ' draw the craft icon
                            ' FIX: Because the data read from DOSBox isn't atomic (sadly), I still need to perform extra sanity checks
                            ' if the objecttype is invalid (most often seen when craft blow up) draw it as if it didn't exist
                            If tag.ObjectType >= 86 Then
                                ' generic icon
                                icon = TIEServices.CraftIcon(0, tag.IFF)
                            Else
                                ' craft icon
                                icon = TIEServices.CraftIcon(tag.ObjectType, tag.IFF)
                            End If

                            e.Graphics.DrawImage(icon, hPosition, vCenter - (icon.Height >> 1))

                    End Select

                End If

                ' increment the position
                hPosition += 17
        End Select

        ' draw the node's string
        e.Graphics.DrawString(tag.Text, tv.Font, textBrush, hPosition, vCenter - (CInt(textSize.Height) >> 1))

        ' draw the focus rectangle if the node's got focus
        If (e.State And TreeNodeStates.Focused) <> 0 Then
            Dim focusPen As New Pen(Color.Black)

            focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            e.Graphics.DrawRectangle(focusPen, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1)

            focusPen.Dispose()
        End If

        'Console.WriteLine(String.Format("redrew node index {0}", tag.Index))
    End Sub

    Private Sub tvSpaceObjects_MouseDown(sender As Object, e As MouseEventArgs) Handles tvSpaceObjects.MouseDown
        Dim tv As TreeView = DirectCast(sender, TreeView)
        Dim clickedNode As TreeNode = tv.GetNodeAt(e.X, e.Y)

        ' check if the click event occured in the node's bounds
        If NodeBounds(clickedNode).Contains(e.X, e.Y) Then
            tv.SelectedNode = clickedNode
        End If
    End Sub

    Private Sub tvSpaceObjects_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvSpaceObjects.AfterSelect
        ' determine what to do based on the selected node's tag
        Dim tag As SpaceObjectTreeNodeTag = DirectCast(e.Node.Tag, SpaceObjectTreeNodeTag)

        Select Case tag.NodeType
            Case SpaceObjectTreeNodeType.Item
                ' update display information based on the selected node
                updateSpaceObject(tag.Index)

        End Select
    End Sub

#End Region

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        ' determine what to do based on the selected node's tag
        Dim tag As SpaceObjectTreeNodeTag = DirectCast(tvSpaceObjects.SelectedNode.Tag, SpaceObjectTreeNodeTag)

        ' update all the nodes' information (hoo doggy)
        updateAllTreeNodes()

        Select Case tag.NodeType
            Case SpaceObjectTreeNodeType.Item
                ' update display information based on the selected node
                updateSpaceObject(tag.Index)

        End Select
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        If TIEServices.Process Is Nothing Then
            ' find a dosbox process
            For Each proc As Process In Process.GetProcesses
                ' if the first 6 chars match then we'll try that one
                If Strings.Left(proc.ProcessName, 6).ToLower.Equals("dosbox") Then
                    TIEServices.OpenProcess(proc)
                    Exit For
                End If
            Next 'proc
        End If

        Dim so As SpaceObject
        Dim tag As SpaceObjectTreeNodeTag

        tvSpaceObjects.Nodes.Clear()

        ' create all the nodes in the tree
        ' NOTE: In spite of the fact that I'm drawing my own text, each node will have to have dummy text,
        ' otherwise the node drawing will not work correctly.
        ' start with the Craft node, for SpaceObject indices 0 to 31
        Dim parentNode As TreeNode
        ' NOTE: Because the scrollbars of a TreeView depend only on the Text property of a node, I am forced to
        ' use long dummy text as a dirty fix to force scrollbars to show up
        parentNode = New TreeNode("dummy dummy dummy dummy dummy dummy")

        ' create the tag info for the node and assign it
        tag = New SpaceObjectTreeNodeTag(SpaceObjectTreeNodeType.Category)
        tag.Text = "Craft"
        parentNode.Tag = tag
        For i As Integer = 0 To 31
            Dim childNode As TreeNode
            ' get the spaceobject
            so = TIEServices.SpaceObjects(i)
            ' create the child node
            childNode = New TreeNode("dummy")
            ' create the tag info for the node and assign it
            tag = New SpaceObjectTreeNodeTag(SpaceObjectTreeNodeType.Item, i)
            childNode.Tag = tag
            ' set the tag's SpaceObject type
            tag.ObjectType = so.ObjectType
            tag.IFF = so.IFF
            tag.Category = so.Category
            ' set the child node's text based on its data
            'childNode.Text = TIEServices.SpaceObjectDescription(i)
            tag.Text = TIEServices.SpaceObjectDescription(i)
            ' add the child to the parent node
            parentNode.Nodes.Add(childNode)
        Next 'i
        tvSpaceObjects.Nodes.Add(parentNode)

        ' expand the parent node
        parentNode.Expand()

        ' select the first Craft node
        tvSpaceObjects.SelectedNode = tvSpaceObjects.Nodes.Item(0).Nodes.Item(0)

        ' enable the timer
        tmrUpdate.Enabled = True
    End Sub

    Private Sub cmdMoreObjectInfo_Click(sender As Object, e As EventArgs) Handles cmdMoreObjectInfo.Click
        ' handle no node being selected for whatever reason
        If tvSpaceObjects.SelectedNode Is Nothing Then
            ' TODO: Say something
            Exit Sub
        End If
        Dim tag As SpaceObjectTreeNodeTag = tvSpaceObjects.SelectedNode.Tag

        Select Case tag.NodeType
            Case SpaceObjectTreeNodeType.Item
                ' TODO: further sanity checking on the selected node's tag

                ' for now, just open the CraftInfo form
                Dim so As SpaceObject = TIEServices.SpaceObjects(tag.Index)
                frmCraftInfo.Show()

                frmCraftInfo.UpdateDisplay(so, TIEServices.CraftObject(so))
        End Select

    End Sub

#End Region

#End Region

End Class