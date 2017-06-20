using Microsoft.VisualStudio.Shell;
using Net.Surviveplus.EditorPlus.UI.Test.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

namespace Net.Surviveplus.EditorPlus.UI.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.RefreshCurrentUICultureButton();
        } // end constructor

        private CultureInfo uiCulture = CultureInfo.CurrentUICulture;

        private void CurrentUICultureButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.uiCulture.Name)
            {
                case "en-US":
                    this.uiCulture = CultureInfo.GetCultureInfo("ja-JP");
                    break;

                default:
                    this.uiCulture = CultureInfo.GetCultureInfo("en-US");
                    break;
            } // end switch
            this.RefreshCurrentUICultureButton();

        } // end sub

        private void RefreshCurrentUICultureButton()
        {
            this.CurrentUICultureButton.Content = "CurrentUICulture : " + this.uiCulture.DisplayName;
        } // end sub

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = this.uiCulture;

            var c = new Net.Surviveplus.EditorPlus.UI.About();
            
            var w = new ToolWindow() { Title = "About", Content = c };
            toolWindows.Add(w);
            w.Show();
        } // end sub

        private void TextFormatButton_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = this.uiCulture;

            var c = new Net.Surviveplus.EditorPlus.UI.TextFormat() { Executable = this.executable };

            // ボタンが押されたら、Text プロパティを表示してみます。
            c.Executed += (s2, e2) =>
            {
                var c2 = s2 as TextFormat;
                MessageBox.Show(c2.Text);
            };

            // メインウィンドウの Executable ボタンと連動して、ボタンの有効・無効を切り替えます。
            this.ExecutableChanged += (s2, e2) =>
            {
                c.Executable = this.executable;
            };

            var w = new ToolWindow() { Title = "Text Format", Content = c };
            this.UpdateResoures(w);
            toolWindows.Add(w);
            w.Show();

        } // end sub


        private void InsertTextButton_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = this.uiCulture;

            var c = new Net.Surviveplus.EditorPlus.UI.InsertText() { Executable = this.executable };

            // ボタンが押されたら、Text プロパティを表示してみます。
            c.InsertToHeadExecuted += (s2, e2) =>
            {
                var c2 = s2 as InsertText;
                MessageBox.Show( "行頭挿入 / スキップ:" + c2.Skip.ToString() +"\r\n" + "\r\n" + c2.Text);
            };

            // ボタンが押されたら、Text プロパティを表示してみます。
            c.InsertToEndExecuted += (s2, e2) =>
            {
                var c2 = s2 as InsertText;
                MessageBox.Show("行末挿入  / スキップ:" + c2.Skip.ToString() + "\r\n" + "\r\n" + c2.Text);
            };

            // メインウィンドウの Executable ボタンと連動して、ボタンの有効・無効を切り替えます。
            this.ExecutableChanged += (s2, e2) =>
            {
                c.Executable = this.executable;
            };

            var w = new ToolWindow() { Title = "Insert Text", Content = c };
            this.UpdateResoures(w);
            toolWindows.Add(w);
            w.Show();
        }

        private void InsertSerialNumberButton_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = this.uiCulture;

            var c = new Net.Surviveplus.EditorPlus.UI.InsertSerialNumber() { Executable = this.executable };

            // ボタンが押されたら、Text プロパティを表示してみます。
            c.Executed += (s2, e2) =>
            {
                MessageBox.Show("挿入:" + e2.InsertPosition.ToString() + " / スキップ:" + e2.Skip.ToString() + " / 埋め：" + e2.PaddingKind.ToString() + "\r\n" + "\r\n" + e2.StartNumberText);
            };

            // メインウィンドウの Executable ボタンと連動して、ボタンの有効・無効を切り替えます。
            this.ExecutableChanged += (s2, e2) =>
            {
                c.Executable = this.executable;
            };

            var w = new ToolWindow() { Title = "Insert Serial Number", Content = c };
            this.UpdateResoures(w);
            toolWindows.Add(w);
            w.Show();
        }

        private void UpdateResoures(ToolWindow w)
        {
            if (this.BuleRadio.IsChecked.Value)
            {
                w.Resources[VsBrushes.WindowKey] = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                w.Resources[VsBrushes.WindowTextKey] = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                w.Resources[VsBrushes.ButtonFaceKey] = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                w.Resources[VsBrushes.ButtonTextKey] = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                w.Resources[VsBrushes.ButtonShadowKey] = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));
            }
            else if (this.LightRadio.IsChecked.Value)
            {
                w.Resources[VsBrushes.WindowKey] = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
                w.Resources[VsBrushes.WindowTextKey] = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                w.Resources[VsBrushes.ButtonFaceKey] = new SolidColorBrush(Color.FromArgb(255, 204, 206, 219));
                w.Resources[VsBrushes.ButtonTextKey] = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                w.Resources[VsBrushes.ButtonShadowKey] = new SolidColorBrush(Color.FromArgb(255, 204, 206, 219));
            }
            else if (this.DarkRadio.IsChecked.Value)
            {
                w.Resources[VsBrushes.WindowKey] = new SolidColorBrush(Color.FromArgb(255, 37, 37, 38));
                w.Resources[VsBrushes.WindowTextKey] = new SolidColorBrush(Color.FromArgb(255, 241, 241, 241));
                w.Resources[VsBrushes.ButtonFaceKey] = new SolidColorBrush(Color.FromArgb(255, 63, 63, 70));
                w.Resources[VsBrushes.ButtonTextKey] = new SolidColorBrush(Color.FromArgb(255, 241, 241, 241));
                w.Resources[VsBrushes.ButtonShadowKey] = new SolidColorBrush(Color.FromArgb(255, 63, 63, 70));
            }
        }

        private List<ToolWindow> toolWindows = new List<ToolWindow>();

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (var w in this.toolWindows.ToList())
            {
                w.Close();
                this.toolWindows.Remove(w);
            } // next w

        }

        private bool executable = true;
        public event EventHandler<EventArgs> ExecutableChanged;

        private void ExecutableButton_Click(object sender, RoutedEventArgs e)
        {
            this.executable = ! this.executable;

            var button = sender as Button;
            if (this.executable)
            {
                button.Content = "Executable";
            }
            else
            {
                button.Content = "Not Executable";
            } // end if

            
            if (this.ExecutableChanged != null) this.ExecutableChanged(this, EventArgs.Empty);
        }

        private void BuleRadio_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var w in this.toolWindows.ToList())
            {
                this.UpdateResoures(w);
            } // next w
        }

        private void LightRadio_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var w in this.toolWindows.ToList())
            {
                this.UpdateResoures(w);
            } // next w
        }

        private void DarkRadio_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var w in this.toolWindows.ToList())
            {
                this.UpdateResoures(w);
            } // next w

        }
    } // end class
} // end namespace
