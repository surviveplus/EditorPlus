Imports System.Diagnostics
Imports EditorPlus.AI
Imports EditorPlus.Core
Imports EditorPlus.UI
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.RegularExpressionQuery
Imports Net.Surviveplus.SakuraMacaron.Core
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Excel
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

                    Dim macaron As New ExcelMacaron(ThisAddIn.Current.Application)
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

                    Dim macaron As New ExcelMacaron(ThisAddIn.Current.Application)
                    macaron.InsertSerialNumber(e2.StartNumber, e2.InsertTo, e2.Padding, e2.SkipIfStartedOrEndWithText)
                End Sub

            Me.insertSerialNumberPane = New ElementControlPane(Of InsertSerialNumber)(c)
            Me.insertSerialNumberPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.insertSerialNumberPane.Control, "Insert Serial Number", ThisAddIn.Current.Application.ActiveWindow)
            Me.insertSerialNumberPane.Pane.Width = 350
        End If

        Me.insertSerialNumberPane?.Show()
    End Sub

    Private Sub IncrementButton_Click(sender As Object, e As RibbonControlEventArgs) Handles IncrementButton.Click

        Dim app = ThisAddIn.Current.Application
        Dim target As Microsoft.Office.Interop.Excel.Range = app.Selection

        Try
            Dim upperCell As Microsoft.Office.Interop.Excel.Range = target.Offset(-1, 0)
            Dim nextCell As Microsoft.Office.Interop.Excel.Range = target.Offset(1, 0)
            Dim text As String = upperCell.Text

            Dim newText = Core.EditorString.IncrementText(text)
            If newText IsNot Nothing Then
                target.Formula = newText
                nextCell.Select()
            End If

        Catch ex2 As Exception
            MsgBox(My.Resources.Message1CannotIncrement, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub InsertNowButton_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertNowButton.Click

        Dim macaron As New ExcelMacaron(ThisAddIn.Current.Application)
        macaron.ReplaceSelectionText(
            Nothing,
            Sub(a)
                a.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFF")
            End Sub)

    End Sub

    Private Sub CopyNoLineBreakTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyNoLineBreakTextButton.Click
        Dim result = New StringBuilder
        Dim app = ThisAddIn.Current.Application

        ' セルの形（左右　表としての位置、タブ区切りと改行で表現）を維持したままのコピー

        Dim getNewText =
            Function(text As String) As String
                Dim newText = text?.Replace(vbLf, "")
                Return newText
            End Function

        Dim macaron As New ExcelMacaron(app)
        macaron.ReplaceSelectionText(
            Nothing,
            Sub(a)
                If a.ColumnIndex > 1 Then
                    result.Append(vbTab)
                Else
                    If a.IsBox AndAlso a.RowIndex > 1 Then result.AppendLine("")
                End If
                result.Append(getNewText(a.Text))
            End Sub)

        System.Windows.Forms.Clipboard.SetText(result.ToString())
    End Sub

    Private Sub IncrementActiveButton_Click(sender As Object, e As RibbonControlEventArgs) Handles IncrementActiveButton.Click
        Dim app = ThisAddIn.Current.Application
        Dim target As Microsoft.Office.Interop.Excel.Range = app.Selection

        Dim text As String = target.Text
        Try
            Dim newText = Core.EditorString.IncrementText(text)
            If newText IsNot Nothing Then
                target.Formula = newText
            End If

        Catch ex As Exception
            MsgBox(My.Resources.Message1CannotIncrement, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub IncrementMaxButton_Click(sender As Object, e As RibbonControlEventArgs) Handles IncrementMaxButton.Click

        Dim cell As Range = ThisAddIn.Current.Application.ActiveCell

        Try
            Dim table As ListObject = cell.ListObject
            If table IsNot Nothing Then
                Dim values =
                From column As ListColumn In table.ListColumns
                Where column.Range.Column = cell.Column
                From row As ListRow In table.ListRows
                Let range As Range = column.DataBodyRange()(row.Index)
                Let text As String = range.Text
                Where String.IsNullOrWhiteSpace(text) = False
                Let a = (From b In text.Matches(Of Core.WithNumberText)(Core.EditorString.Pattern) Select b).FirstOrDefault()
                Where a IsNot Nothing
                Order By a.before Descending
                Order By a.number Descending
                Select New With {.Text = text, .P = a}

                Dim max = values.FirstOrDefault()
                If max IsNot Nothing Then
                    max.P.number += 1
                    Dim newText = Core.EditorString.IncrementText(max.Text, max.P.number)

                    cell.Formula = newText
                Else
                    cell.Formula = "1"
                End If
            End If

        Catch ex As Exception
            MsgBox(My.Resources.Message1CannotIncrement, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub TrimEndButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TrimEndButton.Click
        Dim app = ThisAddIn.Current.Application
        Dim myMacaron As New ExcelMacaron(app)
        myMacaron.ReplaceSelectionParagraphs(
            Nothing,
            Sub(a)
                a.Text = a.Text.TrimEnd()
            End Sub)
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

    Private Sub ReplaceWorksheetNamesButton_Click(sender As Object, e As RibbonControlEventArgs) Handles ReplaceWorksheetNamesButton.Click

        If Me.replacePane Is Nothing Then
            Dim c = New Replace()
            AddHandler c.RepaceButtonClick,
                Sub(sender2, e2)
                    Dim targetSheet As Worksheet
                    For Each targetSheet In ThisAddIn.Current.Application.ActiveWorkbook.Worksheets
                        targetSheet.Name = Strings.Replace(targetSheet.Name, e2.FindText, e2.ReplaceText)
                    Next targetSheet

                End Sub

            Me.replacePane = New ElementControlPane(Of Replace)(c)
            Me.replacePane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.replacePane.Control, "Replace Worksheet Names", ThisAddIn.Current.Application.ActiveWindow)
            Me.replacePane.Pane.Width = 350
        End If

        Me.replacePane?.Show()

    End Sub

    Private Sub CopyAsJsonButton_Click(sender As Object, e As RibbonControlEventArgs) Handles CopyAsJsonButton.Click


        Dim table = ThisAddIn.Current.Application.ActiveCell.ListObject
        If table IsNot Nothing Then

            Dim list As New List(Of Dictionary(Of String, Object))

            ' key = Column Index, value = Column Name
            Dim columns =
                (From column In table.ListColumns.ToEnumerable(Of ListColumn)
                 Select New With {.ColumnIndex = column.Range.Column, .Name = column.Name}
                    ).ToDictionary(Of String, String)(Function(item) item.ColumnIndex, Function(item) item.Name)

            For Each row As ListRow In table.ListRows
                Dim rowDictionary As New Dictionary(Of String, Object)
                For Each cell As Range In row.Range
                    Dim column = columns(cell.Column)
                    Dim value = cell.Text
                    If Not String.IsNullOrWhiteSpace(value) Then

                        Dim decimalValue As Decimal
                        Dim longValue As Long
                        If value.ToString().Contains(".") AndAlso
                            Decimal.TryParse(value, decimalValue) AndAlso
                            value.ToString() = decimalValue.ToString() Then

                            rowDictionary.Add(column, decimalValue)

                        ElseIf Long.TryParse(value, longValue) AndAlso
                            value.ToString() = longValue.ToString() Then

                            rowDictionary.Add(column, longValue)

                        Else
                            rowDictionary.Add(column, value)
                        End If
                    End If
                Next
                If rowDictionary.Count > 0 Then
                    list.Add(rowDictionary)
                End If
            Next

            Dim json = Newtonsoft.Json.JsonConvert.SerializeObject(list)
            'Debug.WriteLine(json)
            System.Windows.Forms.Clipboard.SetText(json)

        End If

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
