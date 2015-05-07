//using System.Collections.Generic;
//using Novel8r.Logic.Interfaces;

//namespace Novel8r.Logic.DomainModel
//{
//    public class Sql8rTable : Sql8rObject, IColumnedObject
//    {
//        private readonly IDictionary<long, Sql8rColumn> _columns = new Dictionary<long, Sql8rColumn>();
//        private readonly IDictionary<string, Sql8rIndex> _indexes = new Dictionary<string, Sql8rIndex>();
//        private readonly bool _isSystemObject;
//        private readonly string _name;
//        private readonly string _schema;
//        private double _dsu;
//        private double _isu;

//        public Sql8rTable(string name, string schema, bool isSystemObject, long objectId)
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

//        public IDictionary<string, Sql8rIndex> Indexes
//        {
//            get { return _indexes; }
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
//            if(string.IsNullOrEmpty(_schema))
//            {
//                return _name;
//            }
//            return string.Format("{0}.{1}", _schema, _name);
//        }

//        public void SetSpaceStats(double dataSpaceUsed, double indexSpaveUsed)
//        {
//            _dsu = dataSpaceUsed;
//            _isu = indexSpaveUsed;
//        }

//        public double DataSpaceUsed
//        {
//            get { return _dsu; }
//        }

//        public double IndexSpaceUsed
//        {
//            get { return _isu; }
//        }
//    }
//}