using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Identity.Profiles;

namespace SimplePoll.Identity.Configurations
{
	internal static class AutoMapperCOnfiguration
	{
		internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
		{
			var profiles = new List<Profile>
			{
				new UserProfile()
			};

			var mapper = new MapperConfiguration(c => c.AddProfiles(profiles)).CreateMapper();

			services.AddSingleton(_ => mapper);

			return services;
		}
	}
}