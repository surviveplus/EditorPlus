Imports EditorPlus.OfficeAddIn.Excel.EditorPlusRibbon
Imports Net.Surviveplus.RegularExpressionQuery

Namespace Core
    Public Module EditorString

        Public Const Pattern As String = "^(?<before>.*?)(?<number>\d+?)(?<after>[^\d]*?)$"

        Public Function IncrementText(text As String, Optional newNumber As Nullable(Of Long) = Nothing) As String


            Dim a = (From b In text.Matches(Of WithNumberText)(Pattern) Select b).FirstOrDefault()
            Dim ab = (From b In text.Matches(Of WithNumberTextB)(Pattern) Select b).FirstOrDefault()
            If a IsNot Nothing Then

                If newNumber.HasValue Then
                    a.number = newNumber.Value
                Else
                    a.number += 1
                End If

                Dim zeroCount = ab.number.Length - a.number.ToString().Length
                If zeroCount < 0 Then
                    zeroCount = 0
                End If
                ab.number = New String("0", zeroCount) & a.number

                Dim newText = text.Replace(Of WithNumberTextB)(Pattern, ab)
                Return newText
            End If

            Return Nothing
        End Function

    End Module

    Public Class WithNumberText

        Public Property before As String
        Public Property number As Long
        Public Property after As String

    End Class

    Public Class WithNumberTextB

        Public Property before As String
        Public Property number As String
        Public Property after As String

    End Class


End Namespace
