namespace SimplePoll.Common.DataAccess.Utils
{
	public static class DbParameterInfoHelper
	{
		public static DbParameterInfo Create(string name, object value)
		{
			return new(name, value);
		}

		public static DbParameterInfo CreateJsonb(string name, object value)
		{
			return new(name, new JsonbParameter(value));
		}
	}
}