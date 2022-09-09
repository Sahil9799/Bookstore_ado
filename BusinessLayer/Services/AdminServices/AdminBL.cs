namespace BusinessLayer.Services.AdminServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.AdminInterfaces;
    using ModelLayer.Models.AdminModels;
    using RepositoryLayer.Interfaces.AdminInterfaces;

    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
           this.adminRL = adminRL;
        }

        public string AdminLogin(AdminLoginModel loginModel)
        {
            try
            {
                return this.adminRL.AdminLogin(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
