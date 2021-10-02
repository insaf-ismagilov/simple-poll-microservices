using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Application.Models;

namespace SimplePoll.Identity.Application.Commands
{
    public class SignInCommand : IRequest<ServiceResponse<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}