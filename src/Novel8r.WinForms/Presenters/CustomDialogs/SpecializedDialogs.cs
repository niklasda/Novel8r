using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQL8r.WinForms.Presenters.Dialogs
{
    public class SpecializedDialogs
    {
        private static SpecializedDialogs _instance;

        public static SpecializedDialogs Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpecializedDialogs();
                }
                return _instance;
            }
        }

        //
        // Constructors
        //

        private SpecializedDialogs()
        {
        }

        public SaveFileDialog GetSaveSqlDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SQL Files|*.sql|All Files|*.*";
           // sfd.ShowHelp = true;
            return sfd;
        }
        public SaveFileDialog GetSaveCSharpCodeDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "C# Files|*.cs|VB.NET Files|*.vb|All Files|*.*";

            return sfd;
        }
        public SaveFileDialog GetSaveVbNetCodeDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "VB.NET Files|*.vb|C# Files|*.cs|All Files|*.*";

            return sfd;
        }
        public SaveFileDialog GetSaveFormatDialog(string format)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = string.Format("{0}|All Files|*.*", format);

            return sfd;
        }

        public OpenFileDialog GetOpenProjectDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Project Files|*.csproj|All Files|*.*";

            return ofd;
        }
    }
}
