namespace Novel8r.Logic.DomainModel.Search
{
    public class SearchHit
    {
        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly bool _isSystemObject;
        private readonly string _objectType;
        private readonly string _schemaName;
        private readonly string _objectName;
        private readonly string _columnName;

        public SearchHit(string serverName, string databaseName, bool isSystemObject, string objectType, string schemaName, string objectName, string columnName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            _isSystemObject = isSystemObject;
            _schemaName = schemaName;
            _objectType = objectType;
            _objectName = objectName;
            _columnName = columnName;
        }

        public string GetObjectType()
        {
            return _objectType;
        }

        public bool GetIsSystemObject()
        {
            return _isSystemObject;
        }

        public string Server
        {
            get { return _serverName; }
        }

        public string Database
        {
            get { return _databaseName; }
        }

        public string Schema
        {
            get { return _schemaName; }
        }

        public string Object
        {
            get { return _objectName; }
        }

        public string Column
        {
            get { return _columnName; }
        }
    }
}