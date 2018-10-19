using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Net.Surviveplus.CodedUIQuery;

namespace PowerPointPreview
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region WinAPI

        private class WinAPI
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

            [System.Runtime.InteropServices.DllImport("User32.dll")]
            public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        } // end class

        #endregion

        private void GetItemButton_Click(object sender, RoutedEventArgs e)
        {
            var query = Desktop.Elements.WaitForChildren("{PPTFrameClass}", TimeSpan.FromSeconds(10))
                .Children().Skip(2).FirstOrDefault()
                //.Find("{MDIClient}")
                .Children().FirstOrDefault()
                .Children().Skip(2).FirstOrDefault()
                .Children().FirstOrDefault()
                .Children()
                ;

            Debug.WriteLine(query.Count());
            foreach (var item in query)
            {
                Debug.WriteLine(item.Text);


                var window = (item as WpfElement)?.UIAutomationElement;
                var windowBounds = window?.Current.BoundingRectangle;

                using (var windowBitmap = new Bitmap((int)windowBounds.Value.Width, (int)windowBounds.Value.Height)) {
                    using (var g = Graphics.FromImage(windowBitmap))
                    {
                        WinAPI.PrintWindow(new IntPtr(window.Current.NativeWindowHandle), g.GetHdc(), 0);
                    } // end using


                } // end using

            }
        }
    }
}
