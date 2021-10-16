using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Domain.Repositories
{
    public interface IPollRepository
    {
        Task<int> CreateAsync(PollDto poll);
        Task<int?> UpdateAsync(PollDto poll);
        Task<ICollection<Poll>> GetAllAsync();
        Task<Poll> GetByIdAsync(int id);
    }
}