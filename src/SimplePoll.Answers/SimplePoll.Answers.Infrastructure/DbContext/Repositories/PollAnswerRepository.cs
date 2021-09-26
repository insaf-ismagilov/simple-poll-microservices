using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Answers.Domain.Repositories;

namespace SimplePoll.Answers.Infrastructure.DbContext.Repositories
{
    public class PollAnswerRepository : IPollAnswerRepository
    {
        private readonly AnswersDbContext _dbContext;

        public PollAnswerRepository(AnswersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PollAnswer> GetByIdAsync(int id)
        {
            return _dbContext.Set<PollAnswer>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PollAnswer> CreateAsync(PollAnswer pollAnswer)
        {
            var entityEntry = await _dbContext.Set<PollAnswer>().AddAsync(pollAnswer);
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}