
Imports Microsoft.Win32

Public Class OfficeTheme

    Public Shared ReadOnly Property Current As Theme
        Get

            Try
                Dim key = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Office\16.0\Common")
                If key IsNot Nothing Then
                    Dim uiTheme = CType(key.GetValue("UI Theme"), Integer)
                    Return CType(uiTheme, Theme)
                End If
            Catch
            End Try

            Return Theme.Colorful
        End Get
    End Property

End Class

Public Class OfficeThemeModel

    Public Shared Property Current As OfficeThemeModel = New OfficeThemeModel With {.Theme = OfficeTheme.Current}

    Public Property Theme As Theme

End Class

Public Enum Theme
    Colorful = 0
    DarkGray = 3
    Black = 4
    White = 5
End Enum

Public Class ControlTeheme
    Public Shared Property Control As ControlColors = New ControlColors()
    Public Shared Property Grid As GridColors = New GridColors()
    Public Shared Property Header As HeaderColors = New HeaderColors()

    Public Shared Sub Apply(officeTheme As Theme)
        Select Case officeTheme
            Case Theme.DarkGray
                With ControlTeheme.Control
                    .Background = New SolidColorBrush(Color.FromArgb(&HFF, &H66, &H66, &H66)) '#666666
                    .Foreground = New SolidColorBrush(Colors.White) 'white
                End With

                With ControlTeheme.Grid
                    .HorizontalGridLines = New SolidColorBrush(Color.FromArgb(&HFF, &HB9, &HB9, &HB9)) '#b9b9b9
                    .VerticalGridLines = New SolidColorBrush(Colors.Silver) 'Silver
                    .Background = Color.FromArgb(&HFF, &HD4, &HD4, &HD4) '#d4d4d4
                    .Foreground = New SolidColorBrush(Color.FromArgb(&HFF, &H4D, &H4D, &H4D)) '#4b4b4b
                    .HilightBackground = Color.FromArgb(&HFF, &HDD, &HF3, &HFE) '#DDF3FE
                    .HilightForeground = Colors.Black 'black
                    .CurrentBorder = New SolidColorBrush(Color.FromArgb(&HFF, &HD, &H58, &H98)) '#0d5898
                End With

                With ControlTeheme.Header
                    .Background = New SolidColorBrush(Color.FromArgb(&HFF, &H66, &H66, &H66)) '#666666
                    .Border = New SolidColorBrush(Colors.Black) 'black
                    .Foreground = New SolidColorBrush(Colors.White) 'white
                End With



            Case Theme.Black
                With ControlTeheme.Control
                    .Background = New SolidColorBrush(Color.FromArgb(&HFF, &H26, &H26, &H26)) '#262626
                    .Foreground = New SolidColorBrush(Colors.White) 'white
                End With

                'TODO: Case Theme.White

            Case Else 'Theme.Colorful

                With ControlTeheme.Control
                    .Background = New SolidColorBrush(Color.FromArgb(&HFF, &HE3, &HE3, &HE3)) '#FFE3E3E3
                    .Foreground = New SolidColorBrush(Color.FromArgb(&HFF, &H44, &H44, &H44)) '#FF444444
                End With

                With ControlTeheme.Grid
                    .HorizontalGridLines = New SolidColorBrush(Colors.Silver) 'Silver
                    .VerticalGridLines = New SolidColorBrush(Color.FromArgb(&HFF, &HF0, &HF0, &HF0)) '#FFF0F0F0
                    .Background = Colors.White 'white
                    .Foreground = New SolidColorBrush(Color.FromArgb(&HFF, &H44, &H44, &H44)) '#FF444444
                    .HilightBackground = Color.FromArgb(&HFF, &HDD, &HF3, &HFE) '#DDF3FE
                    .HilightForeground = Colors.Black 'black
                    .CurrentBorder = New SolidColorBrush(Color.FromArgb(&HFF, &HD, &H58, &H98)) '#0d5898
                End With

                With ControlTeheme.Header
                    .Background = New SolidColorBrush(Color.FromArgb(&HFF, &HFB, &HFB, &HFB)) '#FFFBFBFB
                    .Border = New SolidColorBrush(Colors.Silver) 'Silver
                    .Foreground = New SolidColorBrush(Colors.Black) 'Black
                End With

        End Select
    End Sub

End Class

Public Class ControlColors
    Public Property Background As Brush
    Public Property Foreground As Brush
End Class

Public Class GridColors
    Public Property Foreground As Brush
    Public Property Background As Color
    Public Property HorizontalGridLines As Brush
    Public Property VerticalGridLines As Brush
    Public Property HilightBackground As Color
    Public Property HilightForeground As Color
    Public Property CurrentBorder As Brush
End Class

Public Class HeaderColors
    Public Property Background As Brush
    Public Property Border As Brush
    Public Property Foreground As Brush
End Class