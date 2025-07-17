using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Core.Domain.Entities
{
    public class Vote
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? PollId { get; set; }
        [ForeignKey("PollId")]
        public Poll? Poll { get; set; }
        public Guid PollOptionId { get; set; }
        [ForeignKey("PollOptionId")]
        public PollOption? PollOption { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
