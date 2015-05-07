//using System;
//using System.ComponentModel;
//using System.IO;
//using System.Windows.Forms;
//using Alsing.Windows.Forms;
//using Alsing.Windows.Forms.SyntaxBox;
//using Novel8r.Logic.DomainModel.Draggable;
//using Novel8r.Logic.DomainModel.Templates;
//using Novel8r.Logic.Factories;
//using Novel8r.Logic.Interfaces;

//namespace Novel8r.WinForms.Views.Controls
//{
//    [CLSCompliant(false)]
//    [ToolboxItem(false)]
//    public class MyEditViewControl : EditViewControl
//    {
//        public MyEditViewControl(SyntaxBoxControl parent)
//            : base(parent)
//        {
//        }

//        protected override void OnDragDrop(DragEventArgs drgevent)
//        {
//            string s = string.Empty;
//            var files = drgevent.Data.GetData("FileDrop") as string[];
//            if (files != null && files.Length >= 0)
//            {
//                StreamReader sr = File.OpenText(files[0]);
//                s = sr.ReadToEnd();
//            }

//            if (string.IsNullOrEmpty(s))
//            {
//                s = drgevent.Data.GetData(typeof (string)) as string;
//            }

//            if (string.IsNullOrEmpty(s))
//            {
//                var dtable = drgevent.Data.GetData(typeof (DraggableTable)) as DraggableTable;
//                /* 0
//                 * 4 SHIFT
//                 * 8 CTRL
//                 * 12 CTRL+SHIFT
//                 * 32 LALT
//                 * 40 ALT GR
//                 * 
//                 * */
//                if (drgevent.KeyState == 0)
//                {
//                    s = dtable.Table.GetFullObjectName();
//                }
//                else if (drgevent.KeyState == 4)
//                {
//                    TemplateManagerFactory fac = TemplateManagerFactory.Instance;
//                    ITemplateManager manager = fac.GetSqlTemplateManager(dtable.Server.ServerConnection);
//                    string query = manager.GetQuery("UI.OpenTable");
//                    s = query;
//                }
//                else if (drgevent.KeyState == 8)
//                {
//                    TemplateManagerFactory fac = TemplateManagerFactory.Instance;
//                    ITemplateManager manager = fac.GetCodeTemplateManager();
//                    Template template = manager.GetTemplate("CS.AdoCodeSnippet.cs");
//                    s = template.SqlTemplate;
//                }
//            }

//            drgevent.Data.SetData(DataFormats.StringFormat, s);
//            base.OnDragDrop(drgevent);
//        }

//        protected override void OnDragEnter(DragEventArgs drgevent)
//        {
//            _SyntaxBox.Select();
//        }
//    }

//}