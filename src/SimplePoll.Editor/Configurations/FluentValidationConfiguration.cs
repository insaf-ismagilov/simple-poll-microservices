using Microsoft.Extensions.DependencyInjection;

namespace SimplePoll.Editor.Configurations
{
	internal static class FluentValidationConfiguration
	{
		public static IServiceCollection ConfigureValidators(this IServiceCollection services)
		{

			return services;
		}
	}
}