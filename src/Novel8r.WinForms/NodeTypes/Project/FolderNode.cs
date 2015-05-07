using System;
using System.IO;
using Infragistics.Win.UltraWinTree;

namespace Novel8r.WinForms.NodeTypes.Project
{
    [Serializable]
    public class FolderNode : UltraTreeNode, IComparable
    {
        private readonly DirectoryInfo _folder;

        public FolderNode(DirectoryInfo folder)
        {
            _folder = folder;
            base.Text = folder.Name;
            base.Key = folder.FullName;
            Override.NodeAppearance.Image = 14;
            Override.ActiveNodeAppearance.Image = 14;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var node = obj as FolderNode;
            if (node != null)
            {
                return string.Compare(Text, node.Text, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return -1;
            }
        }

        #endregion

        public string GetFolderPath()
        {            
            return _folder.FullName;
        }
    }
}