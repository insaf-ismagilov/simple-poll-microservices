using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.Validation;
using SimplePoll.Identity.Application.Handlers;
using SimplePoll.Identity.Application.Services;
using SimplePoll.Identity.Domain.Entities;
using SimplePoll.Identity.Domain.Repositories;
using SimplePoll.Identity.Infrastructure.Database.Repositories;

namespace SimplePoll.Identity.WebApi.Configurations
{
    internal static class DiConfiguration
    {
        internal static IServiceCollection ConfigureDi(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(SignInCommandHandler)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}