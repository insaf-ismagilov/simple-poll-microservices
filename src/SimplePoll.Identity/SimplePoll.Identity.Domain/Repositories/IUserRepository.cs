using System.Threading.Tasks;
using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
    }
}