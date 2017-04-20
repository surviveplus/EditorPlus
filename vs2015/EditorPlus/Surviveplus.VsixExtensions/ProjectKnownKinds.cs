using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Surviveplus.VsixExtensions
{

	/// <summary>
	/// EnvDTE.Project.Kind プロパティの値に該当するプロジェクトの種類を表す値です。
	/// </summary>
	public enum ProjectKnownKind
	{
		/// <summary>
		/// その他のプロジェクトを表します。
		/// </summary>
		None,

		/// <summary>
		/// Visual Basic プロジェクトを表します。
		/// </summary>
		VisualBasic,

		/// <summary>
		/// C#プロジェクトを表します。
		/// </summary>
		CSharp,

		/// <summary>
		/// Microsoft Visual Studio Installer プロジェクトを表します。
		/// </summary>
		VisualStudioInstaller,

        /// <summary>
        /// TypeScript プロジェクトを表します。
        /// Visual Studio 2015 では C# プロジェクトと同じ GUID になるため、ファイルの拡張子から検知する必要があります。
        /// </summary>
        TypeScript,

        /// <summary>
        /// JavaScript プロジェクトを表します。
        /// </summary>
        JavaScript,

	} // end enum
} // end namespace
