Imports System.Drawing.Drawing2D
Imports TIE_Watcher.TIE

Public Class FormCombatMap

#Region " --- Enumerations --- "

    ' the type of action to perform on an object's entry in the follow list
    Private Enum ObjectListAction
        None = 0    ' no action
        Add         ' add the object
        Update      ' update the object's info
        Remove      ' remove the object
    End Enum

#End Region
#Region " --- Constants --- "

    ' the number of game units equal to 1 kilometer -- may not be 100% correct because of the way TIE Fighter
    ' calculates stuff, but I can refine it later
    Private Const KILOMETER_SCALE As Integer = 40710

#End Region
#Region " --- Structures --- "

    ' private structure for a path in a craft vector
    Private Structure CraftVectorPath
        Public pathType As ModelFaceType
        Public lineWidth As Single
        Public path As GraphicsPath
    End Structure

    ' private structure to hold basic data on a craft vector
    Private Structure CraftVector
        ' drawing threshold
        Public threshold As Single
        ' number of paths in the vector
        Public numPaths As Integer
        ' paths
        Public paths() As CraftVectorPath
    End Structure

#End Region
#Region " --- Variables --- "

    ' back buffer for the Map display
    Private bmpDisplay As Bitmap

    ' map zoom setting, where this many game units is equal to 1 pixel on the visible map
    Private mapScale As Single = 409.6

    ' map focal point
    Private mapFocusPoint As Point
    ' default to following object index 0
    Private followObjectIndex As Integer = 0
    ' whether the view was following the player craft
    Private wasFollowingPlayer = False

    ' local copies of space and craft objects used when updating the UI and map render
    Private soCraft(31) As SpaceObject          ' space objects in the craft range (0x00 - 0x1F)
    Private craftObjects As New Hashtable       ' table for storing Craft Objects using their pointer as the key
    Private soProjectiles(47) As SpaceObject    ' space objects in the projectile range (0x20 - 0x4F)
    Private weaponObjects(63) As WeaponObject   ' weapon objects (i.e. mines, probes, etc.)
    Private soPlayerIndex As Integer = 255      ' player's object index
    Private soPlayerIndexLast As Integer = 255  ' player's object index on the previous update

    Private soCraftListActions(31) As ObjectListAction

#Region " Drawing Paths "

    ' laser bolt path
    Private laserBoltPath As GraphicsPath
    ' ion bolt path
    Private ionBoltPath As GraphicsPath
    ' concussion missile path
    Private missilePath As GraphicsPath

    ' craft paths
    Private craftVectors() As CraftVector

    ' craft pens and brushes
    Private vectorPens(3) As Pen
    Private vectorBrushes(3) As Brush

#End Region

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' create the back buffer, sized to the largest it can be
        bmpDisplay = New Bitmap(1920, 1080)

        ' create a drawing path for laser bolts
        ' according to some quick comparisons, a laser bolt is drawn as ~2048 game units long!
        ' this looks bad in the combat map, though, so it'll be reduced to 1200 units long
        laserBoltPath = New GraphicsPath
        laserBoltPath.AddLine(0, -0, 0, 1200)
        ' create a drawing path for ion bolts
        ionBoltPath = New GraphicsPath
        ionBoltPath.AddLine(0, 0, 0, 900)
        ' create a drawing path for missiles
        missilePath = New GraphicsPath
        missilePath.AddLine(0, 0, 0, 600)
        missilePath.AddLine(0, 0, -200, 200)
        missilePath.AddLine(0, 0, 200, 200)

        ' load all of the craft vector paths
        initCraftPaths()

        ' create the pens and brushes for craft vector drawing
        Dim lfd As New LFD(ResourceHelper.GetEmbeddedResourceStream("PLAYER.LFD"))
        Dim palRange As PaletteResource = lfd.LoadPalette("range")

        For i As Integer = 0 To 3
            vectorPens(i) = New Pen(palRange.Colors(&HE7 + i * 8))
            vectorBrushes(i) = New SolidBrush(palRange.Colors(&HE0 + i * 8))
        Next

        ' close the LFD object
        lfd.Dispose()

        ' create empty craft objects for the initial state (so there's no crash on current and previous state comparison)
        For i As Integer = 0 To 31
            soCraft(i) = New SpaceObject
        Next
    End Sub

#End Region
#Region " IDisposable Support "

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                ' dispose of my own things
                bmpDisplay.Dispose()
                laserBoltPath.Dispose()
                ionBoltPath.Dispose()
                missilePath.Dispose()
                For i As Integer = 0 To 3
                    vectorPens(i).Dispose()
                    vectorBrushes(i).Dispose()
                Next
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region
#Region " Initialization "

    Private Sub initCraftPaths()
        craftVectors = Array.CreateInstance(GetType(CraftVector), SpaceObjectTypeInfo.Count)

        ' iterate through all the 'known' craft types
        For soIndex As Integer = 0 To SpaceObjectTypeInfo.Count - 1
            Dim soInfo As SpaceObjectTypeInfo = SpaceObjectTypeInfo.Types(soIndex)

            ' if the index has no species name defined, then skip it
            If soInfo.SpeciesName.Equals("") Then Continue For

            ' get a binary reader to process the embedded resource
            Dim bRead As New IO.BinaryReader(ResourceHelper.GetEmbeddedResourceStream(String.Format("{0}.VEC", soInfo.SpeciesName)))

            ' skip past the ID string I put in
            bRead.ReadBytes(8)

            ' read the number of paths
            craftVectors(soIndex).numPaths = bRead.ReadUInt32
            craftVectors(soIndex).paths = Array.CreateInstance(GetType(CraftVectorPath), craftVectors(soIndex).numPaths)
            Dim yMin As Single = 0, yMax As Single = 0

            ' iterate through each path
            For pathIndex As Integer = 0 To craftVectors(soIndex).numPaths - 1
                Dim gPath As New GraphicsPath

                ' set the fields of the struct
                With craftVectors(soIndex).paths(pathIndex)
                    .pathType = bRead.ReadUInt32
                    .lineWidth = bRead.ReadSingle
                    .path = gPath

                    ' read the number of points in the path
                    Dim numPoints As Integer = bRead.ReadUInt32
                    Dim points(numPoints - 1) As Point

                    ' read in the points
                    For i As Integer = 0 To numPoints - 1
                        points(i).X = bRead.ReadSingle
                        points(i).Y = bRead.ReadSingle

                        If points(i).Y < yMin Then yMin = points(i).Y
                        If points(i).Y > yMax Then yMax = points(i).Y
                    Next

                    ' add the lines
                    gPath.AddLines(points)

                    ' close the path if it's a polygon
                    If .pathType = ModelFaceType.Polygon Then
                        gPath.CloseFigure()
                    End If
                End With
            Next

            ' threshold is based off the y-range
            craftVectors(soIndex).threshold = yMax - yMin

            ' close the reader and its underlying stream
            bRead.Close()
        Next
    End Sub

#End Region

    ' return whether a Space Object (in the craft range) is valid for drawing or inclusion in the follow list
    Private Function isSpaceObjectValid(so As SpaceObject) As Boolean
        ' return false if its object type is 0
        If so.ObjectType = 0 Then Return False
        ' return false if the category is invalid
        Select Case so.Category
            Case SpaceObjectCategory.Capital,
                 SpaceObjectCategory.Fighter,
                 SpaceObjectCategory.Freight,
                 SpaceObjectCategory.Platform,
                 SpaceObjectCategory.Transport,
                 SpaceObjectCategory.Utility

                ' get the craft object using the pointer as the key
                Dim co As CraftObject = DirectCast(craftObjects.Item(so.ObjectPointer), CraftObject)
                ' if we got nothing from the table, that means the object is not pointing to a known craft
                ' therefore it is probably invalid
                If co Is Nothing Then Return False
                ' return false if its exploding, until I start drawing explosions on things
                If co.Field_60 = 3 Then Return False

            Case Else
                ' invalid category
                Return False

        End Select

        ' passed all the checks, return true
        Return True
    End Function

    ' updates the local copies of space and craft objects and also builds the list of objects that need to be
    ' added, updated, or removed from the follow dropdown
    Private Sub updateGameState()

        ' update space objects in the craft range and take note of whether their entries should be added, updated, or removed
        For i As Integer = 0 To 31
            ' for comparison to the previous state
            Dim soLast As SpaceObject = soCraft(i)
            soCraft(i) = TIEServices.SpaceObjects(i)
            Dim soCurrent As SpaceObject = soCraft(i)
            ' get whether the previous state was valid
            Dim wasValid As Boolean = isSpaceObjectValid(soLast)
            Dim isValid As Boolean = False

            ' update the craft object if we can
            If soCurrent.ObjectType <> 0 Then
                Select Case soCurrent.Category
                    Case SpaceObjectCategory.Capital,
                         SpaceObjectCategory.Fighter,
                         SpaceObjectCategory.Freight,
                         SpaceObjectCategory.Platform,
                         SpaceObjectCategory.Transport,
                         SpaceObjectCategory.Utility
                        ' update the corresponding craft object
                        craftObjects.Item(soCurrent.ObjectPointer) = TIEServices.CraftObject(soCurrent)
                        ' test the new object for validity
                        isValid = isSpaceObjectValid(soCurrent)

                End Select
            End If

            ' at this point the object/craft is fully updated, but now we need to handle state changes
            If wasValid = False And isValid = True Then
                ' the previous state was invalid but it is now valid, add the object
                soCraftListActions(i) = ObjectListAction.Add

            ElseIf wasValid = True And isValid = False Then
                ' the previous state was valid but it is now invalid, remove the object
                soCraftListActions(i) = ObjectListAction.Remove

            ElseIf wasValid = True And isValid = True Then
                ' craft was valid before and after, check for update conditions

                If soLast.IFF <> soCurrent.IFF Then
                    ' IFF changed
                    soCraftListActions(i) = ObjectListAction.Update

                ElseIf soLast.ObjectType <> soCurrent.ObjectType Then
                    ' object type changed (most likely caused by mission loading)
                    soCraftListActions(i) = ObjectListAction.Update

                ElseIf soLast.FlightGroupIndex <> soCurrent.FlightGroupIndex Then
                    ' flight group index changed (most likely caused by mission loading)
                    soCraftListActions(i) = ObjectListAction.Update

                End If

            End If

        Next

        ' update projectiles
        For i As Integer = 0 To 47
            soProjectiles(i) = TIEServices.SpaceObjects(&H20 + i)
        Next

        ' update weapon objects
        For i As Integer = 0 To 63
            weaponObjects(i) = TIEServices.WeaponObjects(i)
        Next

        ' update player's space object index
        soPlayerIndexLast = soPlayerIndex
        soPlayerIndex = TIEServices.PlayerSpaceObjectIndex
    End Sub

    ' updates the UI elements that are unrelated to the map buffer
    Private Sub updateUI()
        ' update the Space Object Selector
        For i As Integer = 0 To 31
            Select Case soCraftListActions(i)
                Case ObjectListAction.Add
                    'Console.WriteLine("Add: {0}", TIEServices.SpaceObjectDescription(i))
                    selSpaceObject.AddItem(soCraft(i), i)

                Case ObjectListAction.Remove
                    'Console.WriteLine("Remove: {0}", TIEServices.SpaceObjectDescription(i))
                    selSpaceObject.RemoveItemBySpaceObjectIndex(i)

                Case ObjectListAction.Update
                    'Console.WriteLine("Update: {0}", TIEServices.SpaceObjectDescription(i))
                    selSpaceObject.UpdateItem(soCraft(i), i)

            End Select

            ' clear the action for next time
            soCraftListActions(i) = ObjectListAction.None
        Next

        ' check if the object we're following is still a valid follow target
        If chkFollowObject.Checked Then
            ' if what we were following is invalid...
            If Not isSpaceObjectValid(soCraft(followObjectIndex)) Then
                ' try to follow the player instead
                If soPlayerIndex <> 255 AndAlso isSpaceObjectValid(soCraft(soPlayerIndex)) Then
                    selSpaceObject.SelectSpaceObjectIndex(soPlayerIndex)

                Else
                    ' player's no longer valid -- follow nothing at all!
                    chkFollowObject.Checked = False

                End If

            End If

        Else
            ' follow is currently un-checked
            ' if the player was invalid last time but is valid now start following them again
            ' This is to fix occasional issues where restarting a mission would stop tracking the player
            'If soPlayerIndexLast <> soPlayerIndex AndAlso soPlayerIndex <> 255 AndAlso isSpaceObjectValid(soCraft(soPlayerIndex)) Then
            '    chkFollowObject.Checked = True
            '    followObjectIndex = soPlayerIndex
            'End If
            ' if we were following the player, and they're now valid, follow them again
            If wasFollowingPlayer AndAlso soPlayerIndex <> 255 AndAlso isSpaceObjectValid(soCraft(soPlayerIndex)) Then
                chkFollowObject.Checked = True
                followObjectIndex = soPlayerIndex
            End If
        End If

    End Sub

    ' redraws the map back buffer with the current settings
    Private Sub drawMapBackBuffer()
        ' abort if the back buffer doesn't exist yet (i.e. when the form is created and it performs the first Resize event)
        If bmpDisplay Is Nothing Then Exit Sub
        ' get a graphics object to draw to the back buffer
        Dim g As Graphics = Graphics.FromImage(bmpDisplay)
        Dim so As SpaceObject
        Dim wo As WeaponObject
        'Dim co As CraftObject
        Dim fg As MissionFlightGroup
        ' array for remapping IFF color indices
        Dim iffColorRemap() As Integer = {0, 1, 3, 2, 1, 2}

        ' limit the drawing region of the back buffer to the size of the display imagebox
        g.Clip = New Region(New Rectangle(0, 0, picDisplay.Width, picDisplay.Height))

        ' update the map's focal point if an object is being followed
        If chkFollowObject.Checked Then
            mapFocusPoint.X = soCraft(followObjectIndex).XPosition
            mapFocusPoint.Y = soCraft(followObjectIndex).YPosition
        End If

        ' determine the center of the drawing region
        Dim center As New Point(picDisplay.Width / 2, picDisplay.Height / 2)

        ' clear the map to black
        g.Clear(Color.Black)

        ' draw grid lines based on map scale
        ' draw grid lines at 1 km intervals
        ' map x position of leftmost pixel = mapFocusPoint.X - center.X * mapScale
        ' starting position = that mod KILOMETER_SCALE, divide that by mapScale
        Dim lineSpacing As Single = (KILOMETER_SCALE / mapScale)
        Dim lineStart As Single = ((-mapFocusPoint.X + center.X * mapScale) Mod KILOMETER_SCALE) / mapScale
        Dim numLines As Integer = Math.Ceiling(picDisplay.Width / lineSpacing)
        For i As Integer = 0 To numLines
            g.DrawLine(Pens.DarkGreen, i * lineSpacing + lineStart, 0, i * lineSpacing + lineStart, picDisplay.Height)
        Next
        lineStart = ((mapFocusPoint.Y + center.Y * mapScale) Mod KILOMETER_SCALE) / mapScale
        numLines = Math.Ceiling(picDisplay.Height / lineSpacing)
        For i As Integer = 0 To numLines
            g.DrawLine(Pens.DarkGreen, 0, i * lineSpacing + lineStart, picDisplay.Width, i * lineSpacing + lineStart)
        Next

        ' Draw order is now thus:
        ' Vector-based Craft
        ' Projectile Vectors
        ' Icon-based Craft

        ' build the lists of object indices to draw as vectors and icons
        Dim vectorList As New ArrayList
        Dim iconList As New ArrayList
        Dim scaleFix As Single

        ' iterate through all the SpaceObjects in the Craft range (0 to 31)
        For i As Integer = 0 To 31
            ' get the space object
            so = soCraft(i)

            ' if the object is invalid, skip it
            If Not isSpaceObjectValid(so) Then Continue For

            ' determine if it should be drawn or not, and determine scale factor for vector vs. icon check
            Select Case so.Category
                Case SpaceObjectCategory.Fighter
                    scaleFix = 100      ' don't show fighters
                Case SpaceObjectCategory.Capital
                    scaleFix = 30
                Case SpaceObjectCategory.Freight
                    scaleFix = 30
                Case SpaceObjectCategory.Platform
                    scaleFix = 30
                Case SpaceObjectCategory.Transport
                    scaleFix = 20
                Case SpaceObjectCategory.Utility
                    scaleFix = 20

                Case Else
                    ' everything else is not drawn
                    Continue For
            End Select

            ' determine which list to place the space object into based on zoom level
            ' if the vector checkbox is unchecked, force it to be an icon
            If mapScale * scaleFix > craftVectors(so.ObjectType).threshold Or
                chkVectorLargeCraft.Checked = False Then
                ' draw it as an icon
                iconList.Add(i)
            Else
                ' check if a vector has been defined for this object
                If craftVectors(so.ObjectType).numPaths = 0 Then
                    ' no vector defined
                    iconList.Add(i)
                Else
                    ' vector defined
                    vectorList.Add(i)
                End If
            End If
        Next

        ' draw all of the vector-based craft
        For i As Integer = 0 To vectorList.Count - 1
            ' get the space object
            so = soCraft(DirectCast(vectorList(i), Integer))

            ' apply the transformation for the craft vector
            ' first translate to the center of the render area
            g.TranslateTransform(center.X, center.Y)
            ' transform based on map scale
            g.ScaleTransform(1 / mapScale, 1 / mapScale)
            ' translate the projectile into position
            g.TranslateTransform(-(mapFocusPoint.X - so.XPosition), mapFocusPoint.Y - so.YPosition)
            ' rescale the path, because for some reason craft models are twice the size as they are in-game or something
            g.ScaleTransform(0.5, 0.5)
            ' rotate about the center
            g.RotateTransform(so.Yaw / 182.044)

            Dim vector As CraftVector = craftVectors(so.ObjectType)

            For pathIndex As Integer = 0 To vector.numPaths - 1
                Dim path As CraftVectorPath = vector.paths(pathIndex)

                Select Case path.pathType
                    Case ModelFaceType.Line
                        Dim tempPen As Pen = vectorPens(iffColorRemap(so.IFF)).Clone
                        tempPen.Width = path.lineWidth
                        g.DrawPath(tempPen, path.path)
                        tempPen.Dispose()

                    Case ModelFaceType.Polygon
                        g.FillPath(vectorBrushes(iffColorRemap(so.IFF)), path.path)
                        g.DrawPath(vectorPens(iffColorRemap(so.IFF)), path.path)

                End Select
            Next

            g.ResetTransform()
        Next

        ' draw all of the projectiles (SpaceObject range 0x20 - 0x4F)
        For i As Integer = 0 To 47
            Dim projPath As GraphicsPath = laserBoltPath
            Dim projPen As Pen = Pens.White.Clone
            ' get the space object
            so = soProjectiles(i)

            ' determine if it should be drawn or not
            Select Case so.Category
                Case SpaceObjectCategory.PlayerProjectile, SpaceObjectCategory.Projectile
                    ' if the object type is invalid, skip it
                    If so.ObjectType = 0 Then Continue For

                    ' handle the object type
                    Select Case so.ObjectType
                        Case SpaceObjectType.WeaponLaserGreen1,
                             SpaceObjectType.WeaponLaserGreen2,
                             SpaceObjectType.WeaponTurboLaserGreen
                            projPath = laserBoltPath
                            projPen = Pens.Lime.Clone

                        Case SpaceObjectType.WeaponLaserRed1,
                             SpaceObjectType.WeaponLaserRed2,
                             SpaceObjectType.WeaponTurbolaserRed
                            projPath = laserBoltPath
                            projPen = Pens.Red.Clone

                        Case SpaceObjectType.WeaponIon1,
                             SpaceObjectType.WeaponIon2
                            projPath = ionBoltPath
                            projPen = Pens.Cyan.Clone

                        Case SpaceObjectType.WeaponMissile,
                             SpaceObjectType.WeaponAdvMissile
                            projPath = missilePath
                            projPen = Pens.Orange.Clone

                        Case SpaceObjectType.WeaponTorpedo,
                             SpaceObjectType.WeaponAdvTorpedo
                            projPath = missilePath
                            projPen = Pens.LightBlue.Clone

                        Case SpaceObjectType.WeaponRocket
                            projPath = missilePath
                            projPen = Pens.Yellow.Clone

                        Case SpaceObjectType.WeaponMagPulse
                            projPath = missilePath
                            projPen = Pens.Magenta.Clone

                        Case SpaceObjectType.WeaponSpaceBomb
                            projPath = missilePath
                            projPen = Pens.White.Clone

                        Case Else
                            ' unhandled object type
                            Continue For
                    End Select

                    ' transform the path
                    ' first translate to the center of the render area
                    g.TranslateTransform(center.X, center.Y)
                    ' transform based on map scale
                    g.ScaleTransform(1 / mapScale, 1 / mapScale)
                    ' translate the projectile into position
                    g.TranslateTransform(-(mapFocusPoint.X - so.XPosition), mapFocusPoint.Y - so.YPosition)
                    ' scale the projectile based off its pitch
                    'g.ScaleTransform(1, Math.Sin(so.Pitch / 10430.378350470453))
                    ' rotate about the center
                    g.RotateTransform(so.Yaw / 182.044)

                    g.DrawPath(projPen, projPath)
                    g.ResetTransform()

                    projPen.Dispose()
            End Select

        Next i

        ' draw all the icon-based craft
        For i As Integer = 0 To iconList.Count - 1
            ' get the space object
            so = soCraft(DirectCast(iconList(i), Integer))

            Dim icon As Bitmap = TIEServices.CraftIcon(so.ObjectType, so.IFF)
            Dim iconCenter As New Point(icon.Width >> 1, icon.Height >> 1)

            ' draw the craft as an icon
            ' figure out where to draw it based on the map scaling and the map's focal point
            Dim xDraw As Integer = center.X - (mapFocusPoint.X - so.XPosition) / mapScale
            Dim yDraw As Integer = center.Y + (mapFocusPoint.Y - so.YPosition) / mapScale

            ' draw the appropriate icon
            g.DrawImage(icon, xDraw - iconCenter.X, yDraw - iconCenter.Y)
        Next

        ' draw all of the valid WeaponObjects as icons (mines, probes, etc.)
        For i As Integer = 0 To 63
            ' get the weapon object
            wo = weaponObjects(i)
            ' skip it if the object type is invalid
            If wo.ObjectType = 0 Then Continue For

            ' get the object's flight group
            fg = TIEServices.MissionFlightGroups(wo.FlightGroupIndex)

            ' get the icon to draw
            Dim icon As Bitmap = TIEServices.CraftIcon(wo.ObjectType, fg.IFF)
            Dim iconCenter As New Point(icon.Width >> 1, icon.Height >> 1)

            ' draw the object as an icon
            Dim xDraw As Integer = center.X - (mapFocusPoint.X - (wo.XPosition * 256)) / mapScale
            Dim yDraw As Integer = center.Y + (mapFocusPoint.Y - (wo.YPosition * 256)) / mapScale

            ' draw the appropriate icon
            g.DrawImage(icon, xDraw - iconCenter.X, yDraw - iconCenter.Y)
        Next i

        ' dispose of the graphics object
        g.Dispose()
    End Sub

#Region " Event-Handlers "

    Private Sub FormCombatMap_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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

        updateGameState()
        ' default to following the player
        followObjectIndex = soPlayerIndex
        updateUI()
        ' select the player's item in the selector
        selSpaceObject.SelectSpaceObjectIndex(soPlayerIndex)
        wasFollowingPlayer = True
        drawMapBackBuffer()
        picDisplay.Invalidate()

        tmrUpdate.Enabled = True
    End Sub

    Private Sub picDisplay_Paint(sender As Object, e As PaintEventArgs) Handles picDisplay.Paint
        ' for now just paint the display bitmap as-is
        e.Graphics.DrawImage(bmpDisplay, 0, 0)
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        updateGameState()
        updateUI()
        drawMapBackBuffer()
        picDisplay.Invalidate()
    End Sub

    Private Sub FormCombatMap_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' handle form resizing by resizing and moving things, ugh
        Dim width As Integer = Me.ClientSize.Width - 16
        Dim height As Integer = Me.ClientSize.Height - pnlControls.Height - 8
        picDisplay.Size = New Size(width, height)

        ' force an update of the map only
        drawMapBackBuffer()
        picDisplay.Invalidate()
    End Sub

    Private Sub FormCombatMap_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel, selSpaceObject.MouseWheel
        ' convert the mouse co-ordinates to be relative to the parent form
        Dim senderControl As Control = DirectCast(sender, Control)
        Dim mousePoint As Point = New Point(e.Location)
        ' travel up the hierarchy, adjusting the mouse point until we hit the form itself
        Do Until senderControl.Equals(Me)
            ' transform the point by the control's location
            mousePoint.Offset(senderControl.Location)
            ' travel to the parent
            senderControl = senderControl.Parent
        Loop

        ' abort if the event did not occur over the combat map display
        If Not picDisplay.Bounds.Contains(mousePoint) Then Exit Sub

        ' adjust zoom value in or out
        'mapScale -= e.Delta
        If e.Delta > 0 Then
            mapScale /= 1.3 * Math.Abs(e.Delta / 120)
        Else
            mapScale *= 1.3 * Math.Abs(e.Delta / 120)
        End If

        ' clip the mapScale to resonable dimensions
        mapScale = Math.Min(Math.Max(32, mapScale), 2048)

        ' force an update of the map only
        drawMapBackBuffer()
        picDisplay.Invalidate()
    End Sub

    Private Sub FormCombatMap_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        ' if the form is no longer visible (i.e. it's hidden by closing it), disable updates
        If Visible = False Then
            tmrUpdate.Enabled = False
        Else
            tmrUpdate.Enabled = True
        End If
    End Sub

    Private Sub selSpaceObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selSpaceObject.SelectedIndexChanged
        ' handle the selected index changing
        Dim newIndex As Integer = selSpaceObject.SelectedSpaceObjectIndex

        'Console.WriteLine("selection changed")
        If newIndex = -1 Then
            ' dunno what to do about this for now
        Else
            ' change the craft that will be followed
            followObjectIndex = newIndex
            ' if the new object was not the player remember that
            If followObjectIndex <> soPlayerIndex Then wasFollowingPlayer = False
            ' don't force a redraw, as that will occur soon enough on the next update
        End If

        ' give up focus
        'picDisplay.Focus()
    End Sub

    Private Sub cmdFollowPlayer_Click(sender As Object, e As EventArgs) Handles cmdFollowPlayer.Click
        ' check if the player's object is valid
        If isSpaceObjectValid(soCraft(soPlayerIndex)) Then
            ' make sure following is enabled
            chkFollowObject.Checked = True
            ' change the selected index to that of the player
            selSpaceObject.SelectSpaceObjectIndex(soPlayerIndex)

            wasFollowingPlayer = True
        End If
    End Sub

    'Private Sub FormCombatMap_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    '    bmpDisplay.Dispose()
    '    laserBoltPath.Dispose()
    '    ionBoltPath.Dispose()
    '    missilePath.Dispose()
    'End Sub

#End Region


#End Region
#Region " --- Properties --- "



#End Region

End Class