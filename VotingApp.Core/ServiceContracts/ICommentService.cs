using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.DTO;

namespace VotingApp.Core.ServiceContracts
{
    public interface ICommentService
    {
        /// <summary>
        /// Gets all comments for a specific poll.
        /// </summary>
        /// <param name="pollId"></param>
        /// <returns></returns>
        public Task<List<CommentResponse>> GetComments(Guid pollId);
        /// <summary>
        /// Adds a new comment to a poll.
        /// </summary>
        /// <param name="commentAddRequest"></param>
        /// <returns></returns>
        public Task<CommentResponse> AddComment(CommentAddRequest commentAddRequest, string userName);

        /// <summary>
        /// Deletes a comment by its ID if the user is authorized to do so.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Task<bool> DeleteComment(Guid commentId, Guid userID );

    }
}
