using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<PollOption> PollOptions { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
