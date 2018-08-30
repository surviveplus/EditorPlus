Class MainWindow
    Private Sub ApplyOfficeTheme_Click(sender As Object, e As RoutedEventArgs)

        Me.ApplyOfficeTheme.Content = "Theme : " & OfficeTheme.Current.ToString()
        Me.bulk.ApplyTheme()

    End Sub
End Class
