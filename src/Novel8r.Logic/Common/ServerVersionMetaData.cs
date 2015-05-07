//using System;
//using System.ComponentModel.Composition;
//using System.ComponentModel.Composition.Primitives;
//using Novel8r.Logic.Interfaces;
//using Novel8r.Logic.Exceptions;

//namespace Novel8r.Logic.Common
//{
//    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
//    [MetadataAttribute]
//    public sealed class ServerVersionMetaData : Attribute, IServerVersionMetaData
//    {
//        private readonly string _databaseVersion;
//        private readonly string _handlerVersion;
//        private readonly bool _isPublished;

//        public ServerVersionMetaData(string databaseVersion)
//            : this(databaseVersion, "1.0", false)
//        {
//        }
//        public ServerVersionMetaData(string databaseVersion, string handlerVersion)
//            : this(databaseVersion, handlerVersion, false)
//        {
//        }
//        public ServerVersionMetaData(string databaseVersion, string handlerVersion, bool isPublished)
//        {
//            _databaseVersion = databaseVersion;
//            _handlerVersion = handlerVersion;
//            _isPublished = isPublished;
//        }
//        public string DatabaseVersion
//        {
//            get { return _databaseVersion; }
//        }
//        public string HandlerVersion
//        {
//            get { return _handlerVersion; }
//        }
//        public bool IsPublished
//        {
//            get { return _isPublished; }
//        }
//    }
//}