Class MainWindow


    Private openedWindows As New List(Of Window)

    Private Sub LayerButton_Click(sender As Object, e As RoutedEventArgs)

        Dim w As New LayerWindow()

        If TypeOf w Is IHasUsercontrol Then
            With CType(w, IHasUsercontrol)
                .MainUserControl.DataContext = OfficeThemeModel.Current
                .MainUserControl.Resources.Apply(OfficeAccentColor.Current)
            End With
        End If

        w.Show()
        Me.openedWindows.Add(w)

    End Sub

    Private Sub InsertTextButton_Click(sender As Object, e As RoutedEventArgs)
        Dim w As New InsertTextWindow()

        If TypeOf w Is IHasUsercontrol Then
            With CType(w, IHasUsercontrol)
                .MainUserControl.DataContext = OfficeThemeModel.Current
                .MainUserControl.Resources.Apply(OfficeAccentColor.Current)
            End With
        End If

        w.Show()
        Me.openedWindows.Add(w)
    End Sub

    Private Sub ThemeRadioButton_Checked(sender As Object, e As RoutedEventArgs)

        Dim radio As RadioButton = sender

        For Each w In Me.openedWindows
            If TypeOf w Is IHasUsercontrol Then
                CType(w, IHasUsercontrol).MainUserControl.DataContext = Nothing
            End If
        Next w

        OfficeThemeModel.Current.Theme = CType(radio.Tag, Theme)

        For Each w In Me.openedWindows
            If TypeOf w Is IHasUsercontrol Then
                CType(w, IHasUsercontrol).MainUserControl.DataContext = OfficeThemeModel.Current
            End If
        Next w
    End Sub

    Private Sub AccentColorsRadioButton_Checked(sender As Object, e As RoutedEventArgs)

        Dim radio As RadioButton = sender
        OfficeAccentColor.Current = CType(radio.Tag, AccentColors)
        For Each w In Me.openedWindows
            If TypeOf w Is IHasUsercontrol Then
                CType(w, IHasUsercontrol).MainUserControl.Resources.Apply(OfficeAccentColor.Current)
            End If
        Next w

    End Sub
End Class
