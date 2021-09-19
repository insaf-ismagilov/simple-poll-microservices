using Microsoft.EntityFrameworkCore;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Answers.Infrastructure.DbContext.Configurations;

namespace SimplePoll.Answers.Infrastructure.DbContext
{
    public class AnswersDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<PollAnswer> PollAnswers { get; set; }

        public AnswersDbContext(DbContextOptions<AnswersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PollAnswer>(new PollAnswerConfiguration().Configure);
        }
    }
}