Partial Class EditorPlusRibbon
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Windows.Forms クラス作成デザイナーのサポートに必要です
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        'この呼び出しは、コンポーネント デザイナーで必要です。
        InitializeComponent()

    End Sub

    'Component は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
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

    'コンポーネント デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャはコンポーネント デザイナーで必要です。
    'コンポーネント デザイナーを使って変更できます。
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.EditorPlusTab = Me.Factory.CreateRibbonTab
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.Group2 = Me.Factory.CreateRibbonGroup
        Me.OpenFolderButton = Me.Factory.CreateRibbonButton
        Me.TopMostToggleButton = Me.Factory.CreateRibbonToggleButton
        Me.EditorPlusTab.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.Group2.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlusTab
        '
        Me.EditorPlusTab.Groups.Add(Me.Group2)
        Me.EditorPlusTab.Groups.Add(Me.Group1)
        Me.EditorPlusTab.Label = "Editor Plus"
        Me.EditorPlusTab.Name = "EditorPlusTab"
        '
        'Group1
        '
        Me.Group1.Items.Add(Me.TopMostToggleButton)
        Me.Group1.Label = "Window"
        Me.Group1.Name = "Group1"
        '
        'Group2
        '
        Me.Group2.Items.Add(Me.OpenFolderButton)
        Me.Group2.Label = "Item"
        Me.Group2.Name = "Group2"
        '
        'OpenFolderButton
        '
        Me.OpenFolderButton.Image = Global.EditorPlus.OfficeAddIn.Outlook.My.Resources.Resources.OpenFolderfortheActiveFile
        Me.OpenFolderButton.Label = "Open Folder"
        Me.OpenFolderButton.Name = "OpenFolderButton"
        Me.OpenFolderButton.ShowImage = True
        '
        'TopMostToggleButton
        '
        Me.TopMostToggleButton.Image = Global.EditorPlus.OfficeAddIn.Outlook.My.Resources.Resources.AlwaysOnTop
        Me.TopMostToggleButton.Label = "Always on Top"
        Me.TopMostToggleButton.Name = "TopMostToggleButton"
        Me.TopMostToggleButton.ScreenTip = "Always on Top"
        Me.TopMostToggleButton.ShowImage = True
        Me.TopMostToggleButton.SuperTip = "Keep this window on top. Always."
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = "Microsoft.Outlook.Contact, Microsoft.Outlook.Mail.Read, Microsoft.Outlook.Meeting" &
    "Request.Read, Microsoft.Outlook.MeetingRequest.Send, Microsoft.Outlook.Task"
        Me.Tabs.Add(Me.EditorPlusTab)
        Me.EditorPlusTab.ResumeLayout(False)
        Me.EditorPlusTab.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.Group2.ResumeLayout(False)
        Me.Group2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EditorPlusTab As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents TopMostToggleButton As Microsoft.Office.Tools.Ribbon.RibbonToggleButton
    Friend WithEvents Group2 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents OpenFolderButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
