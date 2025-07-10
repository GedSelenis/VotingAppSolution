using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.Domain.RepositoryContracts;
using VotingApp.Infrastructure.DbContext;

namespace VotingApp.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Comment> AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            _db.Comments.RemoveRange(_db.Comments.Where(c => c.Id == commentId));
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            return await _db.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<List<Comment>> GetCommentsByPollId(Guid pollId)
        {
            return await _db.Comments.Where(c => c.PollId == pollId).ToListAsync();
        }
    }
}
