using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Models;
using SimplePoll.Identity.Services;

namespace SimplePoll.Identity.Configurations
{
	public static class DiConfiguration
	{
		public static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			
			services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddTransient<IJwtGenerator, JwtGenerator>();
			services.AddTransient<IIdentityService, IdentityService>();
			
			return services;
		}
	}
}