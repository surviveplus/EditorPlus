Imports System.Collections.ObjectModel

Public Class LayerWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.layer
        End Get
    End Property

    Private TestItems As ObservableCollection(Of LayerTreeItem2)

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Dim items As New ObservableCollection(Of LayerTreeItem2)
        Me.layer.Items = items
        Me.TestItems = items

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

    End Sub

    Private Sub layer_SelectedObjectsChanged(sender As Object, e As LayerItemsEventArgs)

        Debug.WriteLine($"layer_SelectedObjectsChanged : {DateTime.Now.ToString()}")
        For Each item In e.Items
            Debug.WriteLine(item.Text)
        Next
        Debug.WriteLine("")

    End Sub

    Private Sub TestSelectionChange_Click(sender As Object, e As RoutedEventArgs)

        Me.layer.SuppressEvents = True

        Dim newSelectedItem = (From a In Me.TestItems.Skip(1) Where Not a.ObjectIsSelected).FirstOrDefault()
        newSelectedItem.ObjectIsSelected = True

        Dim changeObjectIsSelected As Action(Of IEnumerable(Of LayerTreeItem2)) =
            Sub(items)
                For Each c As LayerTreeItem2 In items
                    If c IsNot newSelectedItem Then
                        c.ObjectIsSelected = False
                    End If
                    changeObjectIsSelected(c.Children)
                Next
            End Sub
        changeObjectIsSelected(Me.TestItems)

        Me.layer.SuppressEvents = False

    End Sub
End Class
