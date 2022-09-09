namespace RepositoryLayer.Services.AdminServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ModelLayer.Models.AdminModels;
    using RepositoryLayer.Interfaces.AdminInterfaces;

    public class AdminRL : IAdminRL
    {
        private readonly string connectionString;

        public AdminRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreCon");
        }

        // Method to Login Admin
        public string AdminLogin(AdminLoginModel loginModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("AdminLoginSP", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminEmailId", loginModel.AdminEmailId);
                    cmd.Parameters.AddWithValue("@AdminPassword", loginModel.AdminPassword);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    AdminResponseModel response = new AdminResponseModel();
                    if (reader.Read())
                    {
                        response.AdminId = reader["AdminId"] == DBNull.Value ? default : reader.GetInt32("AdminId");
                        response.AdminEmailId = reader["AdminEmailId"] == DBNull.Value ? default : reader.GetString("AdminEmailId");
                        response.AdminPassword = reader["AdminPassword"] == DBNull.Value ? default : reader.GetString("AdminPassword");
                    }

                    return GenerateJWTToken_Admin(response.AdminEmailId, response.AdminId);
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

        //Method to Generate JWT Token for Authentication and Athorization when Admin Login Sucessful
        private string GenerateJWTToken_Admin(string adminEmailId, int adminId)
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
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("AdminEmailId", adminEmailId),
                        new Claim("AdminId",adminId.ToString()),
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
    }
}
