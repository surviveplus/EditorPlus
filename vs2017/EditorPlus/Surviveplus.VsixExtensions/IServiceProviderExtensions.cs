using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// IServiceProvider に対する拡張メソッドを定義する静的クラスです。
	/// </summary>
	public static class IServiceProviderExtensions
	{
		/// <summary>
		/// Visual Studio のメニューを登録します。
		/// コマンドは .vsct ファイルに定義され、実際のメソッドは IServiceProvider 実装オブジェクトに MenuCommand 属性付きのメソッドで実装されている必要があります。
		/// </summary>
		/// <param name="me">拡張メソッドを追加する元の型のオブジェクトです。</param>
		public static void InitializeCommands(this IServiceProvider me, Guid menuGroup) {
			if (me == null) throw new ArgumentNullException("me");

          var mcs = me.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (null != mcs) {
				var methods = me.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
				var targets = from m in methods
							  let att = m.GetCustomAttributes(typeof(MenuCommandAttribute), false).FirstOrDefault()
							  where att != null
							  select new
							  {
								  Method = m,
								  Attribute = att as MenuCommandAttribute
							  };

				foreach (var ma in targets) {
					var method = ma.Method;
					var id = ma.Attribute.Id;

					var menu = new MenuCommand((sender, e) => { method.Invoke(me, new object[] { sender, e }); }, new CommandID(menuGroup, id));

					mcs.AddCommand(menu);
                    VisualStudioMacaron.MenuCommands.Add(ma.Attribute.Id, menu);
				}
			} // end if

		} // end function
	
	} // end class
} // end namespace
