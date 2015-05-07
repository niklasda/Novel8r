using System;
using System.IO;
using Infragistics.Win.UltraWinTree;

namespace Novel8r.WinForms.NodeTypes.Project
{
    [Serializable]
    public class ProjectNode : UltraTreeNode
    {
        private readonly DirectoryInfo _baseDir;
        private string _name;

        public ProjectNode(string name, DirectoryInfo baseDir)
        {
            _name = name;
            _baseDir = baseDir;
            base.Text = _name;
            base.Key = baseDir.FullName;

            Override.NodeAppearance.Image = 14;
            Override.ActiveNodeAppearance.Image = 14;
        }

        public DirectoryInfo FolderObject
        {
            get { return _baseDir; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}