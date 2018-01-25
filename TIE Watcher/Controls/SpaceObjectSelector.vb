Imports TIE_Watcher.TIE

''' <summary>
''' Control bearing a drop-down combo box for selecting SpaceObjects.
''' </summary>
Public Class SpaceObjectSelector

#Region " --- Structures --- "

    ' internal structure representing an entry in the combo box
    Private Structure ComboItem
        ' space object index the item represents
        Public spaceObjectIndex As Integer
        ' icon to use for the object
        Public bmpCraftIcon As Bitmap
        ' outline box to use for the object
        Public bmpCraftIconBox As Bitmap
        ' name of the object
        Public craftName As String
    End Structure

#End Region
#Region " --- Variables --- "

    ' whether mouse wheel scrolling should be enabled or disabled
    Private _disableMouseWheelScroll As Boolean = False

#End Region
#Region " --- Methods --- "

#Region " Constructors "



#End Region
#Region " Event-Handlers "

    Private Sub cmbSpaceObject_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbSpaceObject.DrawItem
        ' draw the background as appropriate
        e.DrawBackground()

        ' draw the item if the index is valid
        If e.Index <> -1 Then
            Dim item As ComboItem = DirectCast(cmbSpaceObject.Items(e.Index), ComboItem)
            Dim hPosition As Integer = e.Bounds.Left + 1
            Dim vCenter As Integer = (e.Bounds.Height >> 1) + e.Bounds.Top
            Dim textSize As SizeF

            ' measure the text
            textSize = e.Graphics.MeasureString(item.craftName, e.Font)
            ' draw the icon box
            e.Graphics.DrawImage(item.bmpCraftIconBox, hPosition, vCenter - (item.bmpCraftIconBox.Height >> 1))
            ' draw the icon
            e.Graphics.DrawImage(item.bmpCraftIcon, hPosition, vCenter - (item.bmpCraftIcon.Height >> 1))
            hPosition += item.bmpCraftIcon.Width + 1
            ' draw the item's text, centered vertically, besided the icon
            e.Graphics.DrawString(item.craftName, e.Font, SystemBrushes.ControlText, hPosition, vCenter - (CInt(textSize.Height) >> 1))
        End If
    End Sub

    Private Sub cmbSpaceObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpaceObject.SelectedIndexChanged
        ' raise an appropriate event to pass the information along
        RaiseEvent SelectedIndexChanged(Me, e)
    End Sub

#End Region

    ''' <summary>
    ''' Adds a new SpaceObject to the selection, representing the given SpaceObject index.
    ''' </summary>
    ''' <param name="spaceObject"></param>
    ''' <param name="index"></param>
    Public Sub AddItem(spaceObject As SpaceObject, index As Integer)
        ' handle argument exceptions
        If spaceObject Is Nothing Then Throw New ArgumentNullException("spaceObject")
        ' TODO: Check if the SpaceObject index has already been assigned

        ' create the new item and set its fields
        Dim newItem As ComboItem

        newItem.spaceObjectIndex = index
        newItem.bmpCraftIcon = TIEServices.CraftIcon(spaceObject.ObjectType, spaceObject.IFF)
        newItem.bmpCraftIconBox = TIEServices.CraftIconBox(spaceObject.IFF)
        newItem.craftName = TIEServices.SpaceObjectDescription(index)

        ' add the item to the combo box
        cmbSpaceObject.Items.Add(newItem)
    End Sub

    ''' <summary>
    ''' Removes an item by its SpaceObject index.
    ''' </summary>
    ''' <param name="index"></param>
    Public Sub RemoveItemBySpaceObjectIndex(index As Integer)
        ' TODO: Throw an exception if an item is not found

        ' loop through all the items until a match is found and then remove that item
        For i As Integer = 0 To cmbSpaceObject.Items.Count - 1
            Dim item As ComboItem = DirectCast(cmbSpaceObject.Items(i), ComboItem)

            If item.spaceObjectIndex = index Then
                ' remove the item
                cmbSpaceObject.Items.RemoveAt(i)
                ' jump out
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates an existing item by its SpaceObject index.
    ''' </summary>
    ''' <param name="spaceObject"></param>
    ''' <param name="index"></param>
    Public Sub UpdateItem(spaceObject As SpaceObject, index As Integer)
        ' handle argument exceptions
        If spaceObject Is Nothing Then Throw New ArgumentNullException("spaceObject")
        ' TODO: Throw an exception if an item is not found

        ' loop through all the items until a match is found and then update that item
        For i As Integer = 0 To cmbSpaceObject.Items.Count - 1
            Dim item As ComboItem = DirectCast(cmbSpaceObject.Items(i), ComboItem)

            If item.spaceObjectIndex = index Then
                ' update the item
                item.bmpCraftIcon = TIEServices.CraftIcon(spaceObject.ObjectType, spaceObject.IFF)
                item.bmpCraftIconBox = TIEServices.CraftIconBox(spaceObject.IFF)
                item.craftName = TIEServices.SpaceObjectDescription(index)
                cmbSpaceObject.Items(i) = item
                ' invalidate the appropriate item
                ' TODO: Find a way to invalidate JUST the item that's being updated
                cmbSpaceObject.Refresh()

                ' jump out
                Exit Sub
            End If
        Next
    End Sub

    ''' <summary>
    ''' Changes the selected item by SpaceObject index.
    ''' </summary>
    ''' <param name="index"></param>
    Public Sub SelectSpaceObjectIndex(index As Integer)
        ' TODO: Throw an exception if an item is not found

        ' loop through all the items until a match is found and then select that item
        For i As Integer = 0 To cmbSpaceObject.Items.Count - 1
            Dim item As ComboItem = DirectCast(cmbSpaceObject.Items(i), ComboItem)

            If item.spaceObjectIndex = index Then
                cmbSpaceObject.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub cmbSpaceObject_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbSpaceObject.MouseWheel
        If _disableMouseWheelScroll Then
            ' disable use of the mouse wheel for scrolling
            Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            mwe.Handled = True
        End If

        ' pass the event along to the owner
        MyBase.OnMouseWheel(e)
    End Sub

#End Region
#Region " --- Properties --- "

    ''' <summary>
    ''' Gets the SpaceObject index of the selected item.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SelectedSpaceObjectIndex As Integer
        Get
            ' if nothing is selected return -1
            If cmbSpaceObject.SelectedIndex = -1 Then Return -1

            Dim item As ComboItem = DirectCast(cmbSpaceObject.SelectedItem, ComboItem)

            Return item.spaceObjectIndex
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets whether the mouse wheel should be able to scroll the combo box.
    ''' </summary>
    ''' <returns></returns>
    Public Property DisableMouseWheelScrolling As Boolean
        Get
            Return _disableMouseWheelScroll
        End Get
        Set(value As Boolean)
            _disableMouseWheelScroll = value
        End Set
    End Property

#End Region
#Region " --- Events --- "

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event SelectedIndexChanged(sender As Object, e As EventArgs)

#End Region

End Class
