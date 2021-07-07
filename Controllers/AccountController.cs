using BMS.Models;
using BMS.Repository;
using BMS.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountController));
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
            
        }

        //called for User Login 
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(User user)
        {
           try
            {
                var token = await this.accountService.Login(user);
                if (token!=null)
                {
                    _log.Info("Login success for " + user.UserName);
                    string customerId = await  this.accountService.GetCustomerIdByUserName(user.UserName);
                    return Ok(new {token,customerId});
                   
                }  
                else
                {
                    _log.Error("Invalid credentials");
                    return BadRequest("Invalid UserName/password");
                }          
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest("Something went wrong");
            }
        }
        // called for User Registeration
        [HttpPost("User-Registeration")]
        public async Task<IActionResult> UserRegisteration(Account accountDto)
        {
            try
            {
                if(accountDto!=null)
                {
                    await this.accountService.RegisterAccount(accountDto);
                    _log.Info("Account registered");
                    return Ok();
                }
                else
                {
                    return BadRequest("null object ");
                }
                
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest("something went wrong");
            }
        }
        //called for user to update details
        [Authorize]
        [HttpPut("Update-Details")]
        public async Task<IActionResult> UpdateUserDetails(Account account)
        {
            try
            {
                await this.accountService.UpdateAccountDetails(account);
                _log.Info("Updated account details");
                return Ok();
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest("something went wrong");
            }
        }
        //called for updating password
        [HttpPut("Update-password")]
        public async Task<IActionResult> UpdateUserpassword(User user)
        {
            try
            {
                await this.accountService.UpdatePassword(user);
                _log.Info("password updated successfully");
                return Ok();
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest("something went wrong");
            }
        }
        //called to get Username post it on navbar
        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUserNamebyId(string id)
        {
            try
            {
                var res = await this.accountService.GetUserNameById(id);
                return Ok(res);
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("getAccount/{custid}")]
        public async Task<IActionResult> GetUserById(string custid)
        {
            try
            {
                var account = await this.accountService.GetAccountById(custid);
                if(account!=null)
                {
                    return Ok(account);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest();
            }
        }
        [HttpPost("getuserdata")]
        public async Task<IActionResult> GetUserData(UserDto userdto)
        {
            try
            {
                var user = await this.accountService.GetUserData(userdto);
                if (user)
                    return Ok();
                else
                    return BadRequest("User Not Found");
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpPost("confirmpassword")]
        public async Task<IActionResult> ConfirmPassword(UserDto1 Udto)
        {
            try
            {
                var res = await this.accountService.GetUserPassword(Udto.CustomerId,Udto.Password);
                if (res)
                    return Ok();
                else
                    return BadRequest("Password not matching");
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("checkuniqueusername/{username}")]
        public async Task<IActionResult> CheckUniqueUserName(string username)
        {
            try
            {
                var res= await this.accountService.CheckUniqueUsername(username);
                if (res)
                    return Ok(true);
                else
                    return BadRequest();
            }
            catch(Exception e)
            {
                _log.Error(e.Message);
                return BadRequest();
            }
        }

       
    }
}
