using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_FINAL_LABO_BACKEND.Services;
using TP_FINAL_LABO_BACKEND.Models.Comment.Dto;
using TP_FINAL_LABO_BACKEND.Models.Post.Dto;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;

namespace TP_FINAL_LABO_BACKEND.Controllers
{
    [Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentServices _commentServices;
        private readonly UserServices _userServices;

        public CommentController(CommentServices commentServices, UserServices userServices)
        {
            _commentServices = commentServices;
            _userServices = userServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CommentsDto>>> Get()
        {
            return Ok(await _commentServices.GetAll());
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool isVerified = await _commentServices.VerifyAuthor(HttpContext, id);
                bool isAdmin = HttpContext.User.IsInRole("Admin");
                bool isModerator = HttpContext.User.IsInRole("Moderator");

                if (!isVerified && !isAdmin && !isModerator)
                {
                    // Si no está autorizado, devolver un Forbidden (403)
                    return Forbid();
                }

                await _commentServices.DeleteOneById(id);
                return Ok(new
                {
                    message = $"Comment with Id = {id} was deleted"
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDto>> Put(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            try
            {
                var commentUpdate = await _commentServices.UpdateById(id, updateCommentDto);
                return Ok(commentUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDto>> Post([FromBody] CreateCommentDto createComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDto user;
            try
            {
                user = await _userServices.GetOneById(createComment.UserId);
            }
            catch
            {
                ModelState.AddModelError("UserName", "User does not exist");
                return BadRequest(ModelState);
            }
            var commentCreated = await _commentServices.Create(createComment);
            return Created("CreatePost", commentCreated);
        }
    }
}
