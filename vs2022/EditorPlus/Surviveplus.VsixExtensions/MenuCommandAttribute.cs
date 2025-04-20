using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Surviveplus.VsixExtensions
{

	/// <summary>
	/// Visual Studio コマンドのイベントハンドラとして動作するメソッドであることを示す属性です。
	/// この属性でマークされたメソッドが IServiceProviderExtensions.InitializeCommands で使用されます。
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)] public sealed class MenuCommandAttribute : Attribute
	{
		/// <summary>
		/// .vsct ファイルに記述したコマンドのIDを指定します。
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// MenuCommandAttribute クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="id">.vsct ファイルに記述したコマンドのIDを指定します。</param>
		public MenuCommandAttribute(int id) {
			this.Id = id;
		} // end constructor

	} // end class
} // end namespace
