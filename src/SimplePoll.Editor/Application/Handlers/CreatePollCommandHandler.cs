using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Editor.Application.Commands;

namespace SimplePoll.Editor.Application.Handlers
{
	public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, int>
	{
		private readonly IDatabaseRepository _databaseRepository;

		public CreatePollCommandHandler(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}
		
		public Task<int> Handle(CreatePollCommand request, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}