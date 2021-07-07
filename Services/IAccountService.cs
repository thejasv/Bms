using BMS.Models;
using System.Threading.Tasks;

namespace BMS.Services
{
    public interface IAccountService
    {
        public Task<string> Login(User user);
        public Task RegisterAccount(Account accountDto);
        public Task UpdateAccountDetails(Account account);
        public Task UpdatePassword(User user);
        public Task<string> GetUserNameById(string id);
        public Task<string> GetCustomerIdByUserName(string username);
        public Task<Account> GetAccountById(string custid);
        public Task<bool> GetUserData(UserDto userDto);
        public Task<bool> GetUserPassword(string id, string password);
        public Task<string> GenerateJWT(User usercred);
        public  Task<bool> CheckUniqueUsername(string username);

    }
}
