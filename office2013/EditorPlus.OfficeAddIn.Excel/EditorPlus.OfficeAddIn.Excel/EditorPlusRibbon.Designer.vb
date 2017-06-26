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
        Me.EditorPlus = Me.Factory.CreateRibbonTab
        Me.EditSelectionGroup = Me.Factory.CreateRibbonGroup
        Me.IncrementButton = Me.Factory.CreateRibbonButton
        Me.InsertTextButton = Me.Factory.CreateRibbonButton
        Me.InsertNowButton = Me.Factory.CreateRibbonButton
        Me.EditorPlus.SuspendLayout()
        Me.EditSelectionGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlus
        '
        Me.EditorPlus.Groups.Add(Me.EditSelectionGroup)
        Me.EditorPlus.Label = "Editor Plus"
        Me.EditorPlus.Name = "EditorPlus"
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Items.Add(Me.IncrementButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertTextButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertNowButton)
        Me.EditSelectionGroup.Label = "Edit Selection"
        Me.EditSelectionGroup.Name = "EditSelectionGroup"
        '
        'IncrementButton
        '
        Me.IncrementButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.IncrementIcon
        Me.IncrementButton.KeyTip = "IN"
        Me.IncrementButton.Label = "Increment from cell above"
        Me.IncrementButton.Name = "IncrementButton"
        Me.IncrementButton.ScreenTip = "Increment from cell above"
        Me.IncrementButton.ShowImage = True
        Me.IncrementButton.SuperTip = "Increment the number from cell above. Target is most right value of the text."
        '
        'InsertTextButton
        '
        Me.InsertTextButton.Label = "Insert Text"
        Me.InsertTextButton.Name = "InsertTextButton"
        '
        'InsertNowButton
        '
        Me.InsertNowButton.Label = "Insert Now"
        Me.InsertNowButton.Name = "InsertNowButton"
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = "Microsoft.Excel.Workbook"
        Me.Tabs.Add(Me.EditorPlus)
        Me.EditorPlus.ResumeLayout(False)
        Me.EditorPlus.PerformLayout()
        Me.EditSelectionGroup.ResumeLayout(False)
        Me.EditSelectionGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EditorPlus As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents EditSelectionGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents InsertTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents IncrementButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents InsertNowButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
