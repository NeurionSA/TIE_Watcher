Imports TIE_Watcher.TIE

Public Class FormModelBrowser

#Region " --- Variables --- "

    ' the 3 LFD files we need for this
    Private lfdSpecies() As LFD

    ' the model currently being viewed
    Private model As ModelResource

    ' the model browser header info control
    Private headerPanel As ModelHeaderPanel
    Private partHeaderPanel As ModelPartHeaderPanel
    Private meshPanel As ModelMeshPanel

    ' the form for rendering models
    Private frmModelRender As FormModelRender
    Private frmModelVector As FormModelVectorize

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' create the Header Info panel and add it to the Element Information container
        headerPanel = New ModelHeaderPanel
        ' dock to fill the container
        headerPanel.Dock = DockStyle.Fill
        ' hide the control
        headerPanel.Visible = False
        grpElementInfo.Controls.Add(headerPanel)

        partHeaderPanel = New ModelPartHeaderPanel
        partHeaderPanel.Dock = DockStyle.Fill
        partHeaderPanel.Visible = False
        grpElementInfo.Controls.Add(partHeaderPanel)

        meshPanel = New ModelMeshPanel
        meshPanel.Dock = DockStyle.Fill
        meshPanel.Visible = False
        grpElementInfo.Controls.Add(meshPanel)

        ' open the 3 SPECIES.LFD files
        lfdSpecies = {
            New LFD("C:\Data Alpha\DOSBOX\GAMES\TIECD\RES640\SPECIES.LFD"),
            New LFD("C:\Data Alpha\DOSBOX\GAMES\TIECD\RES640\SPECIES2.LFD"),
            New LFD("C:\Data Alpha\DOSBOX\GAMES\TIECD\RES640\SPECIES3.LFD")}

        ' loop through all of the SpaceObjectTypeInfos and add objets that have models to the dropdown
        For i As Integer = 0 To SpaceObjectTypeInfo.Count - 1
            Dim soInfo As SpaceObjectTypeInfo = SpaceObjectTypeInfo.Types(i)

            If soInfo.SpeciesFileIndex <> -1 Then
                Dim item As New ModelBrowserComboItem(soInfo.Name, i)

                ' add an item to the dropdown
                cmbModelSelect.Items.Add(item)
            End If
        Next

        ' select the first item in the dropdown
        cmbModelSelect.SelectedIndex = 0
    End Sub

#End Region
#Region " Event-Handlers "

    Private Sub cmdTestRender_Click(sender As Object, e As EventArgs) Handles cmdTestRender.Click
        If frmModelRender Is Nothing Then
            frmModelRender = New FormModelRender
        End If
        Dim tag As ModelBrowserTreeNodeTag = DirectCast(tvModelElements.SelectedNode.Tag, ModelBrowserTreeNodeTag)

        Select Case tag.NodeType
            Case ModelBrowserTreeNodeType.Header
                ' render the whole model
                frmModelRender.RenderModel(model)
            Case ModelBrowserTreeNodeType.Part
                Dim part As ModelPart = DirectCast(tag.NodeData, ModelPart)
                ' draw the first mesh in the part
                frmModelRender.RenderModelMesh(model, part.Meshes(0))
            Case ModelBrowserTreeNodeType.Mesh
                Dim mesh As ModelMesh = DirectCast(tag.NodeData, ModelMesh)
                ' draw the mesh
                frmModelRender.RenderModelMesh(model, mesh)
            Case Else
                MsgBox("Please select a mesh first.", MsgBoxStyle.Exclamation)
        End Select

    End Sub

    Private Sub cmdVector_Click(sender As Object, e As EventArgs) Handles cmdVector.Click
        If frmModelVector Is Nothing Then
            frmModelVector = New FormModelVectorize
        End If

        Dim tag As ModelBrowserTreeNodeTag = DirectCast(tvModelElements.SelectedNode.Tag, ModelBrowserTreeNodeTag)

        frmModelVector.VectorizeModel(model)
        'Select Case tag.NodeType
        '    Case Else
        '        MsgBox("Please select a model first.", MsgBoxStyle.Exclamation)
        'End Select
    End Sub

    Private Sub cmbModelSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbModelSelect.SelectedIndexChanged
        Dim item As ModelBrowserComboItem = DirectCast(cmbModelSelect.SelectedItem, ModelBrowserComboItem)
        ' load the new model
        Dim soInfo As SpaceObjectTypeInfo = SpaceObjectTypeInfo.Types(item.SpaceObjectIndex)

        model = lfdSpecies(soInfo.SpeciesFileIndex).LoadModel(soInfo.SpeciesName)

        ' populate the model element tree
        populateModelElementTree()

        ' select the first item (header)
        tvModelElements.SelectedNode = tvModelElements.Nodes.Item(0)
    End Sub

    Private Sub tvModelElements_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvModelElements.AfterSelect
        Dim tag As ModelBrowserTreeNodeTag = DirectCast(e.Node.Tag, ModelBrowserTreeNodeTag)

        ' behave based on the node type
        Select Case tag.NodeType
            Case ModelBrowserTreeNodeType.Header
                ' make the header panel visible and populate its information
                headerPanel.Visible = True
                partHeaderPanel.Visible = False
                meshPanel.Visible = False
                headerPanel.UpdateDisplay(tag.NodeData)

            Case ModelBrowserTreeNodeType.Part
                headerPanel.Visible = False
                partHeaderPanel.Visible = True
                meshPanel.Visible = False
                partHeaderPanel.UpdateDisplay(tag.NodeData)

            Case ModelBrowserTreeNodeType.Mesh
                headerPanel.Visible = False
                partHeaderPanel.Visible = False
                meshPanel.Visible = True
                meshPanel.updatedisplay(tag.NodeData)

        End Select
    End Sub

#End Region

    ' populates the model element tree with the currently loaded model's data
    Private Sub populateModelElementTree()
        Dim node As TreeNode
        Dim tag As ModelBrowserTreeNodeTag

        ' stop update flicker as we build the tree
        tvModelElements.BeginUpdate()

        ' delete the entire tree
        tvModelElements.Nodes.Clear()

        ' create the first node, the header
        node = New TreeNode("Header")
        tag = New ModelBrowserTreeNodeTag(ModelBrowserTreeNodeType.Header)
        tag.NodeData = model.Header
        node.Tag = tag
        ' add the header node
        tvModelElements.Nodes.Add(node)

        ' TODO: LOD Tree

        ' create the parent node for parts
        node = New TreeNode("Parts")
        tag = New ModelBrowserTreeNodeTag(ModelBrowserTreeNodeType.Category)
        node.Tag = tag
        ' iterate through all the children parts and add them
        For partIndex As Integer = 0 To model.Header.PartCount - 1
            Dim partNode As New TreeNode(String.Format(
                                         "Part {0} - {1}",
                                         partIndex,
                                         model.Parts(partIndex).PartType.ToString))
            tag = New ModelBrowserTreeNodeTag(ModelBrowserTreeNodeType.Part)
            tag.NodeData = model.Parts(partIndex)
            partNode.Tag = tag

            ' iterate through all the children meshes and add them
            For meshIndex As Integer = 0 To model.Parts(partIndex).MeshCount - 1
                Dim meshNode As New TreeNode(String.Format(
                                             "Mesh {0}",
                                             meshIndex))
                tag = New ModelBrowserTreeNodeTag(ModelBrowserTreeNodeType.Mesh)
                tag.NodeData = model.Parts(partIndex).Meshes(meshIndex)

                meshNode.Tag = tag
                ' add it to the parent node
                partNode.Nodes.Add(meshNode)
            Next

            ' add it to the parent node
            node.Nodes.Add(partNode)
            Next
            ' add the parts node and expand it
            tvModelElements.Nodes.Add(node)
        node.Expand()

        ' allow the tree to update itself
        tvModelElements.EndUpdate()
    End Sub

#End Region

End Class