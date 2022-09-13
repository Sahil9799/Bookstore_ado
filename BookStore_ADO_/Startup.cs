namespace BookStore_ADO_DatabaseFirst
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interfaces.AddressInterfaces;
    using BusinessLayer.Interfaces.AdminInterfaces;
    using BusinessLayer.Interfaces.BookInterfaces;
    using BusinessLayer.Interfaces.CartInterfaces;
    using BusinessLayer.Interfaces.FeedbackInterfaces;
    using BusinessLayer.Interfaces.OrderInterfaces;
    using BusinessLayer.Interfaces.UserInterfaces;
    using BusinessLayer.Interfaces.WishListInterfaces;
    using BusinessLayer.Services.AddressServices;
    using BusinessLayer.Services.AdminServices;
    using BusinessLayer.Services.BookServices;
    using BusinessLayer.Services.CartServices;
    using BusinessLayer.Services.FeedbackServices;
    using BusinessLayer.Services.OrderServices;
    using BusinessLayer.Services.UserServices;
    using BusinessLayer.Services.WishListServices;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using RepositoryLayer.Interfaces.AddressInterfaces;
    using RepositoryLayer.Interfaces.AdminInterfaces;
    using RepositoryLayer.Interfaces.BookInterfaces;
    using RepositoryLayer.Interfaces.CartInterfaces;
    using RepositoryLayer.Interfaces.FeedbackInterfaces;
    using RepositoryLayer.Interfaces.OrderInterfaces;
    using RepositoryLayer.Interfaces.UserInterfaces;
    using RepositoryLayer.Interfaces.WishListInterfaces;
    using RepositoryLayer.Services.AddressServices;
    using RepositoryLayer.Services.AdminServices;
    using RepositoryLayer.Services.BookServices;
    using RepositoryLayer.Services.CartServices;
    using RepositoryLayer.Services.FeedbackRL;
    using RepositoryLayer.Services.OrderServices;
    using RepositoryLayer.Services.UserServices;
    using RepositoryLayer.Services.WishListServices;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthorization(x =>
            {
                x.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                x.AddPolicy("RequireUsersRole", policy => policy.RequireRole("Users"));
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN")),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });

            services.AddSwaggerGen(
                setup =>
                {
                    // Include 'SecurityScheme' to use JWT Authentication
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme,
                        },
                    };
                    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() },
                    });
                });

            services.AddTransient<IUserRL, UserRL>();
            services.AddTransient<IUserBL, UserBL>();

            services.AddTransient<IAdminRL, AdminRL>();
            services.AddTransient<IAdminBL, AdminBL>();

            services.AddTransient<IBookRL, BookRL>();
            services.AddTransient<IBookBL, BookBL>();

            services.AddTransient<ICartRL, CartRL>();
            services.AddTransient<ICartBL, CartBL>();

            services.AddTransient<IWishListRL, WishListRL>();
            services.AddTransient<IWishListBL, WishListBL>();

            services.AddTransient<IAddressRL, AddressRL>();
            services.AddTransient<IAddressBL, AddressBL>();

            services.AddTransient<IOrderRL, OrderRL>();
            services.AddTransient<IOrderBL, OrderBL>();

            services.AddTransient<IFeedbackRL, FeedbackRL>();
            services.AddTransient<IFeedbackBL, FeedbackBL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " BookStore ");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
