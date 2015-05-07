//using System;
//using Infragistics.Win.UltraWinTree;
//using Novel8r.Logic.DomainModel;

//namespace Novel8r.WinForms.NodeTypes
//{
//    [Serializable]
//    public class ViewNode : UltraTreeNode, IComparable
//    {
//        public ViewNode(string name, string schema)
//        {
//            base.Text = string.Format("{0}.{1}", schema, name);
//            ViewName = name;
//            SchemaName = schema;

//            Override.NodeAppearance.Image = 7;
//            Override.ActiveNodeAppearance.Image = 7;
//        }

////        public Sql8rView ViewObject { get; set; }

//        public string ViewName { get; private set; }

//        public string SchemaName { get; private set; }

//        #region IComparable Members

//        public int CompareTo(object obj)
//        {
//            var node = obj as ViewNode;
//            if (node != null)
//            {
//                return string.Compare(Text, node.Text, StringComparison.OrdinalIgnoreCase);
//            }
//            return 1;
//        }

//        #endregion
//    }
//}