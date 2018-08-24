Partial Class EditorPlusOutlookRibbon
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
        Me.Tab2 = Me.Factory.CreateRibbonTab
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.BulkAddTasksButton = Me.Factory.CreateRibbonButton
        Me.Tab2.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tab2
        '
        Me.Tab2.Groups.Add(Me.Group1)
        Me.Tab2.Label = "Editor Plus"
        Me.Tab2.Name = "Tab2"
        '
        'Group1
        '
        Me.Group1.Items.Add(Me.BulkAddTasksButton)
        Me.Group1.Label = "Items"
        Me.Group1.Name = "Group1"
        '
        'BulkAddTasksButton
        '
        Me.BulkAddTasksButton.Label = "Bulk Add Tasks"
        Me.BulkAddTasksButton.Name = "BulkAddTasksButton"
        '
        'EditorPlusOutlookRibbon
        '
        Me.Name = "EditorPlusOutlookRibbon"
        Me.RibbonType = "Microsoft.Outlook.Explorer"
        Me.Tabs.Add(Me.Tab2)
        Me.Tab2.ResumeLayout(False)
        Me.Tab2.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tab2 As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents BulkAddTasksButton As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusOutlookRibbon() As EditorPlusOutlookRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusOutlookRibbon)()
        End Get
    End Property
End Class
