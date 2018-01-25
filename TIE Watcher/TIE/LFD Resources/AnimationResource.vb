Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Namespace TIE

    ''' <summary>
    ''' Class that encapsulates a TIE Fighter Animation (ANIM) Resource.
    ''' </summary>
    Public Class AnimationResource
        Implements IDisposable

#Region " --- Constants --- "

        ' exception message for bad animation data
        Private Const BAD_ANIMATION_DATA_MESSAGE As String = "Animation Resource data is invalid."

#End Region
#Region " --- Variables --- "

        ' the number of frames in the animation
        Private _numFrames As Integer
        ' the rectangle indicating the bounds of the entire animation
        Private _animBounds As Rectangle
        ' array of Rectangles indicating the bounds of each frame
        Private _frameBounds() As Rectangle
        ' array of 256-color Bitmaps representing each frame of the animation
        Private _bmpFrames() As Bitmap

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class using the data in the provided stream.
        ''' Only accessible from within this assembly, specifically meant to be accessed by an instance of the LFD class.
        ''' </summary>
        ''' <param name="srcStream"></param>
        Friend Sub New(srcStream As IO.Stream)
            ' check arguments
            If srcStream Is Nothing Then Throw New ArgumentNullException("srcStream")

            Dim frame_xPos, frame_yPos, frame_width, frame_height As Int16
            ' remember the starting position of the resource
            Dim resStartPosition As Long = srcStream.Position

            ' create the binary reader
            Dim bRead As New IO.BinaryReader(srcStream)

            ' read the number of frames
            _numFrames = bRead.ReadUInt16

            ' throw an exception if animation length is bad
            If _numFrames <= 0 Then Throw New IO.InvalidDataException(BAD_ANIMATION_DATA_MESSAGE)

            ' create the array of frame bounds
            _frameBounds = Array.CreateInstance(GetType(Rectangle), _numFrames)
            ' create the array of frame bitmaps
            _bmpFrames = Array.CreateInstance(GetType(Bitmap), _numFrames)

            ' iterate through all the frames to calculate the dimensions of the animation
            '_animBounds = New Rectangle(0, 0, 0, 0)
            For i As Integer = 0 To _numFrames - 1
                ' read the byte length of the frame
                Dim frameSize As Integer = bRead.ReadInt32

                ' adjust the bounds accordingly
                frame_xPos = bRead.ReadInt16
                frame_yPos = bRead.ReadInt16
                frame_width = bRead.ReadInt16 - frame_xPos + 1
                frame_height = bRead.ReadInt16 - frame_yPos + 1

                ' record the bounds of the frame
                _frameBounds(i) = New Rectangle(frame_xPos, frame_yPos, frame_width, frame_height)
                If i = 0 Then
                    ' used the bounds of the first frame
                    _animBounds = _frameBounds(i)
                Else
                    ' perform a union on subsequent frames
                    _animBounds = Rectangle.Union(_animBounds, _frameBounds(i))
                End If

                ' seek to the next frame
                srcStream.Seek(frameSize - 8, IO.SeekOrigin.Current)
            Next

            ' adjust animation width to account for Bitmap object stride issues
            ' TODO: Handle cases where the animation's bounds do not start at 0, 0
            Dim strideWidth As Integer = ((_animBounds.Width + 3) >> 2) << 2

            ' with the frame size calculated, create the bitmap(s)
            ' seek back to the resource's position, then past the header
            srcStream.Seek(resStartPosition + 2, IO.SeekOrigin.Begin)
            For frameIndex As Integer = 0 To _numFrames - 1
                ' create the array of pixel data
                Dim pixelData As Byte() = Array.CreateInstance(GetType(Byte), strideWidth * _animBounds.Height)

                ' read past the frame size
                bRead.ReadInt32()
                '  read the frame dimensions
                frame_xPos = bRead.ReadInt16
                frame_yPos = bRead.ReadInt16
                frame_width = bRead.ReadInt16 - frame_xPos + 1
                frame_height = bRead.ReadInt16 - frame_yPos + 1

                ' enter the loop to render the frame
                Do
                    ' read a WORD, stop drawing if it's == 0
                    Dim spanCmd As UInt16 = bRead.ReadUInt16
                    If spanCmd = 0 Then Exit Do

                    Dim span_x As UInt16 = bRead.ReadUInt16
                    Dim span_y As UInt16 = bRead.ReadUInt16
                    Dim numPixels As UInt16 = spanCmd >> 1
                    'If center Then
                    '    span_x = span_x - frame_xPos + ((animRECT.Width - frame_width) >> 1)
                    '    span_y = span_y - frame_yPos + ((animRECT.Height - frame_height) >> 1)
                    'End If

                    Dim offset As Integer = span_y * strideWidth + span_x
                    ' handle the span command
                    If (spanCmd And 1) = 0 Then
                        ' span command is even, copy the next N bytes
                        For i = 1 To numPixels
                            pixelData(offset) = bRead.ReadByte
                            offset += 1
                        Next i
                    Else
                        ' span command is odd, there are N encoded pixels
                        Do Until numPixels = 0
                            ' read a command byte
                            Dim cmdByte As Byte = bRead.ReadByte

                            ' handle the command byte
                            If (cmdByte And 1) = 0 Then
                                ' command byte is even, write the next N bytes
                                For i = 1 To cmdByte >> 1
                                    pixelData(offset) = bRead.ReadByte
                                    offset += 1
                                Next 'i
                            Else
                                ' command byte is odd, write the next byte N times
                                Dim c As Byte = bRead.ReadByte
                                For i = 1 To cmdByte >> 1
                                    pixelData(offset) = c
                                    offset += 1
                                Next 'i
                            End If
                            numPixels -= cmdByte >> 1
                        Loop
                    End If

                Loop

                ' make the bitmap
                Dim bmpFrame As Bitmap = New Bitmap(_animBounds.Width, _animBounds.Height, PixelFormat.Format8bppIndexed)
                Dim bmpData As BitmapData = bmpFrame.LockBits(_animBounds, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed)

                Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length)

                bmpFrame.UnlockBits(bmpData)

                ' add it to the list
                _bmpFrames(frameIndex) = bmpFrame
            Next

            ' that should do it
        End Sub

#End Region
#Region " IDisposable Support "
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    ' dispose of all bitmap objects
                    For i As Integer = 0 To _numFrames - 1
                        _bmpFrames(i).Dispose()
                        _bmpFrames(i) = Nothing
                    Next
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

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property FrameCount As Integer
            Get
                Return _numFrames
            End Get
        End Property

        Public ReadOnly Property AnimationBounds As Rectangle
            Get
                ' return a copy so the original cannot be modified
                Return New Rectangle(_animBounds.Location, _animBounds.Size)
            End Get
        End Property

        Public ReadOnly Property FrameBounds(index As Integer) As Rectangle
            Get
                ' check arguments
                If index < 0 Or index >= _numFrames Then Throw New ArgumentOutOfRangeException("index")

                ' return a copy so the original cannot be modified
                Return New Rectangle(_frameBounds(index).Location, _frameBounds(index).Size)
            End Get
        End Property

        ''' <summary>
        ''' Gets one of the animation's frames drawn with the specified palette and rendering options.
        ''' </summary>
        ''' <param name="index">Index of the frame.</param>
        ''' <param name="palette">PaletteResource object whose colors will be used to render the frame.</param>
        ''' <param name="transparent">Whether or not the frame should be rendered with color index 0 as transparent.</param>
        ''' <param name="centered">Whether the frame should be re-centered or left as-is.</param>
        ''' <returns></returns>
        Public ReadOnly Property Frames(index As Integer, palette As PaletteResource, transparent As Boolean, centered As Boolean) As Bitmap
            Get
                ' check arguments
                If index < 0 Or index >= _numFrames Then Throw New ArgumentOutOfRangeException("index")
                If palette Is Nothing Then Throw New ArgumentNullException("palette")

                ' create a new bitmap object
                Dim bmpReturn As New Bitmap(_animBounds.Width, _animBounds.Height)

                ' change the colors of the frame to those of the palette
                Dim framePal As ColorPalette = _bmpFrames(index).Palette
                For i As Integer = 0 To 255
                    framePal.Entries(i) = palette.Colors(i)
                Next
                ' handle color transparency
                If transparent Then
                    framePal.Entries(0) = Color.Transparent
                End If
                _bmpFrames(index).Palette = framePal

                ' create a graphics object to draw to the return bitmap
                Dim g As Graphics = Graphics.FromImage(bmpReturn)

                ' clear the bitmap
                If transparent Then
                    g.Clear(Color.Transparent)
                Else
                    g.Clear(Color.Black)
                End If

                ' draw the bitmap
                If centered Then
                    ' draw the bitmap offset so the frame is centered
                    g.DrawImage(_bmpFrames(index),
                                ((_animBounds.Width - _frameBounds(index).Width) >> 1) - _frameBounds(index).Left,
                                ((_animBounds.Height - _frameBounds(index).Height) >> 1) - _frameBounds(index).Top)
                Else
                    ' draw the bitmap as normal
                    g.DrawImage(_bmpFrames(index), 0, 0)
                End If

                ' dispose of the graphics object
                g.Dispose()

                Return bmpReturn
            End Get
        End Property

#End Region

    End Class

End Namespace
