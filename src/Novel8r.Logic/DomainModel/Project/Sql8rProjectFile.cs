using System;
using System.Xml.Serialization;

namespace Novel8r.Logic.DomainModel.Project
{
    [XmlRoot("Sql8rProjectFile")]
    public class Sql8rProjectFile : IEquatable<Sql8rProjectFile>
    {
        private string _name;
        private string _fullPath;
         
        public Sql8rProjectFile()
        {            
        }

        public Sql8rProjectFile(string name, string fullPath)
        {
            _name = name;
            _fullPath = fullPath;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Path
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }

        public bool Equals(Sql8rProjectFile other)
        {
            return Path.Equals(other.Path);
        }
    }
}
