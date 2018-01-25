Namespace TIE

    ''' <summary>
    ''' Class for handling TIE Fighter's LFD resource files, as well as accessing resources embedded within.
    ''' </summary>
    Public Class LFD
        Implements IDisposable

#Region " --- Constants --- "

        ' string to use for bad LFD file format exceptions
        Private Const BAD_LFD_FILE_MESSAGE As String = "LFD resource is invalid or contains errors."

        ' FORMATTED string to use for resource not found exceptions
        Private Const RESOURCE_NOT_FOUND_MESSAGE As String = "Resource '{0}' of type '{1}' could not be found."

#End Region
#Region " --- Structs --- "

        ''' <summary>
        ''' Internal structure, used for resource entries in the LFD file.
        ''' </summary>
        Private Structure Res_Item
            Dim resType As String   ' resource type string
            Dim resName As String   ' resource name string
            Dim size As Integer     ' size of the resource in bytes
            Dim offset As Integer   ' offset of the resource in the LFD
        End Structure

#End Region
#Region " --- Variables --- "

        ' source stream of the LFD resource
        Private srcStream As IO.Stream
        ' number of resources contained within the LFD
        Private numResources As Integer
        ' array storing the list of resources in the LFD
        Private resList As Res_Item()

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class using the provided stream.
        ''' </summary>
        ''' <param name="stream">The stream to create this instance from.</param>
        Private Sub createFromStream(stream As IO.Stream)
            Dim bRead As IO.BinaryReader
            Dim scratch As String
            Dim offsetCounter As Integer

            ' start reading from the file
            bRead = New IO.BinaryReader(stream)

            ' perform a few sanity checks first
            scratch = bRead.ReadChars(12)
            If scratch.Equals("RMAPresource") = False Then
                ' throw an exception
                Throw New IO.InvalidDataException(BAD_LFD_FILE_MESSAGE)
            End If

            ' get the number of resources
            offsetCounter = bRead.ReadUInt32
            numResources = offsetCounter >> 4
            ' push the offset counter past the header to the first item
            offsetCounter += &H20

            ' read the list of resources
            resList = Array.CreateInstance(GetType(Res_Item), numResources)

            For i As Integer = 0 To numResources - 1
                resList(i).resType = bRead.ReadChars(4)
                resList(i).resName = bRead.ReadChars(8)
                resList(i).size = bRead.ReadUInt32
                ' trim any trailing nulls from the strings
                resList(i).resType = resList(i).resType.Trim(vbNullChar)
                resList(i).resName = resList(i).resName.Trim(vbNullChar)
                resList(i).offset = offsetCounter
                ' push the offset counter to the next item's data
                offsetCounter += resList(i).size + &H10
            Next 'i

            ' retain the source stream
            srcStream = stream
        End Sub

        ''' <summary>
        ''' Creates a new instance of this class using a file.
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Sub New(fileName As String)
            Dim fStream As IO.FileStream

            ' handle argument exceptions
            If fileName Is Nothing Then Throw New ArgumentNullException("fileName")
            Dim fInfo As New IO.FileInfo(fileName)
            If Not fInfo.Exists Then Throw New IO.FileNotFoundException("File not found.", fileName)

            ' attempt to load the file
            fStream = New IO.FileStream(fileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

            createFromStream(fStream)
        End Sub

        ''' <summary>
        ''' Creates a new instance of this class using a stream.
        ''' NOTE: Do not close the source stream used to create this class!
        ''' </summary>
        ''' <param name="stream"></param>
        Public Sub New(stream As IO.Stream)
            ' handle argument exceptions
            If stream Is Nothing Then Throw New ArgumentNullException("stream")

            ' use the source stream
            createFromStream(stream)
        End Sub

#End Region
#Region " IDisposable Support "
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    ' close the source stream
                    srcStream.Dispose()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region


        ' returns the resource's index by its type and name
        Private Function getResourceIndex(resType As String, resName As String) As Integer
            ' loop through the loaded resources and return the index if a match is found
            For i As Integer = 0 To numResources - 1
                If resList(i).resType.Equals(resType) AndAlso resList(i).resName.Equals(resName) Then Return i
            Next 'i
            Return -1
        End Function

        ''' <summary>
        ''' Returns if the LFD contains a resource of the specified name and type.
        ''' </summary>
        ''' <param name="resType">Type code of the resource to check.</param>
        ''' <param name="resName">Name of the resource to check.</param>
        ''' <returns></returns>
        Public Function ContainsResource(resType As String, resName As String) As Boolean
            If getResourceIndex(resType, resName) = -1 Then Return False
            ' matches found, return true
            Return True
        End Function

        '' Loads a PLTT resource from the LFD, for now as a ColorPalette
        'Public Function LoadPLTT(resName As String) As ColorPalette
        '    Dim ret As ColorPalette = Nothing
        '    Dim resIndex As Integer
        '    Dim bRead As IO.BinaryReader
        '    Dim firstColor As Byte
        '    Dim lastColor As Byte
        '    Dim r As Byte, g As Byte, b As Byte

        '    ' make sure the resource exists first
        '    resIndex = getResourceIndex("PLTT", resName)
        '    If resIndex = -1 Then
        '        ' TODO: throw an exception
        '        Return Nothing
        '    End If

        '    ' seek to the resource's position
        '    srcStream.Seek(resList(resIndex).offset, IO.SeekOrigin.Begin)
        '    ' make a binary reader
        '    bRead = New IO.BinaryReader(srcStream)
        '    ' read the first and last color indices
        '    firstColor = bRead.ReadByte
        '    lastColor = bRead.ReadByte
        '    ' get the 'host' ColorPalette struct
        '    Dim dummyBmp As New Bitmap(1, 1, PixelFormat.Format8bppIndexed)
        '    ret = dummyBmp.Palette

        '    For i As Integer = firstColor To lastColor
        '        r = bRead.ReadByte
        '        g = bRead.ReadByte
        '        b = bRead.ReadByte
        '        ret.Entries(i) = Color.FromArgb(r, g, b)
        '    Next 'i

        '    Return ret
        'End Function

        ''' <summary>
        ''' Loads a Palette (PLTT) resource from the LFD.
        ''' </summary>
        ''' <param name="resName">Name of the resource to load.</param>
        ''' <returns></returns>
        Public Function LoadPalette(resName As String) As PaletteResource
            ' check arguments
            If resName Is Nothing Then Throw New ArgumentNullException("resName")

            Dim resIndex As Integer

            ' make sure the resource exists first
            resIndex = getResourceIndex("PLTT", resName)
            If resIndex = -1 Then
                ' throw an exception
                Throw New ArgumentException(String.Format(RESOURCE_NOT_FOUND_MESSAGE, resName, "PLTT"))
            End If

            ' seek to the resource's position
            srcStream.Seek(resList(resIndex).offset, IO.SeekOrigin.Begin)

            Return New PaletteResource(srcStream)
        End Function

        ''' <summary>
        ''' Loads an Animation (ANIM) resource from the LFD.
        ''' </summary>
        ''' <param name="resName">Name of the resource to load.</param>
        ''' <returns></returns>
        Public Function LoadAnimation(resName As String) As AnimationResource
            ' check arguments
            If resName Is Nothing Then Throw New ArgumentNullException("resName")

            Dim resIndex As Integer

            ' make sure the resource exists first
            resIndex = getResourceIndex("ANIM", resName)
            If resIndex = -1 Then
                ' throw an exception
                Throw New ArgumentException(String.Format(RESOURCE_NOT_FOUND_MESSAGE, resName, "ANIM"))
            End If

            ' seek to the resource's position
            srcStream.Seek(resList(resIndex).offset, IO.SeekOrigin.Begin)

            Return New AnimationResource(srcStream)
        End Function

        ''' <summary>
        ''' Loads a 3D Model (SHIP) resource from the LFD.
        ''' </summary>
        ''' <param name="resName">Name of the resource to load.</param>
        ''' <returns></returns>
        Public Function LoadModel(resName As String) As ModelResource
            ' check arguments
            If resName Is Nothing Then Throw New ArgumentNullException("resName")

            Dim resIndex As Integer

            ' make sure the resource exists first
            resIndex = getResourceIndex("SHIP", resName)
            If resIndex = -1 Then
                ' throw an exception
                Throw New ArgumentException(String.Format(RESOURCE_NOT_FOUND_MESSAGE, resName, "SHIP"))
            End If

            ' seek to the resource's position
            ' and then seek +4 more, because SHIP entries in LFD files contain extra bytes the game tends to ignore
            srcStream.Seek(resList(resIndex).offset + 4, IO.SeekOrigin.Begin)

            Return New ModelResource(srcStream)
        End Function

        '' Loads an ANIM resource from the LFD, for now as an array of bitmaps
        'Public Function LoadANIM(resName As String, center As Boolean) As Bitmap()
        '    Dim bmpFrame As Bitmap
        '    Dim ret As Bitmap()
        '    Dim resIndex As Integer
        '    Dim bRead As IO.BinaryReader
        '    Dim numFrames As Integer
        '    Dim animRECT As Rectangle
        '    Dim pixelData As Byte()
        '    Dim frame_xPos As Int16, frame_yPos As Int16, frame_width As Int16, frame_height As Int16

        '    ' make sure the resource exists first
        '    resIndex = getResourceIndex("ANIM", resName)
        '    If resIndex = -1 Then
        '        ' TODO: throw an exception
        '        Return Nothing
        '    End If

        '    ' seek to the resource's position
        '    srcStream.Seek(resList(resIndex).offset, IO.SeekOrigin.Begin)
        '    ' make a binary reader
        '    bRead = New IO.BinaryReader(srcStream)

        '    ' read the number of frames
        '    numFrames = bRead.ReadUInt16
        '    animRECT = New Rectangle(0, 0, 0, 0)

        '    ' iterate through all the frames to calculate the dimensions of the animation
        '    For i As Integer = 0 To numFrames - 1
        '        Dim frameSize As Integer = bRead.ReadInt32
        '        ' adjust the rect accordingly
        '        frame_xPos = bRead.ReadInt16
        '        frame_yPos = bRead.ReadInt16
        '        frame_width = bRead.ReadInt16 - frame_xPos + 1
        '        frame_height = bRead.ReadInt16 - frame_yPos + 1
        '        Dim frameRECT As New Rectangle(frame_xPos, frame_yPos, frame_width, frame_height)
        '        animRECT = Rectangle.Union(animRECT, frameRECT)

        '        ' seek to the next frame
        '        srcStream.Seek(frameSize - 8, IO.SeekOrigin.Current)
        '    Next 'i

        '    ' FIX: this is a fix for BITMAP stride issues
        '    animRECT.Width = ((animRECT.Width + 3) >> 2) << 2

        '    ret = Array.CreateInstance(GetType(Bitmap), numFrames)

        '    ' with the frame size calculated, create the bitmap(s)
        '    ' seek back to the resource's position, then past the header
        '    srcStream.Seek(resList(resIndex).offset + 2, IO.SeekOrigin.Begin)
        '    For j As Integer = 0 To numFrames - 1
        '        ' clear any extant pixel data
        '        pixelData = Array.CreateInstance(GetType(Byte), animRECT.Width * animRECT.Height)

        '        ' read past the frame size
        '        bRead.ReadInt32()
        '        '  read the frame dimensions
        '        frame_xPos = bRead.ReadInt16
        '        frame_yPos = bRead.ReadInt16
        '        frame_width = bRead.ReadInt16 - frame_xPos + 1
        '        frame_height = bRead.ReadInt16 - frame_yPos + 1

        '        ' enter the loop to render the frame
        '        Do
        '            ' read a WORD, stop drawing if it's == 0
        '            Dim spanCmd As UInt16 = bRead.ReadUInt16
        '            If spanCmd = 0 Then Exit Do
        '            Dim span_x As UInt16 = bRead.ReadUInt16
        '            Dim span_y As UInt16 = bRead.ReadUInt16
        '            Dim numPixels As UInt16 = spanCmd >> 1
        '            If center Then
        '                span_x = span_x - frame_xPos + ((animRECT.Width - frame_width) >> 1)
        '                span_y = span_y - frame_yPos + ((animRECT.Height - frame_height) >> 1)
        '            End If

        '            Dim offset As Integer = span_y * animRECT.Width + span_x
        '            ' handle the span command
        '            If (spanCmd And 1) = 0 Then
        '                ' span command is even, copy the next N bytes
        '                For i = 1 To numPixels
        '                    pixelData(offset) = bRead.ReadByte
        '                    offset += 1
        '                Next i
        '            Else
        '                ' span command is odd, there are N encoded pixels
        '                Do Until numPixels = 0
        '                    ' read a command byte
        '                    Dim cmdByte As Byte = bRead.ReadByte

        '                    ' handle the command byte
        '                    If (cmdByte And 1) = 0 Then
        '                        ' command byte is even, write the next N bytes
        '                        For i = 1 To cmdByte >> 1
        '                            pixelData(offset) = bRead.ReadByte
        '                            offset += 1
        '                        Next 'i
        '                    Else
        '                        ' command byte is odd, write the next byte N times
        '                        Dim c As Byte = bRead.ReadByte
        '                        For i = 1 To cmdByte >> 1
        '                            pixelData(offset) = c
        '                            offset += 1
        '                        Next 'i
        '                    End If
        '                    numPixels -= cmdByte >> 1
        '                Loop
        '            End If

        '        Loop

        '        ' make the bitmap
        '        bmpFrame = New Bitmap(animRECT.Width, animRECT.Height, PixelFormat.Format8bppIndexed)
        '        Dim bmpData As BitmapData = bmpFrame.LockBits(animRECT, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed)

        '        Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length)

        '        bmpFrame.UnlockBits(bmpData)

        '        ' add it to the list
        '        ret(j) = bmpFrame
        '    Next 'j


        '    Return ret
        'End Function

#End Region
#Region " --- Properties --- "



#End Region

    End Class

End Namespace