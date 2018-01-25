''' <summary>
''' Class for use in the FormModelBrowser craft selection combo box.
''' </summary>
Public Class ModelBrowserComboItem

#Region " --- Variables --- "

    Private _text As String
    Private _spaceObjectIndex As Integer

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New(text As String, spaceObjectIndex As Integer)
        Me._text = text
        Me._spaceObjectIndex = spaceObjectIndex
    End Sub

#End Region

    Public Overrides Function ToString() As String
        Return Me._text
    End Function

#End Region
#Region " --- Properties --- "

    Public ReadOnly Property Text As String
        Get
            Return Me._text
        End Get

    End Property

    Public ReadOnly Property SpaceObjectIndex As Integer
        Get
            Return Me._spaceObjectIndex
        End Get
    End Property

#End Region

End Class
