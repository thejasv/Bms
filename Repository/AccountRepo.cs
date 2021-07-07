using BMS.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Repository
{
    public class AccountRepo : IAccountRepo
    {
        public List<Account> Accounts;
        private AccountContext accountcontext;
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountRepo));
        public AccountRepo(AccountContext _accountcontext)
        {
            this.accountcontext = _accountcontext;
        }
        public async Task<Account> GetUser(User user)
        {
            try
            {
                var account = await this.accountcontext.Accounts.FirstOrDefaultAsync(acc => acc.UserName == user.UserName && acc.Password == user.Password);
                return account;
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public async  Task Register(Account account)
        {
            try
            {
                _log.Info("Registering Account");
                await this.accountcontext.AddAsync(account);
                await this.accountcontext.SaveChangesAsync();

            }
            catch(Exception)
            {
                throw;
            }

        }

        public async Task Update(Account account)
        {
            try
            {
                _log.Info("Updating Account details");
                var Acc = this.accountcontext.Accounts.FirstOrDefault(acc => acc.CustomerId == account.CustomerId);
                Acc.AccountType = account.AccountType;
                Acc.Address = account.Address;
                Acc.BranchName = account.BranchName;
                Acc.Citizenship = account.Citizenship;
                Acc.CitizenStatus = account.CitizenStatus;
                Acc.ContactNumber = account.ContactNumber;
                Acc.Country = account.Country;
                Acc.DateofBirth = account.DateofBirth;
                Acc.Email = account.Email;
                Acc.Gender = account.Gender;
                Acc.GuardianName = account.GuardianName;
                Acc.GuardianType = account.GuardianType;
                Acc.IdCardNumber = account.IdCardNumber;
                Acc.IdentificationType = account.IdentificationType;
                Acc.InitialDeposit = account.InitialDeposit;
                Acc.MaritalStatus = account.MaritalStatus;
                Acc.Name = account.Name;
                Acc.ReferenceAccountHolderAccountNumber = account.ReferenceAccountHolderAccountNumber;
                Acc.ReferenceAccountHolderAddress = account.ReferenceAccountHolderAddress;
                Acc.ReferenceAccountHolderName = account.ReferenceAccountHolderName;
                Acc.RegisterationDate = account.RegisterationDate;
                Acc.State = account.State;
                Acc.UserName = account.UserName;
                await this.accountcontext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async  Task UpdatePassword(User user)
        {
            try
            {
                _log.Info("Updating  user password");
                var Acc = this.accountcontext.Accounts.FirstOrDefault(acc => acc.UserName == user.UserName);
                Acc.Password = user.Password;
                await this.accountcontext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<string> GetUserNameById(string id)
        {
            try
            {
                var account =await this.accountcontext.Accounts.FirstOrDefaultAsync<Account>(acc=>acc.CustomerId==id);
                return account.UserName;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            try
            {
                var account = await this.accountcontext.Accounts.FirstOrDefaultAsync(acc => acc.UserName ==username);
                return account.CustomerId;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<Account> GetAccountById(string custid)
        {
            try
            {
                var account = await this.accountcontext.Accounts.FirstOrDefaultAsync(acc => acc.CustomerId == custid);
                return account;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> GetUserData(UserDto userDto)
        {
            var result =await this.accountcontext.Accounts.AnyAsync(acc => acc.CustomerId == userDto.CustomerId && acc.UserName == userDto.UserName);
            return result;
        }

        public async Task<bool> GetPassword(string id, string password)
        {
            var res = await this.accountcontext.Accounts.AnyAsync(acc => acc.CustomerId == id && acc.Password == password);
            return res;
        }

        public async  Task<bool> CheckUniqueUsername(string username)
        {
            var output = await this.accountcontext.Accounts.AnyAsync(acc => acc.UserName == username);
            return output;
        }
    }
}
