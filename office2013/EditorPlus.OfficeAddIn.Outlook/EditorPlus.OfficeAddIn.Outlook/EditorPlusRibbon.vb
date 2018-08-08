Imports System.Diagnostics
Imports Microsoft.Office.Tools.Ribbon

Public Class EditorPlusRibbon

    ''' <summary>
    ''' Allows managed code to call unmanaged functions with Platform Invocation Services (PInvoke).
    ''' </summary>
    Friend NotInheritable Class NativeMethods

#Region " Constructors (Can't initializes a new instance of the this class)"

        Private Sub New()
        End Sub

#End Region

#Region " Win32API Definitions "

        ' TODO: Insert the code of Declare of DllImport. (see static code analysis CA1060)
        Declare Function GetActiveWindow Lib "user32" () As IntPtr
        Declare Function FindWindow Lib "user32" Alias "FindWindow" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr


        Declare Function SetWindowPos Lib "user32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As IntPtr
        public Const HWND_TOPMOST = -1      '「常に手前」にする
        Public Const HWND_NOTOPMOST = -2    '「常に手前」を解除

        Public Const SWP_SHOWWINDOW = &H40   '表示する
        Public Const SWP_NOSIZE = &H1        'サイズを変更しない
        Public Const SWP_NOMOVE = &H2        '位置を変更しない
#End Region

    End Class

    Private Sub EditorPlusRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub

    Private Sub TopMostToggleButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TopMostToggleButton.Click

        Dim b = CType(sender, RibbonToggleButton)
        Dim topMost = b.Checked

        'MsgBox(topMost)

        Dim hwnd = NativeMethods.GetActiveWindow()
        Debug.WriteLine("hwnd : " + hwnd.ToString())

        'Dim inspector = ThisAddIn.Current.Application.ActiveInspector
        'Dim caption = inspector.GetType().InvokeMember("caption", System.Reflection.BindingFlags.GetProperty, Nothing, inspector, Nothing).ToString()
        'Dim hwnd2 = NativeMethods.FindWindow("rctrl_renwnd32\0", caption)
        'Debug.WriteLine("hwnd2 : " + hwnd2.ToString())

        If topMost Then
            Dim r = NativeMethods.SetWindowPos(hwnd, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, NativeMethods.SWP_SHOWWINDOW Or NativeMethods.SWP_NOMOVE Or NativeMethods.SWP_NOSIZE)
        Else
            Dim r = NativeMethods.SetWindowPos(hwnd, NativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, NativeMethods.SWP_SHOWWINDOW Or NativeMethods.SWP_NOMOVE Or NativeMethods.SWP_NOSIZE)
        End If

    End Sub
End Class
