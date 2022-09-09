namespace BusinessLayer.Interfaces.AdminInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.AdminModels;

    public interface IAdminBL
    {
        public string AdminLogin(AdminLoginModel loginModel);

    }
}
