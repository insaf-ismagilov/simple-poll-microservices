using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Editor.Application.Extensions;
using SimplePoll.Editor.Application.Models.DataAccess;
using SimplePoll.Editor.Application.Queries;
using SimplePoll.Editor.Constants;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Application.Handlers
{
	public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, Poll>
	{
		private readonly IDatabaseRepository _databaseRepository;

		public GetPollByIdQueryHandler(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}
		
		public async Task<Poll> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
		{
			var records = await _databaseRepository.GetCollectionAsync<PollRecord>(Functions.Poll.GetById,
				DbParameterInfoHelper.Create(nameof(request.Id), request.Id));

			return records.ToPoll().FirstOrDefault();
		}
	}
}