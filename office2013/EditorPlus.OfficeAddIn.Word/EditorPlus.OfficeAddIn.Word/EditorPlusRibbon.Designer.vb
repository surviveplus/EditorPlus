Partial Class EditorPlusRibbon
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.EditorPlusTab = Me.Factory.CreateRibbonTab
        Me.EditSelectionGroup = Me.Factory.CreateRibbonGroup
        Me.InsertTextButton = Me.Factory.CreateRibbonButton
        Me.EditorPlusTab.SuspendLayout()
        Me.EditSelectionGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlusTab
        '
        Me.EditorPlusTab.Groups.Add(Me.EditSelectionGroup)
        Me.EditorPlusTab.Label = "Editor Plus"
        Me.EditorPlusTab.Name = "EditorPlusTab"
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Items.Add(Me.InsertTextButton)
        Me.EditSelectionGroup.Label = "Edit Selection"
        Me.EditSelectionGroup.Name = "EditSelectionGroup"
        '
        'InsertTextButton
        '
        Me.InsertTextButton.Image = Global.EditorPlus.OfficeAddIn.Word.My.Resources.Resources.InsertText
        Me.InsertTextButton.Label = "Insert Text"
        Me.InsertTextButton.Name = "InsertTextButton"
        Me.InsertTextButton.ShowImage = True
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = "Microsoft.Word.Document"
        Me.Tabs.Add(Me.EditorPlusTab)
        Me.EditorPlusTab.ResumeLayout(False)
        Me.EditorPlusTab.PerformLayout()
        Me.EditSelectionGroup.ResumeLayout(False)
        Me.EditSelectionGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EditorPlusTab As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents EditSelectionGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents InsertTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
