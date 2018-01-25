
''' <summary>
''' A class for holding additional info about a node in the ModelBrowser TreeView.
''' </summary>
Public Class ModelBrowserTreeNodeTag

#Region " --- Variables --- "

    ' Node's type
    Private _nodeType As ModelBrowserTreeNodeType
    ' Node's data object (i.e. ModelHeader, ModelPart, etc.)
    Private _nodeData As Object

#End Region
#Region " --- Methods --- "
#Region " Constructors "

    Public Sub New(nodeType As ModelBrowserTreeNodeType)
        Me._nodeType = nodeType
    End Sub

#End Region
#Region " --- Properties --- "

    Public ReadOnly Property NodeType As ModelBrowserTreeNodeType
        Get
            Return Me._nodeType
        End Get
    End Property

    Public Property NodeData As Object
        Get
            Return _nodeData
        End Get
        Set(value As Object)
            _nodeData = value
        End Set
    End Property

#End Region

#End Region
End Class

''' <summary>
''' Enumeration for SpaceObject Tree Node types.
''' </summary>
Public Enum ModelBrowserTreeNodeType
    ''' <summary>
    ''' The node is structural (i.e. 'Parts' heading) and has no relevant tag data.
    ''' </summary>
    Category
    ''' <summary>
    ''' The node represents Model Header information.
    ''' </summary>
    Header
    ''' <summary>
    ''' The node represents Model Part information.
    ''' </summary>
    Part
    ''' <summary>
    ''' The node represents a ModelMesh within a ModelPart.
    ''' </summary>
    Mesh
End Enum