using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IRole
    {
        void AddRole(Role role);

        void UpdateRole(Role role);

        void DeleteRole(int roleId);

        Role GetRole(int roleId);

        List<Role> GetRolesByAcc(int accId);

        List<Role> GetAllRoles();
    }
}
