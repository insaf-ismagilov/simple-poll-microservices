using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimplePoll.Answers.Application.Queries;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Answers.Infrastructure.DbContext;

namespace SimplePoll.Answers.Application.Handlers
{
    public class GetPollAnswerByIdQueryHandler : IRequestHandler<GetPollAnswerByIdQuery, PollAnswer>
    {
        private readonly AnswersDbContext _dbContext;

        public GetPollAnswerByIdQueryHandler(AnswersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PollAnswer> Handle(GetPollAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Set<PollAnswer>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        }
    }
}