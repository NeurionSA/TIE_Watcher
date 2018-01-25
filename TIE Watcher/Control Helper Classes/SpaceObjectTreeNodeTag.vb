Imports TIE_Watcher.TIE

''' <summary>
''' A class for holding additional info about a node in the SpaceObject TreeView.
''' </summary>
Public Class SpaceObjectTreeNodeTag

#Region " --- Variables --- "

    ' Node's type
    Private _nodeType As SpaceObjectTreeNodeType
    ' SpaceObject type of the SpaceObject the node represents
    Private _objectType As Byte
    ' Object Category of the SpaceObject the node represents
    Private _objectCategory As SpaceObjectCategory
    ' IFF code of SpaceObject the node represents
    Private _IFF As Byte
    ' Index of the SpaceObject the node represents
    Private _index As Integer
    ' Node's Text
    Private _text As String

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New(nodeType As SpaceObjectTreeNodeType, Optional index As Integer = -1)
        Me._nodeType = nodeType
        Me._index = index
    End Sub

#End Region

#End Region
#Region " --- Properties --- "

    Public ReadOnly Property NodeType As SpaceObjectTreeNodeType
        Get
            Return Me._nodeType
        End Get
    End Property

    Public Property IFF As Byte
        Get
            Return Me._IFF
        End Get
        Set(value As Byte)
            Me._IFF = value
        End Set
    End Property

    Public Property Index As Integer
        Get
            Return Me._index
        End Get
        Set(value As Integer)
            Me._index = value
        End Set
    End Property

    Public Property ObjectType As Byte
        Get
            Return Me._objectType
        End Get
        Set(value As Byte)
            Me._objectType = value
        End Set
    End Property

    Public Property Category As SpaceObjectCategory
        Get
            Return Me._objectCategory
        End Get
        Set(value As SpaceObjectCategory)
            Me._objectCategory = value
        End Set
    End Property

    Public Property Text As String
        Get
            Return Me._text
        End Get
        Set(value As String)
            Me._text = value
        End Set
    End Property

#End Region
End Class

''' <summary>
''' Enumeration for SpaceObject Tree Node types.
''' </summary>
Public Enum SpaceObjectTreeNodeType
    ''' <summary>
    ''' The node is structural (category heading) and has no relevant tag data.
    ''' </summary>
    Category
    ''' <summary>
    ''' The node is an item that contains relevant tag data.
    ''' </summary>
    Item
End Enum