using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;

namespace VotingApp.Core.Domain.RepositoryContracts
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Adds a new comment to a poll.
        /// </summary>
        /// <param name="comment">The comment to be added.</param>
        /// <returns>The added comment.</returns>
        Task<Comment> AddComment(Comment comment);
        /// <summary>
        /// Deletes a comment by its ID if the user is authorized to do so.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>True if the comment was deleted, otherwise false.</returns>
        Task<bool> DeleteComment(Guid commentId);

        /// <summary>
        /// Retrieves specific comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<Comment> GetCommentById(Guid commentId);

        /// <summary>
        /// Retrieves all comments for a specific poll.
        /// </summary>
        /// <param name="pollId"></param>
        /// <returns></returns>
        Task<List<Comment>> GetCommentsByPollId(Guid pollId);
    }
}
