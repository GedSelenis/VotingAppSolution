using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.DTO;

namespace VotingApp.Core.ServiceContracts
{
    public interface IPollService
    {
        /// <summary>
        /// Adds a new poll asynchronously.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Copy of an added poll</returns>
        public Task<PollResponse> AddPollAsync(PollAddRequest request);

        /// <summary>
        /// Retrieves a poll by its ID asynchronously.
        /// </summary>
        /// <param name="id">Id of the pool</param>
        /// <returns>A poll by id</returns>
        public Task<PollResponse> GetPoll(Guid id);

        /// <summary>
        /// Retrieves all polls asynchronously.
        /// </summary>
        /// <returns>A list of Polls</returns>
        public Task<List<PollResponse>> GetAllPolls();

        /// <summary>
        /// Adds a vote to a poll asynchronously.
        /// </summary>
        /// <param name="pollAddVoteRequest">Information on poll</param>
        /// <returns>Copy of updated poll</returns>
        public Task<PollResponse> AddVote(PollAddVoteRequest pollAddVoteRequest);
    }
}
