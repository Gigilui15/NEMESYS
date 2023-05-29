using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NEMESYS.Models;
using System.Xml.Linq;

namespace NEMESYS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Investigation> Investigations { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed Admin User
            ApplicationUser investigatorA = new ApplicationUser()
            {
                //Used GUID Generator: https://www.uuidgenerator.net/guid
                Id = "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                UserName = "investigator@mail.com",
                NormalizedUserName = "INVESTIGATOR@MAIL.COM",
                Email = "investigator@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                ReporterAlias = "Investigator A",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = ""
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            investigatorA.PasswordHash = passwordHasher.HashPassword(investigatorA, "S@fePassw0rd1"); //make sure you adhere to policies (incl confirmed etc…)
            modelBuilder.Entity<ApplicationUser>().HasData(investigatorA);

            ApplicationUser investigatorB = new ApplicationUser()
            {
                Id = "1e0a2010-f9aa-11ed-be56-0242ac120002",
                UserName = "investigator@gmail.com",
                NormalizedUserName = "INVESTIGATOR@GMAIL.COM",
                Email = "investigator@gmail.com",
                NormalizedEmail = "INVESTIGATOR@GMAIL.COM",
                ReporterAlias = "Investigator B",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = ""
            };
            investigatorB.PasswordHash = passwordHasher.HashPassword(investigatorB, "S@feRPassw0rd42"); //make sure you adhere to policies (incl confirmed etc…)
            modelBuilder.Entity<ApplicationUser>().HasData(investigatorB);

            ApplicationUser investigatorC = new ApplicationUser()
            {
                Id = "2f2e610c-f9ab-11ed-be56-0242ac120002",
                UserName = "tester@gmail.com",
                NormalizedUserName = "TESTER@GMAIL.COM",
                Email = "tester@gmail.com",
                NormalizedEmail = "TESTER@GMAIL.COM",
                ReporterAlias = "Tester",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = ""
            };
            investigatorC.PasswordHash = passwordHasher.HashPassword(investigatorC, "S@fe$tPassw0rd129"); //make sure you adhere to policies (incl confirmed etc…)
            modelBuilder.Entity<ApplicationUser>().HasData(investigatorC);
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
                },
                new Category()
                {
                    Id = 4,
                    Name = "Assault"
                }, 
                new Category()
                {
                    Id = 5,
                    Name = "Equipment"
                },
                new Category()
                {
                    Id = 6,
                    Name = "No Category"
                }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status
                { 
                    Id = 1,
                    //Awaiting Investigator
                    Title = "Open"
                },
                new Status
                {
                    Id = 2,
                    //Being Investigated or Has been Investigated
                    Title = "In Progress"
                },
                new Status
                {
                    Id = 3,
                    //Solved
                    Title = "Closed"
                },
                new Status
                {
                    Id = 4,
                    //Not Solved
                    Title = "Not Solved"
                }
                );

            //Creating the Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "b582190c-f9af-11ed-be56-0242ac120002",
                    Name = "Reporter",
                    NormalizedName = "REP",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "2e33b0ea-f9b0-11ed-be56-0242ac120002",
                    Name = "Investigator",
                    NormalizedName = "INV",
                    ConcurrencyStamp = "1"
                }
                );

            //Assigning Investigator Roles to Seed Investigators
            modelBuilder.Entity<IdentityUserRole<string>>().HasData
                (new IdentityUserRole<string>()
                {
                    //Investigator Role
                    RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002",
                    //Investigator A's ID
                    UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                },
                new IdentityUserRole<string>()
                {
                    //Investigator Role
                    RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002",
                    //Investigator B's ID
                    UserId = "1e0a2010-f9aa-11ed-be56-0242ac120002"
                },
                new IdentityUserRole<string>()
                {
                    //Investigator Role
                    RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002",
                    //Tester's ID
                    UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002"
                }
                );

            modelBuilder.Entity<Report>().HasData(
                new Report()
                {
                    Id = 1,
                    Title = "Bumper-to-Bumper in RingRoad",
                    Content = "Today at around 2.15pm a bumper-to-bumper incident caused a traffic jam...",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.UtcNow,
                    ImageUrl = "/images/uom.jpg",
                    CategoryId = 1,
                    UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002",
                    InvestigationId = 1,
                    StatusId = 1
                },
                new Report()
                {
                    Id = 2,
                    Title = "Hornet Nests Around Quad!",
                    Content = "Two hornet nests have been spotted under...",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.UtcNow.AddDays(-1),
                    ImageUrl = "/images/quad.jpg",
                    CategoryId = 2,
                    UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                    InvestigationId = 2,
                    StatusId = 2
                },
                new Report()
                {
                    Id = 3,
                    Title = "AC Filters in the Faculty of ICT",
                    Content = "Numerous students have been noticing the quality of air in...",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.UtcNow.AddDays(-2),
                    ImageUrl = "/images/ICT.jpg",
                    CategoryId = 3,
                    UserId = "1e0a2010-f9aa-11ed-be56-0242ac120002",
                    InvestigationId = 3,
                    StatusId = 3
                }
                );

            modelBuilder.Entity<Investigation>().HasData(
                 new Investigation
                 {
                     Id = 1,
                     Content = "LESA on the Way...",
                     CreatedDate = new DateTime(2023, 3, 15),
                     UpdatedDate = new DateTime(2023, 3, 15),
                     UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                 },
                new Investigation
                {
                    Id = 2,
                    Content = "Spotted all of them and called pest removal...",
                    CreatedDate = new DateTime(2023, 4, 1),
                    UpdatedDate = new DateTime(2023, 4, 1),
                    UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                },
                new Investigation
                {
                    Id = 3,
                    Content = "Should have been cleaned last week however...",
                    CreatedDate = new DateTime(2023, 5, 1),
                    UpdatedDate = new DateTime(2023, 5, 1),
                    UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002"
                }
                );
        }
    }
}