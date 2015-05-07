//using System.Data;
//using System.Windows.Forms;
//using Novel8r.Logic.DomainModel;
//using Novel8r.Logic.Interfaces;
//using Novel8r.WinForms.Views.MdiChildren;

//namespace Novel8r.WinForms.Presenters.MdiChildren
//{
//    public class EditorWithDataGridPresenterXaXX : IPresenter, IViewPresenter<EditorWithDataGridChildForm>
//    {
//        private EditorWithDataGridChildForm _view;
//        private EditorPresenter _editorPresenter;

//        public bool EditorIsModified
//        {
//            get { return _editorPresenter.IsModified; }
//        }

//        #region IPresenter<EditorWithDataGridChildForm> Members

//        public void Init(EditorPresenter editorPresenter)
//        {
//            _view.MdiParent = MainPresenter.Instance.View;

//            _editorPresenter = editorPresenter;
//        }

//        // todo editor save dbo.tablename without extension to file...
//        public EditorWithDataGridChildForm View
//        {
//            get { return _view; }
//            set { _view = value; }
//        }

//        public DialogResult ShowDialog()
//        {
//            return _view.ShowDialog();
//        }

//        public void Show()
//        {
//            _view.Show();
//        }

//        #endregion

//        public void OpenView(Query query, Sql8rView view)
//        {
//            if (_editorPresenter.EditView(query, view.GetFullObjectName()))
//            {
//            }
//        }

//        public void OpenTable(Query query, Sql8rTable table, bool editable)
//        {
//           if(_editorPresenter.EditTable(query, table.GetFullObjectName()))
//           {
//           }
//        }

//        public void OpenCode(Query query, string fileName)
//        {
//        }

//        public void OpenQuery(DataSet ds, string fileName)
//        {
//        }

//        public void EditIndex(Query query, string fileName)
//        {
//            _editorPresenter.EditIndex(query, fileName);
//        }

//        public void OpenFile(string filePath)
//        {
//            _editorPresenter.OpenFile(filePath); 
//        }

//        public void EditView(Query query, string fileName)
//        {
//            _editorPresenter.EditView(query, fileName);
//        }

//        public void EditTable(Query query, string fileName)
//        {
//            _editorPresenter.EditTable(query, fileName);
//        }

//        public void SaveFile()
//        {
//            _editorPresenter.SaveCurrentFile();
//        }

//        public void AddColumnToTable(Query query, string fileName)
//        {
//            _editorPresenter.AddColumnToTable(query, fileName);
// //           _dataGridPresenter.OpenTable(query, table, editable);         
//        }

//        public void OpenTemplate(string templateKey, params string[] args)
//        {
//            _editorPresenter.OpenTemplate(templateKey, args);
//        }

//        public void AddIndexToTable(Query query, string fileName)
//        {
//            _editorPresenter.AddIndexToTable(query, fileName);
//        }
//    }
//}