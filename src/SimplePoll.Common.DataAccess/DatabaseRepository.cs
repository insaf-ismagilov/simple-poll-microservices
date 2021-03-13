using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SimplePoll.Common.DataAccess.Utils;

namespace SimplePoll.Common.DataAccess
{
	public class DatabaseRepository : IDatabaseRepository
	{
		private readonly IDatabaseConnectionProvider _databaseConnectionProvider;

		public DatabaseRepository(IDatabaseConnectionProvider databaseConnectionProvider)
		{
			_databaseConnectionProvider = databaseConnectionProvider;
		}

		public async Task<T> GetAsync<T>(string functionName, params DbParameterInfo[] paramaters)
		{
			using var connection = _databaseConnectionProvider.Create();
			connection.Open();

			return await connection.QueryFirstOrDefaultAsync<T>(functionName, GetParameters(paramaters), null, null, CommandType.StoredProcedure);
		}

		public async Task<IEnumerable<T>> GetCollectionAsync<T>(string functionName, params DbParameterInfo[] paramaters)
		{
			using var connection = _databaseConnectionProvider.Create();
			connection.Open();

			return await connection.QueryAsync<T>(functionName, GetParameters(paramaters), null, null, CommandType.StoredProcedure);
		}

		public async Task<int> ExecuteAsync(string functionName, params DbParameterInfo[] paramaters)
		{
			using var connection = _databaseConnectionProvider.Create();
			connection.Open();

			return await connection.ExecuteAsync(functionName, GetParameters(paramaters), null, null, CommandType.StoredProcedure);
		}

		private static object GetParameters(params DbParameterInfo[] parameters)
		{
			var param = new DynamicParameters();

			foreach (var parameter in parameters) param.Add(parameter.Name, parameter.Value);

			return param;
		}
	}
}