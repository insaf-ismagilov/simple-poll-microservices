using System.Threading.Tasks;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Application.Models.Requests;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Services
{
    public interface IPollAnswerService
    {
        Task<ServiceResponse<PollAnswerDto>> GetByIdAsync(int id);
        Task<ServiceResponse<PollAnswerDto>> CreateAsync(CreatePollAnswerRequest request);
    }
}