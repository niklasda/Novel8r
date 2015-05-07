using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Alsing.SourceCode;
using Infragistics.Win.Printing;
using Novel8r.Logic.Connection;
using Novel8r.Logic.DomainModel;
using Novel8r.Logic.DomainModel.Templates;
using Novel8r.Logic.Exceptions;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Factories;
using Novel8r.WinForms.NodeTypes;
using Novel8r.WinForms.Views.MdiChildren;
using Novel8r.Logic.Common;

namespace Novel8r.WinForms.Presenters.MdiChildren
{
    public class EditorPresenter : IPresenter, IViewPresenter<EditorWithDataGridChildForm>
    {
        private ServerConnectionSettings _lastConnection;
        private EditorWithDataGridChildForm _view;
      //  private EditorWithDataGridPresenter _parentPresenter;

        public bool IsModified
        {
            get { return _view.txtEditor.Document.Modified; }
            set
            {
                if (value)
                {
                    throw new SQL8rException("You are not allowed to explicitly set modified to true");
                }

                _view.txtEditor.Document.Modified = false;
            }
        }

        public void Init()
        {
            _view.FormClosing += _view_FormClosing;
            _view.tsbExecute.ButtonClick += btnExecuteSelected_Click;
            _view.executeSelectedToolStripMenuItem.Click += btnExecuteSelected_Click;

            _view.selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            _view.selectAllToolStripCtxMenuItem.Click += selectAllToolStripMenuItem_Click;

            _view.tsbExecuteOnCurrentConnection.Click += btnExecuteOnCurrentConnection_Click;
            _view.tsbExecuteOnNewConnection.Click += btnExecuteOnNewConnection_Click;
//			_view.tsbExecute.DropDownOpened += tsbExecuteSelected_DropDownOpened;
			_view.tsbCompileToFile.Click += tsbCompileToFile_Click;
			_view.tsbExecuteAsSQL.Click += tsbExecuteAsSQL_Click;

            _view.tsbNew.Tag = "sql";
            _view.tsbNew.ImageTransparentColor = Color.Transparent;
            _view.tsbNew.Image = MainPresenter.Instance.View.TreeImageList.Images[15];
            
            _view.tsbNewCSharpFile.Tag = "cs";
            _view.tsbNewCSharpFile.ImageTransparentColor = Color.Transparent;
            _view.tsbNewCSharpFile.Image = MainPresenter.Instance.View.TreeImageList.Images[13];
            _view.tsbNewCSharpFile.Click += tsbNewFileType_Click;
  
            _view.cToolStripMenuItem1.Tag = "cs";
            _view.cToolStripMenuItem1.ImageTransparentColor = Color.Transparent;
            _view.cToolStripMenuItem1.Image = MainPresenter.Instance.View.TreeImageList.Images[13];
            _view.cToolStripMenuItem1.Click += tsbNew_Click;
            
            _view.tsbNewSqlFile.Tag = "sql";
            _view.tsbNewSqlFile.ImageTransparentColor = Color.Transparent;
            _view.tsbNewSqlFile.Image = MainPresenter.Instance.View.TreeImageList.Images[15];
            _view.tsbNewSqlFile.Click += tsbNewFileType_Click;
            
            _view.sQLToolStripMenuItem.Tag = "sql";
            _view.sQLToolStripMenuItem.ImageTransparentColor = Color.Transparent;
            _view.sQLToolStripMenuItem.Image = MainPresenter.Instance.View.TreeImageList.Images[15];
            _view.sQLToolStripMenuItem.Click += tsbNew_Click;
            
            _view.tsbNewVbNetFile.Tag = "vb";
            _view.tsbNewVbNetFile.ImageTransparentColor = Color.Transparent;
            _view.tsbNewVbNetFile.Image = MainPresenter.Instance.View.TreeImageList.Images[16];
            _view.tsbNewVbNetFile.Click += tsbNewFileType_Click;
            
            _view.vBNETToolStripMenuItem1.Tag = "vb";
            _view.vBNETToolStripMenuItem1.ImageTransparentColor = Color.Transparent;
            _view.vBNETToolStripMenuItem1.Image = MainPresenter.Instance.View.TreeImageList.Images[16];
            _view.vBNETToolStripMenuItem1.Click += tsbNew_Click;

            _view.tsbNew.ButtonClick += tsbNew_Click;
            _view.tsbOpen.Click += tsbOpen_Click;
			_view.openToolStripMenuItem.Click += tsbOpen_Click;
            _view.tsbSave.Click += tsbSave_Click;
			_view.saveToolStripMenuItem.Click += tsbSave_Click;
			_view.saveAsToolStripMenuItem.Click += tsbSaveAs_Click;
			_view.printToolStripMenuItem.Click += tsbPrint_Click;

            _view.tsbCut.Click += tsbCut_Click;
            _view.cutToolStripCtxMenuItem.Click += tsbCut_Click;
            _view.cutToolStripMenuItem.Click += tsbCut_Click;
            _view.cutToolStripCtxMenuItem.Image = _view.cutToolStripMenuItem.Image;
//            _view.cutToolStripCtxMenuItem.Click += tsbCut_Click;
//            _view.cutToolStripMenuItem.Click += tsbCut_Click;
            
			_view.tsbCopy.Click += tsbCopy_Click;
            _view.copyToolStripMenuItem.Click += tsbCopy_Click;
            _view.copyToolStripCtxMenuItem.Click += tsbCopy_Click;
            _view.copyToolStripCtxMenuItem.Image = _view.copyToolStripMenuItem.Image;
//            _view.copyToolStripMenuItem.Click += tsbCopy_Click;
//            _view.copyToolStripCtxMenuItem.Click += tsbCopy_Click;
            
			_view.tsbPaste.Click += tsbPaste_Click;
            _view.pasteToolStripMenuItem.Click += tsbPaste_Click;
            _view.pasteToolStripCtxMenuItem.Click += tsbPaste_Click;
            _view.pasteToolStripCtxMenuItem.Image = _view.pasteToolStripMenuItem.Image;
//            _view.pasteToolStripMenuItem.Click += tsbPaste_Click;
//            _view.pasteToolStripCtxMenuItem.Click += tsbPaste_Click;
            
			_view.tsbDelete.Click += tsbDelete_Click;
			_view.deleteToolStripMenuItem.Click += tsbDelete_Click;
            _view.deleteToolStripCtxMenuItem.Click += tsbDelete_Click;
            _view.deleteToolStripMenuItem.Image = _view.tsbDelete.Image;
            _view.deleteToolStripCtxMenuItem.Image = _view.deleteToolStripMenuItem.Image;            

            _view.tsbUndo.Click += tsbUndo_Click;
            _view.undoToolStripMenuItem.Click += tsbUndo_Click;
            _view.undoToolStripCtxMenuItem.Click += tsbUndo_Click;
            _view.undoToolStripMenuItem.Image = _view.tsbUndo.Image;
            _view.undoToolStripCtxMenuItem.Image = _view.undoToolStripMenuItem.Image;

            _view.tsbRedo.Click += tsbRedo_Click;
            _view.redoToolStripMenuItem.Image = _view.tsbRedo.Image;
            _view.redoToolStripMenuItem.Click += tsbRedo_Click;

            _view.tsbFindText.KeyUp += tsbFindText_KeyUp;
            _view.tsbFind.Click += tsbFind_Click;
            _view.tsbFindNext.Click += tsbFindNext_Click;
            _view.tsbReplace.Click += tsbReplace_Click;
            _view.gotoLineToolStripMenuItem.Click += tsbGotoL_Click;
            _view.findToolStripMenuItem.Click += tsbFind_Click;
            _view.replaceToolStripMenuItem.Click += tsbReplace_Click;

            _view.gotoNextBookmarkToolStripMenuItem.Click += tsbNextBookmark_Click;
            _view.gotoPreviousBookmarkToolStripMenuItem.Click += tsbPreviousBookmark_Click;
            _view.toggleBookmarkToolStripMenuItem.Click += tsbToggleBookmark_Click;
            _view.clearAllBookmarksToolStripMenuItem.Click += tsbClearBookmarks_Click;

            _view.cmiStandard.Tag = _view.tstEditor;
            _view.cmiStandard.Click += cmiStandard_Click;

            setSyntax("SQLServer2K_SQL");
            _view.txtEditor.Document.ModifiedChanged += Document_ModifiedChanged;

//            _view.createDatabaseToolStripMenuItem.Click += createDatabaseToolStripMenuItem_Click;
//            _view.createTableToolStripMenuItem.Click += createTableToolStripMenuItem_Click;
//            _view.createViewToolStripMenuItem.Click += createViewToolStripMenuItem_Click;

//            _view.tsmiCSharpAdoSnippet.Click += tsmiCSharpAdoSnippet_Click;
//			_view.tsmiVbNetAdoSnippet.Click += tsmiVbNetAdoSnippet_Click;
//			_view.tsmiCreateClrUsp.Click += tsmiCreateClrUsp_Click;
//			_view.tsmiCreateClrUdf.Click += tsmiCreateClrUdf_Click;
//			_view.tsmiCreateClrUdt.Click += tsmiCreateClrUdt_Click;
//			_view.tsmiCreateClrTrigger.Click += tsmiCreateClrTrigger_Click;
//			_view.tsmiCreateClrAggregate.Click += tsmiCreateClrAggregate_Click;

            tsbNewFileType_Click(_view.tsbNewSqlFile, EventArgs.Empty);
        }

		private void setSyntax(string synName)
        {
            string resName = string.Format("SQL8r.WinForms.SyntaxFiles.{0}.syn", synName);
        
         //   string xml = ResourceReader.GetResourceSyntax(resName);
         
      //      var s = new SyntaxDefinitionLoader();
      //      SyntaxDefinition l = s.LoadXML(xml);
       //     _view.txtEditor.Document.Parser.Init(l);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _view.txtEditor.SelectAll();
        }

        public EditorWithDataGridChildForm View
        {
            get { return _view; }
            set { _view = value; }
        }


        //public void SetParentPresenter(IViewPresenter<EditorWithDataGridChildForm> parentPresenter)
        //{
        //    _parentPresenter = (EditorWithDataGridPresenter)parentPresenter;
        //}

        private void _view_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.MdiFormClosing && e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (IsModified)
                {
                    DialogResult result = checkAndSave();
                    if (result == DialogResult.Yes)
                    {
                        SaveCurrentFile();
                        if (IsModified)
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        IsModified = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void tsbNewFileType_Click(object sender, EventArgs e)
        {
            var item = (ToolStripDropDownItem) sender;
            var parent = item.OwnerItem as ToolStripSplitButton;
            if (parent != null)
            {
                parent.Text = string.Format("New {0} File...", item.Text);

                parent.Image = item.Image;
                parent.Tag = item.Tag;
                tsbNew_Click(parent, e);
            }
        }

        private void insertTemplateCode(Template t)
        {
			setExtensionSynatx(t.Name, _lastConnection);

            _view.txtEditor.Selection.DeleteSelection();
            TextPoint current = _view.txtEditor.Caret.Position;
            _view.txtEditor.Document.InsertText(t.SqlTemplate, current.X, current.Y);
        }

        //private void insertTemplate(string templateKey)
        //{
        //    var tf = TemplateManagerFactory.Instance;
        //    ITemplateManager man = tf.GetSqlTemplateManager(_lastConnection);

        //    Template t = man.GetTemplate(templateKey);
        //    insertTemplateCode(t);
        //}

        //public void OpenTemplate(string templateKey, params string[] args)
        //{
        //    if (closeCurrentFile())
        //    {
        //        var tf = TemplateManagerFactory.Instance;
        //        ITemplateManager man = tf.GetSqlTemplateManager(_lastConnection);

        //        Template t = man.GetTemplate(templateKey);
        //        _view.txtEditor.Document.Text = string.Format(t.SqlTemplate, args);
        //    }
        //}

        //private void createDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    insertTemplate(MainPresenter.SqlTemplateKeys.CreateDatabase);
        //}

        //private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    insertTemplate(MainPresenter.SqlTemplateKeys.CreateTable);
        //}

//        private void createViewToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            insertTemplate(MainPresenter.SqlTemplateKeys.CreateView);
//        }
//
//        private void tsmiVbNetAdoSnippet_Click(object sender, EventArgs e)
//        {
//            var tf = TemplateManagerFactory.Instance;
//            ITemplateManager man = tf.GetCodeTemplateManager();
//
//            Template t = man.GetTemplate("VB.AdoCodeSnippet.vb");
//            insertTemplateCode(t);
//        }
//
//        private void tsmiCSharpAdoSnippet_Click(object sender, EventArgs e)
//        {
//            var tf = TemplateManagerFactory.Instance;
//            ITemplateManager man = tf.GetCodeTemplateManager();
//
//            Template t = man.GetTemplate("CS.AdoCodeSnippet.cs");
//            insertTemplateCode(t);
//        }
//
//		private void tsmiCreateClrUsp_Click(object sender, EventArgs e)
//		{
//			var tf = TemplateManagerFactory.Instance;
//			ITemplateManager man = tf.GetCodeTemplateManager(); 
//
//			Template t = man.GetTemplate("CS.CreateClrUspSnippet.cs");
//			insertTemplateCode(t);
//		}
//
//		private void tsmiCreateClrUdf_Click(object sender, EventArgs e)
//		{
//			var tf = TemplateManagerFactory.Instance;
//			ITemplateManager man = tf.GetCodeTemplateManager();
//
//			Template t = man.GetTemplate("CS.CreateClrUdfSnippet.cs");
//			insertTemplateCode(t);
//		}
//
//    	private void tsmiCreateClrUdt_Click(object sender, EventArgs e)
//    	{
//			var tf = TemplateManagerFactory.Instance;
//			ITemplateManager man = tf.GetCodeTemplateManager();
//
//			Template t = man.GetTemplate("CS.CreateClrUdtSnippet.cs");
//			insertTemplateCode(t);
//    	}
//
//    	private void tsmiCreateClrAggregate_Click(object sender, EventArgs e)
//		{
//			var tf = TemplateManagerFactory.Instance;
//			ITemplateManager man = tf.GetCodeTemplateManager();
//
//			Template t = man.GetTemplate("CS.CreateClrAggregateSnippet.cs");
//			insertTemplateCode(t);
//		}
//
//    	private void tsmiCreateClrTrigger_Click(object sender, EventArgs e)
//		{
//			var tf = TemplateManagerFactory.Instance;
//			ITemplateManager man = tf.GetCodeTemplateManager();
//
//			Template t = man.GetTemplate("CS.CreateClrTriggerSnippet.cs");
//			insertTemplateCode(t);
//		}
//
    	private void Document_ModifiedChanged(object sender, EventArgs e)
        {
            if (IsModified && !_view.tslFileName.Text.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
            {
                _view.tslFileName.Text += " *";
            }
            else if (!IsModified && _view.tslFileName.Text.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
            {
                _view.tslFileName.Text = _view.tslFileName.Text.Substring(0, _view.tslFileName.Text.LastIndexOf(" *", StringComparison.OrdinalIgnoreCase));
            }
        }

        private void cmiStandard_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            var strip = (ToolStrip) item.Tag;
            item.Checked = !item.Checked;
            strip.Visible = item.Checked;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            if (closeCurrentFile())
            {
                var ctrl = sender as ToolStripItem;
                _view.txtEditor.Document.Text = string.Empty;
            	if (ctrl != null)
            	{
            		string fileName = string.Format("<no file>.{0}", ctrl.Tag);
            		_view.tslFileName.Text = fileName;
            		setExtensionSynatx(fileName, _lastConnection);
            	}
            }
        }

        private bool closeCurrentFile()
        {
            if (IsModified)
            {
                DialogResult result = checkAndSave();
                if (result == DialogResult.Yes)
                {
                    SaveCurrentFile();
                    if (!IsModified)
                    {
                        _view.txtEditor.Document.Text = string.Empty;
                        IsModified = false;                        
                    }
                }
                else if (result == DialogResult.No)
                {
                    _view.txtEditor.Document.Text = string.Empty;
                    IsModified = false;
                }
            }
            else
            {
                _view.txtEditor.Document.Text = string.Empty;
                IsModified = false;
            }
            return !IsModified;
        }

        private DialogResult checkAndSave()
        {
            var mfp = PresenterFactory.Instance.GetModifiedFilesPresenter();

            IList<string> filesToSave = new List<string>(1);
            filesToSave.Add(_view.tslFileName.Text);
            return mfp.ShowFiles(filesToSave);
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _view.txtEditor.Open(ofd.FileName);
                _view.tslFileName.Text = ofd.FileName;
            }
        }

        public void SaveCurrentFile()
        {
            string fileName = _view.tslFileName.Text;
            if (fileName.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName.Substring(0, fileName.Length - 2);
            }

            if (fileName.StartsWith("<", StringComparison.OrdinalIgnoreCase))
            {
                SaveFileAs(fileName);
            }
            else
            {
                _view.txtEditor.Save(fileName);
                IsModified = false; 
            }
        }

        private void SaveFileAs(string oldFileName)
        {
            SaveFileDialog sfd = DialogFactory.Instance.GetSaveFileDialog(oldFileName);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _view.txtEditor.Save(sfd.FileName);
                IsModified = false;
                OpenFile(sfd.FileName);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                SaveCurrentFile();
            }
            else
            {
                MainPresenter.Instance.SetError("File is not modified");
            }
        }

        private void tsbSaveAs_Click(object sender, EventArgs e)
        {
            string oldFileName = _view.tslFileName.Text;
            SaveFileAs(oldFileName);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            var pd = new SourceCodePrintDocument(_view.txtEditor.Document);

            var ppv = new UltraPrintPreviewDialog();
            ppv.Document = pd;
            ppv.Show();
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Cut();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Copy();
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Paste();
        }

		private void tsbDelete_Click(object sender, EventArgs e)
		{
			_view.txtEditor.Delete();
		}

    	private void tsbUndo_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Undo();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Redo();
        }

        private void tsbFind_Click(object sender, EventArgs e)
        {
            _view.txtEditor.ShowFind();
        }

        private void tsbFindNext_Click(object sender, EventArgs e)
        {
            _view.txtEditor.FindNext();
        }

        private void tsbReplace_Click(object sender, EventArgs e)
        {
            _view.txtEditor.ShowReplace();
        }

        private void tsbNextBookmark_Click(object sender, EventArgs e)
        {
            _view.txtEditor.GotoNextBookmark();
        }

        private void tsbPreviousBookmark_Click(object sender, EventArgs e)
        {
            _view.txtEditor.GotoPreviousBookmark();
        }

        private void tsbToggleBookmark_Click(object sender, EventArgs e)
        {
            _view.txtEditor.ToggleBookmark();
        }

        private void tsbClearBookmarks_Click(object sender, EventArgs e)
        {
            _view.txtEditor.Document.ClearBookmarks();
        }

        private void tsbGotoL_Click(object sender, EventArgs e)
        {
            _view.txtEditor.ShowGotoLine();
        }

        private void tsbFindText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string crit = _view.tsbFindText.Text;
                _view.txtEditor.Select();
                _view.txtEditor.FindNext(crit, false, false, false);
            }
        }

		private void setExtensionSynatx(string fileName, ServerConnectionSettings cn)
		{
			_view.tslFileName.Text = fileName;

			if (fileName.ToUpperInvariant().EndsWith(".CS", StringComparison.OrdinalIgnoreCase))
			{
				setSyntax("CSharp");
			}
			else if (fileName.ToUpperInvariant().EndsWith(".VB", StringComparison.OrdinalIgnoreCase))
			{
				setSyntax("VB.NET");
			}

			else if (cn != null)
			{
				if (cn.ServerVersion == ServerVersionId.MySql_5)
				{
					setSyntax("MySQL_SQL");
				}
				else if (cn.ServerVersion == ServerVersionId.Oracle_10)
				{
					setSyntax("Oracle_SQL");
				}
				else
				{
					setSyntax("SQLServer2K_SQL");
				}
			}
			else
			{
				setSyntax("SQLServer2K_SQL");
			}
		}

		public void OpenFile(string fileName)
		{
			if (closeCurrentFile())
			{
				setExtensionSynatx(fileName, _lastConnection); // this seems to set Modified=false

				_view.txtEditor.Open(fileName);
				_view.Text = Path.GetFileName(fileName);
			}
		}

		public bool OpenQuery(Query viewQ, string fileName)
		{
			if (closeCurrentFile())
			{
				_lastConnection = viewQ.ServerConnection;
				setExtensionSynatx(fileName, _lastConnection);

				_view.txtEditor.Document.Tag = viewQ;
				_view.txtEditor.Document.Text = viewQ.Sql;
				_view.tslFileName.Text = fileName;

				_view.Text = fileName;
				return true;
			}
			return false;
		}

		public bool EditTable(Query tableQ, string fileName)
		{
			return OpenQuery(tableQ, fileName);
		}

		public bool EditView(Query viewQ, string fileName)
		{
			return OpenQuery(viewQ, fileName);
		}

		public void EditIndex(Query indexQ, string fileName)
		{
			OpenQuery(indexQ, fileName);
		}

		public void Edit2Code(Query codeQ, string fileName)
		{
			OpenQuery(codeQ, fileName);
		}

		public void AddColumnToTable(Query columnQ, string fileName)
		{
			OpenQuery(columnQ, fileName);
		}

		public void AddIndexToTable(Query indexQ, string fileName)
		{
			OpenQuery(indexQ, fileName);
		}

		private string getExtension(string fileName)
		{
			if (fileName.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
			{
				fileName = fileName.Substring(0, fileName.LastIndexOf(" *", StringComparison.OrdinalIgnoreCase));
			}

			int dotpos = fileName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase) + 1;
			int length = fileName.Length;
			string ext = fileName.Substring(dotpos, length - dotpos);
			return ext.ToUpperInvariant();
		}





















        private ServerConnectionSettings getConnection()
        {
            ServerNode sn = MainPresenter.Instance.GetServerNode();
            if (sn != null)
            {
                return sn.ServerObject.ServerConnection;
            }
        	
        	return null;
        }
		
		private ServerConnectionSettings getCurrentConnection()
        {
            var query = (Query) _view.txtEditor.Document.Tag;

            if (query == null)
            {
            	if(_lastConnection != null)
                {
                    return _lastConnection;
                }
            	return null;
            }
			return query.ServerConnection;
        }




        private void btnExecuteOnCurrentConnection_Click(object sender, EventArgs e)
        {
			//executeQuery(getCurrentConnection(), _view.tslFileName.Text);
        }

		private void tsbCompileToFile_Click(object sender, EventArgs e)
		{
			string code = _view.txtEditor.Document.Text;
			string fileName = _view.tslFileName.Text;
			string ext = getExtension(fileName);

		}

		private void tsbExecuteAsSQL_Click(object sender, EventArgs e)
		{
			//string sql = _view.txtEditor.Document.Text;
			//string sql;
			//if (_view.txtEditor.Selection.IsValid)
			//{
			//    sql = _view.txtEditor.Selection.Text;
			//}
			//else
			//{
			//    sql = _view.txtEditor.Document.Text;
			//}
			//string fileName = _view.tslFileName.Text;

//			executeQuery(getConnection(), "temp.sql");
		}

        //private void tsbExecuteSelected_DropDownOpened(object sender, EventArgs e)
        //{
        //    ServerConnectionSettings scs = getCurrentConnection();
        //    if (scs != null)
        //    {
        //        _view.tsbExecuteOnCurrentConnection.Enabled = true;
        //        _view.tsbExecuteOnCurrentConnection.ToolTipText = scs.GetConnectionString();
        //    }
        //    else
        //    {
        //        _view.tsbExecuteOnCurrentConnection.Enabled = false;
        //    }

        //    _view.tsbExplorerConnections.DropDownItems.Clear();
        //    IList<ServerNode> servers = MainPresenter.Instance.GetServerNodes();
        //    if (servers.Count > 0)
        //    {
        //        foreach (ServerNode sn in servers)
        //        {
        //            ToolStripItem item = _view.tsbExplorerConnections.DropDownItems.Add(sn.ServerObject.Name);
        //            item.Tag = sn.ServerObject.ServerConnection;
        //            item.Click += executeOnConnection_Click;
        //            item.ToolTipText = sn.ServerObject.ServerConnection.GetConnectionString();
        //        }
        //    }
        //    else
        //    {
        //        ToolStripItem item = _view.tsbExplorerConnections.DropDownItems.Add("<none>");
        //        item.Click += btnExecuteOnNewConnection_Click;
        //    }
        //}

        private void executeOnConnection_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem) sender;
            var settings = (ServerConnectionSettings) item.Tag;

//			executeQuery(settings, _view.tslFileName.Text);
        }

        private void btnExecuteOnNewConnection_Click(object sender, EventArgs e)
        {
        }

        private void btnExecuteSelected_Click(object sender, EventArgs e)
        {
            ServerConnectionSettings cn = getConnection();

//			executeQuery(cn, _view.tslFileName.Text);
        }

        //private void executeCode(ServerConnectionSettings cn, string language, string code, string fileName)
        //{
        //    //var dnh = new DotNetManagerFront();
        //    ICodeManager man = CodeManagerFactory.Instance.GetCodeManager();
        //    DataSet ds = man.CompileAndRun(language, code);
        //    if (ds != null)
        //    {
        //        var query = new Query(cn, cn.ServerName, cn.DatabaseName, code, ds.Tables[0]);
        //        _parentPresenter.OpenCode(query, fileName);
        //    }
        //}

//		private void compileCode(string language, string code, string outFileName)
//		{
//			//
//			//var dnh = new DotNetManagerFront();
//			ICodeManager man = CodeManagerFactory.Instance.GetCodeManager();
//			man.CompileToFile(language, code, outFileName);
//		}

        //private void executeQuery(ServerConnectionSettings cn, string fileName)
        //{
        //    string sql;
        //    if (_view.txtEditor.Selection.IsValid)
        //    {
        //        sql = _view.txtEditor.Selection.Text;
        //    }
        //    else
        //    {
        //        sql = _view.txtEditor.Document.Text;
        //    }
        //    //string fileName = _view.tslFileName.Text;

        //    executeQuery2(cn, sql, fileName);
        //}

        //private void executeQuery2(ServerConnectionSettings cn, string sql, string fileName)
        //{
        //    if (cn != null)
        //    {
        //        _lastConnection = cn;
        //        if (!string.IsNullOrEmpty(sql.Trim()))
        //        {
        //            string ext = getExtension(fileName);
        //            if (ext.Equals("CS") || ext.Equals("VB"))
        //            {
        //                string code = sql;
        //                string language = ext;

        //                executeCode(cn, language, code, fileName);
        //            }
        //            else
        //            {
        //                var fac = DatabaseManagerFactory.Instance;
        //                IDatabaseManager man = fac.GetDatabaseManager(cn);

        //                try
        //                {
        //                    DataSet ds = man.ExecuteQuery(cn.ServerName, cn.DatabaseName, sql);
        //                    _parentPresenter.OpenQuery(ds, "ad hoc query");
        //                    MainPresenter.Instance.SetError("Query executed without errors");
        //                }
        //                catch (SqlException ex)
        //                {
        //                    _view.txtEditor.GotoLine(ex.LineNumber - 1);
        //                    _view.txtEditor.Caret.MoveHome(false);
        //                    _view.txtEditor.Caret.MoveEnd(true);
        ////                    _view.txtEditor.Selection.Bounds.LastRow = _view.txtEditor.Selection.Bounds.FirstRow + 1;
        //                    MainPresenter.Instance.SetError(ex.Message);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MainPresenter.Instance.SetError("Nothing to execute");
        //        }
        //    }
        //    else
        //    {
        //        MainPresenter.Instance.SetError("Query has no connection");
        //    }
        //}    

                public DialogResult ShowDialog()
        {
            return _view.ShowDialog();
        }

        public void Show()
        {
            _view.Show();
        }


    }
}