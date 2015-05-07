using System;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.DomainModel;

namespace Novel8r.WinForms.NodeTypes
{
    [Serializable]
    public class ServerNode : UltraTreeNode
    {
        public ServerNode(string name)
        {
            base.Text = name;
            base.Key = name;
            Override.NodeAppearance.Image = 6;
            Override.ActiveNodeAppearance.Image = 6;
        }

        public Sql8rServer ServerObject { get; set; }
    }
}