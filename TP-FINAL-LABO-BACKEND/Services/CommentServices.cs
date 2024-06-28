using AutoMapper;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using TP_FINAL_LABO_BACKEND.Enums;
using TP_FINAL_LABO_BACKEND.Models.Comment;
using TP_FINAL_LABO_BACKEND.Models.Comment.Dto;
using TP_FINAL_LABO_BACKEND.Models.Post;
using TP_FINAL_LABO_BACKEND.Repositories;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class CommentServices
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentServices(ICommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }
        private async Task<Comment> GetOneByIdOrException(int id)
        {
            var comment = await _commentRepo.GetOne(c => c.IdComment == id);

            if (comment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return comment;
        }

        public async Task<List<CommentsDto>> GetAll()
        {
            var lista = await _commentRepo.GetAll();
            return _mapper.Map<List<CommentsDto>>(lista);
        }

        public async Task<List<CommentsDto>> GetAllByUserId(int userId)
        {
            var lista = await _commentRepo.GetAll(u => u.UserId == userId);
            return _mapper.Map<List<CommentsDto>>(lista);
        }

        public async Task<CommentDto> GetById(int id)
        {
            var post = await GetOneByIdOrException(id);

            return _mapper.Map<CommentDto>(post);
        }

        public async Task<CommentDto> Create(CreateCommentDto createPostDto)
        {
            var post = _mapper.Map<Comment>(createPostDto);

            await _commentRepo.Add(post);

            return _mapper.Map<CommentDto>(post);
        }

        public async Task<CommentDto> UpdateById(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await GetOneByIdOrException(id);

            var updated = _mapper.Map(updateCommentDto, comment);

            return _mapper.Map<CommentDto>(await _commentRepo.Update(updated));
        }

        public async Task DeleteOneById(int id)
        {
            var comment = await GetOneByIdOrException(id);
            await _commentRepo.Delete(comment);
        }

        public async Task<List<CommentDto>> GetCommentsForPost(int postId)
        {
            var comments = await _commentRepo.GetCommentsForPost(postId);
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<bool> VerifyAuthor(HttpContext context, int postId)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var userId = int.Parse(identity.FindFirst("id").Value);
            var roles = identity.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

            var post = await GetOneByIdOrException(postId);

            return (userId == post.UserId || roles.Contains(ROLES.ADMIN));
        }
    }
}
