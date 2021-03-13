using MediatR;
using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Models;

namespace SimplePoll.Identity.Queries
{
	public class GetUserByEmailQuery : IRequest<User>
	{
		public string Email { get; set; }
	}
}