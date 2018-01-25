Imports System.ComponentModel
Imports TIE_Watcher.TIE
Imports System.Drawing.Drawing2D

''' <summary>
''' Class for rendering a TIE Fighter Model using the knowledge I've got so far.
''' </summary>
Public Class FormModelRender

#Region " --- Variables --- "

    Private bmpRender As New Bitmap(1920, 1080)

    Private model As ModelResource
    Private mesh As ModelMesh

    Private renderModelNotMesh As Boolean = False   ' whether to render the model or the mesh

    Private rollAngle As Single = 0

    Private zoomStep As Single = 1.2
    Private zoomLevel As Single = 1

#End Region
#Region " --- Methods --- "

#Region " Event-Handlers "

    Private Sub FormModelRender_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                ' hide the form
                tmrUpdate.Enabled = False
                Me.Hide()

        End Select
    End Sub

    Private Sub FormModelRender_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        ' abort if the scroll did not occur within picture's bounds
        If Not picDisplay.Bounds.Contains(e.Location) Then Exit Sub

        If e.Delta > 0 Then
            zoomLevel *= zoomStep
        Else
            zoomLevel /= zoomStep
        End If

        ' redraw
        doRender()
        picDisplay.Invalidate()
    End Sub

    Private Sub FormModelRender_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub FormModelRender_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If mesh Is Nothing Then Exit Sub

        doRender()
        picDisplay.Invalidate()
    End Sub

    Private Sub picDisplay_Paint(sender As Object, e As PaintEventArgs) Handles picDisplay.Paint
        'e.Graphics.Clear(SystemColors.Control)

        e.Graphics.DrawImage(bmpRender, 0, 0)
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        rollAngle += 5
        If rollAngle >= 720 Then rollAngle -= 720

        doRender()
        picDisplay.Invalidate()
    End Sub

#End Region

    Private Sub renderMesh(mesh As ModelMesh, g As Graphics)
        ' create the transformation matrix
        Dim transMatrix As New Matrix3D
        ' apply a scale transformation
        transMatrix.RotateTransform(RotationAxis.Yaw, rollAngle)
        'transMatrix.RotateTransform(RotationAxis.Roll, 90)
        'transMatrix.RotateTransform(RotationAxis.Pitch, -90)
        transMatrix.RotateTransform(RotationAxis.Pitch, Math.Sin(rollAngle / 2 * Math.PI / 180) * 10)

        ' define the camera's point, using a large number so distance-to-camera calculations will work properly
        Dim camera As New Point3D(0, 50000, 0)

        ' transform the mesh's vertices
        Dim transVertex() As Point3D = Array.CreateInstance(GetType(Point3D), mesh.VertexCount)
        For i As Integer = 0 To mesh.VertexCount - 1
            ' get a transformed copy of the vertex
            transVertex(i) = Point3D.Transform(mesh.Vertex(i).Point, transMatrix)
        Next

        ' transform the mesh's face normals
        Dim transFaceNormals() As Point3D = Array.CreateInstance(GetType(Point3D), mesh.FaceCount)
        'Dim transFaceCentroids() As Point3D = Array.CreateInstance(GetType(Point3D), mesh.FaceCount)
        Dim transFaceDistance() As Single = Array.CreateInstance(GetType(Single), mesh.FaceCount)
        For i As Integer = 0 To mesh.FaceCount - 1
            ' get a transformed copy of the face normal
            transFaceNormals(i) = Point3D.Transform(mesh.Faces(i).Normal, transMatrix)
            ' calculate the distance from the camera to the mesh's transformed centroid
            transFaceDistance(i) = camera.Distance(Point3D.Transform(mesh.Faces(i).Centroid, transMatrix))
        Next

        ' determine what order to draw the faces in based on their distance from the camera
        Dim faceOrder() As Integer = Array.CreateInstance(GetType(Integer), mesh.FaceCount)
        ' set the initial state
        For i As Integer = 0 To mesh.FaceCount - 1
            faceOrder(i) = i
        Next
        ' for now we'll just do a slow-ass bubble sort because I'm lazy and want to see this working ASAP
        'Dim swapped As Boolean
        'Do
        '    swapped = False
        '    For i As Integer = 0 To mesh.FaceCount - 2
        '        ' compare the two distances
        '        If transFaceDistance(faceOrder(i)) > transFaceDistance(faceOrder(i + 1)) Then
        '            Dim temp As Integer = faceOrder(i)
        '            swapped = True
        '            faceOrder(i) = faceOrder(i + 1)
        '            faceOrder(i + 1) = temp
        '        End If
        '    Next
        'Loop While swapped = True

        ' draw the mesh as a wireframe
        For faceIndex As Integer = 0 To mesh.FaceCount - 1
            Dim face As ModelFace = mesh.Faces(faceOrder(faceIndex))

            ' get the transformed centroid of the face
            Dim transCentroid As Point3D = Point3D.Transform(face.Centroid, transMatrix)

            'Dim edgeBytes() As Byte = face.EdgeBytes

            ' calculate the dot product of the camera and the face's normal
            ' perform a backface cull check if flag is clear
            'If Not face.Flags And ModelFaceFlags.NoCulling Then
            '    ' skip Drawing this face If the dot product Is negative, indicating the face Is pointed away from the camera
            '    If camera.DotProduct(transFaceNormals(faceOrder(faceIndex))) < 0 Then Continue For
            'End If

            ' handle face type
            If face.FaceType = ModelFaceType.Polygon Then
                ' create the path to draw the face
                Dim facePath As New GraphicsPath

                ' draw the edges as normal
                For edgeIndex As Integer = 0 To face.VertexCount - 1
                    ' determine which vertex indices to use
                    Dim v1 As Byte = face.VertexIndices(edgeIndex)
                    Dim v2 As Byte = face.VertexIndices((edgeIndex + 1) Mod face.VertexCount)

                    Dim x1 As Single = transVertex(v1).X
                    Dim y1 As Single = -transVertex(v1).Z
                    Dim x2 As Single = transVertex(v2).X
                    Dim y2 As Single = -transVertex(v2).Z

                    ' add the line to the path
                    facePath.AddLine(x1, y1, x2, y2)
                Next

                ' fill the path
                'g.FillPath(Brushes.Green, facePath)
                ' draw the path
                g.DrawPath(Pens.Lime, facePath)

                ' dispose of the path
                facePath.Dispose()
            Else
                ' seems to indicate a line of a particular thickness
                Dim linePen As Pen = Pens.Lime.Clone
                ' NOTE: The pen width seems to be approximately correct here, though it may need to be tweaked a little
                linePen.Width = face.LineWidth * 2.7

                Dim x1 As Single = transVertex(face.VertexIndices(0)).X
                Dim y1 As Single = -transVertex(face.VertexIndices(0)).Z
                Dim x2 As Single = transVertex(face.VertexIndices(1)).X
                Dim y2 As Single = -transVertex(face.VertexIndices(1)).Z

                g.DrawLine(linePen, x1, y1, x2, y2)
                linePen.Dispose()
            End If

            '' if the face has any markings try handling those (yikes)
            'For markingIndex As Integer = 0 To face.MarkingCount - 1
            '    Dim marking As ModelFaceMarking = face.Markings(markingIndex)

            '    ' markings with only 2 vertices need special handling
            '    If marking.VertexCount <> 2 Then

            '    Else
            '        Console.WriteLine("Skipped drawing marking {0} of face {1} -- only 2 vertices.", markingIndex, faceIndex)
            '    End If
            'Next
        Next
    End Sub

    Private Sub doRender()
        ' get a graphics object for the render bitmap
        Dim g As Graphics = Graphics.FromImage(bmpRender)

        ' set the graphics object's bounds to that of the display picture
        g.Clip = New Region(New Rectangle(Point.Empty, picDisplay.Size))

        ' clear it to black
        g.Clear(Color.Black)

        ' determine the center of the viewing area
        Dim center As New Point(picDisplay.Width >> 1, picDisplay.Height >> 1)

        ' apply a transformation matrix to the graphics object so the render is centered in the view
        g.TranslateTransform(center.X, center.Y)

        ' scale the drawing so it will fit in the display
        Dim scaleFactor As Single = Math.Min(center.X, center.Y) / model.Header.Field_A * zoomLevel
        g.ScaleTransform(scaleFactor, scaleFactor)

        If renderModelNotMesh Then
            ' render all of the model's meshes
            For i As Integer = 0 To model.Header.PartCount - 1
                renderMesh(model.Parts(i).Meshes(0), g)
            Next
        Else
            ' render just the mesh we were provided
            renderMesh(mesh, g)
        End If

        ' dispose of the graphics object
        g.Dispose()
    End Sub

    Public Sub RenderModelMesh(model As ModelResource, mesh As ModelMesh)
        If model Is Nothing Then Throw New ArgumentNullException("model")
        If mesh Is Nothing Then Throw New ArgumentNullException("mesh")

        Me.model = model
        Me.mesh = mesh

        ' reset roll angle
        rollAngle = 0
        ' reset zoom level
        zoomLevel = 1

        renderModelNotMesh = False
        doRender()

        ' show the form if it's not visible
        If Not Me.Visible Then
            Me.Show()
        Else
            ' make sure we have focus and are not minimized
            Me.Focus()
            Me.WindowState = FormWindowState.Normal
        End If

        ' invalidate the bitmap
        picDisplay.Invalidate()

        tmrUpdate.Enabled = True
    End Sub

    Public Sub RenderModel(model As ModelResource)
        If model Is Nothing Then Throw New ArgumentNullException("model")

        Me.model = model

        ' reset roll angle
        rollAngle = 0
        ' reset zoom level
        zoomLevel = 1

        renderModelNotMesh = True
        doRender()

        ' show the form if it's not visible
        If Not Me.Visible Then
            Me.Show()
        Else
            ' make sure we have focus and are not minimized
            Me.Focus()
            Me.WindowState = FormWindowState.Normal
        End If

        ' invalidate the bitmap
        picDisplay.Invalidate()

        tmrUpdate.Enabled = True
    End Sub

#End Region
#Region " --- Properties --- "



#End Region

End Class