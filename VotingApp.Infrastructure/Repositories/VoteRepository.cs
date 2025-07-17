using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.Domain.RepositoryContracts;
using VotingApp.Infrastructure.DbContext;

namespace VotingApp.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _db;

        public VoteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddVoteAsync(Vote vote)
        {
            _db.Votes.Add(vote);
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> HasAlreadyVoted(Guid pollId, string userName)
        {
            return await _db.Votes
                .AnyAsync(v => v.PollId == pollId && v.UserName == userName);
        }
    }
}
