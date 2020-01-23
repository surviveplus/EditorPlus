Class MainWindow
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)


        AddHandler Me.layer.Refresh,
            Sub(sender2, e2)

                Dim items As New List(Of LayerTreeItem)

                Dim root As New LayerTreeItem With {.Text = "Slide"}
                items.Add(root)

                Dim item1 As New LayerTreeItem With {.Text = "👁 Item 1"}
                items.Add(item1)

                Dim item2 As New LayerTreeItem With {.Text = "👁 📁 Item 2"}
                item2.IsExpanded = True
                items.Add(item2)

                Dim item21 As New LayerTreeItem With {.Text = "👁 Item 2-1"}
                item2.Children.Add(item21)

                e2.Items = items
            End Sub
    End Sub
End Class
