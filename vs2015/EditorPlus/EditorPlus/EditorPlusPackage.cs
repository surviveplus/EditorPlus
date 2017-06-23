using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

using Net.Surviveplus.VsixExtensions;
using Net.Surviveplus.EditorPlus.Core;
using System.IO;
using System.Linq;
using Net.Surviveplus.EditorPlus.ToolWindows;

namespace Net.Surviveplus.EditorPlus
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// The minimum requirement for a class to be considered a valid package for Visual Studio is to implement the IVsPackage interface and register itself with the shell. This package uses the helper classes defined inside the Managed Package Framework (MPF) to do it: it derives from the Package class that provides the implementation of the  IVsPackage interface and uses the registration attributes defined in the framework to  register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    // This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(MyToolWindow)),
     ProvideToolWindow(typeof(TextFormatToolWindow)),
     ProvideToolWindow(typeof(InsertTextToolWindow)),
     ProvideToolWindow(typeof(InsertSerialNumberToolWindow))]
    [Guid(GuidList.guidEditorPlusPkgString)]
    public sealed class EditorPlusPackage : Package
    {
        // Overridden Package Implementation

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            this.InitializeCommands(GuidList.guidEditorPlusCmdSet);

            var macaron = new VisualStudioMacaron(this);
            macaron.Events.WindowEvents.WindowActivated += WindowEvents_WindowActivated;

        } // end sub

        #endregion


        // インスタンスメンバ

        #region コンストラクタ

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require any Visual Studio service because at this point the package object is created but not sited yet inside Visual Studio environment. The place to do all the other initialization is the Initialize method.
        /// </summary>
        public EditorPlusPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        } // end constructor

        #endregion

        #region メニューコマンド

        [MenuCommand(0x0106)]
        public void CreateWorkTextFile(object sender, EventArgs e)
        {

            var macaron = new VisualStudioMacaron(this);

            var file = FileMacro.GetNewWorkTextFile();
            FileMacro.CreateFile(file);

            macaron.Dte.ItemOperations.OpenFile(file.FullName);
            macaron.Dte.ActiveDocument.Activate();
        } // end function

        /// <summary>
        /// アクティブなファイルが保存されているフォルダを、Windows エクスプローラで開きます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Visual Studio 2008 標準の機能と異なり、ファイルの保存場所を開きます。
        /// また、フォルダツリーを表示しません（いわゆる エクスプローラ スタイルではありません）。
        /// ファイルが選択されていない時は、アクティブなプロジェクトのパスを開きます。
        /// また、ソリューションツリーでセットアッププロジェクトを選択しているときは、そのプロジェクトのパスを開きます。
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), 
         MenuCommand(0x0107)]
        public void OpenActiveFileFolder(object sender, EventArgs e)
        {

            FileInfo file = null;
            DirectoryInfo folder = null;

            var macaron = new VisualStudioMacaron(this);
            if (macaron.Dte == null) return;

            if (macaron.Dte.ActiveDocument != null)
            {
                file = new FileInfo(macaron.Dte.ActiveDocument.FullName);
                if (file.Exists == false) file = null;
            } // end if

            var project = macaron.ActiveSolutionProjects.FirstOrDefault();
            if (project != null &&
                string.IsNullOrWhiteSpace(project.FullName) == false)
            {

                folder = new DirectoryInfo(Path.GetDirectoryName(project.FullName));
                if (folder.Exists == false) folder = null;

                switch (project.KnownKind())
                {
                    case ProjectKnownKind.VisualStudioInstaller:
                        file = null;
                        break;

                    default:
                        break;
                } // end switch

            } // end if

            if (file == null &&
                folder == null &&
                macaron.Dte.Solution != null &&
                string.IsNullOrWhiteSpace(macaron.Dte.Solution.FullName) == false)
            {

                file = new FileInfo(macaron.Dte.Solution.FullName);
                if (file.Exists == false) file = null;
            } // end if

            if (file != null)
            {
                FileMacro.OpenFolderAndSelectFile(file);

            }
            else if (folder != null)
            {
                FileMacro.OpenFolder(folder);

            }
            else
            {
                macaron.ShowMessageBox(Resources.OpenActiveFolderCaption, Resources.MessageCanNotOpenFolder);
            } // end if

        } // end function

        /// <summary>
        /// プロシージャブロック、およびクラス等の定義の末尾に Visual Basic 風のコメントを挿入します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        ///  C# コードを記述するときに、例えば "} // end class " の様に
        ///  ブロックの後ろに Visual Basic の "End Class" に似せたコメントを挿入することで、
        ///  Visual Basic 開発者への視認性・訴求力を高め、高速リーディングを可能にします。
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "EnvDTE.TextSelection.Insert(System.String,System.Int32)"), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"),  
         MenuCommand(0x0102)]
        public void InsertCommentOnEndOfFunction(object sender, EventArgs e)
        {
            var macaron = new VisualStudioMacaron(this);

            // アクティブドキュメントがない場合は処理を抜けます。
            if (macaron.DocumentIsActive == false) return;

            // C# 以外の場合はコメントを挿入せずに、処理を抜けます。
            var project = macaron.Dte.ActiveDocument.ProjectItem.ContainingProject;
            if (project == null || project.KnownKind() != ProjectKnownKind.CSharp) return;

            var textSelection = macaron.Dte.ActiveDocument.Selection as EnvDTE.TextSelection;
            textSelection.StartOfLine(EnvDTE.vsStartOfLineOptions.vsStartOfLineOptionsFirstText);

            var elements = new EnvDTE.vsCMElement[]{
				EnvDTE.vsCMElement.vsCMElementFunction,
				EnvDTE.vsCMElement.vsCMElementProperty,
				EnvDTE.vsCMElement.vsCMElementClass,
				EnvDTE.vsCMElement.vsCMElementInterface,
				EnvDTE.vsCMElement.vsCMElementStruct,
				EnvDTE.vsCMElement.vsCMElementEnum,
				EnvDTE.vsCMElement.vsCMElementModule,
				EnvDTE.vsCMElement.vsCMElementNamespace };

            var comments = new string[] { 
				" // end sub" ,
				" // end property" ,
				" // end class" ,
				" // end interface" ,
				" // end structure" ,
				" // end enum" ,
				" // end module" ,
				" // end namespace"};

            var i = 0;

            foreach (var element in elements)
            {
                try
                {
                    var codeElement = textSelection.ActivePoint.CodeElement[element];

                    if (codeElement != null)
                    {
                        textSelection.MoveToPoint(codeElement.EndPoint);
                        textSelection.SelectLine();

                        if ((i != 0 && textSelection.Text.Contains(comments[i]) == false) ||
                            (i == 0 && (
                                textSelection.Text.Contains(comments[i]) == false &&
                                textSelection.Text.Contains(" // end function") == false &&
                                textSelection.Text.Contains(" // end constructor") == false
                                ))
                            )
                        {

                            var isSub = false;
                            var isConstructor = false;

                            if (i == 0)
                            {
                                textSelection.MoveToPoint(codeElement.StartPoint);
                                textSelection.SelectLine();

                                isSub = (textSelection.Text.Contains("void"));
                                if (isSub == false)
                                {
                                    var nameText = codeElement.FullName.Split('.');
                                    try
                                    {
                                        isConstructor = nameText[nameText.Length - 2].Equals(nameText[nameText.Length - 1]);
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                    } // end try
                                } // end if
                            } // end if

                            textSelection.MoveToPoint(codeElement.EndPoint);

                            if (i == 0 && isSub == false)
                            {
                                if (isConstructor)
                                {
                                    textSelection.Insert(" // end constructor");
                                }
                                else
                                {
                                    textSelection.Insert(" // end function");
                                } // end if
                            }
                            else
                            {
                                textSelection.Insert(comments[i]);
                            } // end if

                        }
                        else
                        {
                            textSelection.MoveToPoint(codeElement.EndPoint);
                        } // end if

                        break; // exit for
                    } // end if

                }
                catch
                {
                } // end try

                i += 1;
            } // next element

        } // end sub

        /// <summary>
        /// 選択された範囲を、アウトライン（#Region）ブロックで囲みます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "EnvDTE.TextSelection.Insert(System.String,System.Int32)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "endregion"), MenuCommand(0x0103)]
        public void WriteOutline(object sender, EventArgs e)
        {

            // 言語毎に異なる内容を調査・定義します。

            // ブロック開始行の文字
            var startRegionText = string.Empty;

            // ブロック終了行の文字
            var endRegionText = string.Empty;

            // ブロック開始行のカーソルを合わせる位置
            var offsetOfRegionExplanation = 0;

            // インデントの空白
            var indentSpace = string.Empty;

            var macaron = new VisualStudioMacaron(this);
            if (macaron.DocumentIsActive == false) return;

            var project = macaron.Dte.ActiveDocument.ProjectItem.ContainingProject;
            if (project == null) return;

            var activeSelection = macaron.Dte.ActiveDocument.Selection as EnvDTE.TextSelection;

            var kind = project.KnownKind();
            try
            {
                var extension = System.IO.Path.GetExtension(macaron.Dte.ActiveDocument.FullName);
                switch (extension)
                {
                    case ".ts":
                        kind = ProjectKnownKind.TypeScript;
                        break;
                    case ".js":
                        kind = ProjectKnownKind.JavaScript;
                        break;
                    case ".vb":
                        kind = ProjectKnownKind.VisualBasic;
                        break;
                    case ".cs":
                        kind = ProjectKnownKind.CSharp;
                        break;
                }
            }
            catch { }

            switch (kind)
            {
                case ProjectKnownKind.VisualBasic:

                    startRegionText = "#Region \"  \"";
                    endRegionText = "#End Region";
                    offsetOfRegionExplanation = startRegionText.Length - 2;

                    // Visual Basic はインデントの空白は "" です（常に左端に #Region が記述されます）。
                    indentSpace = String.Empty;

                    break;

                case ProjectKnownKind.CSharp:

                    startRegionText = "#region ";
                    endRegionText = "#endregion";
                    offsetOfRegionExplanation = startRegionText.Length;

                    // C# の時はインデントの空白を取得します（コードのインデントレベルに #region が記述されます）。
                    {
                        var lineText = activeSelection.Text.Split('\n').FirstOrDefault();
                        if (string.IsNullOrEmpty(lineText) == false) indentSpace = lineText.Substring(0, lineText.Length - lineText.TrimStart().Length);
                    }
                    break;

                case ProjectKnownKind.JavaScript:
                case ProjectKnownKind.TypeScript:

                    startRegionText = "// #region ";
                    endRegionText = "// #endregion";
                    offsetOfRegionExplanation = startRegionText.Length;

                    // TypeScript の時はインデントの空白を取得します（コードのインデントレベルに // #region が記述されます）。
                    {
                        var lineText = activeSelection.Text.Split('\n').FirstOrDefault();
                        if (string.IsNullOrEmpty(lineText) == false) indentSpace = lineText.Substring(0, lineText.Length - lineText.TrimStart().Length);
                    }
                    break;

                case ProjectKnownKind.None:
                case ProjectKnownKind.VisualStudioInstaller:
                default:
                    return;
            } // end switch


            // テキストを挿入します。
            activeSelection.Insert(
                indentSpace + startRegionText + "\r\n" + "\r\n" +
                activeSelection.Text +
                "\r\n" + indentSpace + endRegionText + "\r\n"
                ,
                (int)EnvDTE.vsInsertFlags.vsInsertFlagsContainNewText);

            //カーソル位置を移動します。
            activeSelection.MoveToLineAndOffset(activeSelection.TopPoint.Line, indentSpace.Length + offsetOfRegionExplanation + 1);

            macaron.Dte.ActiveDocument.Activate();
        } // end sub

        [MenuCommand(0x0110)]
        public void ToCSharpText(object sender, EventArgs e)
        {
            var macaron = new VisualStudioMacaron(this);
            if (macaron.DocumentIsActive == false) return;
            
            macaron.ReplaceSelectionText(null, (a) =>
            {
                a.Text = @""""+ a.Text.Replace( @"\", @"\\" ).Replace(@"""", @"\""").Replace("\r\n", @"\r\n"" + " + "\r\n" + @"""") + @"""";
            });

        } // end sub

        [MenuCommand(0x0111)]
        public void ToVisualBasicText(object sender, EventArgs e)
        {
            var macaron = new VisualStudioMacaron(this);
            if (macaron.DocumentIsActive == false) return;

            macaron.ReplaceSelectionText(null, (a) =>
            {
                a.Text = @"""" + a.Text.Replace(@"""", @"""""").Replace("\r\n", @""" & vbCrLf & " + "\r\n" + @"""") + @"""";
            });

        } // end sub

        [MenuCommand(0x0112)]
        public void ToUpperCamel(object sender, EventArgs e)
        {
            var macaron = new VisualStudioMacaron(this);
            if (macaron.DocumentIsActive == false) return;

            macaron.ReplaceSelectionWords(null, (a) =>
            {
                a.Text = TextMacro.CamelWords.GetUpperCamelWord(a.Text);
            });

        } // end sub

        [MenuCommand(0x0113)]
        public void ToLowerCamel(object sender, EventArgs e)
        {
            var macaron = new VisualStudioMacaron(this);
            if (macaron.DocumentIsActive == false) return;

            macaron.ReplaceSelectionWords(null, (a) =>
            {
                a.Text = TextMacro.CamelWords.GetLowerCamelWord(a.Text);
            });

        } // end sub
        [MenuCommand(0x101)]
        public void TextFormat(object sender, EventArgs e)
        {
            this.ActivateToolWindow(typeof(TextFormatToolWindow));
        } // end function

        [MenuCommand(0x0108)]
        public void InsertText(object sender, EventArgs e)
        {
            this.ActivateToolWindow(typeof(InsertTextToolWindow));
        } // end function

        [MenuCommand(0x0109)]
        public void InsertSerialNumber(object sender, EventArgs e)
        {
            this.ActivateToolWindow(typeof(InsertSerialNumberToolWindow));
        } // end function


        #endregion
        
        #region WindowEvents イベント処理

        void WindowEvents_WindowActivated(EnvDTE.Window GotFocus, EnvDTE.Window LostFocus)
        {
            var macaron = new VisualStudioMacaron(this);

            var executable = macaron.DocumentIsActive;
            var menuCommandIsEnabled = macaron.DocumentIsActiveWindow;


            if (TextFormatToolWindow.MainControl != null) TextFormatToolWindow.MainControl.Executable = executable;
            if (InsertTextToolWindow.MainControl != null) InsertTextToolWindow.MainControl.Executable = executable;
            if (InsertSerialNumberToolWindow.MainControl != null) InsertSerialNumberToolWindow.MainControl.Executable = executable;


            // InsertCommentOnEndOfFunction MenuCoomand
            {
                var m = VisualStudioMacaron.MenuCommands[0x0102];
                var enabled = false;

                if (menuCommandIsEnabled)
                {
                    var project = macaron.Dte.ActiveDocument.ProjectItem.ContainingProject;
                    enabled = (project != null && project.KnownKind() == ProjectKnownKind.CSharp);
                }// end if

                m.Enabled = enabled;
            }

            // WriteOutline MenuCommand
            {
                var m = VisualStudioMacaron.MenuCommands[0x0103];
                m.Enabled = menuCommandIsEnabled;
            }


        } // end sub

        #endregion

    } // end class
} // end namespace
