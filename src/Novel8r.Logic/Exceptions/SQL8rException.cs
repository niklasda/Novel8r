using System;

namespace Novel8r.Logic.Exceptions
{
	[Serializable]
	public class SQL8rException : Exception
	{
		public SQL8rException(string message)
			: base(message)
		{
		}

		public SQL8rException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}
}
