using System.Threading.Tasks;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Application.Models;
using SimplePoll.Identity.Application.Models.Requests;

namespace SimplePoll.Identity.Application.Services
{
	public interface IIdentityService
	{
		Task<ServiceResponse<AuthenticationResult>> SignInAsync(SignInRequest request);
		Task<ServiceResponse> SignUpAsync(SignUpRequest request);
	}
}