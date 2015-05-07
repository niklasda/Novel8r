using System;
using Infragistics.Win.UltraWinTree;

namespace Novel8r.WinForms.NodeTypes
{
    [Serializable]
    public class ColumnNode : UltraTreeNode, IComparable
    {
        public ColumnNode(string name, long id)
        {
            base.Text = name;
            ID = id;
        }

        public long ID { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var node = obj as ColumnNode;
            if (node != null)
            {
                if (ID < node.ID)
                {
                    return -1;
                }
                if (ID > node.ID)
                {
                    return 1;
                }
                return 0;                
            }
            return 1;
        }

        #endregion
    }
}