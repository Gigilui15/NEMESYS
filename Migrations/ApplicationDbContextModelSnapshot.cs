﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEMESYS.Data;

#nullable disable

namespace NEMESYS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "b582190c-f9af-11ed-be56-0242ac120002",
                            ConcurrencyStamp = "1",
                            Name = "Reporter",
                            NormalizedName = "REP"
                        },
                        new
                        {
                            Id = "2e33b0ea-f9b0-11ed-be56-0242ac120002",
                            ConcurrencyStamp = "1",
                            Name = "Investigator",
                            NormalizedName = "INV"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                            RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            UserId = "1e0a2010-f9aa-11ed-be56-0242ac120002",
                            RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002",
                            RoleId = "2e33b0ea-f9b0-11ed-be56-0242ac120002"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NEMESYS.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ReporterAlias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "747e8b40-3bfc-4b96-b4e8-0cb2b05e5713",
                            Email = "investigator@mail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "INVESTIGATOR@MAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEPv0g6ZRLIbVwaXKsQBiP/FbFbwNb9IpLScXYUeNqj/fkp3kr2vFmPRbuLAgNTg5UA==",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            ReporterAlias = "Investigator A",
                            SecurityStamp = "e530e6ee-8dd8-44cd-8f14-4b5eb61f2eb8",
                            TwoFactorEnabled = false,
                            UserName = "investigator@mail.com"
                        },
                        new
                        {
                            Id = "1e0a2010-f9aa-11ed-be56-0242ac120002",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b607fbf8-7668-42b1-9eaf-fd88d46d5f4a",
                            Email = "investigator@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "INVESTIGATOR@GMAIL.COM",
                            NormalizedUserName = "INVESTIGATOR@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDb1z4oIWk0Ge+pilvGQWCdQz3D2c4nfh+wfpoVePbj+tB22z76u00NR3CXyc+Ysqw==",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            ReporterAlias = "Investigator B",
                            SecurityStamp = "0f74ea13-2a61-438d-85bc-5034d2827b68",
                            TwoFactorEnabled = false,
                            UserName = "investigator@gmail.com"
                        },
                        new
                        {
                            Id = "2f2e610c-f9ab-11ed-be56-0242ac120002",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4e7ad8d6-ab17-45fb-92c5-a1a1dde34464",
                            Email = "tester@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TESTER@GMAIL.COM",
                            NormalizedUserName = "TESTER@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEN5cv6wfFohfLwhUzcTzqv4CUQNZtjIMpZrODMy6cgMuyOpxyZl+hxErjqOnICO4aA==",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            ReporterAlias = "Tester",
                            SecurityStamp = "a7a327ce-7319-4d6c-b281-d11c7e86e815",
                            TwoFactorEnabled = false,
                            UserName = "tester@gmail.com"
                        });
                });

            modelBuilder.Entity("NEMESYS.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Accident"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Danger"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Health"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Assault"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Equipment"
                        },
                        new
                        {
                            Id = 6,
                            Name = "No Category"
                        });
                });

            modelBuilder.Entity("NEMESYS.Models.Investigation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Investigations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "LESA on the Way...",
                            CreatedDate = new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedDate = new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Spotted all of them and called pest removal...",
                            CreatedDate = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedDate = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Should have been cleaned last week however...",
                            CreatedDate = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedDate = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002"
                        });
                });

            modelBuilder.Entity("NEMESYS.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvestigationId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InvestigationId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Content = "Today at around 2.15pm a bumper-to-bumper incident caused a traffic jam...",
                            CreatedDate = new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4877),
                            ImageUrl = "/images/uom.jpg",
                            InvestigationId = 1,
                            Title = "Bumper-to-Bumper in RingRoad",
                            UpdatedDate = new DateTime(2023, 5, 29, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4910),
                            UserId = "2f2e610c-f9ab-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Content = "Two hornet nests have been spotted under...",
                            CreatedDate = new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4912),
                            ImageUrl = "/images/quad.jpg",
                            InvestigationId = 2,
                            Title = "Hornet Nests Around Quad!",
                            UpdatedDate = new DateTime(2023, 5, 28, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4914),
                            UserId = "19e2d6a8-f9aa-11ed-be56-0242ac120002"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Content = "Numerous students have been noticing the quality of air in...",
                            CreatedDate = new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4970),
                            ImageUrl = "/images/ICT.jpg",
                            InvestigationId = 3,
                            Title = "AC Filters in the Faculty of ICT",
                            UpdatedDate = new DateTime(2023, 5, 27, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4972),
                            UserId = "1e0a2010-f9aa-11ed-be56-0242ac120002"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NEMESYS.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NEMESYS.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NEMESYS.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NEMESYS.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NEMESYS.Models.Report", b =>
                {
                    b.HasOne("NEMESYS.Models.Category", "Category")
                        .WithMany("Reports")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NEMESYS.Models.Investigation", "Investigation")
                        .WithMany()
                        .HasForeignKey("InvestigationId");

                    b.HasOne("NEMESYS.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Investigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NEMESYS.Models.Category", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
