using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace SimplePoll.Identity.WebApi.Configurations
{
	internal static class SwaggerConfiguration
	{
		internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "SimplePoll.Identity.WebApi API",
					Version = "v1"
				});

				c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				c.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			return services;
		}
	}
}