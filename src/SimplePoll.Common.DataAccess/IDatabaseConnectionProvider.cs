using System.Data;

namespace SimplePoll.Common.DataAccess
{
	public interface IDatabaseConnectionProvider
	{
		string ConnectionString { get; }
		IDbConnection Create();
	}
}