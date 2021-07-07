using BMS.Models;
using BMS.Repository;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo accountRepo;
        private readonly IConfiguration _config;
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountService));
        public AccountService(IAccountRepo _accountRepo,IConfiguration config)
        {
            this.accountRepo = _accountRepo;
            this._config = config;
        }
        public async Task<string> Login(User user)
        {
            try
            {
                _log.Info("procesing password");
                var account = await this.accountRepo.GetUser(user);
                string token = null;
                if (account != null)
                {
                    token = await GenerateJWT(user);
                }
                 return token;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public  async Task<string> GenerateJWT(User usercred)
        {
            try
            {
                Account account =await this.accountRepo.GetUser(usercred);
                if (account != null)
                {
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.NameId,usercred.UserName)
                    };
                    var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credentials);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in Provider while generating token - " + e.Message);
                throw;
            }
        }

        public  async Task RegisterAccount(Account account)
        {
            try
            {
                _log.Info("Registering Account");
                
                await this.accountRepo.Register(account);
            }
            catch(Exception)
            {
                throw;
            }
        }
        


        public async  Task UpdateAccountDetails(Account account)
        {
            try
            {
                _log.Info("Updating Account");
                await this.accountRepo.Update(account);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public  async Task UpdatePassword(User user)
        {
            try
            {
                _log.Info("updating new password");
                await this.accountRepo.UpdatePassword(user);
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
                return await this.accountRepo.GetUserNameById(id);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<string> GetCustomerIdByUserName(string username)
        {
            try
            {
                return await this.accountRepo.GetUserIdByUsername(username);
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
                return await this.accountRepo.GetAccountById(custid);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> GetUserData(UserDto userDto)
        {
            try
            {
                return await this.accountRepo.GetUserData(userDto);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async  Task<bool> GetUserPassword(string id,string password)
        {
            try
            {
                return  await this.accountRepo.GetPassword(id, password);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckUniqueUsername(string username)
        {
            try
            {
                return await  this.accountRepo.CheckUniqueUsername(username);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
