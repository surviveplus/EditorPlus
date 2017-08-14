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

                    Dim insertAction As Action(Of TextActionsParameters) = Nothing
                    Select Case e2.InsertTo
                        Case InsertTo.Head, InsertTo.LineHead
                            insertAction =
                                Sub(a)
                                    If Not (e2.SkipIfStartedOrEndWithText AndAlso a.Text.StartsWith(e2.Text)) Then
                                        a.InsertBeforeText = e2.Text
                                    End If
                                End Sub
                        Case InsertTo.End, InsertTo.LineEnd
                            insertAction =
                                Sub(a)
                                    If Not (e2.SkipIfStartedOrEndWithText AndAlso a.Text.EndsWith(e2.Text)) Then
                                        a.InsertAfterText = e2.Text
                                    End If
                                End Sub
                    End Select

                    Select Case e2.InsertTo
                        Case InsertTo.Head, InsertTo.End
                            macaron.ReplaceSelectionText(Nothing, insertAction)

                        Case InsertTo.LineHead, InsertTo.LineEnd
                            macaron.ReplaceSelectionParagraphs(Nothing, insertAction)
                    End Select


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
                    Dim number As Long = e2.StartNumber
                    Dim numberLength = 0
                    Dim numberCounter As Long = e2.StartNumber

                    macaron.ReplaceSelectionText(
                        Sub(a)
                            numberCounter += 1
                            numberLength = numberCounter.ToString().Length

                        End Sub,
                        Sub(a)
                            Dim text = number.ToString()
                            Dim paddingCount = numberLength - text.Length
                            If paddingCount > 0 Then
                                Select Case e2.Padding
                                    Case NumberPadding.ZeroPadding
                                        text = New String("0", paddingCount) + text

                                    Case NumberPadding.SpacePadding
                                        text = New String(" ", paddingCount) + text
                                End Select
                            End If

                            If e2.SkipIfStartedOrEndWithText AndAlso
                                ((e2.InsertTo = InsertTo.Head OrElse e2.InsertTo = InsertTo.LineHead) AndAlso a.Text.StartsWith(text)) AndAlso
                                ((e2.InsertTo = InsertTo.End OrElse e2.InsertTo = InsertTo.LineEnd) AndAlso a.Text.EndsWith(text)) Then

                                a.IsSkipped = True
                            Else

                                Select Case e2.InsertTo
                                    Case InsertTo.Head
                                        a.InsertBeforeText = text
                                    Case InsertTo.End
                                        a.InsertAfterText = text
                                End Select
                            End If
                            number += 1
                        End Sub)

                End Sub

            Me.insertSerialNumberToolWindow = New ElementControlToolWindow(Of InsertSerialNumber)(c, "Insert Serial Number")
        End If

        Me.insertSerialNumberToolWindow?.Show()
    End Sub
End Class
