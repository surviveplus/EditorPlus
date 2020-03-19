Imports System.Collections.ObjectModel

Public Class LayerWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.layer
        End Get
    End Property

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)


        Dim items As New ObservableCollection(Of LayerTreeItem2)
        Me.layer.Items = items

        Dim root As New LayerTreeItem2 With {.Text = "Slide"}
        items.Add(root)

        Dim item1 As New LayerTreeItem2 With {.Text = "👁 Item 1 spring"}
        items.Add(item1)

        Dim item2 As New LayerTreeItem2 With {.Text = "👁 📁 Item 2 summer"}
        items.Add(item2)

        Dim item21 As New LayerTreeItem2 With {.Text = "👁 Item 2-1 winter", .Parent = item2}
        item2.IsExpanded = True
        item2.Children.Add(item21)

        Dim item211 As New LayerTreeItem2 With {.Text = "👁 Item 2-1-1 xmas", .Parent = item21}
        item21.IsExpanded = True
        item21.Children.Add(item211)



        'AddHandler Me.layer.Refresh,
        '    Sub(sender2, e2)

        '        Dim items As New List(Of LayerTreeItem)

        '        Dim root As New LayerTreeItem With {.Text = "Slide"}
        '        items.Add(root)

        '        Dim item1 As New LayerTreeItem With {.Text = "👁 Item 1"}
        '        items.Add(item1)

        '        Dim item2 As New LayerTreeItem With {.Text = "👁 📁 Item 2"}
        '        item2.IsExpanded = True
        '        items.Add(item2)

        '        Dim item21 As New LayerTreeItem With {.Text = "👁 Item 2-1"}
        '        item2.Children.Add(item21)

        '        e2.Items = items
        '    End Sub

        'AddHandler Me.layer.HideItems,
        '    Sub(sender2, e2)

        '        For Each item As LayerTreeItem In e2.Items
        '            item.IsVisible = False
        '        Next item

        '    End Sub

        'AddHandler Me.layer.ShowItems,
        '    Sub(sender2, e2)

        '        For Each item As LayerTreeItem In e2.Items
        '            item.IsVisible = True
        '        Next item

        '    End Sub

    End Sub
End Class
