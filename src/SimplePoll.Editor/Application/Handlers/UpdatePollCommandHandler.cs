using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Constants;

namespace SimplePoll.Editor.Application.Handlers
{
	public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, int?>
	{
		private readonly IDatabaseRepository _databaseRepository;

		public UpdatePollCommandHandler(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}
		
		public Task<int?> Handle(UpdatePollCommand request, CancellationToken cancellationToken)
		{
			return _databaseRepository.GetAsync<int?>(Functions.Poll.Update,
				DbParameterInfoHelper.Create(nameof(request.Id), request.Id),
				DbParameterInfoHelper.Create(nameof(request.Title), request.Title),
				DbParameterInfoHelper.Create(nameof(request.Status), request.Status),
				DbParameterInfoHelper.Create(nameof(request.Type), request.Type),
				DbParameterInfoHelper.CreateJsonb(nameof(request.Options), request.Options));
		}
	}
}