//using System.ComponentModel.Composition;
//using Novel8r.Logic.Interfaces;

//namespace Novel8r.Logic.Factories
//{
//    public class CodeManagerFactory
//    {
//        private static CodeManagerFactory _instance;

//        [Import(typeof(ICodeManager))]
//        public Export<ICodeManager> CodeManager { get; set; }

//        private CodeManagerFactory()
//        {
//        }

//        public static CodeManagerFactory Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new CodeManagerFactory();
//                }
//                return _instance;
//            }
//        }
//        public ICodeManager GetCodeManager()
//        {
//            return CodeManager.GetExportedObject();
//        }
//    }
//}