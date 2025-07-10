using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.DTO
{
    public class CommentAddRequest
    {
        public string Content { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid PollId { get; set; }

        public Comment ToComment()
        {
            return new Comment
            {
                Content = Content,
                CreatedBy = CreatedBy,
                PollId = PollId
            };
        }
    }
}
