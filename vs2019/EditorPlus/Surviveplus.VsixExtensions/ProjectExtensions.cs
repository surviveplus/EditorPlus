using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// 拡張メソッドを定義する静的クラスです。
	/// </summary>
	public static class ProjectExtensions
	{
		#region KnownKind 拡張メソッド

		/// <summary>
		/// EnvDTE.Project.Kind プロパティの値に該当するプロジェクトの種類を表す値を取得します。
		/// </summary>
		/// <param name="me">拡張メソッドを追加する元の型のオブジェクトです。</param>
		/// <returns>
		/// Kind プロパティが知られているプロジェクトの場合は、種類を表す値を返します。それ以外は ProjectKnownKinds.None を返します。
		/// </returns>
		public static ProjectKnownKind KnownKind(this EnvDTE.Project me) {
			if (me == null) throw new ArgumentNullException("me");

			switch (me.Kind) {
				case "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}":
					return ProjectKnownKind.VisualBasic;

				case "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}":
					return ProjectKnownKind.CSharp;

				case "{54435603-DBB4-11D2-8724-00A0C9A8B90C}":
					return ProjectKnownKind.VisualStudioInstaller;

				default:
					return ProjectKnownKind.None;
			} // end switch

			
		} // end function
		#endregion

	} // end class
} // end namespace

