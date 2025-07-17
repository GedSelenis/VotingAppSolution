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
    public class PollOptionsRepository : IPollOptionsRepository
    {
        private readonly ApplicationDbContext _db;

        public PollOptionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> AddPollOptions(List<PollOption> pollOptions)
        {
            foreach (var option in pollOptions)
            {
                _db.PollOptions.Add(option);
            }
            return await _db.SaveChangesAsync();
        }

        //public async Task<bool> AddVoter(Guid optionId, string userName)
        //{
        //    PollOption? option = await _db.PollOptions.FirstOrDefaultAsync(o => o.Id == optionId);
        //    if (option == null)
        //    {
        //        return false;
        //    }
        //    if (option.Voters == null)
        //    {
        //        option.Voters = new List<string>();
        //    }
        //    option.Voters.Add(userName);
        //    return await _db.SaveChangesAsync() > 0;
        //}
    }
}
