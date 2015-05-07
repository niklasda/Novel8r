using System.Collections.Generic;
using Novel8r.Logic.Connection;
using Novel8r.Logic.Factories;

namespace Novel8r.Logic.DomainModel
{
    public class Sql8rServer
    {
        private readonly IList<Sql8rDatabase> _databases = new List<Sql8rDatabase>();
        private readonly string _name;
        private readonly ServerConnectionSettings _settings;
        private string _version;
        private int _edition;

        public Sql8rServer(ServerConnectionSettings settings)
        {
            _name = settings.ServerName;
            _settings = settings;
        }

        // todo add node title to domain objects to get them vendor specific

        public string Name
        {
            get { return _name; }
        }

        public IList<Sql8rDatabase> Databases
        {
            get { return _databases; }
        }

        public int Edition
        {
            get { return _edition; }
        }

        public string Version
        {
            get { return _version; }
        }

        public ServerVersionId ServerVersion
        {
            get { return _settings.ServerVersion; }
        }

        public ServerConnectionSettings ServerConnection
        {
            get { return _settings; }
        }

        // ------------

        //Database Engine edition of the instance of SQL Server installed on the server.
        //1 = Personal or Desktop Engine (Not available for SQL Server.)
        //2 = Standard (This is returned for Standard and Workgroup.) 
        //3 = Enterprise (This is returned for Enterprise, Enterprise Evaluation, and Developer.)
        //4 = Express (This is returned for Express, Express with Advanced Services, and Windows Embedded SQL.)



        public void SetServerInfo(string version, string productLevel, string edition, int engineEdition)
        {
            _version = version;
            _edition = engineEdition;
        }
    }
}