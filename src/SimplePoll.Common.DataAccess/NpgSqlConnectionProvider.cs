using System.Data;
using Dapper;
using Npgsql;

namespace SimplePoll.Common.DataAccess
{
	public class NpgSqlConnectionProvider : IDatabaseConnectionProvider
	{
		public NpgSqlConnectionProvider(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public string ConnectionString { get; }

		public IDbConnection Create()
		{
			DefaultTypeMap.MatchNamesWithUnderscores = true;
			return new NpgsqlConnection(ConnectionString);
		}
	}
}