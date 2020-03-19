Imports System.Collections.ObjectModel

Public Class Layer2

#Region " Properties "

    Private _Items As ObservableCollection(Of LayerTreeItem2)

    Public Property Items As ObservableCollection(Of LayerTreeItem2)
        Get
            Return _Items
        End Get
        Set
            _Items = Value
            Me.layers.ItemsSource = Me.Items

        End Set
    End Property

#End Region

    Private Sub FilterByKeyword()

        Dim keywords =
            (From s In Me.SearchKeywordBox.Text.Split(" ")
             Let keyword = s.Trim().ToLower()
             Where Not String.IsNullOrWhiteSpace(keyword)
             Select keyword).ToList()

        Dim view = CollectionViewSource.GetDefaultView(Me.Items)

        If keywords.Count() > 0 Then
            Dim filter As Action(Of IEnumerable(Of LayerTreeItem2)) =
            Sub(items)
                If items Is Nothing Then Return

                For Each item In items
                    If keywords.Count = 0 Then
                        item.IsVisibleByFilter = True
                    Else
                        item.IsVisibleByFilter = (From s In keywords Where item.Text.ToLower().Contains(s)).Count = keywords.Count
                    End If

                    If item.IsVisibleByFilter AndAlso item.Parent IsNot Nothing Then item.Parent.IsVisibleByFilter = True
                    filter(item.Children)
                Next
            End Sub
            filter(Me.Items)

            view.Filter = Function(item As LayerTreeItem2) item.IsVisibleByFilter
            Me.clearButton.Visibility = Visibility.Visible
        Else
            view.Filter = Function(item) True
            Me.clearButton.Visibility = Visibility.Collapsed
        End If


    End Sub

    Private Sub ClearFilterKeyword()
        Me.SearchKeywordBox.Text = String.Empty
        Me.FilterByKeyword()
    End Sub

#Region " Event Handlers "

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub clearButton_Click(sender As Object, e As RoutedEventArgs)
        Me.ClearFilterKeyword()
    End Sub

    Private Sub refreshButton_Click(sender As Object, e As RoutedEventArgs)
        Me.FilterByKeyword()
    End Sub

    Private Sub SearchKeywordBox_KeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.Enter Then
            Me.FilterByKeyword()

        ElseIf e.Key = Key.Escape Then
            Me.ClearFilterKeyword()
        End If
    End Sub

    Private Sub layers_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))

    End Sub

    Private Sub HideButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ShowButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub
#End Region

End Class


Public Class LayerTreeItem2
    Inherits BindableBase

    Private _IsExpanded As Boolean
    Private _Filtered As Boolean
    Public Property Parent As LayerTreeItem2

    Public ReadOnly Property Own As LayerTreeItem2
        Get
            Return Me
        End Get
    End Property

    Public Property Children As New ObservableCollection(Of LayerTreeItem2)

    Public Property Text As String

    Public Property IsExpanded As Boolean
        Get
            Return _IsExpanded
        End Get
        Set
            Me.SetProperty(_IsExpanded, Value)
        End Set
    End Property

    Friend Property IsVisibleByFilter As Boolean

End Class