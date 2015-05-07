//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.ComponentModel.Composition.Primitives;
//using Novel8r.Logic.Connection;
//using Novel8r.Logic.Exceptions;
//using Novel8r.Logic.Interfaces;
//using Novel8r.Logic.Common;

//namespace Novel8r.Logic.Factories
//{
//    public class TemplateManagerFactory
//    {
//        private static TemplateManagerFactory _instance;

//        [Import(typeof(ITemplateManager))]
//        public ExportCollection<ITemplateManager, IServerVersionMetaData> TManagers { get; set; }

//        private TemplateManagerFactory()
//        {
//        }

//        public static TemplateManagerFactory Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new TemplateManagerFactory();
//                }
//                return _instance;
//            }
//        }        

//        public ITemplateManager GetCodeTemplateManager()
//        {
//            foreach (var expmanager in TManagers)
//            {
////                string ver = ServerVersionMetaData.GetMetadataString(expmanager, "DatabaseVersion");
//                string ver = expmanager.MetadataView.DatabaseVersion;
//                if (ver == ServerVersionId.DotNetCode)
//                {
//                    ITemplateManager manager = expmanager.GetExportedObject();
//                    return manager;
//                }
//            }
//            throw new SQL8rException("Could not find the .NET TemplateManager");
//        }

//        public ITemplateManager GetSqlTemplateManager(ServerConnectionSettings settings)
//        {
//            if (settings == null)
//            {
//                throw new SQL8rException("No TemplateManager available, please connect to a server first");
//            }

//            ServerVersionId version = settings.ServerVersion;
//            ITemplateManager dbMan;

//            foreach (var expmanager in TManagers)
//            {
//                string dbVersion = expmanager.MetadataView.DatabaseVersion;
////                ITemplateManager manager = expmanager.GetExportedObject();
//                if (dbVersion == version.VersionId)
//                {
//                    dbMan = expmanager.GetExportedObject();
//                    dbMan.Settings = settings;
//                    return dbMan;

//                }
//            }

//            throw new SQL8rException(string.Format("TManager not loaded: {0}", version));
//        }

//    }
//}