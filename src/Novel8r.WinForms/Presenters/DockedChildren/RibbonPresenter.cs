using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Infragistics.Win.UltraWinToolbars;
using Microsoft.Win32;
using Novel8r.Logic.Connection;
using Novel8r.Logic.DomainModel.Project;
using Novel8r.Logic.Exceptions;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Views;
using Novel8r.Logic.Factories;

namespace Novel8r.WinForms.Presenters.DockedChildren
{
    public class RibbonPresenter : IPresenter, IViewPresenter<MainForm>
    {
        private MainForm _view;
        private static RibbonPresenter _instance;

        private RibbonPresenter()
        {
        }

        public static RibbonPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RibbonPresenter();
                }
                return _instance;
            }
        }





        //-----------------------------------------------------------------------------------------------------------

        private readonly IList<Sql8rProjectFile> _knownServers = new List<Sql8rProjectFile>();

        //public void LoadKnownServers()
        //{
        //    var sh = new SettingsIO();
        //    IList<ServerConnectionSettings> knownServers = sh.LoadKnownServers();

        //    removeKnownServers();
        //    for (int i = 0; i < knownServers.Count; i++)
        //    {
        //        ServerConnectionSettings settings = knownServers[i];
        //        AddKnownServer(settings, false);
        //    }
        //}

        public void RemoveKnownServer(ToolBase tb)
        {
            if (tb.Tag is ServerConnectionSettings)
            {
                var settings = (Sql8rProjectFile)tb.Tag;
                _knownServers.Remove(settings);
            }
            _view.tbManager.Tools.Remove(tb);
        }

        private void removeKnownServers()
        {
            RibbonGroup rgKnownServer = _view.tbManager.Ribbon.Tabs[0].Groups[1];
            var btRemoveKnownServer = (PopupMenuTool)_view.tbManager.Ribbon.Tabs[0].Groups[2].Tools[0];

            for (int i = rgKnownServer.Tools.Count - 1; i >= 0; i--)
            {
                ToolBase tb = rgKnownServer.Tools[i];
                RemoveKnownServer(tb);
            }

            for (int i = btRemoveKnownServer.Tools.Count - 1; i >= 0; i--)
            {
                ToolBase tb = btRemoveKnownServer.Tools[i];
                RemoveKnownServer(tb);
            }
        }

        public void AddKnownServer(Sql8rProjectFile project)
        {
            if (addKnownServerInternal(project))
            {
                _knownServers.Add(project);
            }
        }

        private bool addKnownServerInternal(Sql8rProjectFile project)
        {
            var btSvr = new ButtonTool(project.Name);

            if (!_view.tbManager.Tools.Exists(btSvr.Key))
            {
                btSvr.SharedProps.Caption = project.Name;
                btSvr.SharedProps.ToolTipTitle = string.Format("{0} - ({1})", project.Name, project.Path);
                btSvr.SharedProps.AppearancesSmall.Appearance.Image = 6;
                btSvr.ToolClick += RibbonEventHandlers.btSvr_ToolClick;

                btSvr.Tag = project;
                _view.tbManager.Tools.Add(btSvr);

                RibbonGroup rgKnownServers = _view.tbManager.Ribbon.Tabs[0].Groups[1];
                var btRemoveKnownServer = (PopupMenuTool)_view.tbManager.Ribbon.Tabs[0].Groups[2].Tools[0];

                int pos = rgKnownServers.Tools.Add(btSvr);
                ToolBase t = rgKnownServers.Tools[pos];
                t.Tag = project;

                var btRemoveSvr = new ButtonTool("Remove" + btSvr.Key);
                btRemoveSvr.SharedProps.Caption = btSvr.SharedProps.Caption;
                btRemoveSvr.Tag = btSvr;
                _view.tbManager.Tools.Add(btRemoveSvr);
                int id = btRemoveKnownServer.Tools.Add(btRemoveSvr);
                btRemoveKnownServer.Tools[id].Tag = btSvr;


                int nbrOfTools = rgKnownServers.Tools.Count;
                if (nbrOfTools < 10)
                {
                    t.SharedProps.Shortcut =
                        (Shortcut)Enum.Parse(typeof(Shortcut), string.Format("Ctrl{0}", nbrOfTools));
                    t.SharedProps.ToolTipText = string.Format("Shortcut {0}", t.SharedProps.Shortcut);
                }

                //if (connect)
                //{
                //    RibbonEventHandlers.btSvr_ToolClick(btSvr, new ToolClickEventArgs(btSvr, null));
                //}
                return true;
            }
            else
            {
                MessageBox.Show("Server already added", DialogHelper.Instance.GetApplicationName());
                return false;
            }
        }

        //public void SaveKnownServers()
        //{
        //    var sh = new SettingsIO();
        //    sh.SaveKnownServers(_knownServers);
        //}

        public void LoadRecentProjects()
        {
            var sh = new SettingsIO();
            IList<Sql8rProjectFile> recentProjects = sh.LoadRecentProjects();

            for (int i = 0; i < recentProjects.Count; i++)
            {
                Sql8rProjectFile project = recentProjects[i];
                var pmt = (PopupMenuTool)_view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools["pmtRecent"];
                var bt = new ButtonTool(project.Path);
                if (!_view.tbManager.Tools.Exists(bt.Key))
                {
                    bt.Tag = project.Path;
                    bt.SharedProps.Caption = project.Name;
                    bt.SharedProps.ToolTipText = project.Path;
                    bt.SharedProps.AppearancesSmall.Appearance.Image = 1;

                    _view.tbManager.Tools.Add(bt);
                    int recentId = pmt.Tools.Add(bt);
                    pmt.Tools[recentId].Tag = bt.Tag;
                    
                    AddKnownServer(project);
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------















        public void Init()
        {
            setupOther();
            setupConnectionTab();
            setupServerDatabaseTab();
            setupExternalToolsAll();
            setupWindowTab();
            setupHelpTab();

            LoadRecentProjects();
            
        }

        public MainForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        public DialogResult ShowDialog()
        {
            throw new SQL8rException("Do not use this method, the ribbon presenter cannot show anything. It's used by MainPresenter");
        }

        public void Show()
        {
            ShowDialog();
        }

        private void setupOther()
        {
            // Quick Access

            var btConnect = new ButtonTool("btConnect");
            btConnect.SharedProps.Caption = "Connect...";
            btConnect.Tag = "ConnectToBrand";
            btConnect.SharedProps.AppearancesSmall.Appearance.Image = 6;

            _view.tbManager.Tools.Add(btConnect);

            _view.tbManager.Ribbon.QuickAccessToolbar.Tools.Add(btConnect);

            // Footer

            var btExit = new ButtonTool("btExit");
            btExit.SharedProps.Caption = "Exit Novel8r";
            btExit.SharedProps.AppearancesSmall.Appearance.Image = 2;
            btExit.SharedProps.DisplayStyle = ToolDisplayStyle.ImageAndText;
            btExit.ToolClick += RibbonEventHandlers.btExit_ToolClick;

            _view.tbManager.Tools.Add(btExit);

            _view.tbManager.Ribbon.ApplicationMenu.FooterToolbar.Tools.Add(btExit);

            // Left ToolArea

            var btOpenProject = new ButtonTool("btOpenProject");
            btOpenProject.SharedProps.Caption = "Open Project";

            var btImportProject = new ButtonTool("btImportProject");
            btImportProject.SharedProps.Caption = "Import Project";

            var pmtRecent = new PopupMenuTool("pmtRecent");
            pmtRecent.SharedProps.Caption = "Recent Projects";
            pmtRecent.Tag = "Recent";

            _view.tbManager.Tools.Add(btImportProject);
            _view.tbManager.Tools.Add(btOpenProject);
            _view.tbManager.Tools.Add(pmtRecent);

            _view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools.Add(btOpenProject);
            _view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools.Add(btImportProject);
            _view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools.Add(btConnect);

            int recentId = _view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools.Add(pmtRecent);
            _view.tbManager.Ribbon.ApplicationMenu.ToolAreaLeft.Tools[recentId].Tag = pmtRecent.Tag;

            // Right ToolArea

            var mdiWindowListTool = new MdiWindowListTool("MDIWindowList");

            _view.tbManager.Tools.Add(mdiWindowListTool);

            _view.tbManager.Ribbon.ApplicationMenu.ToolAreaRight.Tools.Add(mdiWindowListTool);
        }


        private void setupConnectionTab()
        {
            var pmtConnect = new ButtonTool("ConnectToBrand");
            pmtConnect.SharedProps.Caption = "Connect...";
            pmtConnect.SharedProps.AppearancesSmall.Appearance.Image = 6;
            pmtConnect.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            pmtConnect.Tag = "ConnectToBrand";

//            foreach (ServerVersionId version in DatabaseManagerFactory.Instance.GetSupportedVendors())
//            {
//                var btServer = new ButtonTool(version.ToString());
//                btServer.SharedProps.Caption = version.ToString();
//
//				if (!_view.tbManager.Tools.Exists(btServer.Key))
//				{
//					_view.tbManager.Tools.Add(btServer);
//					pmtConnect.Tools.Add(btServer);
//				}
//            }

            var pmtRemove = new PopupMenuTool("Remove");
            pmtRemove.SharedProps.Caption = "Remove";
            pmtRemove.Tag = "RemoveConnection";


            _view.tbManager.Tools.Add(pmtConnect);
            _view.tbManager.Tools.Add(pmtRemove);


            var rtConnection = new RibbonTab("rtConnection", "Connection");
            var rgConnect = new RibbonGroup("rgConnect", "Connect");
            var rgKnownServers = new RibbonGroup("rgKnownServers", "Known Novels");
            var rgEditKnownServers = new RibbonGroup("rgEditKnownServers", "Edit Servers");

            int connectId = rgConnect.Tools.Add(pmtConnect);
            rgConnect.Tools[connectId].Tag = pmtConnect.Tag;
            rtConnection.Groups.Add(rgConnect);
            rtConnection.Groups.Add(rgKnownServers);
            rtConnection.Groups.Add(rgEditKnownServers);

            int removetId = rgEditKnownServers.Tools.Add(pmtRemove);
            rgEditKnownServers.Tools[removetId].Tag = pmtRemove.Tag;

            _view.tbManager.Ribbon.Tabs.Add(rtConnection);
        }

        private void setupServerDatabaseTab()
        {
            // Server Tab

            var btServerProperties = new ButtonTool("btServerProperties");
            btServerProperties.SharedProps.Caption = "Server Properties";
            btServerProperties.SharedProps.AppearancesSmall.Appearance.Image = 6;

            var btCreateDatabase = new ButtonTool("btCreateDatabase");
            btCreateDatabase.SharedProps.Caption = "Create Database";
            btCreateDatabase.SharedProps.AppearancesSmall.Appearance.Image = 17;

            _view.tbManager.Tools.Add(btCreateDatabase);
            _view.tbManager.Tools.Add(btServerProperties);

            var rtServer = new RibbonTab("rtServer", "Server, Database");
            var rgServer = new RibbonGroup("rgServer", "Server");

            rgServer.Tools.Add(btCreateDatabase);
            rgServer.Tools.Add(btServerProperties);
            rtServer.Groups.Add(rgServer);

            _view.tbManager.Ribbon.Tabs.Add(rtServer);


            var btDatabaseProperties = new ButtonTool("btDatabaseProperties");
            btDatabaseProperties.SharedProps.Caption = "Database Properties";
            btDatabaseProperties.SharedProps.AppearancesSmall.Appearance.Image = 17;

            //var btAnalyzeDatabase = new ButtonTool("btAnalyzeDatabase");
            //btAnalyzeDatabase.SharedProps.Caption = "Analyze Database";
            //btAnalyzeDatabase.SharedProps.AppearancesSmall.Appearance.Image = 17;

            var btCreateTable = new ButtonTool("btCreateTable");
            btCreateTable.SharedProps.Caption = "Create Table";
            btCreateTable.SharedProps.AppearancesSmall.Appearance.Image = 4;

            var btCreateView = new ButtonTool("btCreateView");
            btCreateView.SharedProps.Caption = "Create View";
            btCreateView.SharedProps.AppearancesSmall.Appearance.Image = 7;

            _view.tbManager.Tools.Add(btDatabaseProperties);
            _view.tbManager.Tools.Add(btCreateTable);
            _view.tbManager.Tools.Add(btCreateView);

            //      var rtDatabase = new RibbonTab("rtDatabase", "Database");
            var rgDatabase = new RibbonGroup("rgDatabase", "Database");

            rgDatabase.Tools.Add(btDatabaseProperties);
            rgDatabase.Tools.Add(btCreateTable);
            rgDatabase.Tools.Add(btCreateView);
            rtServer.Groups.Add(rgDatabase);

            //   _view.tbManager.Ribbon.Tabs.Add(rtDatabase);
        }


        private void setupWindowTab()
        {
            var btEditorWindow = new ButtonTool("btEditorWindow");
            btEditorWindow.SharedProps.Caption = "Editor";

            var btSearchWindow = new ButtonTool("btSearchWindow");
            btSearchWindow.SharedProps.Caption = "Search";


            _view.tbManager.Tools.Add(btEditorWindow);
            _view.tbManager.Tools.Add(btSearchWindow);

            var rtWindows = new RibbonTab("rtWindows", "Windows");
            var rgWindows = new RibbonGroup("rgWindows", "Windows");

            rgWindows.Tools.Add(btEditorWindow);
            rgWindows.Tools.Add(btSearchWindow);

            rtWindows.Groups.Add(rgWindows);

            _view.tbManager.Ribbon.Tabs.Add(rtWindows);

            // Pane group

            var btFilePane = new ButtonTool("btFilePane");
            btFilePane.SharedProps.Caption = "Files";

            var btPeoplePane = new ButtonTool("btPeoplePane");
            btPeoplePane.SharedProps.Caption = "People";

            _view.tbManager.Tools.Add(btFilePane);
            _view.tbManager.Tools.Add(btPeoplePane);

            var rgPanes = new RibbonGroup("rgPanes", "Panes");

            rgPanes.Tools.Add(btFilePane);
            rgPanes.Tools.Add(btPeoplePane);
            rtWindows.Groups.Add(rgPanes);

            

            // Themes group

            var btLoadTheme = new ButtonTool("btLoadTheme");
            btLoadTheme.SharedProps.Caption = "Load";

            _view.tbManager.Tools.Add(btLoadTheme);

            var rgThemes = new RibbonGroup("rgThemes", "Themes");

            rgThemes.Tools.Add(btLoadTheme);
            rtWindows.Groups.Add(rgThemes);
        }

        private void setupHelpTab()
        {
            var btAbout = new ButtonTool("btAbout");
            btAbout.SharedProps.Caption = "About";
            btAbout.SharedProps.AppearancesSmall.Appearance.Image = 10;
            btAbout.ToolClick += RibbonEventHandlers.btAbout_ToolClick;

            var btUpgradeCheck = new ButtonTool("btUpgradeCheck");
            btUpgradeCheck.SharedProps.Caption = "Upgrade Check";
            btUpgradeCheck.SharedProps.AppearancesSmall.Appearance.Image = 10;
            btUpgradeCheck.ToolClick += RibbonEventHandlers.btUpgradeCheck_ToolClick;

            _view.tbManager.Tools.Add(btAbout);
            _view.tbManager.Tools.Add(btUpgradeCheck);

            var rtHelp = new RibbonTab("rtHelp", "Help");
            var rgAbout = new RibbonGroup("rgAbout", "About");

            rgAbout.Tools.Add(btAbout);
            rgAbout.Tools.Add(btUpgradeCheck);
            rtHelp.Groups.Add(rgAbout);

            _view.tbManager.Ribbon.Tabs.Add(rtHelp);
        }


        private void setupExternalToolsAll()
        {
            var rtTools = new RibbonTab("rtTools", "Tools");
            _view.tbManager.Ribbon.Tabs.Add(rtTools);

            setupExternalToolsWindows(rtTools);
        }

        private void setupExternalToolsWindows(RibbonTab rtTools)
        {
            var rgMicrosoftW = new RibbonGroup("rgMicrosoftW", "Microsoft Windows");
            rtTools.Groups.Add(rgMicrosoftW);
            //  _view.tbManager.Ribbon.Tabs.Add(rtTools);

            string isPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var fiNotepad = new FileInfo(isPath + @"\notepad.exe");
            if (fiNotepad.Exists)
            {
                var btNotepad = new ButtonTool("btNotepad");
                btNotepad.SharedProps.Caption = "Notepad";
                btNotepad.Tag = fiNotepad.FullName;
                btNotepad.ToolClick += RibbonEventHandlers.btExternalTool_ToolClick;

                _view.tbManager.Tools.Add(btNotepad);
                int pos = rgMicrosoftW.Tools.Add(btNotepad);
                rgMicrosoftW.Tools[pos].Tag = btNotepad.Tag;
            }
        }
    }
}