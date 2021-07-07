using BMS.Models;
using System.Threading.Tasks;

namespace BMS.Repository
{
    public interface IAccountRepo
    {
        public Task<Account> GetUser(User user);
        public Task Register(Account account);
        public Task Update(Account account);
        public Task UpdatePassword(User user);
        public Task<string> GetUserNameById(string id);
        public Task<string> GetUserIdByUsername(string username);
        public Task<Account> GetAccountById(string custid);
        public Task<bool> GetUserData(UserDto userDto);
        public Task<bool> GetPassword(string id, string password);
        public Task<bool> CheckUniqueUsername(string username);

    }
}
