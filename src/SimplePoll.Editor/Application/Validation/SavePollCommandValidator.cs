using FluentValidation;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Models;

namespace SimplePoll.Editor.Application.Validation
{
	public abstract class SavePollCommandValidator<T, Y> : AbstractValidator<T> where T : SavePollCommand<Y>
	{
		public SavePollCommandValidator()
		{
			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.Status).IsInEnum();
			RuleFor(x => x.Type).IsInEnum();
			RuleFor(x => x.Options).NotEmpty();
			RuleForEach(x => x.Options).SetValidator(new SavePollOptionRequestValidator());
		}
	}

	public class CreatePollCommandValidator : SavePollCommandValidator<CreatePollCommand, int>
	{
	}

	public class UpdatePollCommandValidator : SavePollCommandValidator<UpdatePollCommand, int?>
	{
	}

	public class SavePollOptionRequestValidator : AbstractValidator<PollOptionDto>
	{
		public SavePollOptionRequestValidator()
		{
			RuleFor(x => x.Text).NotEmpty();
			RuleFor(x => x.Value).NotEmpty();
		}
	}
}