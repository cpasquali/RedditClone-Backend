using Microsoft.EntityFrameworkCore;
using TP_FINAL_LABO_BACKEND.Models.Comment;
using TP_FINAL_LABO_BACKEND.Services;


namespace TP_FINAL_LABO_BACKEND.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> Update(Comment entity);
        Task<List<Comment>> GetCommentsForPost(int postId);
    }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly AplicationDbContext _db;

        public CommentRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Comment> Update(Comment entity)
        {
            _db.Update(entity);
            await Save();
            return entity;
        }
        public async Task<List<Comment>> GetCommentsForPost(int postId)
        {
            return await _db.Comment
                .Where(c => c.IdPost == postId)
                .ToListAsync();
        }

    }
}
