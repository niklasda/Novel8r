//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel.Composition;
//using Novel8r.Logic.Connection;
//using Novel8r.Logic.Exceptions;
//using Novel8r.Logic.Interfaces;
//using Novel8r.Logic.Common;

//namespace Novel8r.Logic.Factories
//{
//    public class DatabaseManagerFactory
//    {
//        private static DatabaseManagerFactory _instance;

//        [Import(typeof(IDatabaseManager))]
//        public ExportCollection<IDatabaseManager, IServerVersionMetaData> DbManagers { get; set; }

//        private DatabaseManagerFactory()
//        {
//        }

//        public static DatabaseManagerFactory Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new DatabaseManagerFactory();
//                }
//                return _instance;
//            }
//        }

//        public IDatabaseManager GetDatabaseManager(ServerConnectionSettings settings)
//        {
//            if (settings == null)
//            {
//                throw new ArgumentNullException("settings");
//            }

//            ServerVersionId version = settings.ServerVersion;
//            IDatabaseManager dbMan;

//            //Export<IDatabaseManager>
//            foreach (Export<IDatabaseManager, IServerVersionMetaData> expmanager in DbManagers)
//            {
//                string dbVersion = expmanager.MetadataView.DatabaseVersion;
//          //      string dbVersion = ServerVersionMetaData.GetMetadataString(expmanager, "DatabaseVersion");
//                // IDatabaseManager manager = expman.GetExportedObject();
//                if (dbVersion == version.VersionId)
//                {
//                    dbMan = expmanager.GetExportedObject();
//                    dbMan.Settings = settings;
//                    return dbMan;
 
//                }
//            }
//            throw new SQL8rException(string.Format("DbManager not loaded: {0}", version));
//        }

//        public IList<ServerVersionId> GetSupportedVendorXXXs()
//        {
//            if (DbManagers == null)
//            {
//                throw new SQL8rException("No DatabaseManagers loaded");
//            }

//            IList<ServerVersionId> vendors = new List<ServerVersionId>();
//            foreach (var expmanager in DbManagers)
//            {
////			    IDatabaseManager o = expmanager.GetExportedObject();
//                bool isPublished = expmanager.MetadataView.IsPublished;
//                string dbVersion = expmanager.MetadataView.DatabaseVersion;
//                if (isPublished)
//                {
//                    vendors.Add(ServerVersionId.Parse(dbVersion));
//                }
//            }

//            return new ReadOnlyCollection<ServerVersionId>(vendors);
//        }
//    }
//}