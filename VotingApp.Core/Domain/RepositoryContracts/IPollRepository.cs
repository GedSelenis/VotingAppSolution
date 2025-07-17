using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.DTO;

namespace VotingApp.Core.Domain.RepositoryContracts
{
    public interface IPollRepository
    {
        Task<int> AddPoll(Poll request);
        //Task<bool> AddVote(Guid pollId, Guid optionID, string userName);
        Task<List<Poll>> GetAllPolls();
        Task<Poll?> GetPollById(Guid pollId);
        Task<bool> DeletePoll(Guid pollId);
    }
}
