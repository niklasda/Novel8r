//using Novel8r.Logic.Common;

//namespace Novel8r.Logic.DomainModel
//{
//    public class Sql8rIndex : Sql8rObject
//    {
//        private readonly bool _isClustered;
//        private readonly bool _isPrimaryKey;
//        private readonly string _name;
//        private int _spaceUsed; 
//        private double _fragmentation;
//        private IndexAction _recommendation;

//        public Sql8rIndex(string name, bool isClustered, bool isPrimaryKey, long objectId)
//        {
//            _name = name;
//            _isPrimaryKey = isPrimaryKey;
//            _isClustered = isClustered;
//            ObjectId = objectId;
//        }

//        public bool IsClustered
//        {
//            get { return _isClustered; }
//        }

//        public bool IsPrimaryKey
//        {
//            get { return _isPrimaryKey; }
//        }

//        public string Name
//        {
//            get { return _name; }
//        }


//        public void SetSpaceStats(double fragmentation, int spaceUsed)
//        {
//            _fragmentation = fragmentation;
//            _spaceUsed = spaceUsed;
//        }

//        public double AvgFragmentation
//        {
//            get { return _fragmentation; }
//        }

//        public int SpaceUsed
//        {
//            get { return _spaceUsed; }
//        }

//        public IndexAction Recommendation
//        {
//            get { return _recommendation; }
//            set { _recommendation = value; }
//        }
//    }
//}