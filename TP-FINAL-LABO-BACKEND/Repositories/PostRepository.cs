using TP_FINAL_LABO_BACKEND.Models.Post;
using TP_FINAL_LABO_BACKEND.Services;
using TP_FINAL_LABO_BACKEND.Models;

namespace TP_FINAL_LABO_BACKEND.Repositories
{
    public interface IPostRepository : IRepository<Post> { }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AplicationDbContext db) : base(db) { }
    }
}
