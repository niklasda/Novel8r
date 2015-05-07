namespace Novel8r.Logic.DomainModel
{
    public class Sql8rProcedure : Sql8rObject
    {
        private readonly bool _isSystemObject;
        private readonly string _name;
        private readonly string _schema;

        public Sql8rProcedure(string name, string schema, bool isSystemObject, long objectId)
        {
            _name = name;
            _schema = schema;
            _isSystemObject = isSystemObject;
            ObjectId = objectId;
        }

        public bool IsSystemObject
        {
            get { return _isSystemObject; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Schema
        {
            get { return _schema; }
        }
    }
}