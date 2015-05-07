//using System;
//using System.Data;
//using Novel8r.Logic.Connection;
//using Novel8r.Logic.DomainModel;
//using Novel8r.Logic.Factories;

//namespace Novel8r.Logic.Interfaces
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public interface IDatabaseManager
//    {
//    //    ServerVersionId ManagerName { get; }
//    //    bool ManagerPublished { get; }
//        ServerConnectionSettings Settings { set; }
//        string ConnectionString { get; }

//        DataSet ExecuteQuery(string serverName, string databaseName, string sql);
//        void ExecuteNonQuery(string serverName, string databaseName, string sql);
//        Query GetTableContent(Sql8rServer server, Sql8rDatabase database, Sql8rTable table, bool editable);
//        Query GetViewContent(Sql8rServer server, Sql8rDatabase database, Sql8rView view);
//        Query GetTableQuery(Sql8rServer server, Sql8rDatabase database, Sql8rTable table);
//        Query GetViewQuery(Sql8rServer server, Sql8rDatabase database, Sql8rView view);
//        Query GetFunctionQuery(Sql8rServer server, Sql8rDatabase database, Sql8rFunction function);
//        Query GetProcedureQuery(Sql8rServer server, Sql8rDatabase database, Sql8rProcedure procedure);
//        Query GetExecuteFunctionQuery(Sql8rServer server, Sql8rDatabase database, Sql8rFunction function);
//        Query GetExecuteProcedureQuery(Sql8rServer server, Sql8rDatabase database, Sql8rProcedure procedure);
//        Query GetAddColumnQuery(Sql8rServer server, Sql8rDatabase database, Sql8rTable table);
//        Query GetAlterColumnQuery(Sql8rServer server, Sql8rDatabase database, Sql8rTable table, Sql8rColumn column);

//        Sql8rServer GetServer();
//        void GetServerOtherStuff(Sql8rDatabase db);
//        Exception TestConnection();
//        void CreateDatabase(string fileName, bool overwrite);
//        void DropDatabase(string fileName);
//    }
//}