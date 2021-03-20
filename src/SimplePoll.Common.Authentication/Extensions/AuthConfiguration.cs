using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimplePoll.Common.Authentication.Options;

namespace SimplePoll.Common.Authentication.Extensions
{
	public static class AuthConfiguration
	{
		public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
			services.Configure<JwtSettings>(jwtSettingsSection);

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
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettingsSection[nameof(JwtSettings.Issuer)],
						ValidAudience = jwtSettingsSection[nameof(JwtSettings.Audience)],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsSection[nameof(JwtSettings.SigningKey)]))
					};
				});
			
			return services;
		}
	}
}