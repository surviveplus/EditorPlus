using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Net.Surviveplus.VsixExtensions.Properties;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// Package クラスに対する拡張メソッドを定義する静的クラスです。
	/// </summary>
	public static class PackageExtensions
	{
		#region ActivateToolWindow　拡張メソッド

		/// <summary>
		/// ツールウィンドウのインスタンスを探し、無い場合は作成し、表示します。
		/// 表示されたツールウィンドウは、次回の Visual Studio 起動時にはこのメソッドを介さずに復元されるので注意が必要です。
		/// </summary>
		/// <param name="me">拡張メソッドを追加する元の型のオブジェクトです。</param>
		/// <param name="toolWindowType">表示するツールウィンドウの型を指定します。</param>
		public static void ActivateToolWindow(this Package me, Type toolWindowType) {
			if (me == null) throw new ArgumentNullException("me");

			// Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            var window = me.FindToolWindow(toolWindowType, 0, true);
            if ((null == window) || (null == window.Frame))
            {
                // ツールウィンドウが見つかりません。
                throw new NotSupportedException(Resources.ErrorMessageForNotFoundToolWindow);
            }
            var windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());

		} // end function

		#endregion
	} // end class
} // end namespace
