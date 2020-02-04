Public Class InsertTextWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.insertText
        End Get
    End Property
End Class
