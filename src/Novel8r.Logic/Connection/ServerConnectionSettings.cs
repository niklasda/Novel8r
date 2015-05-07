using System;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Interfaces;
using Novel8r.Logic.Exceptions;

namespace Novel8r.Logic.Connection
{
	[Serializable]
	public class ServerConnectionSettings
	{
		private string _database;
		private string _password;
		private string _server;
		private string _user;
		private ServerVersionId _version;
		private bool _windowsAuthentication;

		internal ServerConnectionSettings()
		{
		}

		public ServerConnectionSettings(ServerVersionId serverVersion, string serverName, string databaseName, bool useWindowsAuthentication,
		                                string userName, string userPassword)
		{
			_version = serverVersion;
			_server = serverName;
			_database = databaseName;
			_windowsAuthentication = useWindowsAuthentication;
			_user = userName;
			_password = userPassword;
		}

		public ServerConnectionSettings(ServerVersionId serverVersion, string serverName, string databaseName)
		{
			_version = serverVersion;
			_server = serverName;
			_database = databaseName;
			_windowsAuthentication = true;
		}

		public ServerConnectionSettings(ServerVersionId serverVersion, string serverName)
		{
			_version = serverVersion;
			_server = serverName;
			_windowsAuthentication = true;
		}

		public string ServerName
		{
			get { return _server; }
			set { _server = value; }
		}

		public string DatabaseName
		{
			get { return _database; }
			set { _database = value; }
		}

		public bool UseWindowsAuthentication
		{
			get { return _windowsAuthentication; }
			set { _windowsAuthentication = value; }
		}

		public string UserName
		{
			get { return _user; }
			set { _user = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public ServerVersionId ServerVersion
		{
			get { return _version; }
			set { _version = value; }
		}

        //public string GetConnectionString()
        //{
        //    IDatabaseManager manager = DatabaseManagerFactory.Instance.GetDatabaseManager(this);
        //    return manager.ConnectionString;
        //}

		public override string ToString()
		{
			return string.Format("{0}.{1}", _server, _database);
		}
	}
}