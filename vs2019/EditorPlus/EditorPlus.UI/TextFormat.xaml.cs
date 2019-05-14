using Net.Surviveplus.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Net.Surviveplus.EditorPlus.UI
{
    /// <summary>
    /// Interaction logic for TextFormatControl.xaml
    /// </summary>
    public partial class TextFormat : UserControl
    {
        #region コンストラクタ

        public TextFormat()
        {
            InitializeComponent();

            // ローカライズ
            WpfLocalization.ApplyResources(this, Properties.Resources.ResourceManager);

            this.formatText.SelectAll();
            this.formatText.Focus();
        } // end constructor

        #endregion

        #region プロパティ

        /// <summary>
        /// ユーザーが入力したフォーマットテキストを取得または設定します。
        /// </summary>
        public String Text
        {
            get
            {
                return this.formatText.Text;
            } // end get
            set
            {
                this.formatText.Text = value;
            } // end set
        } // end property

        /// <summary>
        /// フォーマットを実行可能かどうか？を取得または設定します。
        /// </summary>
        public bool Executable
        {
            get
            {
                return this.formatButton.IsEnabled;
            } // end get
            set
            {
                this.formatButton.IsEnabled = value;
            } // end set
        } // end property

        #endregion

        #region イベント

        private void formatButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Executed != null) this.Executed(this, EventArgs.Empty);

        } // end sub

        public event EventHandler<EventArgs> Executed;

        #endregion
    
	} // end class

} // end namespace