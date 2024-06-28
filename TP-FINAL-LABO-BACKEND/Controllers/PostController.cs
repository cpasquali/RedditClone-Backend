using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TP_FINAL_LABO_BACKEND.Models.Post.Dto;
using TP_FINAL_LABO_BACKEND.Services;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;
using TP_FINAL_LABO_BACKEND.Enums;

namespace TP_FINAL_LABO_BACKEND.Controllers
{
    [Authorize]
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostServices _postService;
        private readonly UserServices _userService;

        public PostController(PostServices postServices, UserServices userServices)
        {
            _postService = postServices;
            _userService = userServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostsDto>>> Get()
        {
            return Ok(await _postService.GetAll());
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            try
            {
                return Ok(await _postService.GetOneById(id));
            }
            catch
            {
                return NotFound(new { message = $"No post with Id = {id}" });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDto>> Post([FromBody] CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDto user;
            try
            {
                user = await _userService.GetOneById(createPostDto.UserId);
            }
            catch
            {
                ModelState.AddModelError("UserName", "User does not exist");
                return BadRequest(ModelState);
            }
            var postCreated = await _postService.CreateOne(createPostDto);
            return Created("CreatePost", postCreated);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDto>> Put(int id, [FromBody] UpdatePostDto updatePostDto)
        {
            try
            {
                var postUpdated = await _postService.UpdateOneById(id, updatePostDto);
                return Ok(postUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                // Verificar si el usuario es el autor o tiene los roles de administrador o moderador
                bool isVerified = await _postService.VerifyAuthor(HttpContext, id);
                bool isAdmin = HttpContext.User.IsInRole("Admin");
                bool isModerator = HttpContext.User.IsInRole("Moderator");

                if (!isVerified && !isAdmin && !isModerator)
                {
                    // Si no está autorizado, devolver un Forbidden (403)
                    return Forbid();
                }

                // Eliminar el posteo
                await _postService.DeleteOneById(id);

                return Ok(new
                {
                    message = $"Post with Id = {id} was deleted"
                });
            }
            catch (Exception e)
            {
                // Manejar cualquier excepción y devolver un BadRequest (400) con el mensaje de error
                return BadRequest(e.Message);
            }
        }
    }
}
