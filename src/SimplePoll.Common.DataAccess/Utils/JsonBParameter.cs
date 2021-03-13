using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;
using NpgsqlTypes;
using static Dapper.SqlMapper;

namespace SimplePoll.Common.DataAccess.Utils
{
	public class JsonbParameter : ICustomQueryParameter
	{
		private readonly string _dataJson;

		public JsonbParameter(object data)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
			_dataJson = data as string ?? JsonConvert.SerializeObject(data, Formatting.Indented, settings);
		}

		public void AddParameter(IDbCommand command, string name)
		{
			var parameter = new NpgsqlParameter
			{
				ParameterName = name,
				Value = _dataJson,
				NpgsqlDbType = NpgsqlDbType.Jsonb
			};
			command.Parameters.Add(parameter);
		}

		public override string ToString()
		{
			return _dataJson;
		}
	}
}