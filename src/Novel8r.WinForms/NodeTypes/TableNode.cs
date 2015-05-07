//using System;
//using Infragistics.Win.UltraWinTree;
//using Novel8r.Logic.DomainModel;
//using Novel8r.WinForms.NodeTypes.Interfaces;

//namespace Novel8r.WinForms.NodeTypes
//{
//    [Serializable]
//    public class TableNode : UltraTreeNode, IHaveParentNode, IComparable
//    {
//        public TableNode(string name, string schema)
//        {
//            if (string.IsNullOrEmpty(schema))
//            {
//                base.Text = name;
//            }
//            else
//            {
//                base.Text = string.Format("{0}.{1}", schema, name);
//            }
//            TableName = name;
//            SchemaName = schema;

//            Override.NodeAppearance.Image = 4;
//            Override.ActiveNodeAppearance.Image = 4;
//        }

//        public Sql8rTable TableObject { get; set; }

//        public string TableName { get; private set; }

//        public string SchemaName { get; private set; }

//        #region IComparable Members

//        public int CompareTo(object obj)
//        {
//            var node = obj as TableNode;
//            if (node != null)
//            {
//                return string.Compare(Text, node.Text, StringComparison.OrdinalIgnoreCase);
//            }
//            return 1;
//        }

//        #endregion

//        public virtual UltraTreeNode ParentNode
//        {
//            get { return base.Parent.Parent; }
//        }
//    }
//}