using FluentValidation;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Application.Commands;

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
            RuleForEach(x => x.Options).SetValidator(new PollOptionDtoValidator());
        }
    }

    public class CreatePollCommandValidator : SavePollCommandValidator<CreatePollCommand, ServiceResponse<PollDto>> { }

    public class UpdatePollCommandValidator : SavePollCommandValidator<UpdatePollCommand, ServiceResponse<PollDto>> { }

    public class PollOptionDtoValidator : AbstractValidator<PollOptionDto>
    {
        public PollOptionDtoValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}