''' <summary>
''' Basic class to use for all ComboBox items that support an icon and text.
''' </summary>
Public Class ComboBoxItem

#Region " --- Variables --- "

    Private _icon As Bitmap
    Private _text As String

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New(text As String, icon As Bitmap)
        Me._text = text
        Me._icon = icon
    End Sub

#End Region

    Public Overrides Function ToString() As String
        Return Me._text
    End Function

#End Region
#Region " --- Properties --- "

    Public Property Text As String
        Get
            Return Me._text
        End Get
        Set(value As String)
            Me._text = value
        End Set
    End Property

    Public Property Icon As Bitmap
        Get
            Return Me._icon
        End Get
        Set(value As Bitmap)
            Me._icon = value
        End Set
    End Property

#End Region
End Class
