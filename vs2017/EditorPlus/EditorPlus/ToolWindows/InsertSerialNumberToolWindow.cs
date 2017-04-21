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
   [Guid("52C1236F-4CE4-4020-B363-467881519CEB")]
    public class InsertSerialNumberToolWindow : ToolWindowPane
    {
        #region コンストラクタ

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
       public InsertSerialNumberToolWindow() :
            base(null)
        {
            // Set the window title reading it from the resources.
            this.Caption = Resources.InsertSerialNumberCaption;

            // Set the image that will appear on the tab of the window frame when docked with an other window
            // The resource ID correspond to the one defined in the resx file while the Index is the offset in the bitmap strip. Each image in the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 2;

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable, we are not calling Dispose on this object. 
            // This is because ToolWindowPane calls Dispose on  the object returned by the Content property.

            if (InsertSerialNumberToolWindow.MainControl == null)
            {
                InsertSerialNumberToolWindow.MainControl = new Net.Surviveplus.EditorPlus.UI.InsertSerialNumber();
                InsertSerialNumberToolWindow.MainControl.Executed += MainControl_Executed;
            } // end if
            base.Content = InsertSerialNumberToolWindow.MainControl;

        } // end constructor

       
        #endregion

       #region MainControl（InsertSerialNumber）イベント処理

       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int64.ToString")]
       void MainControl_Executed(object sender, InsertSerialNumberEventArgs e)
       {
           if (e.InsertPosition== InsertPosition.None) return;

           var macro = new Macro(this);
           long number = 0;
           if (long.TryParse(e.StartNumberText, out number) == false)
           {
               // TODO: ローカライズ
               macro.ShowMessageBox(Resources.InsertSerialNumberCaption, Resources.MessageInputNumber);

               //this.MainControl.Focus();
               return;
           }

           var numberLength = 0;
           long numberCounter = number;

           try
           {
               macro.ExecuteLineEditing(
                   (a) =>
                   {
                       numberCounter += 1;
                       numberLength = numberCounter.ToString().Length;
                   },
                   (a) =>
                   {
                       var text = number.ToString();
                       var paddingCount = numberLength - text.Length;
                       if (paddingCount > 0)
                       {
                           switch (e.PaddingKind)
                           {
                               case PaddingKind.Zero:
                                   text = new string('0', paddingCount) + text;
                                   break;
                               case PaddingKind.Space:
                                   text = new string(' ', paddingCount) + text;
                                   break;
                               case PaddingKind.None:
                               default:
                                   break;
                           }
                       } // end if


                       if (e.Skip &&
                           (e.InsertPosition == InsertPosition.HeadOfLine && a.Text.StartsWith(text) ||
                            e.InsertPosition == InsertPosition.EndOfLine && a.Text.EndsWith(text))
                           )
                       {
                           a.Cancel = true;
                       }
                       else
                       {
                           switch (e.InsertPosition)
                           {
                               case InsertPosition.HeadOfLine:
                                   a.Text = text + a.Text;
                                   break;
                               case InsertPosition.EndOfLine:
                                   a.Text = a.Text + text;
                                   break;
                               case InsertPosition.None:
                               default:
                                   break;
                           }
                       }
                       number += 1;
                   });

           }
           catch (ActiveDocumentIsNullException)
           {
               macro.ShowMessageBox(Resources.InsertSerialNumberCaption, Resources.MessageActivateTextEditorForInsertText);
           } // end try       
       } // end sub

        #endregion

        #region static プロパティ

        /// <summary>
        /// 主コントロールを返します。
        /// Visual Studio 上ではツールウィンドウは一つしか生成されないことを前提として、主コントロールを static に公開します。
        /// このプロパティ経由で、他のイベント処理からコントロールのボタン制御などを行います。
        /// </summary>
        public static InsertSerialNumber MainControl { get; private set; }

        #endregion
    
    } // end class
} // end namespace
