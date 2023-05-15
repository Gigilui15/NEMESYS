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

            //Returning a repository depending on the environment
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddTransient<INEMESYSRepository, NemesysRepository>();
            }

            if (builder.Environment.IsProduction())
            {
                //This would be pointing to a different implementation of the repo
                builder.Services.AddTransient<INEMESYSRepository, NemesysRepository>();
            }


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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}