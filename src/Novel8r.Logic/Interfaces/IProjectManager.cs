using Novel8r.Logic.DomainModel.Project;

namespace Novel8r.Logic.Interfaces
{
    public interface IProjectManager
    {
        string ProjectName { get; set;}
        bool IsModified();
        Novel8rProject NewProject(string fileName);
        Novel8rProject LoadProject(string fileName);
        Novel8rProject ImportProject(Novel8rProject oldProject);
        void SaveProject();
        void SaveProjectAs(string fileName);
        void AddFile(string filePath);
        void AddFolder(string folderPath);
        void RenameFile(string oldFilePath, string newFilePath);
        void RenameFolder(string oldFolderPath, string newFolderPath);
        void RemoveFile(string filePath);
        void RemoveFolder(string folderPath);
    }
}
