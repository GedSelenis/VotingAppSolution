using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.DTO
{
    public class PollResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PollEndTime { get; set; }
        public string CreatedBy { get; set; }
        public List<PollOption> Options { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsActive => PollEndTime > DateTime.UtcNow;
        public bool AuthenticatedOnly { get; set; }
    }

    public static class PollExtensions
    {
        public static PollResponse ToResponse(this Poll poll)
        {
            return new PollResponse
            {
                Id = poll.Id,
                Title = poll.Title,
                PollEndTime = poll.PollEndTime,
                CreatedBy = poll.CreatedBy,
                Options = poll.Options,
                Comments = poll.Comments,
                AuthenticatedOnly = poll.AuthenticatedOnly
            };
        }
    }
}
