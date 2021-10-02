using System.Threading.Tasks;
using AutoMapper;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Identity.Domain.Entities;
using SimplePoll.Identity.Domain.Repositories;
using SimplePoll.Identity.Infrastructure.Database.Constants;
using SimplePoll.Identity.Infrastructure.Database.Models;

namespace SimplePoll.Identity.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;

        public UserRepository(
            IDatabaseRepository databaseRepository,
            IMapper mapper)
        {
            _databaseRepository = databaseRepository;
            _mapper = mapper;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var userRecord = await _databaseRepository.GetAsync<UserRecord>(Functions.UserRepository.GetByEmail,
                DbParameterInfoHelper.Create(nameof(email), email));

            return _mapper.Map<User>(userRecord);
        }
    }
}