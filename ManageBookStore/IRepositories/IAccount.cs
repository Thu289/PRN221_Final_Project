using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IAccount
    {
        Account GetAccountById(int id);

        void AddAccount (Account account);

        void UpdateAccount (Account account);

        void DeleteAccount (int id);

        bool CheckRole (int id, string roleName);

        bool CheckIsActive (int id);

        Account GetAccountByLogin(string emailOrUsername, string password);

        List<Account> GetAllAccounts ();

        List<Account> GetAccountByRole(string roleName);

        void AddRole(int accId, string roleName);

        void AddRoles(int accId, int roleId);

        bool CheckUserNameExist(string userName);

        bool CheckEmailExist(string email); 
    }
}
