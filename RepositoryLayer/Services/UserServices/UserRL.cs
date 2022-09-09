namespace RepositoryLayer.Services.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ModelLayer.Models.UserModels;
    using RepositoryLayer.Interfaces.UserInterfaces;

    public class UserRL : IUserRL
    {
        private readonly string connectionString;

        public UserRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreCon");
        }

        //Method to register User 
        public UserRegistrationModel UserRegistration(UserRegistrationModel registrationModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            string Password = EncryptPassword(registrationModel.Password);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("UserRegistrationSP", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", registrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", registrationModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@MobileNo", registrationModel.MobileNo);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return registrationModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        //Method to Get Records of All Users
        public List<GetAllUsersModel> GetAllUsers()
        {
            List<GetAllUsersModel> listOfUsers = new List<GetAllUsersModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllUsersSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetAllUsersModel User = new GetAllUsersModel();
                        User.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        User.FullName = Convert.ToString(reader["FullName"]);
                        User.EmailId = Convert.ToString(reader["EmailId"]);
                        string Password=Convert.ToString(reader["Password"]);
                        User.Password = DecryptPassword(Password);
                        User.MobileNo = Convert.ToInt64(reader["MobileNo"]);
                        listOfUsers.Add(User);
                    }

                    return listOfUsers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Creating Method for UserLogin
        public string UserLogin(UserLoginModel loginModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string Password = EncryptPassword(loginModel.Password);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UserLoginSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAllUsersModel response = new GetAllUsersModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        response.EmailId = reader["EmailId"] == DBNull.Value ? default : reader.GetString("EmailId");
                        response.Password = reader["Password"] == DBNull.Value ? default : reader.GetString("Password");
                    }

                    return GenerateJWTToken(response.EmailId, response.UserId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Method to Generate JWT Token for Authentication and Athorization when User Login Sucessful
        private string GenerateJWTToken(string emailId, int userId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Users"),
                        new Claim("EmailId", emailId),
                        new Claim("UserId",userId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Method to send a Reset password link through mail using MSMQ (ForgotPassword)
        public bool ForgotPassword(string EmailId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("ForgotPasswordSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAllUsersModel response = new GetAllUsersModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        response.EmailId = reader["EmailId"] == DBNull.Value ? default : reader.GetString("EmailId");
                        response.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                    }

                    if(response.UserId == null || response.FullName == null || response.EmailId == null)
                    {
                        return false;
                    }

                    MessageQueue messageQueue;
                    //add message to queue
                    if (MessageQueue.Exists(@".\private$\BookStoreQueue"))
                    {
                        messageQueue = new MessageQueue(@".\private$\BookStoreQueue");
                    }
                    else
                    {
                        messageQueue = MessageQueue.Create(@".\private$\BookStoreQueue");
                    }

                    Message Mymessage = new Message();
                    Mymessage.Formatter = new BinaryMessageFormatter();
                    Mymessage.Body = this.GenerateToken(EmailId);
                    Mymessage.Label = "Forgot Password Email";
                    messageQueue.Send(Mymessage);

                    Message msg = messageQueue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(EmailId, msg.Body.ToString(), response.FullName);
                    messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmQueue_ReceiveCompleted);
                    messageQueue.BeginReceive();
                    messageQueue.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void msmQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access Denied!!" + "Queue might be system queue...");
                }
            }
        }

        private string GenerateToken(string emailId)
        { //generate token
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("EmailId", emailId),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(tokenKey),
                                        SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Method to ResetPssword using token received in mail
        public bool ResetPassword(string EmailId, ResetPassModel passModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string newPassword = EncryptPassword(passModel.NewPassword);
            string confirmPassword = EncryptPassword(passModel.ConfirmPassword); 
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("ResetPasswordSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    var result = 0;
                    if (newPassword == confirmPassword)
                    {
                        result = cmd.ExecuteNonQuery();
                    }

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // method to Encrypt Password
        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] x = Encoding.ASCII.GetBytes(Password);
                    string encryptedpass = Convert.ToBase64String(x);
                    return encryptedpass;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method to Decrypt Encrypted password
        public static string DecryptPassword(string encryptedPass)
        {
            byte[] x;
            string decrypted;
            try
            {
                if (encryptedPass == null)
                {
                    return null;
                }
                else
                {
                    x = Convert.FromBase64String(encryptedPass);
                    decrypted = Encoding.ASCII.GetString(x);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
