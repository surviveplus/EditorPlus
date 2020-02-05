Public Class InsertNumberWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.InsertNumber
        End Get
    End Property
End Class
