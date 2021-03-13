using FluentValidation;
using SimplePoll.Identity.Application.Queries;

namespace SimplePoll.Identity.Application.Validation
{
	public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
	{
		public GetUserByEmailQueryValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
		}
	}
}