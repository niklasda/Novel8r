using System;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.Factories;
using Novel8r.WinForms.NodeTypes.Interfaces;

namespace Novel8r.WinForms.NodeTypes
{
    [Serializable]
    public class ViewsNode : UltraTreeNode, IFolderNode
    {
        public ViewsNode(ServerVersionId @param)
        {
            base.Text = "Views";
            Override.NodeAppearance.Image = 0;
            Override.ActiveNodeAppearance.Image = 1;
        }

        public bool IsHidden { get; set; }

    }
}