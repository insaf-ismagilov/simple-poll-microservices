using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimplePoll.Identity.Application.Options;

namespace SimplePoll.Identity.WebApi.Configurations
{
	internal static class AuthConfiguration
	{
		public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettingsSection = configuration.GetSection(nameof(IdentityJwtSettings));
			services.Configure<IdentityJwtSettings>(jwtSettingsSection);

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettingsSection[nameof(IdentityJwtSettings.Issuer)],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsSection[nameof(IdentityJwtSettings.SigningKey)]))
					};
				});
			
			return services;
		}
	}
}