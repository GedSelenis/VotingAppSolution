using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Core.Domain.Entities
{
    public class Poll
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PollEndTime { get; set; }
        public string CreatedBy { get; set; }
        public List<PollOption> Options { get; set; }
        public List<Comment> Comments { get; set; }
        public List<string>? Voters { get; set; }
        public bool IsActive => PollEndTime > DateTime.UtcNow;
        public bool AuthenticatedOnly { get; set; }

    }
}
