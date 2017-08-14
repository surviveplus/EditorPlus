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
        Me.EditorPlus = Me.Factory.CreateRibbonTab
        Me.EditSelectionGroup = Me.Factory.CreateRibbonGroup
        Me.InsertTextButton = Me.Factory.CreateRibbonButton
        Me.Button1 = Me.Factory.CreateRibbonButton
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
        Me.EditSelectionGroup.Items.Add(Me.InsertTextButton)
        Me.EditSelectionGroup.Items.Add(Me.Button1)
        Me.EditSelectionGroup.Label = "Edit Selection"
        Me.EditSelectionGroup.Name = "EditSelectionGroup"
        '
        'InsertTextButton
        '
        Me.InsertTextButton.Image = Global.EditorPlus.OfficeAddIn.Project.My.Resources.Resources.InsertText
        Me.InsertTextButton.Label = "Insert Text"
        Me.InsertTextButton.Name = "InsertTextButton"
        Me.InsertTextButton.ShowImage = True
        '
        'Button1
        '
        Me.Button1.Image = Global.EditorPlus.OfficeAddIn.Project.My.Resources.Resources.InsertNumbers
        Me.Button1.Label = "Insert Serial Number"
        Me.Button1.Name = "Button1"
        Me.Button1.ShowImage = True
        '
        'EditorPlusRibbon
        '
        Me.Name = "EditorPlusRibbon"
        Me.RibbonType = "Microsoft.Project.Project"
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
    Friend WithEvents Button1 As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property EditorPlusRibbon() As EditorPlusRibbon
        Get
            Return Me.GetRibbon(Of EditorPlusRibbon)()
        End Get
    End Property
End Class
