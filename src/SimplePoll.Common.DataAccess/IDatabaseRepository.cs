using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePoll.Common.DataAccess.Utils;

namespace SimplePoll.Common.DataAccess
{
	public interface IDatabaseRepository
	{
		Task<T> GetAsync<T>(string functionName, params DbParameterInfo[] paramaters);
		Task<IEnumerable<T>> GetCollectionAsync<T>(string functionName, params DbParameterInfo[] paramaters);
		Task<int> ExecuteAsync(string functionName, params DbParameterInfo[] paramaters);
	}
}