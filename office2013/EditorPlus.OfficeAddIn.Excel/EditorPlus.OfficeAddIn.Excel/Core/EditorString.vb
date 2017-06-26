Imports EditorPlus.OfficeAddIn.Excel.EditorPlusRibbon
Imports Net.Surviveplus.RegularExpressionQuery

Namespace Core
    Public Module EditorString

        Public Function IncrementText(text As String) As String


            Dim pattern = "^(?<before>.*?)(?<number>\d+?)(?<after>[^\d]*?)$"
            Dim a = (From b In text.Matches(Of WithNumberText)(pattern) Select b).FirstOrDefault()
            Dim ab = (From b In text.Matches(Of WithNumberTextB)(pattern) Select b).FirstOrDefault()
            If a IsNot Nothing Then

                a.number += 1
                Dim zeroCount = ab.number.Length - a.number.ToString().Length
                If zeroCount < 0 Then
                    zeroCount = 0
                End If
                ab.number = New String("0", zeroCount) & a.number

                Dim newText = text.Replace(Of WithNumberTextB)(pattern, ab)
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
