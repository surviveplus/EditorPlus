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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditorPlusRibbon))
        Me.EditorPlusTab = Me.Factory.CreateRibbonTab
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.TopMostToggleButton = Me.Factory.CreateRibbonToggleButton
        Me.EditorPlusTab.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EditorPlusTab
        '
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
        'TopMostToggleButton
        '
        Me.TopMostToggleButton.Image = Global.EditorPlus.OfficeAddIn.Outlook.My.Resources.Resources.AlwaysOnTop
        Me.TopMostToggleButton.Label = "Always on Top"
        Me.TopMostToggleButton.Name = "TopMostToggleButton"
        Me.TopMostToggleButton.ShowImage = True
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = resources.GetString("$this.RibbonType")
        Me.Tabs.Add(Me.EditorPlusTab)
        Me.EditorPlusTab.ResumeLayout(False)
        Me.EditorPlusTab.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EditorPlusTab As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents TopMostToggleButton As Microsoft.Office.Tools.Ribbon.RibbonToggleButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
