namespace BusinessLayer.Services.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.UserInterfaces;
    using ModelLayer.Models.UserModels;
    using RepositoryLayer.Interfaces.UserInterfaces;

    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public List<GetAllUsersModel> GetAllUsers()
        {
            try
            {
                return this.userRL.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserRegistrationModel UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                return this.userRL.UserRegistration(registrationModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return this.userRL.UserLogin(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgotPassword(string EmailId)
        {
            try
            {
                return this.userRL.ForgotPassword(EmailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string EmailId,ResetPassModel passModel)
        {
            try
            {
                return this.userRL.ResetPassword(EmailId, passModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
