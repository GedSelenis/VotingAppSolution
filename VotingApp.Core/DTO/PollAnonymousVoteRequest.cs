using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Core.DTO
{
    public class PollAnonymousVoteRequest
    {
        public Guid PollId { get; set; }
        [EmailAddress]
        public string? UserEmail { get; set; } // Optional email for anonymous voting
    }
}
