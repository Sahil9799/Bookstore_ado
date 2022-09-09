namespace RepositoryLayer.Interfaces.AdminInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.AdminModels;

    public interface IAdminRL
    {
        public string AdminLogin (AdminLoginModel loginModel);
    }
}
