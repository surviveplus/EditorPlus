using EditorPlus.UI;
using Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EditorPlus.AI.UITest
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

        private ElementControlToolWindow<InsertText> insertTextToolWindow;

        private void InsertTextButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.insertTextToolWindow == null)
            {
                var favorites = new Favorites<string>();
                //favorites.Add("A1");
                //favorites.Add("B1");

                var c = new InsertText();
                c.InsertButtonClick += (sender2, e2) => {

                    //Debug.WriteLine("clicked");
                    favorites.Add(e2.Text);
                    c.Favorites = from f in favorites.GetFavorites() select new InsertTextFavorite { Text = f };
                };
                c.Favorites = from f in favorites.GetFavorites() select new InsertTextFavorite { Text = f };

                this.insertTextToolWindow = new ElementControlToolWindow<InsertText>(c);                
            }
            this.insertTextToolWindow?.Show();
        }
    }
}
