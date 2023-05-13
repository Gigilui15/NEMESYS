using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NEMESYS.Models;
using System.Xml.Linq;

namespace NEMESYS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasNoKey();
            base.OnModelCreating(modelBuilder);
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
                    CategoryId = 1
                },
                new Report() {
                    Id = 2,
                    Title = "Hornet Nests Around Quad!",
                    Content = "Two hornet nests have been spotted under...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/quad.jpg",
                    CategoryId = 2
                },
                new Report()
                {
                    Id = 3,
                    Title = "AC Filters in the Faculty of ICT",
                    Content = "Numerous students have been noticing the quality of air in...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/ICT.jpg",
                    CategoryId = 3
                }
                );
        }
    }
}