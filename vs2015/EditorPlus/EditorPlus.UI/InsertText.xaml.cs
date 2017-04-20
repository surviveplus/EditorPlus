using Net.Surviveplus.Localization;
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

namespace Net.Surviveplus.EditorPlus.UI
{
	/// <summary>
	/// InsertTextControl.xaml の相互作用ロジック
	/// </summary>
	public partial class InsertText : UserControl
	{
        #region コンストラクタ

        public InsertText()
        {
			InitializeComponent();
            // ローカライズ
            WpfLocalization.ApplyResources(this, Properties.Resources.ResourceManager);

            this.InsertTextBox.SelectAll();
            this.InsertTextBox.Focus();

        } // end constructor

        #endregion

        #region プロパティ

        /// <summary>
        /// ユーザーが入力した挿入するテキストを取得または設定します。
        /// </summary>
        public String Text
        {
            get
            {
                return this.InsertTextBox.Text;
            } // end get
            set
            {
                this.InsertTextBox.Text = value;
            } // end set
        } // end property

        /// <summary>
        /// 挿入を実行可能かどうか？を取得または設定します。
        /// </summary>
        public bool Executable
        {
            get
            {
                return this.InsertToHeadButton.IsEnabled;
            } // end get
            set
            {
                this.InsertToHeadButton.IsEnabled = value;
                this.InsertToEndButton.IsEnabled = value;
            } // end set
        } // end property

        /// <summary>
        /// 一致する場合はスキップするかどうか？を取得または設定します。
        /// </summary>
        public bool Skip
        {
            get
            {
                return this.checkSkipStartOrEndWith.IsChecked.Value;
            }
            set
            {
                this.checkSkipStartOrEndWith.IsChecked = value;

            }
        } // end property

        #endregion


		#region ボタンイベント処理

        public event EventHandler<EventArgs> InsertToHeadExecuted;

		private void InsertToHeadButton_Click(object sender, RoutedEventArgs e) 
        {
            if (this.InsertToHeadExecuted != null) this.InsertToHeadExecuted(this, EventArgs.Empty);
		} // end sub

        public event EventHandler<EventArgs> InsertToEndExecuted;

        private void InsertToEndButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.InsertToEndExecuted != null) this.InsertToEndExecuted(this, EventArgs.Empty);
        } // end sub

		#endregion

	} // end class
} // end namespace
