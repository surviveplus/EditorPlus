Imports System.Collections.ObjectModel

Public Class LayerWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.layer
        End Get
    End Property

    Private TestItems As ObservableCollection(Of LayerTreeItem2)
    Private testNumber As Integer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Me.layer.SuppressEvents = True
        Me.layer.UpdateProgressValue()
        Me.layer.ProgrressBarVisible = True
        Dim items As New ObservableCollection(Of LayerTreeItem2)
        Me.layer.Items = items
        Me.TestItems = items

        Dim root As New LayerTreeItem2 With {.Text = "Slide"}
        items.Add(root)

        Dim item1 As New LayerTreeItem2 With {.Text = "Item 1 spring"}
        items.Add(item1)

        Dim item2 As New LayerTreeItem2 With {.Text = "📁 Item 2 summer"}
        items.Add(item2)

        Dim item21 As New LayerTreeItem2 With {.Text = "Item 2-1 winter", .Parent = item2}
        item2.IsExpanded = True
        item2.Children.Add(item21)

        Dim item211 As New LayerTreeItem2 With {.Text = "Item 2-1-1 xmas", .Parent = item21}
        item21.IsExpanded = True
        item21.Children.Add(item211)

        Me.layer.ProgrressBarVisible = False
        Me.layer.SuppressEvents = False

        Me.testNumber = 2
    End Sub

    Private Sub layer_SelectedObjectsChanged(sender As Object, e As LayerItemsEventArgs)

        Debug.WriteLine($"layer_SelectedObjectsChanged : {DateTime.Now.ToString()}")
        For Each item In e.Items
            Debug.WriteLine(item.ToString())
        Next
        Debug.WriteLine("")

    End Sub

    Private Sub layer_VisibleObjectsChanged(sender As Object, e As LayerItemsEventArgs)

        Debug.WriteLine($"layer_VisibleObjectsChanged : {DateTime.Now.ToString()}")
        For Each item In e.Items
            Debug.WriteLine(item.ToString())
        Next
        Debug.WriteLine("")
    End Sub

    Private Sub layer_ObjectVisibleChanged(sender As Object, e As LayerItemEventArgs)
        Debug.WriteLine($"layer_ObjectVisibleChanged : {DateTime.Now.ToString()}")
        Debug.WriteLine(e.Item.ToString())
        Debug.WriteLine("")
    End Sub

    Private Sub TestSelectionChange_Click(sender As Object, e As RoutedEventArgs)

        Me.layer.SuppressEvents = True
        Me.layer.UpdateProgressValue()
        Me.layer.ProgrressBarVisible = True

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

        Me.layer.ProgrressBarVisible = False
        Me.layer.SuppressEvents = False

    End Sub

    Private Sub TestAdd_Click(sender As Object, e As RoutedEventArgs)
        Me.testNumber += 1

        Me.layer.SuppressEvents = True
        Me.layer.UpdateProgressValue()
        Me.layer.ProgrressBarVisible = True

        Dim newItem As New LayerTreeItem2 With {.Text = $"Item {Me.testNumber}"}
        newItem.ObjectIsSelected = True
        Me.TestItems.Add(newItem)

        Dim changeObjectIsSelected As Action(Of IEnumerable(Of LayerTreeItem2)) =
            Sub(items)
                For Each c As LayerTreeItem2 In items
                    If c IsNot newItem Then
                        c.ObjectIsSelected = False
                    End If
                    changeObjectIsSelected(c.Children)
                Next
            End Sub
        changeObjectIsSelected(Me.TestItems)

        Me.layer.ProgrressBarVisible = False
        Me.layer.SuppressEvents = False

    End Sub


End Class
