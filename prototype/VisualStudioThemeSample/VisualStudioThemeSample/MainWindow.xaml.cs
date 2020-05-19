using System;
using System.Collections.Generic;
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

namespace VisualStudioThemeSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme(Theme.Dark);


            var sample = from a in new int[] { 0, 1, 2, 3, 4, 5, 6 }
                         select new { Text1 = $"Item {a}", Text2 = $"Value {a}" };

            this.sampleListView.ItemsSource = sample;

        }

        private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio?.IsChecked == true)
            {
                var key = (Theme)radio.Tag;
                ApplyTheme(key);

            } // end if

        } // end sub

        private void ApplyTheme(Theme key)
        {
            foreach (var kvp in ThemeColors.pallete[key])
            {
                this.Resources[kvp.Key] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kvp.Value));
            }
        }
    } // end class
} // end namespace
