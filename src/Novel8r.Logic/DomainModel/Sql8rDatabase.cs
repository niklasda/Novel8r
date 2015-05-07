using System.Collections.Generic;

namespace Novel8r.Logic.DomainModel
{
    public class Sql8rDatabase : Sql8rObject
    {
        private readonly string _name;
//        private readonly IDictionary<string, Sql8rAggregate> _aggregates = new Dictionary<string, Sql8rAggregate>();
        private readonly IDictionary<string, Sql8rFunction> _functions = new Dictionary<string, Sql8rFunction>();
        private readonly IDictionary<string, Sql8rProcedure> _procedures = new Dictionary<string, Sql8rProcedure>();
//        private readonly IList<Sql8rTable> _tables = new List<Sql8rTable>();
        private readonly IDictionary<string, Sql8rTrigger> _triggers = new Dictionary<string, Sql8rTrigger>();
//        private readonly IList<Sql8rView> _views = new List<Sql8rView>();

        public Sql8rDatabase(string name, int databaseId)
        {
            _name = name;
            ObjectId = databaseId;
        }

        public string Name
        {
            get { return _name; }
        }

//        public IList<Sql8rView> Views
//        {
//            get { return _views; }
//        }

//        public IList<Sql8rTable> Tables
//        {
//            get { return _tables; }
//        }

        public IDictionary<string, Sql8rProcedure> Procedures
        {
            get { return _procedures; }
        }

        public IDictionary<string, Sql8rFunction> Functions
        {
            get { return _functions; }
        }

        public IDictionary<string, Sql8rTrigger> Triggers
        {
            get { return _triggers; }
        }

//        public IDictionary<string, Sql8rAggregate> Aggregates
//        {
//            get { return _aggregates; }
//        }

    }
}