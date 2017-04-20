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
   [Guid("F3050EB9-3632-4A2F-A329-3597ECC1274E")]
    public class InsertTextToolWindow : ToolWindowPane
    {
        #region コンストラクタ

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public InsertTextToolWindow() :
            base(null)
        {
            // Set the window title reading it from the resources.
            this.Caption = Resources.InsertTextCaption;

            // Set the image that will appear on the tab of the window frame when docked with an other window
            // The resource ID correspond to the one defined in the resx file while the Index is the offset in the bitmap strip. Each image in the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 3;

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable, we are not calling Dispose on this object. 
            // This is because ToolWindowPane calls Dispose on  the object returned by the Content property.

            if (InsertTextToolWindow.MainControl == null){
                InsertTextToolWindow.MainControl = new Net.Surviveplus.EditorPlus.UI.InsertText();
                InsertTextToolWindow.MainControl.InsertToHeadExecuted += MainControl_InsertToHeadExecuted;
                InsertTextToolWindow.MainControl.InsertToEndExecuted += MainControl_InsertToEndExecuted;
            } // end if
            base.Content = InsertTextToolWindow.MainControl;

        } // end constructor
        
        #endregion

        #region MainControl（TextFormat）イベント処理

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)")]
        void MainControl_InsertToHeadExecuted(object sender, EventArgs e)
        {
            var c = sender as InsertText;

            var macro = new Macro(this);
            bool skip = c.Skip;
            var text = c.Text;

            try
            {
                macro.ExecuteLineEditing(null, (a) =>
                {
                    if (skip && a.Text.StartsWith(text))
                    {
                        a.Cancel = true;
                    }
                    else
                    {
                        a.Text = text + a.Text;
                    }
                });

            }
            catch (ActiveDocumentIsNullException)
            {
                macro.ShowMessageBox(Resources.InsertTextCaption, Resources.MessageActivateTextEditorForInsertText);
            } // end try

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
        void MainControl_InsertToEndExecuted(object sender, EventArgs e)
        {
            var c = sender as InsertText;

            var macro = new Macro(this);
            bool skip = c.Skip;
            var text = c.Text;

			try {
				macro.ExecuteLineEditing(null, (a) =>
				{
					if (skip && a.Text.EndsWith(text)) {
						a.Cancel = true;
					}
					else { 
						a.Text = a.Text + text;
					}

				});

			}
			catch (ActiveDocumentIsNullException) {
                macro.ShowMessageBox(Resources.InsertTextCaption, Resources.MessageActivateTextEditorForInsertText);
			} // end try
        } 

        #endregion



        #region static プロパティ

        /// <summary>
        /// 主コントロールを返します。
        /// Visual Studio 上ではツールウィンドウは一つしか生成されないことを前提として、主コントロールを static に公開します。
        /// このプロパティ経由で、他のイベント処理からコントロールのボタン制御などを行います。
        /// </summary>
        public static InsertText MainControl { get; private set; }

        #endregion
    
    } // end class
} // end namespace
