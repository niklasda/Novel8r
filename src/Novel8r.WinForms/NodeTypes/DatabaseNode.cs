using System;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.DomainModel;

namespace Novel8r.WinForms.NodeTypes
{
    [Serializable]
    public class DatabaseNode : UltraTreeNode, IComparable
    {
        public DatabaseNode(string name)
        {
            base.Text = name;
            Override.NodeAppearance.Image = 17;
            Override.ActiveNodeAppearance.Image = 17;
            //           ImageIndex = 0;
            //        SelectedImageIndex = 1;
        }

        public Sql8rDatabase DatabaseObject { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var node = obj as DatabaseNode;
            if (node != null)
            {
                return string.Compare(Text, node.Text, StringComparison.OrdinalIgnoreCase);
            }
            return 1;
        }

        #endregion
    }
}