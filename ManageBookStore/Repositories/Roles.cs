using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Repositories
{
    public class Roles : IRole
    {
        Prn221ManageStoreContext _context;
        public Roles(Prn221ManageStoreContext context)
        {
            _context = context;
        }
        void IRole.AddRole(Role role)
        {
           _context.Roles.Add(role);
            _context.SaveChanges();
        }

        void IRole.DeleteRole(int roleId)
        {
            throw new NotImplementedException();
        }

        List<Role> IRole.GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        Role IRole.GetRole(int roleId)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == roleId);
        }

        List<Role> IRole.GetRolesByAcc(int accId)
        {
            return (List<Role>)_context.Accounts.FirstOrDefault(a => a.Id == accId).Roles;
        }

        void IRole.UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
