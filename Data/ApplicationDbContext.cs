using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NEMESYS.Models;
using System.Xml.Linq;

namespace NEMESYS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //IMPORTANT: Required to setup the schema for the identity framework
            base.OnModelCreating(modelBuilder);

            //Seed Admin User
            ApplicationUser user = new ApplicationUser()
            {
                //Generated with: https://www.uuidgenerator.net/guid
                Id = "55c3f668-f76e-11ed-b67e-0242ac120002",
                UserName = "investigator@mail.com", 
                NormalizedUserName = "INVESTIGATOR@MAIL.COM ",
                Email = "investigator@mail.com",
                NormalizedEmail = "INVESTIGATOR@MAIL.COM",
                AuthorAlias = "InvestigatorUser",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = ""
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "S@fePassw0rd1"); //make sure you adhere to policies (incl confirmed etc…)
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Reporter", NormalizedName = "REP" }, new IdentityRole { Id = "2", Name = "Investigator", NormalizedName = "INV"});


            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Accident"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Danger"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Health"
                }
            );

            modelBuilder.Entity<Report>().HasData(
                new Report()
                {
                    Id = 1,
                    Title = "Bumper-to-Bumper in RingRoad",
                    Content = "Today at around 2.15pm a bumper-to-bumper incident caused a traffic jam...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/uom.jpg",
                    CategoryId = 1,
                    UserId = "55c3f668-f76e-11ed-b67e-0242ac120002"
                },
                new Report()
                {
                    Id = 2,
                    Title = "Hornet Nests Around Quad!",
                    Content = "Two hornet nests have been spotted under...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/quad.jpg",
                    CategoryId = 2,
                    UserId = "55c3f668-f76e-11ed-b67e-0242ac120002"
                },
                new Report()
                {
                    Id = 3,
                    Title = "AC Filters in the Faculty of ICT",
                    Content = "Numerous students have been noticing the quality of air in...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/ICT.jpg",
                    CategoryId = 3,
                    UserId = "55c3f668-f76e-11ed-b67e-0242ac120002"
                }
            );
        }
    }
}