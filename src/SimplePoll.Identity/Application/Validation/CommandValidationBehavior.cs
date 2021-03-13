using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace SimplePoll.Identity.Application.Validation
{
	public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IValidator<TRequest> _validator;

		public CommandValidationBehavior(IValidator<TRequest> validator)
		{
			_validator = validator;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			if (_validator == null)
				return next();
			
			_validator.ValidateAndThrow(request);

			return next();
		}
	}
}