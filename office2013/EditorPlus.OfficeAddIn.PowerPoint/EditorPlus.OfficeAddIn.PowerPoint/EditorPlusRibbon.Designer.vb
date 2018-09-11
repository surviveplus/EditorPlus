Partial Class EditorPlusRibbon
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Windows.Forms クラス作成デザイナーのサポートに必要です
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        'この呼び出しは、コンポーネント デザイナーで必要です。
        InitializeComponent()

    End Sub

    'Component は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'コンポーネント デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャはコンポーネント デザイナーで必要です。
    'コンポーネント デザイナーを使って変更できます。
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.EditorPlusTab = Me.Factory.CreateRibbonTab
        Me.EditSelectionGroup = Me.Factory.CreateRibbonGroup
        Me.InsertTextButton = Me.Factory.CreateRibbonButton
        Me.InsertSerialNumberButton = Me.Factory.CreateRibbonButton
        Me.NameGroup = Me.Factory.CreateRibbonGroup
        Me.ReplaceObjectNamesButton = Me.Factory.CreateRibbonButton
        Me.ClipboardGroup = Me.Factory.CreateRibbonGroup
        Me.CopyTextButton = Me.Factory.CreateRibbonButton
        Me.CopyNoLineBreakTextButton = Me.Factory.CreateRibbonButton
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.TopMostToggleButton = Me.Factory.CreateRibbonToggleButton
        Me.EditorPlusTab.SuspendLayout()
        Me.EditSelectionGroup.SuspendLayout()
        Me.NameGroup.SuspendLayout()
        Me.ClipboardGroup.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlusTab
        '
        Me.EditorPlusTab.Groups.Add(Me.EditSelectionGroup)
        Me.EditorPlusTab.Groups.Add(Me.NameGroup)
        Me.EditorPlusTab.Groups.Add(Me.ClipboardGroup)
        Me.EditorPlusTab.Groups.Add(Me.Group1)
        Me.EditorPlusTab.Label = "Editor Plus"
        Me.EditorPlusTab.Name = "EditorPlusTab"
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Items.Add(Me.InsertTextButton)
        Me.EditSelectionGroup.Items.Add(Me.InsertSerialNumberButton)
        Me.EditSelectionGroup.Label = "Edit Selection"
        Me.EditSelectionGroup.Name = "EditSelectionGroup"
        '
        'InsertTextButton
        '
        Me.InsertTextButton.Image = Global.EditorPlus.OfficeAddIn.PowerPoint.My.Resources.Resources.InsertText
        Me.InsertTextButton.Label = "Insert Text"
        Me.InsertTextButton.Name = "InsertTextButton"
        Me.InsertTextButton.ShowImage = True
        '
        'InsertSerialNumberButton
        '
        Me.InsertSerialNumberButton.Image = Global.EditorPlus.OfficeAddIn.PowerPoint.My.Resources.Resources.InsertNumbers
        Me.InsertSerialNumberButton.Label = "Insert Serial Number"
        Me.InsertSerialNumberButton.Name = "InsertSerialNumberButton"
        Me.InsertSerialNumberButton.ShowImage = True
        '
        'NameGroup
        '
        Me.NameGroup.Items.Add(Me.ReplaceObjectNamesButton)
        Me.NameGroup.Label = "Name"
        Me.NameGroup.Name = "NameGroup"
        '
        'ReplaceObjectNamesButton
        '
        Me.ReplaceObjectNamesButton.Label = "Replace Object Names"
        Me.ReplaceObjectNamesButton.Name = "ReplaceObjectNamesButton"
        '
        'ClipboardGroup
        '
        Me.ClipboardGroup.Items.Add(Me.CopyTextButton)
        Me.ClipboardGroup.Items.Add(Me.CopyNoLineBreakTextButton)
        Me.ClipboardGroup.Label = "Clipboard"
        Me.ClipboardGroup.Name = "ClipboardGroup"
        '
        'CopyTextButton
        '
        Me.CopyTextButton.Label = "Copy Text"
        Me.CopyTextButton.Name = "CopyTextButton"
        '
        'CopyNoLineBreakTextButton
        '
        Me.CopyNoLineBreakTextButton.Label = "Copy No Line break Text"
        Me.CopyNoLineBreakTextButton.Name = "CopyNoLineBreakTextButton"
        '
        'Group1
        '
        Me.Group1.Items.Add(Me.TopMostToggleButton)
        Me.Group1.Label = "Window"
        Me.Group1.Name = "Group1"
        '
        'TopMostToggleButton
        '
        Me.TopMostToggleButton.Image = Global.EditorPlus.OfficeAddIn.PowerPoint.My.Resources.Resources.AlwaysOnTop
        Me.TopMostToggleButton.Label = "Always on Top"
        Me.TopMostToggleButton.Name = "TopMostToggleButton"
        Me.TopMostToggleButton.ScreenTip = "Always on Top"
        Me.TopMostToggleButton.ShowImage = True
        Me.TopMostToggleButton.SuperTip = "Keep this window on top. Always."
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = "Microsoft.PowerPoint.Presentation"
        Me.Tabs.Add(Me.EditorPlusTab)
        Me.EditorPlusTab.ResumeLayout(False)
        Me.EditorPlusTab.PerformLayout()
        Me.EditSelectionGroup.ResumeLayout(False)
        Me.EditSelectionGroup.PerformLayout()
        Me.NameGroup.ResumeLayout(False)
        Me.NameGroup.PerformLayout()
        Me.ClipboardGroup.ResumeLayout(False)
        Me.ClipboardGroup.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EditorPlusTab As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents EditSelectionGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents InsertTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents ClipboardGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents CopyTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents CopyNoLineBreakTextButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents InsertSerialNumberButton As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents TopMostToggleButton As Microsoft.Office.Tools.Ribbon.RibbonToggleButton
    Friend WithEvents NameGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents ReplaceObjectNamesButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
