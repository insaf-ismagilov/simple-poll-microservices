using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Answers.Application.Profiles;

namespace SimplePoll.Answers.Configurations
{
    internal static class AutoMapperConfiguration
    {
        internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var profiles = new List<Profile>
            {
                new PollAnswerProfile()
            };

            var mapper = new MapperConfiguration(c => c.AddProfiles(profiles)).CreateMapper();

            services.AddSingleton(_ => mapper);

            return services;
        }
    }
}