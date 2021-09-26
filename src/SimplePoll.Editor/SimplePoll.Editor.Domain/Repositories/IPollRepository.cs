using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePoll.Editor.Domain.Entities;
using SimplePoll.Editor.Domain.Models;

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