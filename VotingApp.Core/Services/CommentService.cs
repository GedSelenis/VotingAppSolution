using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.Domain.RepositoryContracts;
using VotingApp.Core.DTO;
using VotingApp.Core.ServiceContracts;

namespace VotingApp.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly UserManager<IdentityUser> _userManager;

        public CommentService(ICommentRepository commentRepository, UserManager<IdentityUser> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task<CommentResponse> AddComment(CommentAddRequest commentAddRequest, string userName)
        {
            if (commentAddRequest == null)
            {
                throw new ArgumentNullException(nameof(commentAddRequest), "Comment request cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(commentAddRequest.Content))
            {
                throw new ArgumentException("Comment content cannot be empty.", nameof(commentAddRequest.Content));
            }
            if (commentAddRequest.PollId == Guid.Empty)
            {
                throw new ArgumentException("Poll ID must be specified for the comment.", nameof(commentAddRequest.PollId));
            }
            if (commentAddRequest.CreatedBy == Guid.Empty)
            {
                throw new ArgumentException("Created by must be specified for the comment.", nameof(commentAddRequest.CreatedBy));
            }
            Comment comment = new Comment
            {
                Content = commentAddRequest.Content,
                CreatedBy = commentAddRequest.CreatedBy,
                PollId = commentAddRequest.PollId,
                CreatedByName = userName,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddComment(comment);

            return comment.ToResponse();
        }

        public async Task<bool> DeleteComment(Guid commentId, Guid userID)
        {
            if (commentId == Guid.Empty)
            {
                throw new ArgumentException("Comment ID must be specified.", nameof(commentId));
            }
            if (userID == Guid.Empty)
            {
                throw new ArgumentException("User ID must be specified.", nameof(userID));
            }
            Comment comment = await _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }
            if (comment.CreatedBy != userID)
            {
                throw new UnauthorizedAccessException("User is not authorized to delete this comment.");
            }
            return await _commentRepository.DeleteComment(commentId);
        }

        public async Task<List<CommentResponse>> GetComments(Guid pollId)
        {
            return (await _commentRepository.GetCommentsByPollId(pollId)).Select(c => c.ToResponse()).ToList();
        }
    }
}
