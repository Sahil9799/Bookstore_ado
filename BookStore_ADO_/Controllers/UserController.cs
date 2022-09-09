namespace BookStore_ADO_DatabaseFirst.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using BusinessLayer.Interfaces.UserInterfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ModelLayer.Models.UserModels;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        public IActionResult UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = this.userBL.UserRegistration(registrationModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "User Registration Failed!!" });
                }

                return this.Ok(new { success = true, Message = "User Registration Sucessfull", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Something went wrong while Fetching Users Data !!" });
                }

                return this.Ok(new { success = true, Message = "User Data Fetched Sucessfully", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var result = this.userBL.UserLogin(loginModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Login Failed!! Check your EmailId and Password and try again..." });
                }

                return this.Ok(new { success = true, Message = "User Login Sucessful...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string EmailId)
        {
            try
            {
                if (EmailId == null)
                {
                    return this.BadRequest(new { success = false, Message = "Please provide valid Email address!!" });
                }
                var result = this.userBL.ForgotPassword(EmailId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "something went wrong while sending ResetPassword Link!!" });
                }

                return this.Ok(new { success = true, Message = $"Reset Password link sent Sucessfully..."});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassModel passModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var emailId = claims.Where(p => p.Type == @"EmailId").FirstOrDefault()?.Value;
                bool result = this.userBL.ResetPassword(emailId, passModel);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = $"Password Updated successfully for EmailId:{emailId}..." });
                }

                return this.BadRequest(new { success = false, Message = $"Reset Password Unsuccessful for EmailId:{emailId}!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
