namespace BookStore_ADO_DatabaseFirst.Controllers
{
    using System;
    using BusinessLayer.Interfaces.AdminInterfaces;
    using Microsoft.AspNetCore.Mvc;
    using ModelLayer.Models.AdminModels;

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(AdminLoginModel loginModel)
        {
            try
            {
                var result = this.adminBL.AdminLogin(loginModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Login Failed!! Check your EmailId and Password and try again..." });
                }

                return this.Ok(new { success = true, Message = "Admin Login Sucessful...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
