using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Core.DTO
{
    public class PollAddVoteRequest
    {
        public Guid PollId { get; set; }
        public Guid OptionId { get; set; }
        public string? UserName { get; set; }
    }
}
