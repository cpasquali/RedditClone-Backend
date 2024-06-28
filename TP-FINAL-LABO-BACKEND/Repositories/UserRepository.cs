using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TP_FINAL_LABO_BACKEND.Models.User;
using TP_FINAL_LABO_BACKEND.Services;

namespace TP_FINAL_LABO_BACKEND.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(int userId); // Este método ya está en la clase base
        Task UpdateAsync(User user);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AplicationDbContext db) : base(db) { }

        public new async Task<User> GetOne(Expression<Func<User, bool>>? filter = null)
        {
            IQueryable<User> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(u => u.Roles);
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await dbSet.Include(u => u.Roles)
                              .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdateAsync(User user)
        {
            dbSet.Update(user);
            await Save(); // Utilizamos el método Save de la clase base
        }
    }
}