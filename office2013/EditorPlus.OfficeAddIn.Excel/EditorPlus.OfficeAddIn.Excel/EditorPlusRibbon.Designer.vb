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
        Me.IncrementActiveButton = Me.Factory.CreateRibbonButton
        Me.IncrementMaxButton = Me.Factory.CreateRibbonButton
        Me.InsertTextButton = Me.Factory.CreateRibbonButton
        Me.InsertSerialNumberButton = Me.Factory.CreateRibbonButton
        Me.InsertNowButton = Me.Factory.CreateRibbonButton
        Me.TrimEndButton = Me.Factory.CreateRibbonButton
        Me.FillViaHtmlColorButton = Me.Factory.CreateRibbonButton
        Me.NameGroup = Me.Factory.CreateRibbonGroup
        Me.ReplaceWorksheetNamesButton = Me.Factory.CreateRibbonButton
        Me.ClipboardGroup = Me.Factory.CreateRibbonGroup
        Me.CopyNoLineBreakTextButton = Me.Factory.CreateRibbonButton
        Me.CopyAsJsonButton = Me.Factory.CreateRibbonButton
        Me.WindowGroup = Me.Factory.CreateRibbonGroup
        Me.TopMostToggleButton = Me.Factory.CreateRibbonToggleButton
        Me.EditorPlus.SuspendLayout()
        Me.EditSelectionGroup.SuspendLayout()
        Me.NameGroup.SuspendLayout()
        Me.ClipboardGroup.SuspendLayout()
        Me.WindowGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlus
        '
        Me.EditorPlus.Groups.Add(Me.EditSelectionGroup)
        Me.EditorPlus.Groups.Add(Me.NameGroup)
        Me.EditorPlus.Groups.Add(Me.ClipboardGroup)
        Me.EditorPlus.Groups.Add(Me.WindowGroup)
        Me.EditorPlus.Label = "Editor Plus"
        Me.EditorPlus.Name = "EditorPlus"
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Items.Add(Me.IncrementButton)
        Me.EditSelectionGroup.Items.Add(Me.IncrementActiveButton)
        Me.EditSelectionGroup.Items.Add(Me.IncrementMaxButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertTextButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertSerialNumberButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertNowButton)
        Me.EditSelectionGroup.Items.Add(Me.TrimEndButton)
        Me.EditSelectionGroup.Items.Add(Me.FillViaHtmlColorButton)
        Me.EditSelectionGroup.Label = "Edit Selection"
        Me.EditSelectionGroup.Name = "EditSelectionGroup"
        '
        'IncrementButton
        '
        Me.IncrementButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.IncrementIcon
        Me.IncrementButton.KeyTip = "IN"
        Me.IncrementButton.Label = "Increment from Upper cell"
        Me.IncrementButton.Name = "IncrementButton"
        Me.IncrementButton.ScreenTip = "Increment from Upper cell"
        Me.IncrementButton.ShowImage = True
        Me.IncrementButton.SuperTip = "Increment the number from cell above. Target is most right value of the text."
        '
        'IncrementActiveButton
        '
        Me.IncrementActiveButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.IncrementActiveIcon
        Me.IncrementActiveButton.Label = "Increment Active cell / shape"
        Me.IncrementActiveButton.Name = "IncrementActiveButton"
        Me.IncrementActiveButton.ScreenTip = "Increment Active cells or shapes"
        Me.IncrementActiveButton.ShowImage = True
        '
        'IncrementMaxButton
        '
        Me.IncrementMaxButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.IncrementMaxIcon
        Me.IncrementMaxButton.Label = "Increment Max in Table column"
        Me.IncrementMaxButton.Name = "IncrementMaxButton"
        Me.IncrementMaxButton.ScreenTip = "Increment Max in Table column"
        Me.IncrementMaxButton.ShowImage = True
        '
        'InsertTextButton
        '
        Me.InsertTextButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.InsertText
        Me.InsertTextButton.Label = "Insert Text"
        Me.InsertTextButton.Name = "InsertTextButton"
        Me.InsertTextButton.ShowImage = True
        '
        'InsertSerialNumberButton
        '
        Me.InsertSerialNumberButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.InsertNumbers
        Me.InsertSerialNumberButton.Label = "Insert Serial Number"
        Me.InsertSerialNumberButton.Name = "InsertSerialNumberButton"
        Me.InsertSerialNumberButton.ShowImage = True
        '
        'InsertNowButton
        '
        Me.InsertNowButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.InsertNow
        Me.InsertNowButton.Label = "Insert Now"
        Me.InsertNowButton.Name = "InsertNowButton"
        Me.InsertNowButton.ShowImage = True
        '
        'TrimEndButton
        '
        Me.TrimEndButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.TrimEnd
        Me.TrimEndButton.Label = "Trim End"
        Me.TrimEndButton.Name = "TrimEndButton"
        Me.TrimEndButton.ShowImage = True
        '
        'FillViaHtmlColorButton
        '
        Me.FillViaHtmlColorButton.Label = "Fill via HTML color"
        Me.FillViaHtmlColorButton.Name = "FillViaHtmlColorButton"
        '
        'NameGroup
        '
        Me.NameGroup.Items.Add(Me.ReplaceWorksheetNamesButton)
        Me.NameGroup.Label = "Name"
        Me.NameGroup.Name = "NameGroup"
        '
        'ReplaceWorksheetNamesButton
        '
        Me.ReplaceWorksheetNamesButton.Label = "Replace Worksheet Names"
        Me.ReplaceWorksheetNamesButton.Name = "ReplaceWorksheetNamesButton"
        '
        'ClipboardGroup
        '
        Me.ClipboardGroup.Items.Add(Me.CopyNoLineBreakTextButton)
        Me.ClipboardGroup.Items.Add(Me.CopyAsJsonButton)
        Me.ClipboardGroup.Label = "Clipboard"
        Me.ClipboardGroup.Name = "ClipboardGroup"
        '
        'CopyNoLineBreakTextButton
        '
        Me.CopyNoLineBreakTextButton.Label = "Copy No Line break Text"
        Me.CopyNoLineBreakTextButton.Name = "CopyNoLineBreakTextButton"
        '
        'CopyAsJsonButton
        '
        Me.CopyAsJsonButton.Label = "Copy Table As JSON"
        Me.CopyAsJsonButton.Name = "CopyAsJsonButton"
        '
        'WindowGroup
        '
        Me.WindowGroup.Items.Add(Me.TopMostToggleButton)
        Me.WindowGroup.Label = "Window"
        Me.WindowGroup.Name = "WindowGroup"
        '
        'TopMostToggleButton
        '
        Me.TopMostToggleButton.Image = Global.EditorPlus.OfficeAddIn.Excel.My.Resources.Resources.AlwaysOnTop
        Me.TopMostToggleButton.Label = "Always on Top"
        Me.TopMostToggleButton.Name = "TopMostToggleButton"
        Me.TopMostToggleButton.ScreenTip = "Always on Top"
        Me.TopMostToggleButton.ShowImage = True
        Me.TopMostToggleButton.SuperTip = "Keep this window on top. Always."
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
        Me.NameGroup.ResumeLayout(False)
        Me.NameGroup.PerformLayout()
        Me.ClipboardGroup.ResumeLayout(False)
        Me.ClipboardGroup.PerformLayout()
        Me.WindowGroup.ResumeLayout(False)
        Me.WindowGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EditorPlus As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents EditSelectionGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents InsertTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents IncrementButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents InsertNowButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents ClipboardGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents CopyNoLineBreakTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents IncrementActiveButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents IncrementMaxButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents TrimEndButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents InsertSerialNumberButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents WindowGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents TopMostToggleButton As Microsoft.Office.Tools.Ribbon.RibbonToggleButton
    Friend WithEvents NameGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents ReplaceWorksheetNamesButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents CopyAsJsonButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents FillViaHtmlColorButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
