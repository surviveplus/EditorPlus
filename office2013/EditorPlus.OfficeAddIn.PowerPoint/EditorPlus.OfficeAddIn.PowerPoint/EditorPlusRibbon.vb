Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports EditorPlus.AI
Imports EditorPlus.Core
Imports EditorPlus.UI
Imports Microsoft.Office.Interop.PowerPoint
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.Core
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.PowerPoint
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusRibbon

    Private Class MustRecreateItemsException
        Inherits Exception

    End Class

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
        CopyText()

    End Sub

    Private Shared Sub CopyText()
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

    Private layerPanes As New Dictionary(Of DocumentWindow, ElementControlPane(Of Layer2))



    Private Sub LayerButton_Click(sender As Object, e As RibbonControlEventArgs) Handles LayerButton.Click

        If Not Me.layerPanes.ContainsKey(ThisAddIn.Current.Application.ActiveWindow) Then

            Dim c = New Layer2 With {.DataContext = OfficeThemeModel.Current}
            c.Resources.Apply(OfficeAccentColor.Current)

            ' Navigation

            Dim setImage =
                Sub()
                    Dim path = System.IO.Path.GetTempFileName()
                    Dim slide As Slide
                    Try
                        slide = ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide).FirstOrDefault()
                    Catch
                        slide = Nothing
                    End Try
                    If slide IsNot Nothing Then
                        slide.Export(FileName:=path, FilterName:="png", ScaleWidth:=c.PageSize.Width, ScaleHeight:=c.PageSize.Height)

                        Using s As New System.IO.MemoryStream(System.IO.File.ReadAllBytes(path))
                            Dim b As New WriteableBitmap(BitmapFrame.Create(s))
                            c.SetPreviewImage(b)
                        End Using

                        Try
                            System.IO.File.Delete(path)
                        Catch ex As Exception
                        End Try
                    End If
                End Sub

            AddHandler c.Click,
                Sub(sender2, e2)
                    Dim w = ThisAddIn.Current.Application.ActiveWindow

                    Try
                        w.ScrollIntoView(e2.Position.X - 50, e2.Position.Y - 50, 100, 100)

                    Catch
                        ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide).FirstOrDefault?.Shapes.ToEnumerable(Of Shape).FirstOrDefault?.Select()
                        w.ScrollIntoView(e2.Position.X - 50, e2.Position.Y - 50, 100, 100)
                    End Try
                End Sub

            Dim refreshSize =
                Sub()
                    Dim w = ThisAddIn.Current.Application.ActiveWindow
                    Dim setup = w.Presentation.PageSetup
                    Dim size As New Size(setup.SlideWidth, setup.SlideHeight)
                    c.PageSize = size
                End Sub

            refreshSize()
            setImage()

            Dim slideSelectionChanged As EApplication_SlideSelectionChangedEventHandler =
                Sub(SldRange As SlideRange)
                    refreshSize()
                    setImage()
                End Sub
            AddHandler ThisAddIn.Current.Application.SlideSelectionChanged, slideSelectionChanged

            Dim afterShapeSizeChange As EApplication_AfterShapeSizeChangeEventHandler =
                Sub()
                    setImage()
                End Sub

            AddHandler ThisAddIn.Current.Application.AfterShapeSizeChange, afterShapeSizeChange

            ' Layer

            Dim recreateAllItems As Action(Of Boolean) =
                Sub(canDoEvents)
                    c.SuppressEvents = True
                    c.ProgrressBarVisible = True
                    c.UpdateProgressValue()
                    c.Items = Nothing
                    If canDoEvents Then c.DoEvents()

                    Dim items As New ObservableCollection(Of LayerTreeItem2)

                    Dim selection As Selection = Nothing
                    If (ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionShapes OrElse
                         ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionText) Then
                        selection = ThisAddIn.Current.Application.ActiveWindow.Selection
                    End If

                    For Each targetSlide As Slide In ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange
                        items.Add(New LayerTreeItem2 With {.Slide = targetSlide, .Text = targetSlide.Name & " (slide)", .Name = targetSlide.Name})

                        Dim counter As Integer = 0
                        Dim checkShapes As Action(Of LayerTreeItem2, IEnumerable(Of Shape)) =
                            Sub(parent, shapes)
                                For Each item As Shape In (From a In shapes Order By a.ZOrderPosition Descending)

                                    counter += 1
                                    If counter Mod 10 Then
                                        c.UpdateProgressValue()
                                    End If
                                    Dim isGroup As Boolean = CType(item.Type = Microsoft.Office.Core.MsoShapeType.msoGroup, Boolean)

                                    Dim searchTargetText = ""
                                    Dim text As String = ""
                                    Try
                                        searchTargetText = item?.TextFrame2?.TextRange?.Text
                                        If Not String.IsNullOrWhiteSpace(searchTargetText) Then
                                            text = searchTargetText?.Replace(vbVerticalTab, vbCr).Split(vbCr).FirstOrDefault()
                                            text = " : " & Strings.Left(text, 30)
                                        End If
                                    Catch
                                        text = ""
                                    End Try

                                    Dim newItem As New LayerTreeItem2 With {
                                        .Slide = targetSlide,
                                        .Shape = item,
                                        .Parent = parent,
                                        .ObjectIsVisible = item.Visible,
                                        .Text = If(isGroup, "📁", " ") & item.Name & text,
                                        .Name = item.Name,
                                        .IsGroup = isGroup,
                                        .ZOrderPosition = item.ZOrderPosition,
                                        .SearchTargetText = searchTargetText
                                    }

                                    If selection IsNot Nothing Then
                                        If selection.HasChildShapeRange Then
                                            If (From a In selection.ChildShapeRange.ToEnumerable(Of Shape) Where item Is a).Any() Then

                                                newItem.ObjectIsSelected = True
                                            Else
                                                newItem.ObjectIsSelected = False
                                            End If
                                        Else
                                            If (From a In selection.ShapeRange.ToEnumerable(Of Shape) Where item Is a).Any() Then

                                                newItem.ObjectIsSelected = True
                                            Else
                                                newItem.ObjectIsSelected = False
                                            End If
                                        End If
                                    Else
                                        newItem.ObjectIsSelected = False
                                    End If

                                    If parent Is Nothing Then
                                        items.Add(newItem)
                                    Else
                                        parent.Children.Add(newItem)
                                    End If

                                    If isGroup Then
                                        checkShapes(newItem, item.GroupItems.ToEnumerable(Of Shape))
                                    End If
                                Next
                            End Sub

                        Dim topLevelShapes =
                            From item In targetSlide.Shapes.ToEnumerable(Of Shape)
                            Order By item.ZOrderPosition Descending
                            Select item

                        checkShapes(Nothing, topLevelShapes)
                    Next targetSlide

                    c.Items = items
                    c.ProgrressBarVisible = False
                    c.SuppressEvents = False
                End Sub

            Dim refreshObjectsAreSelected As Func(Of Selection, Boolean) =
                Function(selection)
                    Dim result As Boolean = False
                    c.SuppressEvents = True

                    If selection Is Nothing AndAlso
                    (ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionShapes OrElse
                     ThisAddIn.Current.Application.ActiveWindow.Selection.Type = PpSelectionType.ppSelectionText) Then
                        selection = ThisAddIn.Current.Application.ActiveWindow.Selection
                    End If

                    Dim textIsChanged As Boolean = False
                    Dim changeObjectIsSelected As Action(Of IEnumerable(Of LayerTreeItem2)) =
                        Sub(items)
                            For Each item As LayerTreeItem2 In items
                                Dim s As Shape = item.Shape
                                If s IsNot Nothing Then

                                    If s.ZOrderPosition <> item.ZOrderPosition Then
                                        Throw New MustRecreateItemsException()
                                    End If

                                    Dim objectIsSelectedOld As Boolean = item.ObjectIsSelected
                                    If selection IsNot Nothing Then
                                        If selection.HasChildShapeRange Then
                                            If (From a In selection.ChildShapeRange.ToEnumerable(Of Shape) Where s Is a).Any() Then

                                                result = True
                                                item.ObjectIsSelected = True
                                            Else
                                                item.ObjectIsSelected = False
                                            End If
                                        Else
                                            If (From a In selection.ShapeRange.ToEnumerable(Of Shape) Where s Is a).Any() Then

                                                result = True
                                                item.ObjectIsSelected = True
                                            Else
                                                item.ObjectIsSelected = False
                                            End If
                                        End If
                                    Else
                                        item.ObjectIsSelected = False
                                    End If

                                    item.ObjectIsVisible = s.Visible

                                    ' refresh name and text (selected visible shape only)
                                    If objectIsSelectedOld OrElse (item.ObjectIsSelected AndAlso item.ObjectIsVisible) Then
                                        Dim searchTargetText = ""
                                        Dim text As String = ""
                                        Try
                                            searchTargetText = s?.TextFrame2?.TextRange?.Text
                                            If Not String.IsNullOrWhiteSpace(searchTargetText) Then
                                                text = searchTargetText?.Replace(vbVerticalTab, vbCr).Split(vbCr).FirstOrDefault()
                                                text = " : " & Strings.Left(text, 30)
                                            End If
                                        Catch
                                            text = ""
                                        End Try
                                        Dim newText = If(item.IsGroup, "📁", " ") & s.Name & text
                                        item.Text = newText
                                        item.Name = s.Name

                                        If item.SearchTargetText <> searchTargetText Then
                                            item.SearchTargetText = searchTargetText
                                            textIsChanged = True
                                        End If
                                    End If
                                End If

                                changeObjectIsSelected(item.Children)
                            Next
                        End Sub
                    changeObjectIsSelected(c.Items)

                    If textIsChanged Then setImage()
                    c.SuppressEvents = False
                    Return result
                End Function

            Dim windowSelectionChange As EApplication_WindowSelectionChangeEventHandler =
                Sub(Sel As Selection)

                    Debug.WriteLine($"windowSelectionChange {DateTime.Now.ToString()}")
                    Debug.WriteLine($"HasChildShapeRange= {Sel.HasChildShapeRange}")
                    If Sel.HasChildShapeRange Then
                        Debug.WriteLine($"ChildShapeRange.Count= {Sel.ChildShapeRange.Count}")
                    End If

                    Try
                        If Sel.Type = PpSelectionType.ppSelectionShapes Then
                            If Not refreshObjectsAreSelected(Sel) Then
                                recreateAllItems(False)
                            End If

                        ElseIf Sel.Type = PpSelectionType.ppSelectionSlides Then
                            recreateAllItems(True)
                        Else
                            refreshObjectsAreSelected(Nothing)
                        End If

                    Catch ex As MustRecreateItemsException
                        recreateAllItems(False)
                    End Try

                End Sub
            AddHandler ThisAddIn.Current.Application.WindowSelectionChange, windowSelectionChange

            AddHandler c.SelectedObjectsChanged,
                Sub(sender3, e3)

                    Dim mustReplaceSelection As Boolean = True
                    Dim selectShape As Action(Of LayerTreeItem2) =
                        Sub(item)

                            If item?.Shape IsNot Nothing Then
                                Dim shape As Shape = CType(item.Shape, Shape)
                                Dim w = ThisAddIn.Current.Application.ActiveWindow

                                Try
                                    'If Not shape.Visible Then
                                    '    shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
                                    'End If
                                    shape.Select(If(mustReplaceSelection, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse))
                                    mustReplaceSelection = False
                                Catch ex As Exception
                                End Try
                                w.ScrollIntoView(shape.Left, shape.Top, shape.Width, shape.Height)
                            End If
                        End Sub

                    For Each item In e3.Items
                        selectShape(item)
                    Next item
                End Sub

            AddHandler c.ObjectVisibleChanged,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    Dim s As Shape = e2.Item.Shape
                    If s IsNot Nothing Then
                        s.Visible = e2.Item.ObjectIsVisible
                    End If
                    c.SuppressEvents = False

                    setImage()
                End Sub

            AddHandler c.BringForwardButtonClick,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    For Each item In (From a In e2.Items Order By a.ZOrderPosition Descending)
                        Dim s As Shape = item.Shape
                        If s IsNot Nothing Then
                            s.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoBringForward)
                        End If
                    Next
                    recreateAllItems(False)
                    c.SuppressEvents = False
                End Sub

            AddHandler c.BringToFrontButtonClick,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    For Each item In (From a In e2.Items Order By a.ZOrderPosition)
                        Dim s As Shape = item.Shape
                        If s IsNot Nothing Then
                            s.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoBringToFront)
                        End If
                    Next
                    recreateAllItems(False)
                    c.SuppressEvents = False
                End Sub

            AddHandler c.BringForwardButtonClick,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    For Each item In (From a In e2.Items Order By a.ZOrderPosition)
                        Dim s As Shape = item.Shape
                        If s IsNot Nothing Then
                            s.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoBringForward)
                        End If
                    Next
                    recreateAllItems(False)
                    c.SuppressEvents = False
                End Sub

            AddHandler c.SendBackwardButtonClick,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    For Each item In (From a In e2.Items Order By a.ZOrderPosition)
                        Dim s As Shape = item.Shape
                        If s IsNot Nothing Then
                            s.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoSendBackward)
                        End If
                    Next
                    recreateAllItems(False)
                    c.SuppressEvents = False
                End Sub

            AddHandler c.SendToBackButtonClick,
                Sub(sender2, e2)
                    c.SuppressEvents = True
                    For Each item In (From a In e2.Items Order By a.ZOrderPosition Descending)
                        Dim s As Shape = item.Shape
                        If s IsNot Nothing Then
                            s.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoSendToBack)
                        End If
                    Next
                    recreateAllItems(False)
                    c.SuppressEvents = False
                End Sub

            AddHandler c.SelectedObjectsNameChanged,
                Sub(sender2, e2)

                    For Each item As LayerTreeItem2 In e2.Items
                        If item.Shape IsNot Nothing Then
                            ChangeShapeName(item.Shape)

                        ElseIf item.Slide IsNot Nothing Then
                            ChangeSlideName(item.Slide)
                        End If
                    Next

                    recreateAllItems(False)
                End Sub

            AddHandler c.RefreshButtonClicked,
                Sub(sender2, e2)
                    recreateAllItems(False)
                    setImage()
                End Sub

            Dim p As New ElementControlPane(Of Layer2)(c)
            Me.layerPanes.Add(ThisAddIn.Current.Application.ActiveWindow, p)
            p.Pane = ThisAddIn.Current.CustomTaskPanes.Add(p.Control, "Objects Navigation", ThisAddIn.Current.Application.ActiveWindow)
            p.Pane.Width = 350
            AddHandler p.Pane.VisibleChanged,
            Sub()
                If Not p.Pane.Visible Then
                    RemoveHandler ThisAddIn.Current.Application.WindowSelectionChange, windowSelectionChange
                    RemoveHandler ThisAddIn.Current.Application.SlideSelectionChanged, slideSelectionChanged
                    RemoveHandler ThisAddIn.Current.Application.AfterShapeSizeChange, afterShapeSizeChange

                    Me.layerPanes.Remove(ThisAddIn.Current.Application.ActiveWindow)
                    p.Control.Dispose()
                    p.Pane.Dispose()
                End If
            End Sub

            Me.layerPanes(ThisAddIn.Current.Application.ActiveWindow).Show()
            recreateAllItems(True)
        End If

        Me.layerPanes(ThisAddIn.Current.Application.ActiveWindow).Show()

    End Sub

    Private navigationPanes As New Dictionary(Of DocumentWindow, ElementControlPane(Of Navigation))


    Private Sub NavigationButton_Click(sender As Object, e As RibbonControlEventArgs)

        If Not Me.navigationPanes.ContainsKey(ThisAddIn.Current.Application.ActiveWindow) Then
            Dim c = New Navigation With {.DataContext = OfficeThemeModel.Current}

            Dim setImage =
                Sub()
                    Dim path = System.IO.Path.GetTempFileName()
                    Dim slide As Slide
                    Try
                        slide = ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide).FirstOrDefault()
                    Catch
                        slide = Nothing
                    End Try
                    If slide IsNot Nothing Then
                        slide.Export(FileName:=path, FilterName:="png", ScaleWidth:=c.PageSize.Width, ScaleHeight:=c.PageSize.Height)

                        Using s As New System.IO.MemoryStream(System.IO.File.ReadAllBytes(path))
                            Dim b As New WriteableBitmap(BitmapFrame.Create(s))
                            c.SetPreviewImage(b)
                        End Using

                        Try
                            System.IO.File.Delete(path)
                        Catch ex As Exception
                        End Try
                    End If
                End Sub

            AddHandler c.Click,
                Sub(sender2, e2)
                    Dim w = ThisAddIn.Current.Application.ActiveWindow

                    Try
                        w.ScrollIntoView(e2.Position.X - 50, e2.Position.Y - 50, 100, 100)

                    Catch
                        ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide).FirstOrDefault?.Shapes.ToEnumerable(Of Shape).FirstOrDefault?.Select()
                        w.ScrollIntoView(e2.Position.X - 50, e2.Position.Y - 50, 100, 100)
                    End Try
                End Sub

            Dim refreshSize =
                Sub()
                    Dim w = ThisAddIn.Current.Application.ActiveWindow
                    Dim setup = w.Presentation.PageSetup
                    Dim size As New Size(setup.SlideWidth, setup.SlideHeight)
                    c.PageSize = size
                End Sub

            refreshSize()
            setImage()

            AddHandler ThisAddIn.Current.Application.SlideSelectionChanged,
                Sub(SldRange As SlideRange)
                    refreshSize()
                    setImage()
                End Sub

            Dim p = New ElementControlPane(Of Navigation)(c)
            Me.navigationPanes.Add(ThisAddIn.Current.Application.ActiveWindow, p)
            p.Pane = ThisAddIn.Current.CustomTaskPanes.Add(p.Control, "Navigation", ThisAddIn.Current.Application.ActiveWindow)
            p.Pane.Width = 300
        End If

        Me.navigationPanes(ThisAddIn.Current.Application.ActiveWindow).Show()
    End Sub

    Private Sub CopyNotesButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyNotesButton.Click

        Try
            Dim notes = From slide In ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide)
                        Select slide.NotesPage.Shapes.Placeholders(2).TextFrame.TextRange.Text

            Dim texts As New StringBuilder
            For Each n As String In notes
                texts.AppendLine(n)
                texts.AppendLine()
            Next

            System.Windows.Forms.Clipboard.SetText(texts.ToString())

        Catch ex As Exception
            MessageBox.Show("Can not copy Notes." + vbCrLf + ex.Message, "Copy Notes ERROR", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub CopyTextSplitButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyTextSplitButton.Click
        CopyText()
    End Sub

    Private Sub ChangeSlideNameButton_Click(sender As Object, e As RibbonControlEventArgs) Handles ChangeSlideNameButton.Click


        For Each s As Slide In (
            From a In ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide)
            Order By a.SlideIndex)

            ChangeSlideName(s)
        Next

        ' TODO: refresh Object Navigation slide name

    End Sub

    Private Shared Sub ChangeSlideName(s As Slide)
        Dim name = s.Name
        Dim newText = InputBox($"Input new name of [{name}]", "Change Slide Name", name)
        If Not String.IsNullOrWhiteSpace(newText) Then
            Try
                s.Name = newText
            Catch ex As Exception
                MessageBox.Show("Can not change this slide name." + vbCrLf + ex.Message, "Change Slide Name ERROR", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End If
    End Sub

    Private Sub ChangeShapeNameButton_Click(sender As Object, e As RibbonControlEventArgs)

        For Each item As Shape In ThisAddIn.Current.Application.ActiveWindow.Selection.ShapeRange
            ChangeShapeName(item)
        Next
        ' TODO: refresh Object Navigation slide name

    End Sub

    Private Shared Sub ChangeShapeName(s As Shape)
        Dim name = s.Name
        Dim newText = InputBox($"Input new name of [{name}]", "Change Shape Name", name)
        If Not String.IsNullOrWhiteSpace(newText) Then
            Try
                s.Name = newText
            Catch ex As Exception
                MessageBox.Show("Can not change this shape name." + vbCrLf + ex.Message, "Change Shape Name ERROR", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try

        End If
    End Sub

    Private exportSlidesPanes As New Dictionary(Of DocumentWindow, ElementControlPane(Of ExportSlides))

    Private Sub ExportSlidesButton_Click(sender As Object, e As RibbonControlEventArgs) Handles ExportSlidesButton.Click

        If Not Me.exportSlidesPanes.ContainsKey(ThisAddIn.Current.Application.ActiveWindow) Then

            Dim m = New ExportSlidesModel With {.Theme = OfficeThemeModel.Current.Theme}
            Dim c = New ExportSlides With {.DataContext = m}
            c.Resources.Apply(OfficeAccentColor.Current)

            AddHandler c.SaveFilesButtonClick,
                Sub(sender2, e3)

                    Dim folder As New IO.DirectoryInfo(IO.Path.Combine(
                                                       ThisAddIn.Current.Application.ActivePresentation.Path,
                                                       IO.Path.GetFileNameWithoutExtension(ThisAddIn.Current.Application.ActivePresentation.Name)))
                    If Not folder.Exists Then
                        folder.Create()
                        folder.Refresh()
                    End If


                    Dim targets = From slide In If(
                                      m.TargetIsSelection,
                                      ThisAddIn.Current.Application.ActiveWindow.Selection.SlideRange.ToEnumerable(Of Slide),
                                      ThisAddIn.Current.Application.ActivePresentation.Slides.ToEnumerable(Of Slide))
                                  Select New With {
                                      .Name = If(m.FileNameIsSlideName, slide.Name, "Slide" & slide.SlideNumber),
                                      .Slide = slide,
                                      .Notes = slide.NotesPage.Shapes.Placeholders(2).TextFrame.TextRange.Text}

                    If m.TargetIsWithoutHidden Then
                        targets = targets.Where(Function(s) Not s.Slide.SlideShowTransition.Hidden)
                    End If

                    Dim w = ThisAddIn.Current.Application.ActiveWindow
                    Dim setup = w.Presentation.PageSetup
                    Dim size As New Size(setup.SlideWidth, setup.SlideHeight)
                    Dim pngSize = New Size(m.Width, size.Height * m.Width / size.Width)

                    For Each t In targets

                        If m.SaveSlideImage Then
                            Dim filename = t.Name + ".png"
                            Dim file As New System.IO.FileInfo(System.IO.Path.Combine(folder.FullName, filename))

                            t.Slide.Export(FileName:=file.FullName, FilterName:="png", ScaleWidth:=pngSize.Width, ScaleHeight:=pngSize.Height)
                        End If

                        If m.SaveNotes Then
                            Dim filename = t.Name + ".txt"
                            Dim file As New System.IO.FileInfo(System.IO.Path.Combine(folder.FullName, filename))

                            IO.File.WriteAllText(file.FullName, t.Notes, Encoding.UTF8)
                        End If
                    Next
                End Sub

            Dim p As New ElementControlPane(Of ExportSlides)(c)
            Me.exportSlidesPanes.Add(ThisAddIn.Current.Application.ActiveWindow, p)
            p.Pane = ThisAddIn.Current.CustomTaskPanes.Add(p.Control, "Export Slides", ThisAddIn.Current.Application.ActiveWindow)
            p.Pane.Width = 300
            AddHandler p.Pane.VisibleChanged,
                Sub()
                    If Not p.Pane.Visible Then
                        Me.exportSlidesPanes.Remove(ThisAddIn.Current.Application.ActiveWindow)
                        p.Control.Dispose()
                        p.Pane.Dispose()
                    End If
                End Sub
        End If

        Me.exportSlidesPanes(ThisAddIn.Current.Application.ActiveWindow).Show()

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
