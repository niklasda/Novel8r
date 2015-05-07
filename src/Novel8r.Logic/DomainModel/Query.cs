using System.Data;
using System.Data.Common;
using Novel8r.Logic.Connection;
using Novel8r.Logic.Factories;

namespace Novel8r.Logic.DomainModel
{
    public class Query
    {
        private readonly string _databaseName;
        private readonly DataTable _result;
        private readonly string _serverName;
        private readonly ServerConnectionSettings _settings;
        private readonly string _sql;
        private DbDataAdapter _adapter;

        public Query(ServerConnectionSettings settings, string serverName, string databaseName, string sql)
        {
            _settings = settings;
            _serverName = serverName;
            _databaseName = databaseName;
            _sql = sql;
        }

        public Query(ServerConnectionSettings settings, string serverName, string databaseName, string sql, DataTable result)
            : this(settings, serverName, databaseName, sql)
        {
            _result = result;
        }

        public string Sql
        {
            get { return _sql; }
        }

        public ServerConnectionSettings ServerConnection
        {
            get { return _settings; }
        }

        public ServerVersionId ServerVersion
        {
            get { return _settings.ServerVersion; }
        }

        public string ServerName
        {
            get { return _serverName; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public DataTable Result
        {
            get { return _result; }
        }

        public bool HasAdapter
        {
            get { return _adapter != null; }
        }

        public DbDataAdapter Adapter
        {
            get { return _adapter; }
            set { _adapter = value; }
        }
    }
}