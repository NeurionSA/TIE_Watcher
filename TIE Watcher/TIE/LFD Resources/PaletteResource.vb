Namespace TIE

    ''' <summary>
    ''' Class that encapsulates a TIE Fighter Palette (PLTT) Resource.
    ''' </summary>
    Public Class PaletteResource

#Region " --- Constants --- "

        ' exception message for bad palette data
        Private Const BAD_PALETTE_DATA_MESSAGE As String = "Palette Resource data is invalid."

#End Region
#Region " --- Variables --- "

        ' index of the first color this palette applies to
        Private _firstColorIndex As Integer
        ' index of the last color this palette applies to
        Private _lastColorIndex As Integer
        ' number of colors this palette covers
        Private _numColors As Integer
        ' array of 256 colors that represent the colors in this palette, default to black for unused colors
        Private _colors() As Color

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ' initializes the color array to its default state
        Private Sub initColorArray()
            ' create the array
            _colors = Array.CreateInstance(GetType(Color), 256)
            ' paint it black
            For i As Integer = 0 To 255
                _colors(i) = Color.Black
            Next
        End Sub

        ''' <summary>
        ''' Creates a new instance of this class using the data in the provided stream.
        ''' Only accessible from within this assembly, specifically meant to be accessed by an instance of the LFD class.
        ''' </summary>
        ''' <param name="srcStream"></param>
        Friend Sub New(srcStream As IO.Stream)
            ' check arguments
            If srcStream Is Nothing Then Throw New ArgumentNullException("srcStream")

            ' initialize the color array
            initColorArray()

            ' create the binary reader
            Dim bRead As New IO.BinaryReader(srcStream)

            ' read the first and last color index
            _firstColorIndex = bRead.ReadByte
            _lastColorIndex = bRead.ReadByte
            _numColors = _lastColorIndex - _firstColorIndex + 1

            ' sanity check the values we just read
            If _numColors <= 0 Then
                Throw New IO.InvalidDataException(BAD_PALETTE_DATA_MESSAGE)
            End If

            ' read the color entries in
            For i As Integer = _firstColorIndex To _lastColorIndex
                Dim r, g, b As Byte

                r = bRead.ReadByte
                g = bRead.ReadByte
                b = bRead.ReadByte
                _colors(i) = Color.FromArgb(r, g, b)
            Next

            ' success
        End Sub

#End Region

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property FirstColorIndex As Integer
            Get
                Return _firstColorIndex
            End Get
        End Property

        Public ReadOnly Property LastColorIndex As Integer
            Get
                Return _lastColorIndex
            End Get
        End Property

        Public ReadOnly Property ColorCount As Integer
            Get
                Return _numColors
            End Get
        End Property

        Public ReadOnly Property Colors(index As Integer) As Color
            Get
                ' check arguments
                ' for now we'll allow getting any color's index
                If index < 0 Or index >= 256 Then Throw New ArgumentOutOfRangeException("index")

                Return _colors(index)
            End Get
        End Property

#End Region

    End Class

End Namespace