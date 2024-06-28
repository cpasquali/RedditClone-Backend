using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TP_FINAL_LABO_BACKEND.Services;

namespace TP_FINAL_LABO_BACKEND.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T> GetOne(Expression<Func<T, bool>>? filter = null);
        Task<T> GetByIdAsync(int id); // Agregar este método
        Task Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task Save();
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<T> Update(T entity)
        {
            dbSet.Update(entity);
            await Save();
            return entity;
        }

        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await Save();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}