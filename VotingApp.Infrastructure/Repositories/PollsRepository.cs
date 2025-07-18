﻿using Microsoft.EntityFrameworkCore;
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
    public class PollsRepository : IPollRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPollOptionsRepository _optionsRepository;

        public PollsRepository(ApplicationDbContext db, IPollOptionsRepository optionsRepository)
        {
            _db = db;
            _optionsRepository = optionsRepository;
        }

        public async Task<int> AddPoll(Poll request)
        {
            _db.Polls.Add(request);
            return await _db.SaveChangesAsync();
        }

        //public async Task<bool> AddVote(Guid pollId, Guid optionID, string userName)
        //{
        //    bool success = await _optionsRepository.AddVoter(optionID, userName);
        //    Poll? poll = await _db.Polls.FirstOrDefaultAsync(p => p.Id == pollId);
        //    if (poll == null || !success)
        //    {
        //        return false;
        //    }
        //    if (poll.Voters == null)
        //    {
        //        poll.Voters = new List<string>();
        //    }
        //    poll.Voters.Add(userName);
        //    await _db.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> DeletePoll(Guid pollId)
        {
            _db.Polls.RemoveRange(_db.Polls.Where(p => p.Id == pollId));
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<List<Poll>> GetAllPolls()
        {
            return await _db.Polls.Include("Options").Include("Comments").Include("Votes").ToListAsync();
        }

        public async Task<Poll?> GetPollById(Guid pollId)
        {
            return await _db.Polls.Include("Options").Include("Comments").Include("Votes").FirstOrDefaultAsync(p => p.Id == pollId);
        }
    }
}
