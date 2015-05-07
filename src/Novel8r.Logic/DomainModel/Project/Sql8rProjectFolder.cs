using System;
using System.Xml.Serialization;

namespace Novel8r.Logic.DomainModel.Project
{
    [XmlRoot("Sql8rProjectFolder")]
    public class Sql8rProjectFolder : IEquatable<Sql8rProjectFolder>
    {
        private string _name;
        private string _fullPath;
 
        public Sql8rProjectFolder()
        {            
        }

        public Sql8rProjectFolder(string name, string fullPath)
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

        public bool Equals(Sql8rProjectFolder other)
        {
            return Path.Equals(other.Path);
        }
    }
}
