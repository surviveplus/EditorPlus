using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Net.Surviveplus.VsixExtensions
{
	/// <summary>
	/// Visual Studio 2010 以前のマクロや EmEditor のマクロと同様の感覚で、
	/// テキストエディタに対するマクロ的な拡張機能を実装するためのフレームワークです。
	/// </summary>
	/// <remarks>
	/// <para>
	/// アクティブなテキストエディタに対する処理を実行するには、ExecuteNormalEditing、ExecuteLineEditing、ExecuteWordEditing メソッドを使用します。
	/// </para>
	/// <para>
	/// 以前の Visual Studio マクロと同様の処理を実行するためには、Dte プロパティを取得して使用します。
	/// </para>
	/// </remarks>
	public sealed class Macro
	{
		// static メンバ

		#region static プロパティ

		/// <summary>
		/// MenuCommands プロパティのバッキングフィールドです。
		/// </summary>
		private static Dictionary<int, MenuCommand> valueOfMenuCommands = new Dictionary<int, MenuCommand>();

		/// <summary>
		/// Visual Studio に初期化済みの MenuCommand の辞書を取得します。
		/// </summary>
		public static Dictionary<int, MenuCommand> MenuCommands {
			get {
				return Macro.valueOfMenuCommands;
			} // end get
		} // end property 

		#endregion

		// インスタンスメンバ

		#region コンストラクタ

		/// <summary>
		/// Macro クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="service">Package あるいは ToolWindowPane を継承したクラスのオブジェクトを指定します。</param>
		public Macro(IServiceProvider service, [CallerMemberName] string callerMethodName = "" ) {

            if (service == null) throw new ArgumentNullException("service");
			this.packageOrToolWindowPane = service;

			if (string.IsNullOrWhiteSpace(callerMethodName) == false) {
				var methods = service.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
				var targets = from m in methods
							  where m.Name == callerMethodName
							  let att = m.GetCustomAttributes(typeof(MenuCommandAttribute), false).FirstOrDefault()
							  where att != null
							  select new
							  {
								  Method = m,
								  Attribute = att as MenuCommandAttribute
							  };
				var ma = targets.FirstOrDefault();
				if (ma != null && Macro.MenuCommands.ContainsKey(ma.Attribute.Id)) {
					this.valueOfCurrentMenuCommand = Macro.MenuCommands[ma.Attribute.Id];
				} 

			} // end if

		} // end constructor

		/// <summary>
		/// マクロとして操作する DTE を取得する事が出来るオブジェクトです。
		/// </summary>
		private IServiceProvider packageOrToolWindowPane;

		#endregion

		#region プロパティ
		/// <summary>
		/// Dte プロパティのバッキングフィールドです。
		/// </summary>
		private DTE2 valueOfDte;

		/// <summary>
		/// 現在の DTE オブジェクトを取得します。
		/// </summary>
		public DTE2 Dte {
			get {
				if (this.valueOfDte == null) this.valueOfDte = (DTE2)this.packageOrToolWindowPane.GetService(typeof(DTE));
				return this.valueOfDte;
			} // end get
		} // end property

		/// <summary>
		/// 現在の DTE のイベント全てにアクセスできる取得します。
		/// Dte プロパティでアクセス出来るイベントには制限があるため、Event2 型で取得出来るこちらのプロパティを使用してください。
		/// </summary>
		public Events2 Events {
			get {
				return this.Dte.Events as EnvDTE80.Events2;
			} // end get
		} // end property

		/// CurrentMenuCommand プロパティのバッキングフィールドです。
		/// </summary>
		private MenuCommand valueOfCurrentMenuCommand;

		/// <summary>
		/// このMacroオブジェクトが MenuCommand に登録されたメソッドで初期化された時は、MenuCommand を取得します。それ以外は null 参照 ( Visual Basic では Nothing ) を返します。
		/// </summary>
		public MenuCommand CurrentMenuCommand {
			get {
				return this.valueOfCurrentMenuCommand;
			} // end get
		} // end property


		/// <summary>
		/// ドキュメントがアクティブかどうか？を取得します。アクティブな時は True 、それ以外は False です。
		/// </summary>
		public bool DocumentIsActive {
			get {
				return !(this.Dte == null || this.Dte.ActiveDocument == null);
			}
		} // end property

		/// <summary>
		/// ドキュメントがアクティブであり、かつ、アクティブなウィンドウかどうか？を取得します。アクティブな時は True 、それ以外は False です。
		/// </summary>
		public bool DocumentIsActiveWindow {
			get {
				if( this.DocumentIsActive == false ) return false;
				return (this.Dte.ActiveDocument.ActiveWindow == this.Dte.ActiveWindow);
			}
		} // end property

		/// <summary>
		/// アクティブな Visual Studio ソリューション プロジェクト の一覧（クエリ）を返します。
		/// </summary>
		public IEnumerable<EnvDTE.Project> ActiveSolutionProjects {
			get {
				dynamic projects = this.Dte.ActiveSolutionProjects;
				var ps = from p in (projects as System.Object[])
							select p as EnvDTE.Project;
				return ps;
			}
		} // end property

		#endregion	

		#region Execute Normal/Line/Word Editing メソッド

		/// <summary>
		/// アクティブなテキストに対する通常の編集を実行します。
		/// </summary>
		/// <param name="acting"></param>
		/// <param name="act"></param>
		public void ExecuteNormalEditing(Action<TextEditingArgs>acting, Action<TextEditingArgs> act) {

			// 引数の検証
			// acting は null で構いません。
			if (act == null) throw new ArgumentNullException("act");

			// アクティブドキュメントがない場合は例外
			if (this.Dte == null || this.Dte.ActiveDocument == null) throw new ActiveDocumentIsNullException();

			var activeSelection = this.Dte.ActiveDocument.Selection as EnvDTE.TextSelection;

			// 選択範囲が無いときは、全体を選択します。
			if (activeSelection.IsEmpty) activeSelection.SelectAll();

			// 選択範囲に対してカレントマクロを実行します。
			var a = new TextEditingArgs();
			a.Text = activeSelection.Text;

			if(acting != null ) acting(a);

			a.Text = activeSelection.Text;
			a.Cancel = false;
			act(a);

			if (a.Cancel == false ) {
				activeSelection.Insert(a.Text, (int)vsInsertFlags.vsInsertFlagsContainNewText);
				this.Dte.ActiveDocument.Activate();
			} // end if

		} // end sub

		/// <summary>
		/// アクティブなテキストに対する行単位の編集を実行します。
		/// </summary>
		/// <param name="acting"></param>
		/// <param name="act"></param>
		public void ExecuteLineEditing(Action<TextEditingArgs>acting, Action<TextEditingArgs> act) {

			// 引数の検証
			// acting は null で構いません。
			if (act == null) throw new ArgumentNullException("act");

			// アクティブドキュメントがない場合は例外
			if (this.Dte == null || this.Dte.ActiveDocument == null) throw new ActiveDocumentIsNullException();

			var activeSelection = this.Dte.ActiveDocument.Selection as EnvDTE.TextSelection;

			if (activeSelection.IsEmpty) {
				// 選択範囲が無いときは、行を選択します（最後の改行を含めないために、カーソル移動をエミュレートします）。
				activeSelection.StartOfLine();
				activeSelection.EndOfLine( true );
			}else { 
				// 選択範囲があるときは、先頭と末尾の選択範囲を、行頭と行末に拡張します。
				// ただし選択範囲の末尾が行頭の場合は、その行を含まないようにします。
				var lineTop = activeSelection.TopPoint.Line;
				var lineBottom = activeSelection.BottomPoint.Line;
				var bottomIsAtStartOfLine = activeSelection.BottomPoint.AtStartOfLine;

				activeSelection.MoveToLineAndOffset( lineTop, 1 );

				if (bottomIsAtStartOfLine) {
					activeSelection.MoveToLineAndOffset( lineBottom - 1, 1, true );
				}else{
					activeSelection.MoveToLineAndOffset( lineBottom, 1, true );
				} // end if

				activeSelection.EndOfLine( true );
			} // end if

			// 選択範囲の行毎に、カレントマクロを実行します。
			var a = new TextEditingArgs();
			var oldText = activeSelection.Text;
			var lines = oldText.Replace("\r\n", "\n").Split('\n');
			if (acting != null) {
				foreach (var line in lines) {
					a.Text = line;
					acting(a);
				} // next line
			} // end if

			var result = new StringBuilder();
			bool isFirst = true;
			foreach (var line in lines) {
				a.Text = line;
				a.Cancel = false;

				act(a);

				if( isFirst == false) result.Append("\r\n");

				if (a.Cancel == false) {
					result.Append(a.Text);
				}else {
					result.Append(line);
				} // end if

				isFirst = false;
			} // next line

			activeSelection.Insert(result.ToString(), (int)vsInsertFlags.vsInsertFlagsContainNewText);
			this.Dte.ActiveDocument.Activate();
		} // end sub

		/// <summary>
		/// アクティブなテキストに対する単語単位の編集を実行します。
		/// </summary>
		/// <param name="acting"></param>
		/// <param name="act"></param>
		public void ExecuteWordEditing(Action<TextEditingArgs>acting, Action<TextEditingArgs> act) {

			// 引数の検証
			// acting は null で構いません。
			if (act == null) throw new ArgumentNullException("act");

			// アクティブドキュメントがない場合は例外
			if (this.Dte == null || this.Dte.ActiveDocument == null) throw new ActiveDocumentIsNullException();

			var activeSelection = this.Dte.ActiveDocument.Selection as EnvDTE.TextSelection;

			// 選択範囲が無いときは、カーソルの左の単語を選択します。
			if (activeSelection.IsEmpty) {
				activeSelection.WordLeft();
				activeSelection.WordRight(true);
			} // end if

			// 選択範囲に対してカレントマクロを実行します。
			var a = new TextEditingArgs();
			a.Text = activeSelection.Text;
			if (acting != null) acting(a);

			a.Text = activeSelection.Text;
			a.Cancel = false;
			act(a);

			if (a.Cancel == false) {
				activeSelection.Insert(a.Text, (int)vsInsertFlags.vsInsertFlagsContainNewText);
				this.Dte.ActiveDocument.Activate();
			} // end if
		} // end sub

		#endregion

		#region ShowMessageBox メソッド

		/// <summary>
		/// メッセージボックスを表示します。
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		public void ShowMessageBox(string title, string message) {

			// Show a Message Box to prove we were here
			var uiShell = (IVsUIShell)this.packageOrToolWindowPane.GetService(typeof(SVsUIShell));
			Guid clsid = Guid.Empty;
			int result;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
					   0,
					   ref clsid,
					   title,
					   message,
					   string.Empty,
					   0,
					   OLEMSGBUTTON.OLEMSGBUTTON_OK,
					   OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
					   OLEMSGICON.OLEMSGICON_INFO,
					   0,        // false
					   out result));
		} 

		#endregion

	} // end class

} // end namespace
