using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;
using System.Globalization;

namespace Net.Surviveplus.EditorPlus.Core
{
    public static class FileMacro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString"), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        public static FileInfo GetNewWorkTextFile(string prefix = "Work")
        {
            var file = new FileInfo(Path.Combine(Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop, prefix + " - " + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".txt"));
            var counter = 0;
            while (file.Exists)
            {
                file = new FileInfo(Path.Combine(Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop, prefix + " - " + DateTime.Now.ToString("yyyyMMdd HHmmss") + "-" + counter.ToString() + ".txt"));
            } // end while

            return file;
        } // end sub

        public static void CreateFile(FileSystemInfo file)
        {
            if (file == null) throw new ArgumentNullException("file");
            Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText(file.FullName, string.Empty, false);
        } // end sub

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void OpenFolder(DirectoryInfo folder)
        {
            if (folder == null) throw new ArgumentNullException("folder");
            if (folder.Exists == false) throw new ArgumentException("The folder is not exist.", "folder");

            Interaction.Shell("explorer \"" + folder.FullName + "\"", AppWinStyle.NormalFocus);
        } // end sub

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void OpenFolderAndSelectFile(FileInfo selectedFile)
        {
            if (selectedFile == null) throw new ArgumentNullException("selectedFile");
            if (selectedFile.Exists == false) throw new ArgumentException("The file is not exist.", "selectedFile");

            Interaction.Shell("explorer /select,\"" + selectedFile.FullName + "\"", AppWinStyle.NormalFocus);
        } // end function
    } // end class
} // end namespace

