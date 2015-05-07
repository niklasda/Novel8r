namespace Novel8r.Logic.DomainModel.Templates
{
	public class Template
	{
		private readonly string _name;
		private readonly string _sqlTemplate;

		public Template(string name, string template)
		{
			_name = name;
			_sqlTemplate = template;
		}

		public string Name
		{
			get { return _name; }
		}

		public string SqlTemplate
		{
			get { return _sqlTemplate; }
		}
	}
}