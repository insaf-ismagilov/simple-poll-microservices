using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Identity.Application.Models.DataAccess;
using SimplePoll.Identity.Application.Queries;
using SimplePoll.Identity.Constants;
using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Application.Handlers
{
	public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
	{
		private readonly IMapper _mapper;
		private readonly IDatabaseRepository _databaseRepository;

		public GetUserByEmailQueryHandler(
			IMapper mapper,
			IDatabaseRepository databaseRepository)
		{
			_mapper = mapper;
			_databaseRepository = databaseRepository;
		}
		
		public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
		{
			var userRecord = await _databaseRepository.GetAsync<UserRecord>(Functions.UserRepository.GetByEmail,
				DbParameterInfoHelper.Create(nameof(request.Email), request.Email));

			return _mapper.Map<User>(userRecord);
		}
	}
}