Imports EditorPlus.Core
Imports EditorPlus.UI
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.Core
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Project
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusRibbon

    Private Sub EditorPlusRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub

    Private insertTextToolWindow As ElementControlToolWindow(Of InsertText)

    Private Sub InsertTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertTextButton.Click

        If Me.insertTextToolWindow Is Nothing Then
            Dim c = New InsertText With {.LineButtonVisible = False}
            AddHandler c.InsertButtonClick,
                Sub(sender2, e2)

                    Dim macaron As New ProjectMacaron(ThisAddIn.Current.Application)
                    macaron.InsertText(e2.Text, e2.InsertTo, e2.SkipIfStartedOrEndWithText)
                End Sub

            Me.insertTextToolWindow = New ElementControlToolWindow(Of InsertText)(c, "Insert Text")
        End If

        Me.insertTextToolWindow?.Show()
    End Sub


    Private insertSerialNumberToolWindow As ElementControlToolWindow(Of InsertSerialNumber)

    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles Button1.Click

        If Me.insertSerialNumberToolWindow Is Nothing Then
            Dim c = New InsertSerialNumber With {.LineButtonVisible = False}
            AddHandler c.InsertButtonClick,
                Sub(sender2, e2)

                    Dim macaron As New ProjectMacaron(ThisAddIn.Current.Application)
                    macaron.InsertSerialNumber(e2.StartNumber, e2.InsertTo, e2.Padding, e2.SkipIfStartedOrEndWithText)
                End Sub

            Me.insertSerialNumberToolWindow = New ElementControlToolWindow(Of InsertSerialNumber)(c, "Insert Serial Number")
        End If

        Me.insertSerialNumberToolWindow?.Show()
    End Sub
End Class
