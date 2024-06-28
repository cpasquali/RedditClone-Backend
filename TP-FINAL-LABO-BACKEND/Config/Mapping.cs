using AutoMapper;
using TP_FINAL_LABO_BACKEND.Models.Comment;
using TP_FINAL_LABO_BACKEND.Models.Like;
using TP_FINAL_LABO_BACKEND.Models.Like.Dto;
using TP_FINAL_LABO_BACKEND.Models.Post;
using TP_FINAL_LABO_BACKEND.Models.Post.Dto;
using TP_FINAL_LABO_BACKEND.Models.User.Dto;
using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Models.Comment.Dto;

namespace TP_FINAL_LABO_BACKEND.Config
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            //USERS
            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            //NO MAPEAR LOS NULL EN EL UPDATE
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

            //POSTS
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostsDto>().ReverseMap();
            CreateMap<PostDto, Comment>().ReverseMap();
            CreateMap<Comment, PostDto>().ReverseMap();
            CreateMap<CreatePostDto, Post>().ReverseMap();
            CreateMap<CreatePostLikeDto, Like>().ReverseMap();
            //NO MAPEAR LOS NULL EN EL UPDATE
            CreateMap<UpdatePostDto, Post>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

            //COMMENT
            CreateMap<Comment, CommentsDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CreateCommentDto, Comment>().ReverseMap();
            //NO MAPEAR LOS NULL EN EL UPDATE
            CreateMap<UpdateCommentDto, Comment>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));


            //ROLES
            CreateMap<User, UserLoginResponseDto>().ForMember(
                dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles.Select(r => r.NameRole).ToList())
            );
        }
    }
}
