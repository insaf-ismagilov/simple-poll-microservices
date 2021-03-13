using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Models;

namespace SimplePoll.Identity.Services
{
	public interface IJwtGenerator
	{
		string GetToken(User user);
	}
}