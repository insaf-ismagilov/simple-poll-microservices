using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePoll.Common.Models;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Application.Models.Requests;

namespace SimplePoll.Editor.Application.Services
{
	public interface IPollService
	{
		Task<ServiceResponse<PollDto>> CreateAsync(CreatePollRequest request);
		Task<ServiceResponse<PollDto>> UpdateAsync(UpdatePollRequest request);
		Task<PollDto> GetByIdAsync(int id);
		Task<ICollection<PollDto>> GetAllAsync();
	}
}