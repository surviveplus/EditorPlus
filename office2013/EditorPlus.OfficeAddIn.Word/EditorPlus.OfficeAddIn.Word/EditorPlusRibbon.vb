Imports EditorPlus.UI
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.Core
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Word

Public Class EditorPlusRibbon

    Private Sub EditorPlusRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub

    Private insertTextPane As ElementControlPane(Of InsertText)

    Private Sub InsertTextButton_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertTextButton.Click

        If Me.insertTextPane Is Nothing Then

            Dim c = New InsertText
            AddHandler c.InsertButtonClick,
                Sub(sender2, e2)

                    Dim macaron As New WordMacaron(ThisAddIn.Current.Application)

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

            Me.insertTextPane = New ElementControlPane(Of InsertText)(c)
            Me.insertTextPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.insertTextPane.Control, "Insert Text", ThisAddIn.Current.Application.ActiveWindow)
            Me.insertTextPane.Pane.Width = 350
        End If

        Me.insertTextPane?.Show()
    End Sub
End Class
