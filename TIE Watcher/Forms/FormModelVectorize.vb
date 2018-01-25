Imports TIE_Watcher.TIE
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

''' <summary>
''' Debug/development form used to aid creation of craft vector images for Combat Map.
''' </summary>
Public Class FormModelVectorize

#Region " --- Structures --- "

    ' items stored in the path list
    Private Structure PathListItem
        Public facePath As GraphicsPath
        Public lineWidth As Single
        Public faceType As ModelFaceType
    End Structure

#End Region
#Region " --- Variables --- "

    Private model As ModelResource
    Private bmpRender As New Bitmap(1920, 1080)

    Private pathList As New ArrayList
    Private hoverIndex As Integer = 0

#End Region
#Region " --- Methods --- "

#Region " Constructors "



#End Region
#Region " Event-Handlers "

    Private Sub cmdMoveFront_Click(sender As Object, e As EventArgs) Handles cmdMoveFront.Click
        ' do nothing if no cells are selected
        If dgvFaces.SelectedRows.Count = 0 Then Exit Sub
        Dim numSelected As Integer = dgvFaces.SelectedRows.Count

        ' arrays to hold the selected rows' paths and row data
        Dim selectedPaths(numSelected - 1) As PathListItem
        Dim selectedRows(numSelected - 1) As DataGridViewRow

        ' copy the info on each selected item
        For i As Integer = 0 To numSelected - 1
            selectedPaths(i) = pathList(dgvFaces.SelectedRows.Item(i).Index)
            selectedRows(i) = dgvFaces.SelectedRows.Item(i)
        Next

        ' remove the itmes from their positions in their lists and re-add them at the end
        For i As Integer = numSelected - 1 To 0 Step -1
            dgvFaces.Rows.Remove(selectedRows(i))
            pathList.Remove(selectedPaths(i))
            dgvFaces.Rows.Add(selectedRows(i))
            pathList.Add(selectedPaths(i))
        Next

        ' preserve selection
        dgvFaces.ClearSelection()
        For i As Integer = 0 To numSelected - 1
            selectedRows(i).Selected = True
        Next
        ' scroll to the end
        dgvFaces.FirstDisplayedScrollingRowIndex = selectedRows(0).Index

        ' redraw
        renderPaths()
        picRender.Invalidate()
    End Sub

    Private Sub cmdMoveBack_Click(sender As Object, e As EventArgs) Handles cmdMoveBack.Click
        ' do nothing if no cells are selected
        If dgvFaces.SelectedRows.Count = 0 Then Exit Sub
        Dim numSelected As Integer = dgvFaces.SelectedRows.Count

        ' arrays to hold the selected rows' paths and row data
        Dim selectedPaths(numSelected - 1) As PathListItem
        Dim selectedRows(numSelected - 1) As DataGridViewRow

        ' copy the info on each selected item
        For i As Integer = 0 To numSelected - 1
            selectedPaths(i) = pathList(dgvFaces.SelectedRows.Item(i).Index)
            selectedRows(i) = dgvFaces.SelectedRows.Item(i)
        Next

        ' remove the itmes from their positions in their lists and re-add them at the front
        For i As Integer = numSelected - 1 To 0 Step -1
            dgvFaces.Rows.Remove(selectedRows(i))
            pathList.Remove(selectedPaths(i))
            dgvFaces.Rows.Insert(0, selectedRows(i))
            pathList.Insert(0, selectedPaths(i))
        Next

        ' preserve selection
        dgvFaces.ClearSelection()
        For i As Integer = 0 To numSelected - 1
            selectedRows(i).Selected = True
        Next
        ' scroll to the beginning
        dgvFaces.FirstDisplayedScrollingRowIndex = 0

        ' redraw
        renderPaths()
        picRender.Invalidate()
    End Sub

    Private Sub dgvFaces_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFaces.CellContentClick
        ' handle behavior based on column of the cell
        Select Case e.ColumnIndex
            Case colDrawCheck.Index
                ' just the check box matters
                ' render paths again
                renderPaths()
                picRender.Invalidate()
        End Select
    End Sub

    Private Sub dgvFaces_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFaces.CellMouseEnter
        hoverIndex = e.RowIndex
        renderPaths()
        picRender.Invalidate()
    End Sub

    Private Sub dgvFaces_MouseLeave(sender As Object, e As EventArgs) Handles dgvFaces.MouseLeave
        hoverIndex = -1
        renderPaths()
        picRender.Invalidate()
    End Sub

    Private Sub FormModelVectorize_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub picRender_Paint(sender As Object, e As PaintEventArgs) Handles picRender.Paint
        e.Graphics.DrawImage(bmpRender, 0, 0)
    End Sub

    Private Sub cmdSaveAs_Click(sender As Object, e As EventArgs) Handles cmdSaveAs.Click
        ' Save the vector in the following format:
        ' Chars[8]      'SHIPVECT'
        ' UInt32        Number of paths
        ' --- Path struct for each path ---
        ' UInt32        Face Path Type (line/polygon)
        ' Single        Line Width
        ' UInt32        Number of Points
        ' --- Point structs for each point in the path ---
        ' Single        Point X
        ' Single        Point Y
        Dim res As DialogResult
        Dim scale As Single = 1

        res = sdlgSaveAs.ShowDialog()

        If res <> DialogResult.OK Then Exit Sub

        ' proceed with the save
        Dim bWrite As New IO.BinaryWriter(New IO.FileStream(sdlgSaveAs.FileName, IO.FileMode.Create))

        ' set the scale based on a field in the model header
        If model.Header.Field_1E > 0 Then
            ' looks like this actually multiplies things by 4??  I have no idea
            scale = model.Header.Field_1E * 2
        End If

        ' write the header
        bWrite.Write(System.Text.Encoding.ASCII.GetBytes("SHIPVECT"))

        ' count the number of enabled paths
        Dim pathCount As UInt32
        For i As Integer = 0 To pathList.Count - 1
            Dim row As DataGridViewRow = dgvFaces.Rows.Item(i)
            If row.Cells(colDrawCheck.Index).EditedFormattedValue = False Then Continue For
            pathCount += 1
        Next
        ' write the count
        bWrite.Write(pathCount)

        ' handle each path
        For pathIndex As Integer = 0 To pathList.Count - 1
            Dim row As DataGridViewRow = dgvFaces.Rows.Item(pathIndex)
            ' skip unchecked paths
            If row.Cells(colDrawCheck.Index).EditedFormattedValue = False Then Continue For

            ' write the face path type
            Dim pathItem As PathListItem = DirectCast(pathList(pathIndex), PathListItem)
            bWrite.Write(pathItem.faceType)
            ' write the line width
            bWrite.Write(pathItem.lineWidth)
            ' write the number of points
            bWrite.Write(pathItem.facePath.PointCount)
            ' write the points
            For i As Integer = 0 To pathItem.facePath.PointCount - 1
                bWrite.Write(pathItem.facePath.PathPoints(i).X * scale)
                bWrite.Write(pathItem.facePath.PathPoints(i).Y * scale)
            Next
        Next

        ' that's all you need to do

        bWrite.Close()

    End Sub

#End Region

    Private Sub renderPaths()
        ' get the graphics object
        Dim g As Graphics = Graphics.FromImage(bmpRender)
        ' brush and pen for drawing the path
        Dim pathBrush As SolidBrush = Brushes.Green
        Dim pathPen As Pen = Pens.Lime

        ' set the graphics object's bounds to that of the display picture
        g.Clip = New Region(New Rectangle(Point.Empty, picRender.Size))

        ' clear the bitmap
        g.Clear(Color.Black)

        ' determine the center of the viewing area
        Dim center As New Point(picRender.Width >> 1, picRender.Height >> 1)

        ' apply a transformation matrix to the graphics object so the render is centered in the view
        g.TranslateTransform(center.X, center.Y)

        ' scale the drawing so it will fit in the display
        Dim scaleFactor As Single = Math.Min(center.X, center.Y) / model.Header.Field_A * 1.5
        g.ScaleTransform(scaleFactor, scaleFactor)

        ' iterate through all the paths, drawing them
        For i As Integer = 0 To pathList.Count - 1

            Dim row As DataGridViewRow = dgvFaces.Rows.Item(i)

            ' draw the path only if it's enabled
            If row.Cells(colDrawCheck.Index).EditedFormattedValue = False Then Continue For
            Dim pathItem As PathListItem = DirectCast(pathList(i), PathListItem)
            Select Case pathItem.faceType
                Case ModelFaceType.Polygon
                    ' fill the path
                    g.FillPath(pathBrush, pathItem.facePath)
                    ' draw the path
                    g.DrawPath(pathPen, pathItem.facePath)

                Case ModelFaceType.Line
                    Dim tempPen As Pen = pathPen.Clone
                    tempPen.Width = pathItem.lineWidth

                    ' draw the path
                    g.DrawPath(tempPen, pathItem.facePath)

                    ' dispose of the temporary pen
                    tempPen.Dispose()
            End Select

        Next

        ' handle hover index
        If hoverIndex <> -1 Then
            Dim pathItem As PathListItem = DirectCast(pathList(hoverIndex), PathListItem)

            Select Case pathItem.faceType
                Case ModelFaceType.Polygon
                    ' draw the path the mouse is hovering over
                    g.FillPath(Brushes.Blue, pathItem.facePath)
                    g.DrawPath(Pens.White, pathItem.facePath)

                Case ModelFaceType.Line
                    Dim tempPen As Pen = Pens.White.Clone
                    tempPen.Width = pathItem.lineWidth

                    ' draw the path
                    g.DrawPath(tempPen, pathItem.facePath)

                    ' dispose of the temporary pen
                    tempPen.Dispose()
            End Select

        End If

        g.Dispose()
    End Sub

    Private Sub buildPathList()
        ' define the "camera" as being above the model
        Dim camera As New Point3D(0, 0, -1)

        ' roughly order the parts by increasing Z-depth, based on their center
        Dim partOrder(model.Header.PartCount - 1) As Integer
        For i As Integer = 0 To model.Header.PartCount - 1
            partOrder(i) = i
        Next
        Dim switched As Boolean = False
        Do
            switched = False
            For i As Integer = 0 To model.Header.PartCount - 2
                If model.Parts(partOrder(i)).Field_14 > model.Parts(partOrder(i + 1)).Field_14 Then
                    Dim temp As Integer = partOrder(i)
                    partOrder(i) = partOrder(i + 1)
                    partOrder(i + 1) = temp
                    switched = True
                End If
            Next
        Loop While switched = True

        ' first count the total number of faces (and thus, paths)
        Dim totalFaces As Integer = 0
        ' iterate through the parts in the model
        For partIndex As Integer = 0 To model.Header.PartCount - 1
            ' exclude certain part types from the face count
            Select Case model.Parts(partIndex).PartType
                Case ModelPartType.Antenna ',
                    'ModelPartType.BeamSystem,
                    ' ModelPartType.BeamSystem_2,
                    ' ModelPartType.CommSystem,
                    ' ModelPartType.CommSystem_2,
                    ' ModelPartType.CommSystem_3,
                    ' ModelPartType.CommSystem_4,
                    ' ModelPartType.LaserGun,
                    ' ModelPartType.LaserTurret,
                    ' ModelPartType.LaserTurret_2,
                    ' ModelPartType.Hull_2
                    Continue For
            End Select

            ' use the highest quality mesh
            Dim mesh As ModelMesh = model.Parts(partIndex).Meshes(0)

            ' add the number of faces to the counter
            totalFaces += mesh.FaceCount
        Next

        ' create the array of graphics paths
        pathList = New ArrayList

        ' iterate through the parts in the model
        Dim curFace As Integer = 0
        For partIndex As Integer = 0 To model.Header.PartCount - 1
            '' exclude certain part types
            Select Case model.Parts(partOrder(partIndex)).PartType
                Case ModelPartType.Antenna ',
                    'ModelPartType.BeamSystem,
                    'ModelPartType.BeamSystem_2,
                    'ModelPartType.CommSystem,
                    'ModelPartType.CommSystem_2,
                    'ModelPartType.CommSystem_3,
                    'ModelPartType.CommSystem_4,
                    'ModelPartType.LaserGun,
                    'ModelPartType.LaserTurret,
                    'ModelPartType.LaserTurret_2,
                    'ModelPartType.Hull_2
                    Continue For
            End Select

            ' use the highest quality mesh
            Dim mesh As ModelMesh = model.Parts(partOrder(partIndex)).Meshes(0)

            ' iterate through the faces
            For faceIndex As Integer = 0 To mesh.FaceCount - 1
                Dim pathItem As New PathListItem
                Dim face As ModelFace = mesh.Faces(faceIndex)
                ' create a new row in the face draw order list
                Dim row As DataGridViewRow = dgvFaces.Rows.Item(dgvFaces.Rows.Add())

                ' set the values for the rows fields
                row.Cells(colDrawCheck.Index).Value = True
                row.Cells(colPartName.Index).Value = String.Format("{0} - {1}", partOrder(partIndex), model.Parts(partOrder(partIndex)).PartType.ToString)
                row.Cells(colFaceIndex.Index).Value = faceIndex
                row.Cells(colFaceType.Index).Value = face.FaceType.ToString

                ' create the path for the face
                pathItem.facePath = New GraphicsPath
                pathItem.faceType = face.FaceType
                ' handle face type
                If face.FaceType = ModelFaceType.Polygon Then
                    ' if the face's normal is not pointed toward the camera, disable the face to start
                    If face.Normal.DotProduct(camera) <= 0 And (face.Flags And ModelFaceFlags.NoCulling) = 0 Then
                        row.Cells(colDrawCheck.Index).Value = False
                    End If

                    ' add all the lines EXCEPT the final connecting one, which is implicitly added when the path is closed
                    For edgeIndex As Integer = 0 To face.VertexCount - 2
                        ' determine which vertex indices to use
                        Dim v1 As Byte = face.VertexIndices(edgeIndex)
                        Dim v2 As Byte = face.VertexIndices((edgeIndex + 1) Mod face.VertexCount)

                        Dim x1 As Single = mesh.Vertex(v1).Point.X
                        Dim y1 As Single = mesh.Vertex(v1).Point.Y
                        Dim x2 As Single = mesh.Vertex(v2).Point.X
                        Dim y2 As Single = mesh.Vertex(v2).Point.Y

                        ' add the line to the path
                        pathItem.facePath.AddLine(x1, y1, x2, y2)
                    Next

                    ' close the path
                    pathItem.facePath.CloseFigure()

                Else
                    pathItem.lineWidth = face.LineWidth * 2.7
                    Dim x1 As Single = mesh.Vertex(face.VertexIndices(0)).Point.X
                    Dim y1 As Single = mesh.Vertex(face.VertexIndices(0)).Point.Y
                    Dim x2 As Single = mesh.Vertex(face.VertexIndices(1)).Point.X
                    Dim y2 As Single = mesh.Vertex(face.VertexIndices(1)).Point.Y

                    pathItem.facePath.AddLine(x1, y1, x2, y2)

                End If

                ' set the array item to the new path


                pathList.Add(pathItem)
                curFace += 1
            Next
        Next
    End Sub

    Public Sub VectorizeModel(model As ModelResource)
        If model Is Nothing Then Throw New ArgumentNullException("model")

        Me.model = model

        ' dispose of old paths
        If Not pathList Is Nothing Then
            For i As Integer = 0 To pathList.Count - 1
                Dim item As PathListItem = DirectCast(pathList(i), PathListItem)
                item.facePath.Dispose()
                pathList(i) = Nothing
            Next
        End If
        pathList.Clear()
        ' clear all entries in the datagridview
        dgvFaces.Rows.Clear()
        buildPathList()

        ' show the form if it's not visible
        If Not Me.Visible Then
            Me.Show()
        Else
            ' make sure we have focus and are not minimized
            Me.Focus()
            Me.WindowState = FormWindowState.Normal
        End If

        ' render the paths
        renderPaths()
        picRender.Invalidate()

    End Sub

#End Region

End Class