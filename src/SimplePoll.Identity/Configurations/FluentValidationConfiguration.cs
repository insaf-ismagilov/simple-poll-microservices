using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Identity.Application.Models.Requests;
using SimplePoll.Identity.Application.Queries;
using SimplePoll.Identity.Application.Validation;

namespace SimplePoll.Identity.Configurations
{
	public static class FluentValidationConfiguration
	{
		public static IServiceCollection ConfigureValidators(this IServiceCollection services)
		{
			services.AddTransient<IValidator<GetUserByEmailQuery>, GetUserByEmailQueryValidator>();
			services.AddTransient<IValidator<SignInRequest>, SignInRequestValidator>();

			return services;
		}
	}
}