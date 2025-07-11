using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Core.Domain.Entities
{
    public class PollOption
    {
        [Key]
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public List<string>? Voters { get; set; }
        public Guid PollId { get; set; }
        [ForeignKey("PollId")]
        public Poll? Poll { get; set; }
    }
}
