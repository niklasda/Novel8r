using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Infragistics.Win.UltraWinDock;
using Infragistics.Win.UltraWinTabbedMdi;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.Common;
using Novel8r.Logic.Connection;
using Novel8r.Logic.DomainModel;
//using Novel8r.Logic.DomainModel.Draggable;
using Novel8r.Logic.DomainModel.Search;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.ProjectManager;
using Novel8r.WinForms.Factories;
using Novel8r.WinForms.NodeTypes;
using Novel8r.WinForms.Presenters.DockedChildren;
using Novel8r.WinForms.Presenters.MdiChildren;
using Novel8r.WinForms.Views;
using Novel8r.WinForms.Views.MdiChildren;

namespace Novel8r.WinForms.Presenters
{
    public class MainPresenter : IPresenter, IViewPresenter<MainForm>
    {
        //
        // Members
        //

        private static MainPresenter _instance;
        private MainForm _view;
        private string[] filesToOpen;

        private MainPresenter()
        {


        }

        public static MainPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainPresenter();
                }
                return _instance;
            }
        }

        public MainForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        //
        // Constructors
        //

        #region IPresenter<MainForm> Members

        public void Init(string[] args)
        {
            _instance = this;
            // _view = view;

            _view.FormClosing += view_FormClosing;
            _view.Load += _view_Load;

            //            Infragistics.Win.AppStyling.StyleManager.Load(@"C:\beanstalk\trunk\SQL8r\misc\styles\TestStyleSet.isl");

            _view.mdiManager.InitializeTab += mdiManager_InitializeTab;
            _view.mdiManager.TabSelected += mdiManager_TabDisplaying;

           // _view.tvwPeople.AfterSelect += tvwSqlObjects_AfterSelect;
//            _view.tvwPeople.AfterExpand += tvwSqlObjects_AfterExpand;
//            _view.tvwPeople.MouseDown += tvwSqlObjects_MouseDown;
//            _view.tvwPeople.DragOver += tvwSqlObjects_DragOver;
//            _view.tvwPeople.DragDrop += tvwSqlObjects_DragDrop;
            //      _view.tvwSqlObjects.Override.Sort = SortType.Ascending;
            //     _view.tvwSqlObjects.Nodes.Override.Sort = SortType.Ascending;

            _view.tbManager.ToolClick += toolbarsManager_ToolClick;
            _view.tbManager.QuickAccessToolbarModified += tbManager_QuickAccessToolbarModified;


            //make sure the panes has keys
            foreach (DockableControlPane dcp in _view.dockManager.ControlPanes)
            {
                dcp.Key = dcp.Text;
            }

           
            OpenMdiTab(MdiTabKeys.Search);
            OpenMdiTab(MdiTabKeys.RtfEditor);

            OpenPane(DockedPaneKeys.Files);

            PresenterFactory.Instance.InitProjectPresenter();

            PresenterFactory.Instance.InitRibbonPresenter();
            //  var ribbon = new RibbonPresenter();
            //  ribbon.Init(_view);

            _view.tbManager.Ribbon.Caption = DialogHelper.Instance.GetApplicationName();
            //loadSettings();

            filesToOpen = args;
        }

        private void mdiManager_TabDisplaying(object sender, MdiTabEventArgs e)
        {
            if (e.Tab.Key.Equals(MdiTabKeys.Search))
            {
                var p = (SearchPresenter)e.Tab.Tag;
                p.SetCurrentDatabase();
            }
        }

        void _view_Load(object sender, EventArgs e)
        {
            SplashPresenter.Instance.Close();
            
            if (filesToOpen.Length > 0)
            {

                foreach (string fileName in filesToOpen)
                {
                    string ext = Path.GetExtension(fileName);
                    if (ext.ToUpperInvariant().Equals(".NVLPROJ"))
                    {
                        ProjectPresenter.Instance.OpenProject(fileName);
                    }
                    else
                    {
                        ProjectPresenter.Instance.OpenFileFromProject(fileName);
                    }
                }
            }
        }

        //public void SetView(MainForm view)
        //{
        //    _view = view;
        //}

        public DialogResult ShowDialog()
        {
            return _view.ShowDialog();
        }

        public void Show()
        {
            _view.Show();
        }

        #endregion

        private EditorPresenter getPresenterForView(EditorWithDataGridChildForm f)
        {
            return (EditorPresenter)f.Tag;
        }

        private IList<string> checkOrSaveProjectAndFiles(bool performSave)
        {
            IList<string> filesToSave = new List<string>();

            if (_view.mdiManager.TabGroups.Count > 0)
            {
                foreach (MdiTab t in _view.mdiManager.TabGroups[0].Tabs)
                {
                    if (t.Form is EditorWithDataGridChildForm)
                    {
                        var f = t.Form as EditorWithDataGridChildForm;
                        EditorPresenter p = getPresenterForView(f);
                        if (p.IsModified)
                        {
                            if (performSave)
                            {
                                p.SaveCurrentFile();

                                //                     f.txtEditor.Save(f.tslFilename.Text);
                            }
                            else
                            {
                                filesToSave.Add(f.tslFileName.Text);
                            }
                        }
                    }
                }
            }

            if (Novel8rProjectHandler.Instance.IsModified())
            {
                OpenPane(DockedPaneKeys.Files);
                if (performSave)
                {
                    Novel8rProjectHandler.Instance.SaveProject();
                }
                else
                {
                    filesToSave.Add(Novel8rProjectHandler.Instance.ProjectName);
                }
            }

            return filesToSave;
        }

        private void view_FormClosing(object sender, FormClosingEventArgs e)
        {
            IList<string> filesToSave = checkOrSaveProjectAndFiles(false);


            if (filesToSave.Count > 0)
            {
                var mfp = PresenterFactory.Instance.GetModifiedFilesPresenter();
                // mfp.Init(new ModifiedFilesDialog());
                DialogResult result = mfp.ShowFiles(filesToSave);

                if (result == DialogResult.Yes)
                {
                    checkOrSaveProjectAndFiles(true);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

        //    saveSettings();
        }


        //private void tvwSqlObjects_MouseDown(object sender, MouseEventArgs e)
        //{
        //    // http://blog.lib.umn.edu/kuyp0005/think2/2007/05/c_dragdrop_and_doubleclick.html
        //    if (e.Button == MouseButtons.Left && e.Clicks == 1)
        //    {
        //        var tn = _view.tvwPeople.GetNodeFromPoint(new Point(e.X, e.Y)) as TableNode;
        //        if (tn != null)
        //        {
        //            var dbn = tn.Parent.Parent as DatabaseNode;
        //            var sn = dbn.Parent.Parent as ServerNode;

        //            var dt = new DraggableTable(sn.ServerObject, dbn.DatabaseObject, tn.TableObject);

        //            _view.tvwPeople.DoDragDrop(dt, DragDropEffects.Copy | DragDropEffects.All);
        //        }
        //    }
        //}

        //private void tvwSqlObjects_DragOver(object sender, DragEventArgs e)
        //{
        //    var node = e.Data.GetData(typeof(DraggableTable)) as DraggableTable;
        //    if (node != null)
        //    {
        //        Point clientPos = _view.tvwPeople.PointToClient(new Point(e.X, e.Y));
        //        UltraTreeNode nodeAtPoint = _view.tvwPeople.GetNodeFromPoint(clientPos);

        //        if (nodeAtPoint is TablesNode)
        //        {
        //            e.Effect = DragDropEffects.Copy;
        //        }
        //        else
        //        {
        //            e.Effect = DragDropEffects.None;
        //        }
        //    }
        //}

        //private void tvwSqlObjects_DragDrop(object sender, DragEventArgs e)
        //{
        //    if (e.Data != null)
        //    {
        //        var node = e.Data.GetData(typeof(DraggableTable)) as DraggableTable;
        //        if (node != null)
        //        {
        //            Point clientPos = _view.tvwPeople.PointToClient(new Point(e.X, e.Y));
        //            UltraTreeNode nodeAtPoint = _view.tvwPeople.GetNodeFromPoint(clientPos);

        //            if (nodeAtPoint is TablesNode)
        //            {
        //                // todo implement
        //                MessageBox.Show("Copy Table - Not Implemented!", DialogHelper.Instance.GetApplicationName());
        //            }
        //        }
        //    }
        //}

        private void tbManager_QuickAccessToolbarModified(object sender, QuickAccessToolbarModifiedEventArgs e)
        {
            if (e.QuickAccessToolbarChangeType == QuickAccessToolbarChangeType.ToolAdded)
            {
                ToolBase tb = _view.tbManager.Ribbon.QuickAccessToolbar.Tools[e.Tool.Key];
                tb.Tag = e.Tool.Tag;
            }
        }

        private void tvwSqlObjects_AfterSelect(object sender, SelectEventArgs e)
        {
            if (e.NewSelections.Count > 0)
            {
                UltraTreeNode node = e.NewSelections[0];

                if (node.GetType() == typeof(ServerNode))
                {
                    _view.tbManager.Ribbon.SelectedTab = _view.tbManager.Ribbon.Tabs[RibbonTabKeys.Server];
                }
                else if (node.GetType() == typeof(DatabaseNode))
                {
                    _view.tbManager.Ribbon.SelectedTab = _view.tbManager.Ribbon.Tabs[RibbonTabKeys.Database];
                }
//                else if (node.GetType() == typeof(TableNode))
//                {
//                    _view.tbManager.Ribbon.SelectedTab = _view.tbManager.Ribbon.Tabs[RibbonTabKeys.Table];
//                }
//                else if (node.GetType() == typeof(ViewNode))
//                {
//                    _view.tbManager.Ribbon.SelectedTab = _view.tbManager.Ribbon.Tabs[RibbonTabKeys.View];
//                }
            }
        }

        //
        // Event Handlers
        //

        private void mdiManager_InitializeTab(object sender, MdiTabEventArgs e)
        {
            e.Tab.Key = e.Tab.Form.Text;
            e.Tab.Tag = e.Tab.Form.Tag;
        }

        private void toolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
        {

            // Server
            if (e.Tool.Key == "btCreateDatabase")
            {
                createDatabase();
            }

            else if (e.Tool.Key == "btCreateTable")
            {
                createTable();
            }

            // Windows
            else if (e.Tool.Key == "btEditorWindow")
            {
                OpenMdiTab(MdiTabKeys.RtfEditor);
            }
            else if (e.Tool.Key == "btSearchWindow")
            {
                OpenMdiTab(MdiTabKeys.Search);
            }

            // Panes
            else if (e.Tool.Key == "btFilePane")
            {
                OpenPane(DockedPaneKeys.Files);
            }
            else if (e.Tool.Key == "btDatabasePane")
            {
                OpenPane(DockedPaneKeys.Files);
            }

            // Options
//            else if (e.Tool.Key == "btSaveOptions")
//            {
//                saveSettings();
//            }
//            else if (e.Tool.Key == "btLoadOptions")
//            {
//                loadSettings();
//            }

            // Themes
            else if (e.Tool.Key == "btLoadTheme")
            {
                loadTheme();
            }


            // Main Icon Menu
            else if (e.Tool.Key == "btOpenProject")
            {
                var ofd = DialogFactory.Instance.GetOpenProjectDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ProjectPresenter.Instance.OpenProject(ofd.FileName);
                }
            }
            else if (e.Tool.Key == "btConnect")
            {
            }
            else if (e.Tool.OwningMenu != null)
            {
                PopupMenuTool pmt = e.Tool.OwningMenu;
                if (pmt.Tag.ToString() == "Recent")
                {
                    var path = (string)e.Tool.Tag;
                    ProjectPresenter.Instance.OpenProject(path);
                }
                else if (pmt.Tag.ToString() == "RemoveConnection")
                {
                    var btSvr = (ButtonTool)e.Tool.Tag;

                    RibbonPresenter.Instance.RemoveKnownServer(btSvr);
                    RibbonPresenter.Instance.RemoveKnownServer(e.Tool);
                }
                else if (pmt.Tag.ToString() == "ConnectToBrand")
                {
                    //if (e.Tool.Key == ServerVersionId.SqlServer_2005.ToString())
                    //{
                    //    var p = PresenterFactory.Instance.GetSelectDatabasePresenter(ServerVersionId.SqlServer_2005);
                    //    if (p.ShowDialog() == DialogResult.OK)
                    //    {
                    //        ServerConnectionSettings settings = p.GetConnection();
                    //        RibbonPresenter.Instance.AddKnownServer(settings);
                    //    }
                    //}
                    //else if (e.Tool.Key == ServerVersionId.SqlServer_2008.ToString())
                    //{
                    //}
                    //else if (e.Tool.Key == ServerVersionId.MySql_5.ToString())
                    //{
                    //    var p = PresenterFactory.Instance.GetSelectDatabasePresenter(ServerVersionId.MySql_5);
                    //    if (p.ShowDialog() == DialogResult.OK)
                    //    {
                    //        ServerConnectionSettings settings = p.GetConnection();
                    //        RibbonPresenter.Instance.AddKnownServer(settings);
                    //    }
                    //}
                    //else if (e.Tool.Key == ServerVersionId.SQLite.ToString())
                    //{
                    //    var p1 = ServerVersionId.Parse(e.Tool.Key);
                    //    var p = PresenterFactory.Instance.GetSelectDatabasePresenter(ServerVersionId.SQLite);
                    //    if (p.ShowDialog() == DialogResult.OK)
                    //    {
                    //        ServerConnectionSettings settings = p.GetConnection();
                    //        RibbonPresenter.Instance.AddKnownServer(settings);
                    //    }
                    //}
                }
            }
        }





        private void createDatabase()
        {
            //var sn = getSelectedTreeNode() as ServerNode;
            //if (sn != null)
            //{
            //    var p = (EditorRtfPresenter)OpenMdiTab(MdiTabKeys.RtfEditor);
            //    p.OpenTemplate(SqlTemplateKeys.CreateDatabase);
            //}
        }


        internal void OpenPane(string paneKey)
        {
            _view.dockManager.ControlPanes[paneKey].Closed = false;
            _view.dockManager.ControlPanes[paneKey].Activate();
        }

        //private void saveSettings()
        //{
        //    var io = new SettingsIO();
        //    string isPath8 = io.GetDockSettingsFile();
        //    _view.dockManager.SaveAsXML(isPath8);

        //    RibbonPresenter.Instance.SaveKnownServers();
        //}

        //private void loadSettings()
        //{
        //    var io = new SettingsIO();
        //    string isPath8 = io.GetDockSettingsFile();

        //    if (File.Exists(isPath8))
        //    {
        //        _view.dockManager.LoadFromXML(isPath8);
        //    }

        //    RibbonPresenter.Instance.LoadKnownServers();
        //    RibbonPresenter.Instance.LoadRecentProjects();
        //}

        private void loadTheme()
        {
            OpenFileDialog fd = DialogFactory.Instance.GetOpenStyleDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                Infragistics.Win.AppStyling.StyleManager.Load(fileName);
            }

        }

        internal IPresenter OpenMdiTab(string tabName)
        {
            MdiTab tb = getMdiTabFromForm(tabName);
            IPresenter p1 = null;

            if (tb == null || !_view.mdiManager.TabGroups[0].Tabs.Contains(tb))
            {
                switch (tabName)
                {
                    case MdiTabKeys.Search:
                        p1 = PresenterFactory.Instance.GetSearchPresenter();
                        p1.Show();
                        break;
                    case MdiTabKeys.RtfEditor:
                        p1 = PresenterFactory.Instance.GetRtfEditor();
                        p1.Show();
                        break;
                }
            }
            else
            {
                _view.mdiManager.TabGroups[0].Tabs[tabName].Activate();
                p1 = (IPresenter)tb.Tag;

            }
            return p1;
        }

        private MdiTab getMdiTabFromForm(string name)
        {
            Form f2 = null;
            foreach (Form f in _view.MdiChildren)
            {
                if (f.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                {
                    f2 = f;
                    break;
                }
            }
            MdiTab tb = _view.mdiManager.TabFromForm(f2);
            return tb;
        }

        private void createTable()
        {
            //var dbn = getSelectedTreeNode() as DatabaseNode;
            //if (dbn != null)
            //{
            //    Sql8rDatabase db = dbn.DatabaseObject;
            //    var p = (EditorRtfPresenter)OpenMdiTab(MdiTabKeys.RtfEditor);
            //    p.OpenTemplate(SqlTemplateKeys.CreateTable, db.Name);
            //}
        }

        //private void tvwSqlObjects_AfterExpand(object sender, NodeEventArgs e)
        //{
        //    _view.tvwPeople.BeginUpdate();

        //    if (e.TreeNode is TablesNode)
        //    {
        //        var tn = e.TreeNode as TablesNode;
        //        UltraTreeNode systemTablesNode = tn.Nodes[0];
        //        var dbn = tn.Parent as DatabaseNode;

        //        Sql8rDatabase db = dbn.DatabaseObject;

        //        if (0 == (tn.Nodes.Count + tn.Nodes[0].Nodes.Count - 1))
        //        {
        //            startProgressIndication(db.Tables.Count);

        //            foreach (Sql8rTable table in db.Tables)
        //            {
        //                    var tableNode = new TableNode(table.Name, table.Schema);
        //                    tableNode.TableObject = table;

        //                    var columnsNode = new ColumnsNode();
        //                    columnsNode.Nodes.Override.Sort = SortType.Ascending;
        //                    tableNode.Nodes.Add(columnsNode);

        //                    tn.Nodes.Add(tableNode);

        //                incrementProgreeIndication();
        //                Application.DoEvents();
        //            }
        //        }
        //    }
        //    else if (e.TreeNode is TableNode)
        //    {
        //        TableNode tn;

        //            tn = e.TreeNode as TableNode;

        //        Sql8rTable table = tn.TableObject;
        //        UltraTreeNode columnsNode = e.TreeNode.Nodes[0];

        //        columnsNode.Nodes.Clear();
        //        foreach (Sql8rColumn col in table.Columns.Values)
        //        {
        //            var cNode = new ColumnNode(col.NameSpec, col.ObjectId);
        //            if (col.InPrimaryKey)
        //            {
        //                cNode.Override.NodeAppearance.Image = 5;
        //                cNode.Override.ActiveNodeAppearance.Image = 5;
        //            }
        //            else
        //            {
        //                cNode.Override.NodeAppearance.Image = 11;
        //                cNode.Override.ActiveNodeAppearance.Image = 11;
        //            }
        //            columnsNode.Nodes.Add(cNode);
        //            Application.DoEvents();
        //        }
        //    }
        //    else if (e.TreeNode is ViewsNode)
        //    {
        //        var vn = e.TreeNode as ViewsNode;
        //        UltraTreeNode systemViewsNode = vn.Nodes[0];
        //        var dbn = vn.Parent as DatabaseNode;
        //        //   var sn = dbn.Parent.Parent as ServerNode;
        //        //                Sql8rServer svr = sn.ServerObject;

        //        Sql8rDatabase db = dbn.DatabaseObject;

        //        if (0 == (vn.Nodes.Count + vn.Nodes[0].Nodes.Count - 1))
        //        {
        //            startProgressIndication(db.Views.Count);

        //            foreach (Sql8rView view in db.Views)
        //            {
        //                    var viewNode = new ViewNode(view.Name, view.Schema);
        //                    viewNode.ViewObject = view;
        //                    var columnsNode = new ColumnsNode();
        //                    viewNode.Nodes.Add(columnsNode);
        //                    vn.Nodes.Add(viewNode);

        //                incrementProgreeIndication();
        //                Application.DoEvents();
        //            }
        //        }
        //    }
        //    else if (e.TreeNode is ViewNode)
        //    {
        //        ViewNode vn;

        //            vn = e.TreeNode as ViewNode;

        //        Sql8rView view = vn.ViewObject;
        //        UltraTreeNode columnsNode = e.TreeNode.Nodes[0];

        //        columnsNode.Nodes.Clear();
        //        foreach (Sql8rColumn col in view.Columns.Values)
        //        {
        //            var cNode = new ColumnNode(col.NameSpec, col.ObjectId);
        //            if (col.InPrimaryKey)
        //            {
        //                cNode.Override.NodeAppearance.Image = 5;
        //                cNode.Override.ActiveNodeAppearance.Image = 5;
        //            }
        //            else
        //            {
        //                cNode.Override.NodeAppearance.Image = 11;
        //                cNode.Override.ActiveNodeAppearance.Image = 11;
        //            }
        //            columnsNode.Nodes.Add(cNode);
        //            Application.DoEvents();
        //        }
        //    }

        //    _view.tvwPeople.EndUpdate();
        //}

        private void startProgressIndication(int maxValue)
        {
            _view.sbMain.Panels[StatusBarPanelKeys.Progress].ProgressBarInfo.Value = 0;
            _view.sbMain.Panels[StatusBarPanelKeys.Progress].ProgressBarInfo.Maximum = maxValue;
        }

        private void incrementProgreeIndication()
        {
            try
            {
                _view.sbMain.Panels[StatusBarPanelKeys.Progress].ProgressBarInfo.Value++;
            }
            catch
            {

            }
        }



        //public void ConnectToServer(ServerConnectionSettings settings)
        //{
        //    var fac = DatabaseManagerFactory.Instance;
        //    IDatabaseManager man = fac.GetDatabaseManager(settings);

        //    Sql8rServer svr = man.GetServer();



        //    var serverNode = new ServerNode(string.Format("{0} - ({1})", svr.Name, svr.ServerVersion));
        //    int i = _view.tvwPeople.Nodes.IndexOf(serverNode.Text);


        //    if (i < 0)
        //    {
        //        serverNode.ServerObject = svr;

        //        _view.tvwPeople.Nodes.Add(serverNode);

        //        var databasesNode = new DatabasesNode();
        //        serverNode.Nodes.Add(databasesNode);
        //        databasesNode.Nodes.Override.Sort = SortType.Ascending;


        //        _view.tvwPeople.BeginUpdate();
        //        foreach (Sql8rDatabase db in svr.Databases)
        //        {
        //            var dbNode = new DatabaseNode(db.Name);
        //            dbNode.DatabaseObject = db;
        //            databasesNode.Nodes.Add(dbNode);


        //            addTablesNode(dbNode, settings);
        //            addViewsNode(dbNode, settings);
        //        }
        //        _view.tvwPeople.EndUpdate();
        //        serverNode.Expanded = true;
        //        databasesNode.Expanded = true;
        //    }
        //}

        // Methods & Functions
        //

        private UltraTreeNode getSelectedTreeNode()
        {
            //if (_view.tvwPeople.SelectedNodes.Count > 0)
            //{
            //    return _view.tvwPeople.SelectedNodes[0];
            //}
            return null;
        }

        //public void SelectNode(SearchHit hit)
        //{
        //    TreeNodesCollection nodes = _view.tvwPeople.Nodes;
        //    //IList<ServerNode> list = new List<ServerNode>(nodes.Count);
        //    foreach (UltraTreeNode sNode in nodes)
        //    {
        //        var sn = sNode as ServerNode;
        //        if (sn != null)
        //        {
        //            if (sn.ServerObject.Name.Equals(hit.Server))
        //            {
        //                OpenPane(DockedPaneKeys.Files);
        //                sn.Expanded = true;
        //                sn.Selected = true;

        //                foreach (UltraTreeNode dbNode in sn.Nodes[0].Nodes)
        //                {
        //                    var dbn = dbNode as DatabaseNode;
        //                    if (dbn != null)
        //                    {
        //                        if (dbn.DatabaseObject.Name.Equals(hit.Database))
        //                        {
        //                            dbn.Parent.Expanded = true;
        //                            dbn.Expanded = true;
        //                            dbn.Selected = true;
        //                            if (hit.GetObjectType() == SearchAreas.Table)
        //                            {
        //                                var tablesNode = dbn.Nodes[0].Nodes;

        //                                if (hit.GetIsSystemObject())
        //                                {
        //                                    tablesNode = dbn.Nodes[0].Nodes[0].Nodes;
        //                                }

        //                                foreach (UltraTreeNode tNode in tablesNode)
        //                                {
        //                                    var tn = tNode as TableNode;
        //                                    if (tn != null)
        //                                    {
        //                                        if (tn.TableObject.Name.Equals(hit.Object))
        //                                        {
        //                                            if (hit.GetIsSystemObject())
        //                                            {
        //                                                tn.Parent.Parent.Expanded = true;
        //                                            }

        //                                            tn.Parent.Expanded = true;
        //                                            tn.Selected = true;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else if (hit.GetObjectType() == SearchAreas.View)
        //                            {
        //                                var viewsNode = dbn.Nodes[1].Nodes;

        //                                if (hit.GetIsSystemObject())
        //                                {
        //                                    viewsNode = dbn.Nodes[1].Nodes[0].Nodes;
        //                                }

        //                                foreach (UltraTreeNode vNode in viewsNode)
        //                                {
        //                                    var vn = vNode as ViewNode;
        //                                    if (vn != null)
        //                                    {
        //                                        if (vn.ViewObject.Name.Equals(hit.Object))
        //                                        {
        //                                            if (hit.GetIsSystemObject())
        //                                            {
        //                                                vn.Parent.Parent.Expanded = true;
        //                                            }

        //                                            vn.Parent.Expanded = true;
        //                                            vn.Selected = true;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

//        public IList<ServerNode> GetServerNodes()
//        {
            //TreeNodesCollection nodes = _view.tvwPeople.Nodes;
            //IList<ServerNode> list = new List<ServerNode>(nodes.Count);
            //foreach (UltraTreeNode node in nodes)
            //{
            //    if (node is ServerNode)
            //    {
            //        list.Add(node as ServerNode);
            //    }
            //}
            //return list;
//        }

        public ServerNode GetServerNode()
        {
            UltraTreeNode n = getSelectedTreeNode();
            if (n == null)
            {
                //n = _view.tvwPeople.TopNode;
            }
            if (n != null)
            {
                while (n.Parent != null)
                {
                    n = n.Parent;
                }
            }
            return n as ServerNode;
        }

        public DatabaseNode GetDatabaseNode()
        {
            UltraTreeNode n = getSelectedTreeNode();

            while (n != null)
            {
                if (n is DatabaseNode)
                    break;
                n = n.Parent;
            }

            return n as DatabaseNode;
        }

        private void addTablesNode(DatabaseNode parentNode, ServerConnectionSettings settings)
        {
            var tablesNode = new TablesNode(settings.ServerVersion);
            tablesNode.Nodes.Override.Sort = SortType.Ascending;
            parentNode.Nodes.Add(tablesNode);

        }

        private void addViewsNode(DatabaseNode parentNode, ServerConnectionSettings settings)
        {
            var viewsNode = new ViewsNode(settings.ServerVersion);
            viewsNode.Nodes.Override.Sort = SortType.Ascending;
            parentNode.Nodes.Add(viewsNode);

        }

        public void SetError(string error)
        {
            _view.sbMain.Panels[StatusBarPanelKeys.Auto].Text = error;
        }

        #region Nested type: DockedPaneKeys

        internal static class DockedPaneKeys
        {
       //     internal static string People = "People";
        //    internal static string Items = "Items";
            internal static string Files = "Files";
        }

        #endregion

        #region Nested type: FileTypes

        internal static class FileTypes
        {
       //     internal const string CSharp = ".CS";
            internal const string Folder = "FOLDER";
            internal const string SQL = ".SQL";
			internal const string RTF = ".RTF";
        }

        #endregion

        #region Nested type: MdiTabKeys

        internal static class MdiTabKeys
        {
//            internal const string EditorWithDataGrid = "EditorWithDataGrid";
            //       internal const string CodeEditor = "CodeEditor";
           // internal const string PropertyGrid = "PropertyGrid";
            internal const string Search = "Search";
           // internal const string TableUsage = "TableUsage";
            internal const string RtfEditor = "RtfEditor";
        }

        #endregion

        #region Nested type: SqlTemplateKeys

        internal static class SqlTemplateKeys
        {
            internal const string CreateDatabase = "OnServer.CreateDatabase";
            internal const string CreateTable = "InDatabase.CreateTable";
            internal const string CreateView = "InDatabase.CreateView";
            internal const string CreateIndex = "InTable.CreateIndex";
            internal const string CreateUSP = "InDatabase.CreateProcedure";
            internal const string CreateUDF = "InDatabase.CreateFunction";
        }

        #endregion

        #region Nested type: RibbonTabKeys

        private static class RibbonTabKeys
        {
            internal const string Connection = "rtConnection";

            internal const string Server = "rtServer";
            internal const string Database = "rtServer";

            internal const string View = "rtView";
            internal const string Procedure = "rtView";
            internal const string Function = "rtView";

            internal const string Table = "rtTable";
            internal static string Column = "rtTable";
            internal const string Index = "rtTable";

            internal const string Assembly = "rtAssembly";
            internal static string Tools = "rtTools";
            internal static string Windows = "rtWindows";
            internal static string Help = "rtHelp";
        }

        #endregion

        #region Nested type: StatusBarPanelKeys

        private static class StatusBarPanelKeys
        {
            //          internal const string Tables = "pTables";
            internal const string Auto = "pAuto";
            internal const string Progress = "pProgress";
            internal static string Caption = "pCaption";
        }

        #endregion
    }
}