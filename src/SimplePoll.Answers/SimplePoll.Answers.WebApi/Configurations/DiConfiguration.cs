using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Answers.Application.Handlers;
using SimplePoll.Answers.Domain.Repositories;
using SimplePoll.Answers.Infrastructure.DbContext.Repositories;
using SimplePoll.Common.Validation;

namespace SimplePoll.Answers.Configurations
{
    internal static class DiConfiguration
    {
        internal static IServiceCollection ConfigureDi(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(GetPollAnswerByIdQueryHandler)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            services.AddScoped<IPollAnswerRepository, PollAnswerRepository>();

            return services;
        }
    }
}