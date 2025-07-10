using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.DTO
{
    public class PollAddRequest
    {
        public string Title { get; set; }
        public DateTime PollEndTime { get; set; }
        public string CreatedBy { get; set; }
        public List<PollOption> Options { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsActive => PollEndTime > DateTime.UtcNow;
        public bool AuthenticatedOnly { get; set; }

        public Poll ToPoll()
        {
            return new Poll
            {
                Title = Title,
                PollEndTime = PollEndTime,
                CreatedBy = CreatedBy,
                Options = Options,
                Comments = Comments,
                AuthenticatedOnly = AuthenticatedOnly
            };
        }
    }
}
