using Microsoft.AspNetCore.Mvc;
using TP_FINAL_LABO_BACKEND.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TP_FINAL_LABO_BACKEND.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly LikeService _likeService;

        public LikeController(LikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId, int userId)
        {
            var like = await _likeService.AddLikeAsync(postId, userId);
            return Ok(like);
        }

        [HttpDelete("{postId}/unlike")]
        public async Task<IActionResult> UnlikePost(int postId, int userId)
        {
            await _likeService.RemoveLikeAsync(postId, userId);
            return NoContent();
        }

        [HttpGet("{postId}/count")]
        public async Task<IActionResult> GetLikesCount(int postId)
        {
            var count = await _likeService.GetLikesCountAsync(postId);
            return Ok(count);
        }

        [HttpGet("{postId}/isLiked")]
        public async Task<IActionResult> IsPostLikedByUser(int postId, int userId)
        {
            var isLiked = await _likeService.IsPostLikedByUserAsync(postId, userId);
            return Ok(isLiked);
        }
    }
}
