using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimplePoll.Common.DataAccess;
using SimplePoll.Common.DataAccess.Utils;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Domain.Entities;
using SimplePoll.Editor.Domain.Repositories;
using SimplePoll.Editor.Infrastructure.Database.Constants;
using SimplePoll.Editor.Infrastructure.Database.Extensions;
using SimplePoll.Editor.Infrastructure.Database.Models;

namespace SimplePoll.Editor.Infrastructure.Database.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public PollRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public Task<int> CreateAsync(PollDto poll)
        {
            return _databaseRepository.GetAsync<int>(Functions.Poll.Create,
                DbParameterInfoHelper.Create(nameof(poll.Title), poll.Title),
                DbParameterInfoHelper.Create(nameof(poll.Status), poll.Status),
                DbParameterInfoHelper.Create(nameof(poll.Type), poll.Type),
                DbParameterInfoHelper.CreateJsonb(nameof(poll.Options), poll.Options));
        }

        public Task<int?> UpdateAsync(PollDto poll)
        {
            return _databaseRepository.GetAsync<int?>(Functions.Poll.Update,
                DbParameterInfoHelper.Create(nameof(poll.Id), poll.Id),
                DbParameterInfoHelper.Create(nameof(poll.Title), poll.Title),
                DbParameterInfoHelper.Create(nameof(poll.Status), poll.Status),
                DbParameterInfoHelper.Create(nameof(poll.Type), poll.Type),
                DbParameterInfoHelper.CreateJsonb(nameof(poll.Options), poll.Options));
        }

        public async Task<ICollection<Poll>> GetAllAsync()
        {
            var records = await _databaseRepository.GetCollectionAsync<PollRecord>(Functions.Poll.GetAll);

            return records.ToPoll().ToList();
        }

        public async Task<Poll> GetByIdAsync(int id)
        {
            var records = await _databaseRepository.GetCollectionAsync<PollRecord>(Functions.Poll.GetById,
                DbParameterInfoHelper.Create(nameof(id), id));

            return records.ToPoll().FirstOrDefault();
        }
    }
}