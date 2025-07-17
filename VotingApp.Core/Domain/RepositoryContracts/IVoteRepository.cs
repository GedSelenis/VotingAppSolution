using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.Domain.RepositoryContracts
{
    public interface IVoteRepository
    {
        Task<bool> AddVoteAsync(Vote vote);
        Task<bool> HasAlreadyVoted(Guid pollId, string userName);
    }
}
