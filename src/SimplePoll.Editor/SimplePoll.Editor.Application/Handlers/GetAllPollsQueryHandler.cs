using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Application.Queries;
using SimplePoll.Editor.Domain.Repositories;

namespace SimplePoll.Editor.Application.Handlers
{
	public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, ServiceResponse<ICollection<PollDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IPollRepository _pollRepository;

		public GetAllPollsQueryHandler(
			IMapper mapper,
			IPollRepository pollRepository)
		{
			_mapper = mapper;
			_pollRepository = pollRepository;
		}
		
		public async Task<ServiceResponse<ICollection<PollDto>>> Handle(GetAllPollsQuery query, CancellationToken cancellationToken)
		{
			var polls = await _pollRepository.GetAllAsync();
			
			return ServiceResponse<ICollection<PollDto>>.Success(_mapper.Map<ICollection<PollDto>>(polls));
		}
	}
}