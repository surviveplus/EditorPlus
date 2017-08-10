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
            Dim c = New InsertText
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
End Class
