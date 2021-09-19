using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplePoll.Answers.Domain.Entities;

namespace SimplePoll.Answers.Infrastructure.DbContext.Configurations
{
    public class PollAnswerConfiguration
    {
        public void Configure(EntityTypeBuilder<PollAnswer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.PollOptionId).IsRequired();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("timezone('utc'::text, now())");
            builder.Property(x => x.LastModifiedDate).HasDefaultValueSql("timezone('utc'::text, now())");
        }
    }
}