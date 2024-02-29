using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Repositories
{
    public class AccountRepository : IAccount
    {
        Prn221ManageStoreContext _context;

        public AccountRepository(Prn221ManageStoreContext context)
        {
            _context = context;
        }

        public void AddRole(int accId, string roleName)
        {
            Account a = GetAccountById(accId);
            a.Roles.Add(_context.Roles.FirstOrDefault(r => r.Name.ToLower() == roleName.ToLower()));
            _context.Accounts.Update(a);
            _context.SaveChanges();
        }

        public void AddRoles(int accId, int roleId)
        {
            Account a = GetAccountById(accId);
            a.Roles.Add(_context.Roles.FirstOrDefault(r => r.Id == roleId));
            _context.Accounts.Update(a);
            _context.SaveChanges();
        }

        public bool CheckEmailExist(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return _context.Accounts.FirstOrDefault(a=>a.Email.ToLower()==email.ToLower()) != null;
        }

        public bool CheckUserNameExist(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return false;
            return _context.Accounts.FirstOrDefault(a => a.UserName.ToLower() == userName.ToLower()) != null;
        }

        public List<Account> GetAccountByRole(string roleName)
        {
            return _context.Roles.Include(r=>r.Accs).FirstOrDefault(r => r.Name.ToLower().Equals(roleName.ToLower())).Accs.ToList();
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        void IAccount.AddAccount(Models.Account account)
        {
            _context.Accounts.Add(account); 
            _context.SaveChanges();
        }

        bool IAccount.CheckIsActive(int id)
        {
            return (bool)_context.Accounts.FirstOrDefault(a=> a.Id == id).IsActive;
        }

        bool IAccount.CheckRole(int id, string roleName)
        {
            return _context.Accounts.Include(r=>r.Roles).FirstOrDefault(a=>a.Id==id).Roles.FirstOrDefault(r=>r.Name.ToLower()==roleName.ToLower()) != null;
        }

        void IAccount.DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        Models.Account GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == id);
        }

        Account IAccount.GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == id);
        }

        Models.Account IAccount.GetAccountByLogin(string emailOrUsername, string password)
        {
            return _context.Accounts.FirstOrDefault(a=>(a.UserName == emailOrUsername || a.Email == emailOrUsername ) && a.Password == password);
        }

        void IAccount.UpdateAccount(Models.Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
