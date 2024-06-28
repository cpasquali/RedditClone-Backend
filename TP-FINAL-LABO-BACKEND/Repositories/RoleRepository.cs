using TP_FINAL_LABO_BACKEND.Models.Role;
using TP_FINAL_LABO_BACKEND.Services;

namespace TP_FINAL_LABO_BACKEND.Repositories
{
    public interface IRoleRepository : IRepository<Role> { }
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AplicationDbContext db) : base(db) { }
    }
}
