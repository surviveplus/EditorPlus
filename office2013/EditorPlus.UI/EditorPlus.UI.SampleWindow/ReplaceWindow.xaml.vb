Public Class ReplaceWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.replace
        End Get
    End Property



End Class
