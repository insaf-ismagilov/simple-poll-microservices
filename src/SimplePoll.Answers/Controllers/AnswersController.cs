using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Answers.Application.Models.Requests;
using SimplePoll.Answers.Application.Services;
using SimplePoll.Common.Api;

namespace SimplePoll.Answers.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class AnswersController : ApiControllerBase
    {
        private readonly IPollAnswerService _pollAnswerService;

        public AnswersController(IPollAnswerService pollAnswerService)
        {
            _pollAnswerService = pollAnswerService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pollAnswerService.GetByIdAsync(id);

            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreatePollAnswerRequest request)
        {
            var result = await _pollAnswerService.CreateAsync(request);

            return CreateResponse(result);
        }
    }
}