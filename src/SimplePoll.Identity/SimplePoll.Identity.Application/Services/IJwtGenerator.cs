using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Application.Services
{
	public interface IJwtGenerator
	{
		string GetToken(User user);
	}
}