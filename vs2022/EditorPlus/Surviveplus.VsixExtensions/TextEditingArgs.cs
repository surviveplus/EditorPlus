using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// テキスト編集で扱うデータを司るクラスです。
	/// </summary>
	public class TextEditingArgs 
	{
		/// <summary>
		/// テキストを取得または設定します。このテキストを変更すると、アクティブなテキストエディタの当該テキストが変更されます。
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// テキスト編集を実施しない場合は True を設定します。それ以外は False を設定します。
		/// </summary>
		public bool Cancel { get; set; }

	} // end class
} // end namespace
