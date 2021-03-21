﻿using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.Validation;
using SimplePoll.Identity.Application.Services;
using SimplePoll.Identity.Application.Validation;
using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Configurations
{
	internal static class DiConfiguration
	{
		internal static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
			
			services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddTransient<IJwtGenerator, JwtGenerator>();
			services.AddTransient<IIdentityService, IdentityService>();
			
			return services;
		}
	}
}