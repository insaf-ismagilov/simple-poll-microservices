using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplePoll.Answers.Domain.Entities;

namespace SimplePoll.Answers.Infrastructure.DbContext.Configurations
{
    public class PollAnswerConfiguration
    {
        public void Configure(EntityTypeBuilder<PollAnswer> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}