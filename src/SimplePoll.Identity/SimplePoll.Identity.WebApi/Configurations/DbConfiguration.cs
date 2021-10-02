using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.DataAccess;

namespace SimplePoll.Identity.WebApi.Configurations
{
	internal static class DbConfiguration
	{
		internal static IServiceCollection ConfigureDb(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("SimplePoll");

			var connectionProvider = new NpgSqlConnectionProvider(connectionString);

			services.AddSingleton<IDatabaseConnectionProvider>(_ => connectionProvider);
			services.AddTransient<IDatabaseRepository, DatabaseRepository>();

			return services;
		}
	}
}