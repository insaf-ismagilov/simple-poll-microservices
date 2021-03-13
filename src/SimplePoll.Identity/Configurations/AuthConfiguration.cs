using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Identity.Options;

namespace SimplePoll.Identity.Configurations
{
	internal static class AuthConfiguration
	{
		internal static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
			services.Configure<JwtSettings>(jwtSettingsSection);
			
			return services;
		}
	}
}