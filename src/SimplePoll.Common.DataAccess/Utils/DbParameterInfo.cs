using System.Linq;

namespace SimplePoll.Common.DataAccess.Utils
{
	public class DbParameterInfo
	{
		private const string Prefix = "p_";

		public DbParameterInfo(string name, object value)
		{
			Name = Prefix + ToUnderscoreCase(name);
			Value = value;
		}

		public string Name { get; }
		public object Value { get; }

		private static string ToUnderscoreCase(string str)
		{
			return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
		}
	}
}