﻿using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Routela.DataAccess;
using Routela.Models;

namespace Routela.Extentions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
           IConfiguration config)
        {

            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            })
            .AddCookie("Identity.Application")
            .AddCookie("Identity.TwoFactorRememberMe")
            .AddCookie("Identity.TwoFactorUserId")
            .AddGoogle(options =>
            {
                options.ClientId = config["Authentication:Google:ClientId"];
                options.ClientSecret = config["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
            })
            .AddMicrosoftAccount(options =>
            {
                options.ClientId = config["Authentication:Microsoft:ClientId"];
                options.ClientSecret = config["Authentication:Microsoft:ClientSecret"];
                options.CallbackPath = "/signin-microsoft";
            })
            .AddCookie(IdentityConstants.ExternalScheme);



            return services;
        }
    }
}