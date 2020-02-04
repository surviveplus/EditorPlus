Imports EditorPlus.Core
Imports EditorPlus.AI
Imports EditorPlus.UI
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Outlook
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlus

    Private Sub EditorPlus_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub

    Private Sub TopMostToggleButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TopMostToggleButton.Click
        AlwaysOnTop.EqualizeWithRibbonToggleButton(sender)
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

                    Dim macaron As New OutlookMacaron(ThisAddIn.Current.Application)
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

                    Dim macaron As New OutlookMacaron(ThisAddIn.Current.Application)
                    macaron.InsertSerialNumber(e2.StartNumber, e2.InsertTo, e2.Padding, e2.SkipIfStartedOrEndWithText)
                End Sub

            Me.insertSerialNumberPane = New ElementControlPane(Of InsertSerialNumber)(c)
            Me.insertSerialNumberPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.insertSerialNumberPane.Control, "Insert Serial Number", ThisAddIn.Current.Application.ActiveWindow)
            Me.insertSerialNumberPane.Pane.Width = 350
        End If

        Me.insertSerialNumberPane?.Show()
    End Sub
End Class
