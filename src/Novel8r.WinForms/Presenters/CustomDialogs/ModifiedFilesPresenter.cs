using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinListView;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Presenters.DockedChildren;
using Novel8r.WinForms.Views.CustomDialogs;

namespace Novel8r.WinForms.Presenters.CustomDialogs
{
    public class ModifiedFilesPresenter : IPresenter, IViewPresenter<ModifiedFilesDialog>
    {
        private ModifiedFilesDialog _view;

       // public static ModifiedFilesDialog Instance { get; private set; }

        #region IPresenter<ModifiedFilesDialog> Members

        public void Init()
        {
            
        }

        public ModifiedFilesDialog View
        {
            get { return _view; }
            set { _view = value; }
        }

        public DialogResult ShowDialog()
        {
            return _view.ShowDialog();
        }

        public void Show()
        {
        }

        #endregion

        private string getExtension(string fileName)
        {
            int idx = fileName.LastIndexOf('.');
            int len = fileName.Length;
            if (idx >= 0)
            {
                return fileName.Substring(idx, len - idx);
            }
        	return ".rtf";
        }

        public DialogResult ShowFiles(IList<string> filesToSave)
        {
            foreach (string file in filesToSave)
            {
                string fileName;
                if (file.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
                {
                    fileName = file.Substring(0, file.Length - 2);
                }
                else
                {
                    fileName = file;
                }

                UltraListViewItem item = _view.lvwFiles.Items.Add(fileName, fileName);
                string ext = getExtension(fileName).ToUpperInvariant();
                item.Appearance.Image = ProjectPresenter.Instance.GetImageForExtension(ext);
            }

            return ShowDialog();
        }
    }
}