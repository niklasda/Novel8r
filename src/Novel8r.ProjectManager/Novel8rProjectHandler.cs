using System.IO;
using System.Windows.Forms;
using Novel8r.Logic.DomainModel.Project;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using System.Xml.Serialization;

namespace Novel8r.ProjectManager
{
    public class Novel8rProjectHandler : IProjectManager
    {
        private static IProjectManager _instance;
        private Novel8rProject currentProject;

        private Novel8rProjectHandler()
        {
        }

        public static IProjectManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Novel8rProjectHandler();
                }
                return _instance;
            }
        }

        public string ProjectName
        {
            get { return currentProject.Name; }
            set { }
        }

        public bool IsModified()
        {
            if (currentProject != null)
            {
                return currentProject.IsModified;
            }
            return false; 
        }

        public Novel8rProject NewProject(string fileName)
        {            
            var fi = new FileInfo(fileName);
            string fname = fi.Name.Replace(fi.Extension, string.Empty);

            DirectoryInfo baseDir = fi.Directory;
            var projDir = new DirectoryInfo(baseDir + @"\" + fname);

            FileInfo file;
            if (projDir.Exists)
            {
                MessageBox.Show("Folder/project already exists!", DialogHelper.Instance.GetApplicationName());
                file = new FileInfo(projDir + @"\" + fi.Name);
                if (file.Exists)
                {
                    return null;
                }
            }
            else
            {
                projDir.Create();
				var chapDir = projDir.CreateSubdirectory("Chapters"); // jR65gUG835
				var resDir = projDir.CreateSubdirectory("Resources");

                var synopsis = File.Create(projDir.FullName + @"\Synopsis.rtf");
                var teaser = File.Create(projDir.FullName + @"\Teaser.rtf");
				var people = File.Create(resDir.FullName + @"\People.rtf");
				var locations = File.Create(resDir.FullName + @"\Locations.rtf");
				var items = File.Create(resDir.FullName + @"\Items.rtf");
				var research = File.Create(resDir.FullName + @"\Research.rtf");
            	
				synopsis.Close();
				people.Close();
				locations.Close();
				items.Close();
				research.Close();

				var ch1 = File.Create(chapDir.FullName + @"\Chapter1.rtf");
            	ch1.Close();


                file = new FileInfo(projDir + @"\" + fi.Name);                
            }            

            var pro = new Novel8rProject(fname, projDir);
            pro.FullFileName = projDir + @"\" + fi.Name;

            currentProject = pro;

			AddFolder(projDir + @"\Chapters");
			AddFolder(projDir + @"\Resources");
			AddFile(projDir + @"\Chapters\Chapter1.rtf");
			AddFile(projDir + @"\Resources\People.rtf");
			AddFile(projDir + @"\Resources\Locations.rtf");
			AddFile(projDir + @"\Resources\Items.rtf");
            AddFile(projDir + @"\Synopsis.rtf");
            AddFile(projDir + @"\Teaser.rtf");


            SaveProjectAsXml(pro.FullFileName);

            return pro;
        }

        public Novel8rProject LoadProject(string fileName)
        {
            var baseFile = new FileInfo(fileName);
            if (baseFile.Exists)
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);

                XmlSerializer xs = new XmlSerializer(typeof(Novel8rProject));
                Novel8rProject pro = (Novel8rProject)xs.Deserialize(fs);
                pro.FullFileName = fileName;

                DirectoryInfo baseDir = baseFile.Directory;
                pro.BaseDir = baseDir;

                foreach (Sql8rProjectFile _file in pro.ProjectFiles)
                {
                  //  var fi = new FileInfo(_file.Path);

                  //  pro.Files.Add(fi);
                }

                fs.Close();

                pro.IsModified = false;
                currentProject = pro;

                return pro;
            }
            return null;
        }

        public Novel8rProject ImportProject(Novel8rProject oldProject)
        {
            currentProject = oldProject;
            return currentProject;
        }

        public void SaveProjectAsXml(string fileName)
        {            
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(Novel8rProject));
            xs.Serialize(fs, currentProject);
            fs.Close();
            currentProject.IsModified = false;
        }

        public void SaveProject()
        {
            if (currentProject != null && !string.IsNullOrEmpty(currentProject.FullFileName))
            {
                if (currentProject.IsModified)
                {
                    SaveProjectAsXml(currentProject.FullFileName);
                }
            }
            else if (currentProject != null)
            {
             //   SaveFileDialog sfd = new SaveFileDialog();
                SaveFileDialog sfd = DialogFactory.Instance.GetSaveProjectDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveProjectAsXml(sfd.FileName);
                }
            }            
        }

        public void SaveProjectAs(string fileName)
        {
            SaveProjectAsXml(fileName);
        }

        public void AddFile(string filePath)
        {
           // var fi = new FileInfo(filePath);
           // currentProject.Files.Add(fi);

            var pf = new Sql8rProjectFile(Path.GetFileName(filePath), filePath);
            currentProject.ProjectFiles.Add(pf);

            currentProject.IsModified = true;
        }

        public void AddFolder(string folderPath)
        {
            var pf = new Sql8rProjectFolder(Path.GetFileName(folderPath), folderPath);
            currentProject.ProjectFolders.Add(pf);

            currentProject.IsModified = true;
        }

        public void RenameFile(string oldFilePath, string newFilePath)
        {
            RemoveFile(oldFilePath);
            AddFile(newFilePath);
        }

        public void RenameFolder(string oldFolderPath, string newFolderPath)
        {
            RemoveFolder(oldFolderPath);
            AddFolder(newFolderPath);
        }

        public void RemoveFile(string filePath)
        {            
            //foreach (FileInfo fi in currentProject.Files)
            //{
            //    if (fi.FullName == filePath)
            //    {
            //        currentProject.Files.Remove(fi);
            //        currentProject.IsModified = true;
            //        break;                    
            //    }
            //}

            foreach (Sql8rProjectFile pf in currentProject.ProjectFiles)
            {
                if (pf.Path == filePath)
                {
                    currentProject.ProjectFiles.Remove(pf);                    
                    File.Delete(pf.Path);
                    break;
                }
            } 
        }

        public void RemoveFolder(string folderPath)
        {
            foreach (Sql8rProjectFolder pf in currentProject.ProjectFolders)
            {
                if (pf.Path == folderPath)
                {
                    currentProject.ProjectFolders.Remove(pf);
                    currentProject.IsModified = true;
                    if (Directory.Exists(pf.Path))
                        Directory.Delete(pf.Path);
                    break;
                }
            }
        }
    }
}