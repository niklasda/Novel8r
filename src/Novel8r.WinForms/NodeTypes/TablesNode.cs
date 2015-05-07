using System;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.Factories;
using Novel8r.WinForms.NodeTypes.Interfaces;

namespace Novel8r.WinForms.NodeTypes
{
    [Serializable]
    public class TablesNode : UltraTreeNode, IFolderNode
    {
        public TablesNode(ServerVersionId @param)
        {
            base.Text = "Tables";
            Override.NodeAppearance.Image = 0;
            Override.ActiveNodeAppearance.Image = 1;
        }

        public bool IsHidden { get; set; }

    }
}