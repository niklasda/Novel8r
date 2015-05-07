using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Novel8r.Logic.DomainModel.Project
{
	//[CLSCompliant(false)]
	[XmlRoot("Novel8rProject")]
    public class Novel8rProject
    {                
        private DirectoryInfo _baseDir;
   //     private readonly IList<FileInfo> _files = new List<FileInfo>();        
        private string _name;
        private string _fullFileName;
        private bool _isModified;
        
        [XmlArray("Files"), XmlArrayItem("File", typeof(Sql8rProjectFile))]        
        public List<Sql8rProjectFile> ProjectFiles = new List<Sql8rProjectFile>();
        
        [XmlArray("Folders"), XmlArrayItem("Folder", typeof(Sql8rProjectFolder))]
        public List<Sql8rProjectFolder> ProjectFolders = new List<Sql8rProjectFolder>(); 
        

        public Novel8rProject()
        {            
        }

        public Novel8rProject(string projectName, DirectoryInfo baseDir)
        {
            _name = projectName;
            _baseDir = baseDir;            
            _isModified = false;
        }

        //public IList<FileInfo> Files
        //{
        //    get { return _files; }
        //}
                
        public string Name
        {
            get { return _name; }
            set
            {
                _isModified = true;
                _name = value;
            }
        }
        
        public string Path
        {
            get { return _baseDir.FullName; }
            //set
            //{
            //    _isModified = true;
            //    _fullPath = value;
            //}
        }

        [XmlIgnore]
        public DirectoryInfo BaseDir
        {
            get { return _baseDir; }
            set { _baseDir = value; }
        }

        [XmlIgnore]
        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; }
        }

        [XmlIgnore]
        public string FullFileName
        {
            get { return _fullFileName; }
            set { _fullFileName = value;  }
        }

		//[XmlIgnore]
		//public List<Sql8rProjectFile> ProjectFiles
		//{
		//    get { return projectFiles; }
		//    set { projectFiles = value; }
		//}

		//[XmlIgnore]
		//public List<Sql8rProjectFolder> ProjectFolders
		//{
		//    get { return projectFolders; }
		//    set { projectFolders = value; }
		//}
    }
}