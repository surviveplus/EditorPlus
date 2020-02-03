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

namespace OfficeThemeSample
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

        private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if( radio?.IsChecked == true )
            {

                // Hack: DataTrigger can read binding property on only first time.
                // If value of property is changed, we must reset datacontext of binding. 
                // INotifyPropertyChanged can't solve this problem.
                this.DataContext = null;
                OfficeTheme.Current.Theme = (Theme)radio.Tag;
                this.DataContext = OfficeTheme.Current;
            }

        } // end sub

        private void AccentColorsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.Resources.Apply((AccentColors)(sender as RadioButton)?.Tag);
        }
    } // end class
} // end namespace
