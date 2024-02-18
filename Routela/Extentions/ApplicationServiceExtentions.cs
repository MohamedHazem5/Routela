using Microsoft.EntityFrameworkCore;
using Routela.DataAccess;
using Routela.DataAccess.Repository;
using Routela.DataAccess.Repository.IRepository;
using Routela.Services.IServices;
using Routela.Services;
using Routela.Settings;
using Microsoft.AspNetCore.Identity;
using Routela.Models;

namespace Routela.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
/*            using (var serviceProvider = services.BuildServiceProvider())
            {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.SeedData();
            }*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<RoleManager<AppRole>>();

            return services;
        }
    }
}