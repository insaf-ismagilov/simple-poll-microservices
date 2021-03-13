using FluentValidation;
using SimplePoll.Identity.Application.Models.Requests;

namespace SimplePoll.Identity.Application.Validation
{
	public class SignInRequestValidator : AbstractValidator<SignInRequest>
	{
		public SignInRequestValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}