using System.Net;
using System.Web.Http;
using TP_FINAL_LABO_BACKEND.Models.Role;
using TP_FINAL_LABO_BACKEND.Repositories;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class RoleServices
    {
        private readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> GetByName(string name)
        {
            var role = await _roleRepository.GetOne(r => r.NameRole == name);
            if(role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return role;
        }

        public async Task<List<Role>> GetByIds(List<int> roleIds)
        {
            if(roleIds == null ||  roleIds.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var roles = await _roleRepository.GetAll(r => roleIds.Contains(r.IdRole));
            return roles.ToList();
        }
    }
}
