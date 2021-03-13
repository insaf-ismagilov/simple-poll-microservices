using System.Threading.Tasks;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Models;
using SimplePoll.Identity.Models.Requests;

namespace SimplePoll.Identity.Services
{
	public interface IIdentityService
	{
		Task<ServiceResponse<AuthenticationResult>> SignInAsync(SignInRequest request);
		Task<ServiceResponse> SignUpAsync(SignUpRequest request);
	}
}