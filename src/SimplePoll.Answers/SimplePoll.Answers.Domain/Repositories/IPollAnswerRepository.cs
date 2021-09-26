using System.Threading.Tasks;
using SimplePoll.Answers.Domain.Entities;

namespace SimplePoll.Answers.Domain.Repositories
{
    public interface IPollAnswerRepository
    {
        Task<PollAnswer> GetByIdAsync(int id);
        Task<PollAnswer> CreateAsync(PollAnswer pollAnswer);
    }
}