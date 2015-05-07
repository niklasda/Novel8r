using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Novel8r.Logic.DomainModel.Project;

namespace Novel8r.Logic.Connection
{
    public class SettingsIO
    {
        public IList<ServerConnectionSettings> LoadKnownServers()
        {
            string fileName = getKnownServersSettingsFile();
            return loadStuff<ServerConnectionSettings>(fileName);
        }

        public IList<Sql8rProjectFile> LoadRecentProjects()
        {
            string fileName = getRecentProjectsSettingsFile();
            return loadStuff<Sql8rProjectFile>(fileName);
        }

        private IList<T> loadStuff<T>(string fileName)
        {
            IList<T> settings = new List<T>();
            if (File.Exists(fileName))
            {
                Stream stream = File.Open(fileName, FileMode.Open);
                var formatter = new XmlSerializer(typeof(List<T>));

                settings = (IList<T>)formatter.Deserialize(stream);
                stream.Close();
            }
            return settings;
        }

        public void SaveKnownServers(IList<ServerConnectionSettings> settings)
        {
            string fileName = getKnownServersSettingsFile();
            saveStuff(settings, fileName);
        }

        public void SaveRecentProjects(IList<Sql8rProjectFile> settings)
        {
            string fileName = getRecentProjectsSettingsFile();
            saveStuff(settings, fileName);
        }

        private void saveStuff<T>(IList<T> settings, string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            var formatter = new XmlSerializer(typeof(List<T>));

            formatter.Serialize(stream, settings);
            stream.Close();

            File.Encrypt(fileName);
        }

        public void ResetKnownServers()
        {
            string fileName = getKnownServersSettingsFile();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private string getKnownServersSettingsFile()
        {
            string isPath5 = getSettingsPath();

            string isPath8 = Path.Combine(isPath5, "knownServers.xml");
            return isPath8;
        }

        private string getRecentProjectsSettingsFile()
        {
            string isPath5 = getSettingsPath();

            string isPath8 = Path.Combine(isPath5, "recentProjects.xml");
            return isPath8;
        }

        private string getSettingsPath()
        {
            string isPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string isPath2 = Application.CompanyName;
            string isPath3 = Path.Combine(isPath, isPath2);
            string isPath4 = Application.ProductName;
            string isPath5 = Path.Combine(isPath3, isPath4);

            var di = new DirectoryInfo(isPath5);
            if (!di.Exists)
            {
                di.Create();
            }
            return isPath5;
        }

        public string GetDockSettingsFile()
        {
            string isPath5 = getSettingsPath();

            string isPath8 = Path.Combine(isPath5, "dockManager.xml");
            return isPath8;
        }
    }
}