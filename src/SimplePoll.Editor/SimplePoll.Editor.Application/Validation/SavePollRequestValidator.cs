using FluentValidation;
using SimplePoll.Editor.Application.Models.Requests;

namespace SimplePoll.Editor.Application.Validation
{
	public abstract class SavePollRequestValidator<T> : AbstractValidator<T> where T : SavePollRequest
	{
		public SavePollRequestValidator()
		{
			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.Status).IsInEnum();
			RuleFor(x => x.Type).IsInEnum();
			RuleFor(x => x.Options).NotEmpty();
			RuleForEach(x => x.Options).SetValidator(new PollOptionDtoValidator());
		}
	}

	public class CreatePollRequestValidator : SavePollRequestValidator<CreatePollRequest>
	{
	}

	public class UpdatePollRequestValidator : SavePollRequestValidator<UpdatePollRequest>
	{
	}
}