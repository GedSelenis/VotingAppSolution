using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.Domain.RepositoryContracts
{
    public interface IPollOptionsRepository
    {
        Task<int> AddPollOptions(List<PollOption> pollOptions);
        Task<bool> AddVoter(Guid optionId, string userName);
    }
}
