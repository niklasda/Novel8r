using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinDock;
using Infragistics.Win.UltraWinTree;
using Novel8r.Logic.Connection;
using Novel8r.Logic.DomainModel.Project;
using Novel8r.Logic.Exceptions;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.ProjectManager;
using Novel8r.WinForms.NodeTypes.Project;
using Novel8r.WinForms.Presenters.MdiChildren;
using Novel8r.WinForms.Views;

namespace Novel8r.WinForms.Presenters.DockedChildren
{
    public class ProjectPresenter : IPresenter, IViewPresenter<MainForm>
    {
        private MainForm _view;
        private static ProjectPresenter _instance;

        private ProjectPresenter()
        {
        }

        public static ProjectPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProjectPresenter();
                } 
                return _instance;
            }
        }


        public void Init()
        {
            //Instance = this;
           // _view = view;

            _view.tvwProject.Override.UseEditor = DefaultableBoolean.True;
            _view.tvwProject.Override.LabelEditAppearance.BackColor = Color.Snow;
            _view.tvwProject.ValidateLabelEdit += tvwProject_ValidateLabelEdit;
            _view.tvwProject.BeforeCellEnterEditMode += tvwProject_BeforeCellEnterEditMode;
            _view.tvwProject.KeyUp += tvwProject_KeyUp;

            _view.tvwProject.Override.CellClickAction = CellClickAction.EditCell;
            _view.tvwProject.Override.LabelEdit = DefaultableBoolean.True;


            _view.tvwProject.MouseDoubleClick += tvwProject_MouseDoubleClick;
            _view.tvwProject.Override.Sort = SortType.Ascending;
            // _view.tvwProject.Override.SortComparer = new ProjectNodeComparer();


            _view.tsbAddFileFolder.ButtonClick += tsbAddFileFolder_ButtonClick;
            _view.tsbAddFileFolder.Tag = MainPresenter.FileTypes.SQL;
            _view.tsbAddFileFolder.Image = _view.TreeImageList.Images[15];
            _view.tsbAddFileFolder.ImageTransparentColor = Color.Transparent;

            _view.tsbAddRtfFile.Click += tsbAddFile_Click;
            _view.tsbAddRtfFile.Tag = MainPresenter.FileTypes.SQL;
            _view.tsbAddRtfFile.Image = _view.TreeImageList.Images[15];
            _view.tsbAddRtfFile.ImageTransparentColor = Color.Transparent;

            _view.tsbAddFolder.Click += tsbAddFolder_Click;
            _view.tsbAddFolder.Tag = MainPresenter.FileTypes.Folder;

            _view.tsbRemoveFile.Click += tsbRemoveFile_Click;
            _view.tsbNewProject.Click += tsbNewProject_Click;
            _view.tsbOpenProject.Click += tsbOpenProject_Click;
            _view.tsbSaveProject.Click += tsbSaveProject_Click;
        }

        public MainForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        public DialogResult ShowDialog()
        {
            throw new SQL8rException("Do not use");
        }

        public void Show()
        {
            throw new SQL8rException("Do not use");
        }

        public Image GetImageForExtension(string type)
        {
            if (type.Equals(MainPresenter.FileTypes.SQL))
            {
                return _view.TreeImageList.Images[15];
            }
        	return _view.TreeImageList.Images[15];
        }


        private void tvwProject_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_view.tvwProject.SelectedNodes.Count > 0)
                {
                    var node = _view.tvwProject.SelectedNodes[0] as FileNode;
                    openFileFromProject(node);
                }
            }
        }

        private void tvwProject_BeforeCellEnterEditMode(object sender, BeforeCellEnterEditModeEventArgs e)
        {
            if (e.Node is ProjectNode || e.Node is FolderNode)
            {
                e.Cancel = true;
            }
        }

        private ProjectNode getProjectNode(UltraTreeNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node is ProjectNode)
            {
                return node as ProjectNode;
            }

            while (node.Parent != null)
            {
            	if (node.Parent is ProjectNode)
                {
                    return node.Parent as ProjectNode;
                }
            	node = node.Parent;
            }
        	return null;
        }

        private void tvwProject_ValidateLabelEdit(object sender, ValidateLabelEditEventArgs e)
        {
            if (e.LabelEditText.Equals(e.OriginalText))
            {
                e.Cancel = true;
            }
            else
            {
                UltraTreeNode node = e.Node;
                string newName = e.LabelEditText;
                string oldName = e.OriginalText;

                if (node is ProjectNode)
                {
                    var pn = node as ProjectNode;
                    pn.Name = newName;
                }
                else if (node is FolderNode)
                {
                    ProjectNode pn = getProjectNode(node);
                    var foNode = node as FolderNode;
                    //string newFileName = foNode.FullPath + @"\" + newName;
                    string dirName = foNode.Parent.FullPath;
                    var sb = new StringBuilder(dirName);
                    string newdirname = pn.FolderObject + sb.Replace(pn.Name, "", 0, pn.Name.Length).ToString();
                    string oldFileName = newdirname + @"\" + oldName;
                    string newFileName = newdirname + @"\" + newName;
                    //                    string newFileName = fn.FolderObject.Parent.FullName + @"\" + newName;
                    // string newFileName2 = fn.FolderObject.Parent + @"\" + newName;
                    //           fn.FolderObject.MoveTo(newFileName);
                    var fi = new DirectoryInfo(oldFileName);
                    fi.MoveTo(newFileName);

                    Novel8rProjectHandler.Instance.RenameFolder(oldFileName, newFileName);

                    // TODO funkar inte, alla filer måste döpas om
                    //  ProjectHandler.Instance.RemoveFile(oldName);
                    //   ProjectHandler.Instance.AddFile(newName);
                }
                else if (node is FileNode)
                {
                    ProjectNode pn = getProjectNode(node);
                    var fiNode = node as FileNode;
                    string dirName = fiNode.Parent.FullPath;
                    var sb = new StringBuilder(dirName);
                    string newReldirname = sb.Replace(pn.Name, "", 0, pn.Name.Length).ToString();
                    string newdirname = pn.FolderObject + newReldirname;
               //   string newRelFilename = newReldirname + @"\" + newName;
                 //   string oldRelFilename = newReldirname + @"\" + oldName;
                    string oldFileName = newdirname + @"\" + oldName;
                    var fi = new FileInfo(oldFileName);
                    string newFileName = newdirname + @"\" + newName;
                    fi.MoveTo(newFileName);

                    Novel8rProjectHandler.Instance.RenameFile(oldFileName, newFileName);
                }

                handleProjectModifiedStatus();
            }
        }

        private void handleProjectModifiedStatus()
        {
            DockableControlPane pane = _view.dockManager.ControlPanes[MainPresenter.DockedPaneKeys.Files];

            if (Novel8rProjectHandler.Instance.IsModified())
            {
                if (!pane.Text.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
                {
                    pane.Text += " *";
                }
            }
            else
            {
                if (pane.Text.EndsWith(" *", StringComparison.OrdinalIgnoreCase))
                {
                    pane.Text = pane.Text.Substring(0, pane.Text.Length - 2);
                }
            }
        }

        private void tsbAddFileFolder_ButtonClick(object sender, EventArgs e)
        {
            var item = (ToolStripSplitButton) sender;

            string selectedFileType = item.Tag.ToString();

            if (selectedFileType == MainPresenter.FileTypes.Folder)
            {
                createFolder();
            }
            else
            {
                createFile(selectedFileType);
            }

            handleProjectModifiedStatus();
        }

        private void tsbAddFile_Click(object sender, EventArgs e)
        {
            var item = (ToolStripDropDownItem) sender;
            var parent = (ToolStripSplitButton) item.OwnerItem;
            parent.Text = string.Format("Add {0} File...", item.Tag);
            parent.Image = item.Image;
            parent.Tag = item.Tag;

            tsbAddFileFolder_ButtonClick(parent, e);
        }

        private void tsbAddFolder_Click(object sender, EventArgs e)
        {
            var item = (ToolStripDropDownItem) sender;
            var parent = (ToolStripSplitButton) item.OwnerItem;
            parent.Image = item.Image;
            parent.Tag = item.Tag;

            tsbAddFileFolder_ButtonClick(parent, e);
        }

        private void createFolder()
        {
            if (_view.tvwProject.SelectedNodes != null && _view.tvwProject.SelectedNodes.Count > 0)
            {
                UltraTreeNode node = _view.tvwProject.SelectedNodes[0];
                if (node is ProjectNode)
                {
                    var pNode = node as ProjectNode;
                    DirectoryInfo dir = pNode.FolderObject;
                    DirectoryInfo newdir = dir.CreateSubdirectory("New Folder");
                    var foNode = new FolderNode(newdir);
                    node.Nodes.Add(foNode);
                    //_view.tvwProject.Nodes.                    
                    foNode.BeginEdit();
                    Novel8rProjectHandler.Instance.AddFolder(newdir.FullName);                   
                }
                else if (node is FolderNode)
                {
                    ProjectNode pn = getProjectNode(node);
                    var foNode = node as FolderNode;
                    string dirName = foNode.FullPath;
                    var sb = new StringBuilder(dirName);
                    string newdirname = pn.FolderObject + sb.Replace(pn.Name, "", 0, pn.Name.Length).ToString();
                    var dir = new DirectoryInfo(newdirname);
                    DirectoryInfo newdir = dir.CreateSubdirectory("New Folder");
                    var fNode = new FolderNode(newdir);
                    node.Nodes.Add(fNode);                    
                    fNode.BeginEdit();
                    Novel8rProjectHandler.Instance.AddFolder(newdir.FullName);
                }
            }
        }

        private void createFile(string fileType)
        {
            string typeSpecificFileName = string.Empty;
            if (fileType == MainPresenter.FileTypes.SQL)
            {
                typeSpecificFileName = "untitled.sql";
            }
            else if (fileType == MainPresenter.FileTypes.RTF)
            {
                typeSpecificFileName = "untitled.rtf";
            }

            if (_view.tvwProject.SelectedNodes != null && _view.tvwProject.SelectedNodes.Count > 0)
            {
                // TODO: maybe ProjectNode should inherit from FolderNode
                UltraTreeNode node = _view.tvwProject.SelectedNodes[0];
                if (node is ProjectNode)
                {
                    var pNode = node as ProjectNode;
                    DirectoryInfo dir = pNode.FolderObject;

                    var file = new FileInfo(dir + @"\" + typeSpecificFileName);
                    if (!file.Exists)
                    {
                        FileStream fs = file.Create();
                        fs.Close();

                        var fiNode = new FileNode(file);
                        node.Nodes.Add(fiNode);
                        node.Expanded = true;
                        Novel8rProjectHandler.Instance.AddFile(fiNode.GetFilePath());
                        fiNode.BeginEdit();
                    }
                    else
                    {
                        MessageBox.Show("File already exists in project", DialogHelper.Instance.GetApplicationName());
                    }
                }
                else if (node is FolderNode)
                {
                    ProjectNode pn = getProjectNode(node);
                    var foNode = node as FolderNode;
                    //  DirectoryInfo dir = pNode.FolderObject;
                    string dirName = foNode.FullPath;
                    var sb = new StringBuilder(dirName);
                    string newReldirname = sb.Replace(pn.Name, "", 0, pn.Name.Length).ToString();
                 //   string newRelFilename = newReldirname + @"\" + typeSpecificFileName;
                    string newdirname = pn.FolderObject + newReldirname;

                    var file = new FileInfo(newdirname + @"\" + typeSpecificFileName);
                    //   FileInfo file = new FileInfo(dir.ToString() + @"\" + typeSpecificFileName);
                    if (!file.Exists)
                    {
                        FileStream fs = file.Create();
                        fs.Close();

                        var fiNode = new FileNode(file);
                        node.Nodes.Add(fiNode);
                        node.Expanded = true;
                        Novel8rProjectHandler.Instance.AddFile(newdirname + @"\" + typeSpecificFileName);
                        fiNode.BeginEdit();
                    }
                    else
                    {
                        MessageBox.Show("File already exists in folder", DialogHelper.Instance.GetApplicationName());
                    }
                }
            }
        }

        private void tsbRemoveFile_Click(object sender, EventArgs e)
        {
            if (_view.tvwProject.SelectedNodes.Count > 0)
            {
                UltraTreeNode node = _view.tvwProject.SelectedNodes[0];
                var fileNode = node as FileNode;
                if (fileNode != null)
                {                    
                    Novel8rProjectHandler.Instance.RemoveFile(fileNode.GetFilePath());
                    _view.tvwProject.SelectedNodes[0].Remove();
                }
                else
                {
                    var folderNode = node as FolderNode;
                    if (folderNode != null)
                    {
                        Novel8rProjectHandler.Instance.RemoveFolder(folderNode.GetFolderPath());
                        _view.tvwProject.SelectedNodes[0].Remove();
                    }
                }
            }
            handleProjectModifiedStatus();
        }

        private void tsbNewProject_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = DialogFactory.Instance.GetCreateProjectDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Novel8rProject pro = Novel8rProjectHandler.Instance.NewProject(sfd.FileName);
                if (pro != null)
                {
                    presentFiles(pro);
                }
                MainPresenter.Instance.OpenPane(MainPresenter.DockedPaneKeys.Files);
                handleProjectModifiedStatus();
            }
        }

        private void tsbOpenProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = DialogFactory.Instance.GetOpenProjectDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenProject(ofd.FileName);
            }
        }

        public void OpenProject(string fileName)
        {
            Novel8rProject pro = Novel8rProjectHandler.Instance.LoadProject(fileName);
            if (pro != null)
            {
                presentFiles(pro);

                MainPresenter.Instance.OpenPane(MainPresenter.DockedPaneKeys.Files);
                handleProjectModifiedStatus();
                updateRecentProject(fileName);
            }
            else
            {
                MessageBox.Show(string.Format("Project could not be loaded: \n{0}", fileName), DialogHelper.Instance.GetApplicationName());
            }
        }

        private void updateRecentProject(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            var proj = new Sql8rProjectFile(fileName, filePath);
        	var io = new SettingsIO();
            IList<Sql8rProjectFile> projs = io.LoadRecentProjects();
            if (!projs.Contains(proj))  
            {
                projs.Add(proj);
                io.SaveRecentProjects(projs);
            }
        }

        private void tsbSaveProject_Click(object sender, EventArgs e)
        {
            Novel8rProjectHandler.Instance.SaveProject();
            handleProjectModifiedStatus();
        }

        private void tvwProject_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UltraTreeNode node = _view.tvwProject.GetNodeFromPoint(new Point(e.X, e.Y));
            var fNode = node as FileNode;
            if (fNode != null)
            {
                openFileFromProject(fNode);
            }
        }

        private void presentFiles(Novel8rProject pro)
        {
            _view.tvwProject.BeginUpdate();
            _view.tvwProject.Nodes.Clear();

            var pNode = new ProjectNode(pro.Name, pro.BaseDir);
            _view.tvwProject.Nodes.Add(pNode);

            foreach (Sql8rProjectFile file in pro.ProjectFiles)
            {
                string relName = file.Path.Replace(pNode.FolderObject.FullName, "");
                if (relName.StartsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    relName = relName.Substring(1, relName.Length - 1);
                }
                UltraTreeNode parentNode = pNode;
                
                if (relName.Contains(Path.DirectorySeparatorChar.ToString()))
                {
                    IList<string> partlist = relName.Split(new[] {Path.DirectorySeparatorChar},
                                                           StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < partlist.Count - 1; i++)
                    {
                        string s = Path.Combine(pNode.FolderObject.FullName, partlist[i]);
                        var di = new DirectoryInfo(s);

                        var foNode = new FolderNode(di);
                        if (!parentNode.Nodes.Exists(foNode.Key))
                        {
                            parentNode.Nodes.Add(foNode);
                            parentNode = foNode;
                            
                        }
                        else
                        {
                            parentNode = parentNode.Nodes[foNode.Key];
                        }
                    }
                }
                
                var fiNode = new FileNode( new FileInfo(file.Path) );
                parentNode.Nodes.Add(fiNode);
            }

            foreach (Sql8rProjectFolder folder in pro.ProjectFolders)
            {
                string relName = folder.Path.Replace(pNode.FolderObject.FullName, "");
                if (relName.StartsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    relName = relName.Substring(1, relName.Length - 1);
                }
                UltraTreeNode parentNode = pNode;

                if (relName.Contains(Path.DirectorySeparatorChar.ToString()))
                {
                    IList<string> partlist = relName.Split(new[] { Path.DirectorySeparatorChar },
                                                           StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < partlist.Count - 1; i++)
                    {
                        string s = Path.Combine(pNode.FolderObject.FullName, partlist[i]);
                        var di = new DirectoryInfo(s);

                        var foNode = new FolderNode(di);
                        if (!parentNode.Nodes.Exists(foNode.Key))
                        {
                            parentNode.Nodes.Add(foNode);
                            parentNode = foNode;
                        }
                        else
                        {
                            parentNode = parentNode.Nodes[foNode.Key];
                        }
                    }
                }

                var parentFolderNode = new FolderNode(new DirectoryInfo(folder.Path));
                if (!parentNode.Nodes.Exists(parentFolderNode.Key))
                {
                    parentNode.Nodes.Add(parentFolderNode);
                }
            }

            pNode.Expanded = true;
            pNode.Selected = true;
            _view.tvwProject.EndUpdate();
        }

        public void OpenFileFromProject(string filePath)
        {
            if (filePath.ToUpperInvariant().EndsWith(".RTF", StringComparison.OrdinalIgnoreCase))
            {
                var p = (EditorRtfPresenter)MainPresenter.Instance.OpenMdiTab(MainPresenter.MdiTabKeys.RtfEditor);
                p.OpenFile(filePath);
            }
            else
            {
                var p = (EditorRtfPresenter)MainPresenter.Instance.OpenMdiTab(MainPresenter.MdiTabKeys.RtfEditor);
                p.OpenFile(filePath);
            }
        }

        private void openFileFromProject(FileNode fiNode)
        {
            if (fiNode != null)
            {
                string filePath = fiNode.GetFilePath();

                OpenFileFromProject(filePath);   
            }
        }

    }
}