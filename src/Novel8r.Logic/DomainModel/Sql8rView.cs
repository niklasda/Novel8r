//using System.Collections.Generic;
//using Novel8r.Logic.Interfaces;

//namespace Novel8r.Logic.DomainModel
//{
//    public class Sql8rView : Sql8rObject, IColumnedObject
//    {
//        private readonly IDictionary<long, Sql8rColumn> _columns = new Dictionary<long, Sql8rColumn>();
//        private readonly bool _isSystemObject;
//        private readonly string _name;
//        private readonly string _schema;

//        public Sql8rView(string name, string schema, bool isSystemObject, long objectId)
//        {
//            _name = name;
//            _schema = schema;
//            _isSystemObject = isSystemObject;
//            ObjectId = objectId;
//        }

//        public bool IsSystemObject
//        {
//            get { return _isSystemObject; }
//        }

//        #region IColumnedObject Members

//        public string Name
//        {
//            get { return _name; }
//        }

//        public string Schema
//        {
//            get { return _schema; }
//        }

//        public IDictionary<long, Sql8rColumn> Columns
//        {
//            get { return _columns; }
//        }

//        #endregion

//        public string GetFullObjectName()
//        {
//            if (string.IsNullOrEmpty(_schema))
//            {
//                return _name;
//            }
//            return string.Format("{0}.{1}", _schema, _name);
//        }
//    }
//}