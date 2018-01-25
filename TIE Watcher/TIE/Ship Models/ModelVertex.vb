Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Model Vertex.
    ''' </summary>
    Public Class ModelVertex

#Region " --- Variables --- "

        ' vertex's actual coordinates
        Friend _point As Point3D
        ' vertex's normal
        Friend _normal As Point3D

#End Region
#Region " --- Methods --- "

#Region " --- Constructors --- "

        Friend Sub New()
            ' nothing special
        End Sub

#End Region

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property Point As Point3D
            Get
                ' return a clone so the internal one cannot be transformed
                Return _point.Clone
            End Get
        End Property

        Public ReadOnly Property Normal As Point3D
            Get
                ' return a clone so the internal one cannot be transformed
                Return _normal.Clone
            End Get
        End Property

#End Region

    End Class

End Namespace
