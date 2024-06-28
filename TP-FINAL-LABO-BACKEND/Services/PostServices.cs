using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TP_FINAL_LABO_BACKEND.Repositories;
using System.Web.Http;
using System.Net;
using System.Security.Claims;
using TP_FINAL_LABO_BACKEND.Models.Post.Dto;
using TP_FINAL_LABO_BACKEND.Models.Post;
using TP_FINAL_LABO_BACKEND.Models.User;
using Microsoft.Extensions.Hosting;
using TP_FINAL_LABO_BACKEND.Enums;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class PostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostServices(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<List<PostsDto>> GetAll()
        {
            var posts = await _postRepository.GetAll();
            return _mapper.Map<List<PostsDto>>(posts);
        }

        private async Task<Post> GetOneByIdOrException(int id)
        {
            var post = await _postRepository.GetOne(t => t.IdPost == id);
            if (post == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return post;
        }

        public async Task<PostDto> GetOneById(int id)
        {
            var post = await GetOneByIdOrException(id);
            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreateOne(CreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);

            await _postRepository.Add(post);

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> UpdateOneById(int id, UpdatePostDto updatePostDto)
        {
            var post = await GetOneByIdOrException(id);

            var mappedPost = _mapper.Map(updatePostDto, post);

            var updatePost = await _postRepository.Update(mappedPost);

            return _mapper.Map<PostDto>(updatePost);
        }

        public async Task DeleteOneById(int id)
        {
            var post = await GetOneByIdOrException(id);
            await _postRepository.Delete(post);
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
