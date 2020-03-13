Imports System.Diagnostics
Imports System.Windows
Imports EditorPlus.AI
Imports EditorPlus.Core
Imports EditorPlus.UI
Imports Microsoft.Office.Interop.PowerPoint
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.Core
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.PowerPoint
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusRibbon

    Private Sub EditorPlusRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
    End Sub

    Private insertTextPane As ElementControlPane(Of InsertText)
    Private insertTextFavorites As New Favorites(Of String)

    Private Sub InsertTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertTextButton.Click

        If Me.insertTextPane Is Nothing Then

            Dim c = New InsertText With {.DataContext = OfficeThemeModel.Current}
            c.Resources.Apply(OfficeAccentColor.Current)
            Dim updateFavorites =
                Sub()
                    c.Favorites = From f In Me.insertTextFavorites.GetFavorites() Select New InsertTextFavorite With {.Text = f}
                End Sub
            updateFavorites()

            AddHandler c.InsertButtonClick,
                Sub(sender2, e2)

                    Dim macaron As New PowerPointMacaron(ThisAddIn.Current.Application)
                    macaron.InsertText(e2.Text, e2.InsertTo, e2.SkipIfStartedOrEndWithText)

                    Me.insertTextFavorites.Add(e2.Text)
                    updateFavorites()
                End Sub

            Me.insertTextPane = New ElementControlPane(Of InsertText)(c)
            Me.insertTextPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.insertTextPane.Control, "Insert Text", ThisAddIn.Current.Application.ActiveWindow)
            Me.insertTextPane.Pane.Width = 350
        End If

        Me.insertTextPane?.Show()
    End Sub

    Private insertSerialNumberPane As ElementControlPane(Of InsertSerialNumber)

    Private Sub InsertSerialNumberButton_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertSerialNumberButton.Click

        If Me.insertSerialNumberPane Is Nothing Then

            Dim c = New InsertSerialNumber With {.DataContext = OfficeThemeModel.Current}
            c.Resources.Apply(OfficeAccentColor.Current)
            AddHandler c.InsertButtonClick,
                Sub(sender2, e2)

                    Dim macaron As New PowerPointMacaron(ThisAddIn.Current.Application)
                    macaron.InsertSerialNumber(e2.StartNumber, e2.InsertTo, e2.Padding, e2.SkipIfStartedOrEndWithText)
                End Sub

            Me.insertSerialNumberPane = New ElementControlPane(Of InsertSerialNumber)(c)
            Me.insertSerialNumberPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.insertSerialNumberPane.Control, "Insert Serial Number", ThisAddIn.Current.Application.ActiveWindow)
            Me.insertSerialNumberPane.Pane.Width = 350
        End If

        Me.insertSerialNumberPane?.Show()
    End Sub
    Private Sub CopyTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyTextButton.Click

        Dim text As New StringBuilder
        Dim macaron As New PowerPointMacaron(ThisAddIn.Current.Application)
        macaron.ReplaceSelectionText(
            Nothing,
            Sub(a)
                text.AppendLine(a.Text)
            End Sub)

        System.Windows.Forms.Clipboard.SetText(text.ToString())

    End Sub

    Private Sub CopyNoLineBreakTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyNoLineBreakTextButton.Click

        Dim getNewText =
            Function(t As String) As String
                Dim newText = t?.Replace(vbLf, "").Replace(vbCr, "").Replace(vbVerticalTab, "")
                Return newText
            End Function

        Dim text As New StringBuilder
        Dim macaron As New PowerPointMacaron(ThisAddIn.Current.Application)
        macaron.ReplaceSelectionText(
            Nothing,
            Sub(a)
                text.AppendLine(getNewText(a.Text))
            End Sub)

        System.Windows.Forms.Clipboard.SetText(text.ToString())
    End Sub

    ''' <summary>
    ''' Allows managed code to call unmanaged functions with Platform Invocation Services (PInvoke).
    ''' </summary>
    Friend NotInheritable Class NativeMethods

#Region " Constructors (Can't initializes a new instance of the this class)"

        Private Sub New()
        End Sub

#End Region

#Region " Win32API Definitions "

        '
        ' Insert the code of Declare of DllImport. (see static code analysis CA1060)
        '
        Declare Function GetActiveWindow Lib "user32" () As IntPtr

        Declare Function SetWindowPos Lib "user32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As IntPtr
        Public Const HWND_TOPMOST = -1
        Public Const HWND_NOTOPMOST = -2
        Public Const SWP_SHOWWINDOW = &H40
        Public Const SWP_NOSIZE = &H1
        Public Const SWP_NOMOVE = &H2
#End Region

    End Class

    Private Sub TopMostToggleButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TopMostToggleButton.Click

        Dim b = CType(sender, RibbonToggleButton)
        Dim topMost = b.Checked
        Dim hwnd = NativeMethods.GetActiveWindow()

        If topMost Then
            Dim r = NativeMethods.SetWindowPos(hwnd, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, NativeMethods.SWP_SHOWWINDOW Or NativeMethods.SWP_NOMOVE Or NativeMethods.SWP_NOSIZE)
        Else
            Dim r = NativeMethods.SetWindowPos(hwnd, NativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, NativeMethods.SWP_SHOWWINDOW Or NativeMethods.SWP_NOMOVE Or NativeMethods.SWP_NOSIZE)
        End If
    End Sub

    Private replacePane As ElementControlPane(Of Replace)

    Private Sub ReplaceObjectNamesButton_Click(sender As Object, e As RibbonControlEventArgs) Handles ReplaceObjectNamesButton.Click

        If Me.replacePane Is Nothing Then
            Dim c = New Replace With {.DataContext = OfficeThemeModel.Current}
            AddHandler c.RepaceButtonClick,
                Sub(sender2, e2)
                    For Each targetSlide As Slide In ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange
                        For Each targetShape As Shape In targetSlide.Shapes
                            targetShape.Name = Strings.Replace(targetShape.Name, e2.FindText, e2.ReplaceText)
                        Next
                    Next
                End Sub

            Me.replacePane = New ElementControlPane(Of Replace)(c)
            Me.replacePane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.replacePane.Control, "Replace Object Names", ThisAddIn.Current.Application.ActiveWindow)
            Me.replacePane.Pane.Width = 350
        End If

        Me.replacePane?.Show()
    End Sub

    Private layerPane As ElementControlPane(Of Layer)

    Private Sub LayerButton_Click(sender As Object, e As RibbonControlEventArgs) Handles LayerButton.Click

        If Me.layerPane Is Nothing Then

            Dim c = New Layer With {.DataContext = OfficeThemeModel.Current}
            c.Resources.Apply(OfficeAccentColor.Current)
            AddHandler c.Refresh,
                Sub(sender2, e2)
                    Dim d As New List(Of UI.LayerTreeItem)

                    Dim w = ThisAddIn.Current.Application.ActiveWindow
                    Dim setup = w.Presentation.PageSetup
                    Dim size As New System.Drawing.Size(setup.SlideWidth, setup.SlideHeight)

                    Dim counter As Integer = 0

                    For Each targetSlide As Slide In ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange
                        d.Add(New LayerTreeItem With {.Text = targetSlide.Name + " (slide)"})

                        Dim items =
                            From item In targetSlide.Shapes.ToEnumerable(Of Shape)
                            Order By item.ZOrderPosition Descending
                            Select item


                        Dim g As Action(Of LayerTreeItem, IEnumerable(Of Shape))
                        g = Sub(parent As LayerTreeItem, s As IEnumerable(Of Shape))
                                For Each item As Shape In s
                                    Dim isGroup As Boolean = CType(item.Type = Microsoft.Office.Core.MsoShapeType.msoGroup, Boolean)


                                    Dim text As String = ""
                                    Try
                                        text = item?.TextFrame2?.TextRange?.Text?.Split(vbCr).FirstOrDefault()
                                        text = " ''" & Strings.Left(text, 30) & "''"

                                    Catch ex As Exception
                                    End Try


                                    Dim newItem As New LayerTreeItem(parent) With {.Text =
                                        If(item.Visible, "👁", "-") &
                                        If(isGroup, "📁", " ") &
                                        item.Name &
                                        text,
                                        .Shape = item
                                    }

                                    If ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionShapes OrElse
                                    ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionText Then
                                        For Each selectedShape As Shape In ThisAddIn.Current.Application.ActiveWindow.Selection.ShapeRange
                                            If item Is selectedShape Then
                                                newItem.IsSelected = True
                                                Exit For
                                            End If
                                        Next
                                    End If

                                    If parent Is Nothing Then
                                        d.Add(newItem)
                                    Else
                                        parent.Children.Add(newItem)
                                    End If
                                    counter += 1
                                    If counter Mod 10 Then e2.DoEvents.Invoke()

                                    If isGroup Then
                                        g(newItem, item.GroupItems.ToEnumerable(Of Shape))
                                    End If
                                Next
                            End Sub

                        g(Nothing, items)

                    Next
                    e2.Items = d
                End Sub

            AddHandler c.SelectionChanged,
                Sub(sender2, e2)



                    Dim g As Action(Of IEnumerable(Of LayerTreeItem))
                    g = Sub(s As IEnumerable(Of LayerTreeItem))

                            For Each item As LayerTreeItem In s
                                g(item.Children)

                                item.IsSelected = False
                                If ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionShapes OrElse
                                    ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionText Then
                                    For Each selectedShape As Shape In ThisAddIn.Current.Application.ActiveWindow.Selection.ShapeRange
                                        If item.Shape Is selectedShape Then
                                            item.IsSelected = True
                                            'Exit For
                                        End If
                                    Next
                                End If

                            Next
                        End Sub

                    g(e2.Items)

                End Sub



            AddHandler c.SelectedItemChanged,
                Sub(sender3, e3)

                    Dim item = e3.Item

                    If item?.Shape IsNot Nothing Then
                        Dim shape As Shape = CType(item.Shape, Shape)
                        Dim w = ThisAddIn.Current.Application.ActiveWindow

                        Try
                            If Not shape.Visible Then
                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
                            End If
                            shape.Select(If(e3.MustReplaceSelection, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse))
                        Catch ex As Exception
                        End Try
                        w.ScrollIntoView(shape.Left, shape.Top, shape.Width, shape.Height)
                    End If

                End Sub

            AddHandler c.ShowItems,
                Sub(sender2, e2)
                    For Each item In e2.Items
                        If item?.Shape IsNot Nothing Then
                            Dim shape As Shape = CType(item.Shape, Shape)
                            If Not shape.Visible Then shape.Visible = True
                        End If
                    Next
                End Sub

            AddHandler c.HideItems,
                Sub(sender2, e2)
                    For Each item In e2.Items
                        If item?.Shape IsNot Nothing Then
                            Dim shape As Shape = CType(item.Shape, Shape)
                            If shape.Visible Then shape.Visible = False
                        End If
                    Next
                End Sub
            Dim mustUpdate As Boolean = False
            Dim lastEditShape As Shape = Nothing
            AddHandler ThisAddIn.Current.Application.WindowSelectionChange,
                Sub(Sel As Selection)
                    If Sel.Type = PpSelectionType.ppSelectionText Then
                        Dim shape As Shape = Sel.ShapeRange(1)
                        'If shape.Type = Microsoft.Office.Core.MsoShapeType.msoPlaceholder Then
                        If lastEditShape IsNot Nothing AndAlso
                            lastEditShape IsNot shape Then

                            mustUpdate = True
                        End If
                        lastEditShape = shape
                        'End If

                    Else
                        If lastEditShape IsNot Nothing Then
                            mustUpdate = True
                        End If
                    End If

                    If mustUpdate Then
                        c.Update()
                        mustUpdate = False
                    Else
                        c.RefreshSelection()

                    End If
                End Sub

            AddHandler ThisAddIn.Current.Application.SlideSelectionChanged,
                Sub(SldRange As SlideRange)
                    c.Update()
                End Sub

            Me.layerPane = New ElementControlPane(Of Layer)(c)
            Me.layerPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.layerPane.Control, "Show Objects", ThisAddIn.Current.Application.ActiveWindow)
            Me.layerPane.Pane.Width = 350
        End If

        Me.layerPane?.Show()

    End Sub

    Private navigationPane As ElementControlPane(Of Navigation)

    Private Sub NavigationButton_Click(sender As Object, e As RibbonControlEventArgs) Handles NavigationButton.Click

        If Me.navigationPane Is Nothing Then
            Dim c = New Navigation With {.DataContext = OfficeThemeModel.Current}
            AddHandler c.Click,
                Sub(sender2, e2)
                    Dim w = ThisAddIn.Current.Application.ActiveWindow

                    w.ScrollIntoView(e2.Position.X, e2.Position.Y, 1, 1)
                End Sub

            Dim refreshSize =
                Sub()
                    Dim w = ThisAddIn.Current.Application.ActiveWindow
                    Dim setup = w.Presentation.PageSetup
                    Dim size As New Size(setup.SlideWidth, setup.SlideHeight)
                    c.PageSize = size
                End Sub

            refreshSize()

            AddHandler ThisAddIn.Current.Application.SlideSelectionChanged,
                Sub(SldRange As SlideRange)
                    refreshSize()
                End Sub

            Me.navigationPane = New ElementControlPane(Of Navigation)(c)
            Me.navigationPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.navigationPane.Control, "Navigation", ThisAddIn.Current.Application.ActiveWindow)
            Me.navigationPane.Pane.Width = 300
        End If

        Me.navigationPane?.Show()
    End Sub
End Class

''' <summary>
''' Static class which is defined extension methods for Object.
''' </summary>
''' <remarks></remarks>
Public Module IEnumerableExtensions

    ''' <summary>
    ''' Return the IEnumerable&lt;T&gt; for a classic collection that do not implement IEnumerable&lt;T&gt; but it is possible to be set on foreach.
    ''' </summary>
    ''' <typeparam name="T">The type of this elements.</typeparam>
    ''' <param name="this">The instance of the type which is added this extension method. Set a null reference (Nothing in Visual Basic), to return empty IEnumerable&lt;T&gt;.</param>
    ''' <returns>Return the IEnumerable&lt;T&gt;.</returns>
    ''' <remarks></remarks>
    <Runtime.CompilerServices.Extension()>
    Public Iterator Function ToEnumerable(Of T)(ByVal this As Object) As IEnumerable(Of T)
        If this IsNot Nothing Then

            For Each item As T In this
                Yield item
            Next
        End If
    End Function

End Module
