Imports Net.Surviveplus.SakuraMacaron.Core

Public Module MacaronExtensions


    <Runtime.CompilerServices.Extension()>
    Public Sub InsertText(macaron As Macaron, text As String, insertTo As InsertTo, skipIfStartedOrEndWithText As Boolean)

        Dim insertAction As Action(Of TextActionsParameters) = Nothing
        Select Case insertTo
            Case InsertTo.Head, InsertTo.LineHead
                insertAction =
                    Sub(a)
                        If Not (skipIfStartedOrEndWithText AndAlso text.StartsWith(text)) Then
                            a.InsertBeforeText = text
                        End If
                    End Sub
            Case InsertTo.End, InsertTo.LineEnd
                insertAction =
                    Sub(a)
                        If Not (skipIfStartedOrEndWithText AndAlso a.Text.EndsWith(text)) Then
                            a.InsertAfterText = text
                        End If
                    End Sub
        End Select

        Select Case insertTo
            Case InsertTo.Head, InsertTo.End
                macaron.ReplaceSelectionText(Nothing, insertAction)

            Case InsertTo.LineHead, InsertTo.LineEnd
                macaron.ReplaceSelectionParagraphs(Nothing, insertAction)
        End Select


    End Sub


    <Runtime.CompilerServices.Extension()>
    Public Sub InsertSerialNumber(macaron As Macaron, startNumber As Long, insertTo As InsertTo, padding As NumberPadding, skipIfStartedOrEndWithText As Boolean)

        Dim number As Long = startNumber
        Dim numberLength = 0
        Dim numberCounter As Long = startNumber

        Dim preAction As Action(Of TextActionsParameters) =
            Sub(a)
                numberCounter += 1
                numberLength = numberCounter.ToString().Length

            End Sub

        Dim insertAction As Action(Of TextActionsParameters) =
            Sub(a)
                Dim text = number.ToString()
                Dim paddingCount = numberLength - text.Length
                If paddingCount > 0 Then
                    Select Case padding
                        Case NumberPadding.ZeroPadding
                            text = New String("0", paddingCount) + text

                        Case NumberPadding.SpacePadding
                            text = New String(" ", paddingCount) + text
                    End Select
                End If

                If skipIfStartedOrEndWithText AndAlso
                    ((insertTo = InsertTo.Head OrElse insertTo = InsertTo.LineHead) AndAlso a.Text.StartsWith(text)) AndAlso
                    ((insertTo = InsertTo.End OrElse insertTo = InsertTo.LineEnd) AndAlso a.Text.EndsWith(text)) Then

                    a.IsSkipped = True
                Else

                    Select Case insertTo
                        Case InsertTo.Head, InsertTo.LineHead
                            a.InsertBeforeText = text
                        Case InsertTo.End, InsertTo.LineEnd
                            a.InsertAfterText = text
                    End Select
                End If
                number += 1
            End Sub

        Select Case insertTo
            Case InsertTo.Head, InsertTo.End
                macaron.ReplaceSelectionText(preAction, insertAction)

            Case InsertTo.LineHead, InsertTo.LineEnd
                macaron.ReplaceSelectionParagraphs(preAction, insertAction)
        End Select
    End Sub


End Module


Public Enum InsertTo
    Head
    LineHead
    LineEnd
    [End]
End Enum

Public Enum NumberPadding
    NonePadding
    SpacePadding
    ZeroPadding
End Enum