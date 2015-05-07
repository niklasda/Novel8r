//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using Novel8r.Logic.Connection;
//using Novel8r.Logic.Exceptions;
//using Novel8r.Logic.Interfaces;
//using Novel8r.Logic.Common;

//namespace Novel8r.Logic.Factories
//{
//    public class PerformanceManagerFactory
//    {
//        private static PerformanceManagerFactory _instance;

//        [Import(typeof(IPerformanceManager))]
//        public ExportCollection<IPerformanceManager, IServerVersionMetaData> PerfManagers { get; set; }

//        private PerformanceManagerFactory()
//        {
//        }

//        public static PerformanceManagerFactory Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new PerformanceManagerFactory();
//                }
//                return _instance;
//            }
//        }

//        public IPerformanceManager GetPerformanceManager(ServerConnectionSettings settings)
//        {
//            if (settings == null)
//            {
//                throw new ArgumentNullException("settings");
//            }

//            ServerVersionId version = settings.ServerVersion;
//            IPerformanceManager perfMan;

//            foreach (var expmanager in PerfManagers)
//            {
////                string dbVersion = ServerVersionMetaData.GetMetadataString(expmanager, "DatabaseVersion");
//                string dbVersion = expmanager.MetadataView.DatabaseVersion;
//                //IPerformanceManager manager = expmanager.GetExportedObject();
//                if (dbVersion == version.VersionId)
//                {
//                    perfMan = expmanager.GetExportedObject();
//                    perfMan.Settings = settings;
//                    return perfMan;

//                }
//            }

//            throw new SQL8rException(string.Format("PerfManager not loaded: {0}", version));
//        }
//    }
//}