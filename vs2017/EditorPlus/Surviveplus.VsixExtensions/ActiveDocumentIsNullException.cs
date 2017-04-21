using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// アクティブなドキュメントが無い場合にスローされる例外です。
	/// </summary>
	/// <remarks>
	/// <para>変更履歴</para>
	/// </remarks>
	[Serializable]
	public class ActiveDocumentIsNullException : Exception
	{
		// オーバーライド・インターフェイス実装

		#region Exception メンバ

		/// <summary>
		/// パラメーター名と追加の例外情報を使用して SerializationInfo オブジェクトを設定します。
		/// </summary>
		/// <param name="info">シリアル化されたオブジェクト データを保持するオブジェクト。</param>
		/// <param name="context">転送元または転送先に関するコンテキスト情報。</param>
		/// <exception cref="System.ArgumentNullException">info オブジェクトが null 参照 (Visual Basic の場合は Nothing) です。</exception>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
			base.GetObjectData(info, context);

			// TODO: プライベートメンバを info オブジェクトに記録します。例えば整数の "Value" プロパティを記録するには、次のように記述します。
			// info.AddValue("Value", this.Value);
		} // end sub

		#endregion

		#region コンストラクタ

		/// <summary>
		/// ActiveDocumentIsNullException クラスの新しいインスタンスを初期化します。
		/// </summary>
		public ActiveDocumentIsNullException() { }

		/// <summary>
		/// 指定したエラー メッセージを使用して、ActiveDocumentIsNullException クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="message">エラーを説明するメッセージ。</param>
		public ActiveDocumentIsNullException(string message) : base(message) { }

		/// <summary>
		/// 指定したエラー メッセージと、この例外の原因である内部例外への参照を使用して、ActiveDocumentIsNullException クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="message">例外の原因を説明するエラー メッセージ。</param>
		/// <param name="inner">現在の例外の原因である例外。内部例外が指定されていない場合は null 参照 (Visual Basic の場合は Nothing) 。</param>
		public ActiveDocumentIsNullException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// シリアル化したデータを使用して、ActiveDocumentIsNullException クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="info">シリアル化されたオブジェクト データを保持するオブジェクト。</param>
		/// <param name="context">転送元または転送先に関するコンテキスト情報。</param>
		protected ActiveDocumentIsNullException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) {

			// TODO: プライベートメンバを info オブジェクトを使って初期化します。例えば整数の "Value" プロパティを初期化するには、次のように記述します。
			// this.Value = info.GetInt32("Value");

		} // end constructor

		#endregion

	} // end class
} // end namespace
