using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Net.Surviveplus.EditorPlus.UI;
using Net.Surviveplus.VsixExtensions;
using Net.Surviveplus.TextMacro;

namespace Net.Surviveplus.EditorPlus.ToolWindows
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, usually implemented by the package implementer.
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its implementation of the IVsUIElementPane interface.
    /// </summary>
    [Guid("32C3F3C0-6DD7-4D48-A923-442EB64B2411")]
    public class TextFormatToolWindow : ToolWindowPane
    {
        #region コンストラクタ

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public TextFormatToolWindow() :
            base(null)
        {
            // Set the window title reading it from the resources.
            this.Caption = Resources.TextFormatCaption;

            // Set the image that will appear on the tab of the window frame when docked with an other window
            // The resource ID correspond to the one defined in the resx file while the Index is the offset in the bitmap strip. Each image in the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 4;

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable, we are not calling Dispose on this object. 
            // This is because ToolWindowPane calls Dispose on  the object returned by the Content property.

            if (TextFormatToolWindow.MainControl == null) { 
                TextFormatToolWindow.MainControl = new Net.Surviveplus.EditorPlus.UI.TextFormat();
                TextFormatToolWindow.MainControl.Executed += MainControl_Executed;
            } // end if
            base.Content = TextFormatToolWindow.MainControl;

        } // end constructor

        #endregion

        #region MainControl（TextFormat）イベント処理

        /// <summary>
        /// メインコントロール の TextFormat で、ボタンが押されたときの処理を実行します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainControl_Executed(object sender, EventArgs e)
        {
            var c = sender as TextFormat;
            var macro = new Macro(this);
            try
            {
                macro.ExecuteLineEditing(null, (a) => 
                {
                    // c（ツールウィンドウ）Text をフォーマット、a（エディタ）選択行を値として、フォーマット変換を実行します。
                    a.Text = TextFormatMacro.Format(c.Text, a.Text);
                });

            }
            catch (ActiveDocumentIsNullException)
            {
                macro.ShowMessageBox(Resources.TextFormatCaption, Resources.MessageActivateTextEditorForFormatText);
            } // end try
        
        } // end sub 
        #endregion

        #region static プロパティ

        /// <summary>
        /// 主コントロールを返します。
        /// Visual Studio 上ではツールウィンドウは一つしか生成されないことを前提として、主コントロールを static に公開します。
        /// このプロパティ経由で、他のイベント処理からコントロールのボタン制御などを行います。
        /// </summary>
        public static TextFormat MainControl { get; private set; }

        #endregion
    
    } // end class
} // end namespace
