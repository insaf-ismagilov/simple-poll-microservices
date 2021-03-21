using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Editor.Application.Profiles;

namespace SimplePoll.Editor.Configurations
{
	internal static class AutoMapperConfiguration
	{
		internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
		{
			var profiles = new List<Profile>
			{
				new PollProfile()
			};

			var mapper = new MapperConfiguration(c => c.AddProfiles(profiles)).CreateMapper();

			services.AddSingleton(_ => mapper);

			return services;
		}
	}
}