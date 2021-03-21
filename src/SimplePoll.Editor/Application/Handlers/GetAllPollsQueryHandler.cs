using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Editor.Application.Extensions;
using SimplePoll.Editor.Application.Models.DataAccess;
using SimplePoll.Editor.Application.Queries;
using SimplePoll.Editor.Constants;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Application.Handlers
{
	public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, ICollection<Poll>>
	{
		private readonly IDatabaseRepository _databaseRepository;

		public GetAllPollsQueryHandler(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}
		
		public async Task<ICollection<Poll>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
		{
			var records = await _databaseRepository.GetCollectionAsync<PollRecord>(Functions.Poll.GetAll);
			
			return records.ToPoll().ToList();
		}
	}
}