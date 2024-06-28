using Microsoft.EntityFrameworkCore;
using TP_FINAL_LABO_BACKEND.Models.Like;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class LikeService
    {
        private readonly AplicationDbContext _context;

        public LikeService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Like> AddLikeAsync(int postId, int userId)
        {
            var like = new Like { PostId = postId, UserId = userId };
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task RemoveLikeAsync(int postId, int userId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _context.Likes.CountAsync(l => l.PostId == postId);
        }

        public async Task<bool> IsPostLikedByUserAsync(int postId, int userId)
        {
            return await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }
    }
}
