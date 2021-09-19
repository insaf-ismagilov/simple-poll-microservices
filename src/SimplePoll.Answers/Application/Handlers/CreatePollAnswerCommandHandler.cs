using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Answers.Application.Commands;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Answers.Infrastructure.DbContext;

namespace SimplePoll.Answers.Application.Handlers
{
    public class CreatePollAnswerCommandHandler : IRequestHandler<CreatePollAnswerCommand, int>
    {
        private readonly AnswersDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePollAnswerCommandHandler(
            AnswersDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreatePollAnswerCommand command, CancellationToken cancellationToken)
        {
            var pollAnswer = _mapper.Map<PollAnswer>(command);

            await _dbContext.Set<PollAnswer>().AddAsync(pollAnswer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return pollAnswer.Id;
        }
    }
}