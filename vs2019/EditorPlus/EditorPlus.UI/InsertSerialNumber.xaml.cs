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
	/// InsertSerialNumber.xaml の相互作用ロジック
	/// </summary>
	public partial class InsertSerialNumber : UserControl
	{
		#region コンストラクタ

		public InsertSerialNumber() {
			InitializeComponent();

            // ローカライズ
            WpfLocalization.ApplyResources(this, Properties.Resources.ResourceManager);

            this.StartNumberBox.SelectAll();
            this.StartNumberBox.Focus();

		} // end constructor
		#endregion


        #region イベント

        public event EventHandler<InsertSerialNumberEventArgs> Executed;

		private void InsertToHeadButton_Click(object sender, RoutedEventArgs e) {
            if (this.Executed != null) this.Executed(this, new InsertSerialNumberEventArgs() { InsertPosition = InsertPosition.HeadOfLine, PaddingKind = this.PaddingKind, Skip = this.Skip , StartNumberText = this.StartNumberText });
		} // end sub

		private void InsertToEndButton_Click(object sender, RoutedEventArgs e) {
            if (this.Executed != null) this.Executed(this, new InsertSerialNumberEventArgs() { InsertPosition = InsertPosition.EndOfLine, PaddingKind = this.PaddingKind, Skip = this.Skip, StartNumberText = this.StartNumberText });
        } // end sub

        #endregion

        #region プロパティ

        private PaddingKind PaddingKind
        {
            get
            {
                PaddingKind padding = PaddingKind.None;
                if (this.spacePadding.IsChecked.GetValueOrDefault()) padding = PaddingKind.Space;
                if (this.zeroPadding.IsChecked.GetValueOrDefault()) padding = PaddingKind.Zero;
                return padding;
            }
        } // end property


        /// <summary>
        /// ユーザーが入力した挿入する番号のテキストを取得または設定します。
        /// </summary>
        public String StartNumberText
        {
            get
            {
                return this.StartNumberBox.Text;
            } // end get
            set
            {
                this.StartNumberBox.Text = value;
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


	} // end class

    public class InsertSerialNumberEventArgs : EventArgs
    {
        public InsertPosition InsertPosition { get; set; }
        public PaddingKind PaddingKind { get; set; }

        public string StartNumberText { get; set; }

        public bool Skip { get; set;  }

    } // end class

    public enum PaddingKind
    {
        None,
        Zero,
        Space,
    }

    public enum InsertPosition
    {
        None,
        HeadOfLine,
        EndOfLine,
    }

} // end namespace
