using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.Domain.RepositoryContracts;
using VotingApp.Core.DTO;
using VotingApp.Core.Exceptions;
using VotingApp.Core.ServiceContracts;

namespace VotingApp.Core.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IPollOptionsRepository _optionsRepository;


        public PollService(IPollRepository pollRepository, IPollOptionsRepository pollOptionsRepository, IVoteRepository voteRepository)
        {
            _pollRepository = pollRepository;
            _optionsRepository = pollOptionsRepository;
            _voteRepository = voteRepository;
        }
        public async Task<PollResponse> AddPoll(PollAddRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Poll request cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Poll title cannot be empty.", nameof(request.Title));
            }
            if (request.Options == null || request.Options.Count < 2)
            {
                throw new ArgumentException("Poll must have at least two options.", nameof(request.Options));
            }
            if (request.PollEndTime <= DateTime.UtcNow)
            {
                throw new ArgumentException("Poll end time must be in the future.", nameof(request.PollEndTime));
            }
            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                throw new ArgumentException("CreatedBy must be specified for all polls.", nameof(request.CreatedBy));
            }

            Poll poll = request.ToPoll();
            poll.Id = Guid.NewGuid();
            // TODO: Chek if options are added to database
            await _pollRepository.AddPoll(poll);

            return poll.ToResponse();
        }

        public async Task<PollResponse> AddVote(PollAddVoteRequest pollAddVoteRequest)
        {
            bool isValid = true;
            if (pollAddVoteRequest == null)
            {
                throw new ArgumentNullException(nameof(pollAddVoteRequest), "Poll vote request cannot be null.");
            }
            Poll? poll = await _pollRepository.GetPollById(pollAddVoteRequest.PollId);
            if (poll is null)
            {
                throw new ObjectNotFoundException($"Poll with ID {pollAddVoteRequest.PollId} not found.");
            }
            if (pollAddVoteRequest.OptionId == Guid.Empty)
            {
                throw new ArgumentException("Option ID must be specified.");
            }
            if (!poll.Options.Any(o => o.Id == pollAddVoteRequest.OptionId))
            {
                throw new ArgumentException("Option ID does not exist in the poll.");
            }
            if (!poll.IsActive)
            {
                throw new InvalidOperationException("Cannot vote on a poll that has ended.");
            }
            if (poll.AuthenticatedOnly && (pollAddVoteRequest.UserId == null || pollAddVoteRequest.UserId == Guid.Empty))
            {
                throw new ArgumentException("Voter must be specified for authenticated polls.");
            }
            else if (pollAddVoteRequest.UserId == null || pollAddVoteRequest.UserId == Guid.Empty)
            {
                // Vote is anonymous, no user validation needed

                if (await _voteRepository.HasAlreadyVoted(pollAddVoteRequest.PollId, pollAddVoteRequest.UserName))
                {
                    throw new InvalidOperationException("User has already voted in this poll.");
                }

                //isValid = isValid && await _optionsRepository.AddVoter(pollAddVoteRequest.OptionId, pollAddVoteRequest.UserName ?? "");
                Vote vote = new Vote
                {
                    Id = Guid.NewGuid(),
                    PollId = pollAddVoteRequest.PollId,
                    PollOptionId = pollAddVoteRequest.OptionId,
                    UserName = pollAddVoteRequest.UserName ?? "",
                    UserId = Guid.Empty // Assuming anonymous votes do not have a UserId
                };
                isValid = await _voteRepository.AddVoteAsync(vote);
            }
            else
            {
                if (await _voteRepository.HasAlreadyVoted(pollAddVoteRequest.PollId, pollAddVoteRequest.UserName))
                {
                    throw new InvalidOperationException("User has already voted in this poll.");
                }

                //isValid = isValid && await _optionsRepository.AddVoter(pollAddVoteRequest.OptionId, pollAddVoteRequest.UserName ?? "");
                Vote vote = new Vote
                {
                    Id = Guid.NewGuid(),
                    PollId = pollAddVoteRequest.PollId,
                    PollOptionId = pollAddVoteRequest.OptionId,
                    UserName = pollAddVoteRequest.UserName ?? "",
                    UserId = pollAddVoteRequest.UserId.Value // Assuming anonymous votes do not have a UserId
                };
                isValid = await _voteRepository.AddVoteAsync(vote);
            }
            if (isValid) {
                Poll? updatedPoll = await _pollRepository.GetPollById(pollAddVoteRequest.PollId);
                return updatedPoll?.ToResponse() ?? poll.ToResponse();
            }
            else
            {
                throw new InvalidOperationException("Failed to add vote to the poll.");
            }
        }

        public async Task<List<PollResponse>> GetAllPolls()
        {
            return (await _pollRepository.GetAllPolls()).Select(p => p.ToResponse()).ToList() ;
        }

        public async Task<PollResponse> GetPoll(Guid id)
        {
            return (await _pollRepository.GetPollById(id)).ToResponse();
        }
    }
}
