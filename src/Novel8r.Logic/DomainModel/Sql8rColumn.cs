
namespace Novel8r.Logic.DomainModel
{
    public class Sql8rColumn : Sql8rObject
    {
        private readonly bool _inPrimaryKey;
        private readonly int _maxLength;
        private readonly string _name;
        private readonly string _typeName;

        public Sql8rColumn(string name, bool inPrimaryKey, string typeName, int maxLength, long objectId)
        {
            _name = name;
            _inPrimaryKey = inPrimaryKey;
            _typeName = typeName;
            _maxLength = maxLength;
            ObjectId = objectId;
        }

        public bool InPrimaryKey
        {
            get { return _inPrimaryKey; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string NameSpec
        {
            get { return string.Format("{0} ({1})", _name, TypeSpec); }
        }

        public string TypeSpec
        {
            get { return string.Format("{0} ({1})", _typeName, _maxLength); }
        }
    }
}