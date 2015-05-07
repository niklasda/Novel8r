using System;
using System.Collections.Generic;
using Novel8r.Logic.Exceptions;

namespace Novel8r.Logic.Factories
{
    [Serializable]
    public class ServerVersionId : IEquatable<ServerVersionId>
    {
		public const string SqlServer2000 = "SQL Server 2000";
		public const string SqlServer2005 = "SQL Server 2005";
		public const string SqlServer2008 = "SQL Server 2008";
		public const string MySql5 = "MySQL 5";
		public const string Oracle10 = "Oracle 10i";
		public const string SQLite3 = "SQLite 3";
		public const string FireBird2 = "FireBird 2";
		public const string ODBCAccess2003 = "Access 2003 (ODBC)";
		public const string ODBCExcel2003 = "Excel 2003 (ODBC)";
		public const string DotNetCode = "DotNetCode";

//        public static ServerVersionId SqlServer_2000 = new ServerVersionId(SqlServer2000);
        public static ServerVersionId SqlServer_2005 = new ServerVersionId(SqlServer2005);
        public static ServerVersionId SqlServer_2008 = new ServerVersionId(SqlServer2008);
        public static ServerVersionId MySql_5 = new ServerVersionId(MySql5);
        public static ServerVersionId Oracle_10 = new ServerVersionId(Oracle10);
		public static ServerVersionId SQLite_3 = new ServerVersionId(SQLite3);
		public static ServerVersionId FireBird_2 = new ServerVersionId(FireBird2);
        public static ServerVersionId ODBC_Access = new ServerVersionId(ODBCAccess2003);
        public static ServerVersionId ODBC_Excel = new ServerVersionId(ODBCExcel2003);
        public static ServerVersionId DotNet = new ServerVersionId(DotNetCode);

        public ServerVersionId()
        {
        }

        private string _versionId;
        public ServerVersionId(string versionId)
        {
            VersionId = versionId;
        }

        public string VersionId
        {
            get { return _versionId; }
            set { _versionId = value; }
        }

        public bool Equals(ServerVersionId other)
        {
            if (_versionId == other._versionId)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(ServerVersionId o1, ServerVersionId o2)
        {
            return o1.Equals(o2);
        }

        public static bool operator !=(ServerVersionId o1, ServerVersionId o2)
        {
            return !o1.Equals(o2);
        }

        public override string ToString()
        {
            return _versionId;
        }

        private static IList<ServerVersionId> GetKnownVendors()
        {
            IList<ServerVersionId> vendors = new List<ServerVersionId>();

            vendors.Add(new ServerVersionId(SqlServer2000));
            vendors.Add(new ServerVersionId(SqlServer2005));
            vendors.Add(new ServerVersionId(SqlServer2008));
            vendors.Add(new ServerVersionId(MySql5));
            vendors.Add(new ServerVersionId(ODBCExcel2003));
            vendors.Add(new ServerVersionId(ODBCAccess2003));
            vendors.Add(new ServerVersionId(Oracle10));
            vendors.Add(new ServerVersionId(SQLite3));
            vendors.Add(new ServerVersionId(FireBird2));
            vendors.Add(new ServerVersionId(DotNetCode));
            
            return vendors;
        }

        public static ServerVersionId Parse(string p)
        {
            foreach (ServerVersionId id in GetKnownVendors())
            {
                if(p.Equals(id.ToString()))
                {
                    return id;
                }
            }

            throw new SQL8rException(string.Format("Unable to parse ServerVersionId: {0}", p));
        }
    }
}