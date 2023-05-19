using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NEMESYS.Data;
using NEMESYS.Models.Interfaces;
using NEMESYS.Models.Repositories;
using System;

namespace NEMESYS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Services configuration
            var builder = WebApplication.CreateBuilder(args);
            //Configures MVC services, including MvcCore, Authorization, Cors, Data annotations, response formatters, caching, views and razor view engine

            //This service could be varied by environment - passing different connection strings as required
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new
                    InvalidOperationException("Connection string for AppDbContext not found")));

            //These are only for illustration purposes only (only use what is required)
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

                // Password settings. (note - complexity not always == security -> consider passphrases)
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;

            });

            builder.Services.AddTransient<INEMESYSRepository, NemesysRepository>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //Request pipeline configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Custome error page
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}