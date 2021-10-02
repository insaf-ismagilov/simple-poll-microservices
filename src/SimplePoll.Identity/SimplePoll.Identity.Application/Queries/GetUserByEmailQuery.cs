using MediatR;
using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Application.Queries
{
	public class GetUserByEmailQuery : IRequest<User>
	{
		public string Email { get; set; }
	}
}