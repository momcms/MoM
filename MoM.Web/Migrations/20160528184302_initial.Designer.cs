using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MoM.Module.Models;

namespace MoM.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160528184302_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("RoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("Role","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MoM.Blog.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UrlSlug");

                    b.HasKey("CategoryId");

                    b.ToTable("Category","Blog");
                });

            modelBuilder.Entity("MoM.Blog.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<int>("IsPublished");

                    b.Property<string>("Meta");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<DateTime>("PostedDate");

                    b.Property<string>("Teaser");

                    b.Property<string>("Title");

                    b.Property<string>("UrlSlug");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Post","Blog");
                });

            modelBuilder.Entity("MoM.Blog.Models.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag","Blog");
                });

            modelBuilder.Entity("MoM.Blog.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UrlSlug");

                    b.HasKey("TagId");

                    b.ToTable("Tag","Blog");
                });

            modelBuilder.Entity("MoM.CMS.Models.NavigationMenu", b =>
                {
                    b.Property<int>("NavigationMenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("IconClass")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("NavigationMenuId");

                    b.ToTable("NavigationMenu","Core");
                });

            modelBuilder.Entity("MoM.CMS.Models.NavigationMenuItem", b =>
                {
                    b.Property<int>("NavigationMenuItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("IconClass")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("NavigationMenuId");

                    b.Property<int?>("ParentNavigationMenuItemId");

                    b.Property<string>("RouterLink");

                    b.Property<int>("SortOrder");

                    b.HasKey("NavigationMenuItemId");

                    b.HasIndex("NavigationMenuId");

                    b.HasIndex("ParentNavigationMenuItemId");

                    b.ToTable("NavigationMenuItem","Core");
                });

            modelBuilder.Entity("MoM.Module.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("User","Identity");
                });

            modelBuilder.Entity("MoM.Module.Models.ClientRouteConfig", b =>
                {
                    b.Property<int>("ClientRouteConfigId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Component")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("IconClass")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("ImportPath")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("ParentClientRouteConfigId");

                    b.Property<string>("Path")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<int>("SortOrder");

                    b.Property<int>("Type");

                    b.Property<bool>("UseAsDefault");

                    b.HasKey("ClientRouteConfigId");

                    b.HasIndex("ParentClientRouteConfigId");

                    b.ToTable("ClientRouteConfig","Core");
                });

            modelBuilder.Entity("MoM.Module.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsInstalled");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<int>("VersionMajor");

                    b.Property<int>("VersionMinor");

                    b.HasKey("ModuleId");

                    b.ToTable("Module","Core");
                });

            modelBuilder.Entity("MoM.Tutorial.Models.HelloPlanet", b =>
                {
                    b.Property<int>("HelloPlanetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("HelloPlanetId");

                    b.ToTable("HelloPlanet","Tutorial");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MoM.Blog.Models.Post", b =>
                {
                    b.HasOne("MoM.Blog.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("MoM.Blog.Models.PostTag", b =>
                {
                    b.HasOne("MoM.Blog.Models.Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoM.Blog.Models.Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MoM.CMS.Models.NavigationMenuItem", b =>
                {
                    b.HasOne("MoM.CMS.Models.NavigationMenu")
                        .WithMany()
                        .HasForeignKey("NavigationMenuId");

                    b.HasOne("MoM.CMS.Models.NavigationMenuItem")
                        .WithMany()
                        .HasForeignKey("ParentNavigationMenuItemId");
                });

            modelBuilder.Entity("MoM.Module.Models.ClientRouteConfig", b =>
                {
                    b.HasOne("MoM.Module.Models.ClientRouteConfig")
                        .WithMany()
                        .HasForeignKey("ParentClientRouteConfigId");
                });
        }
    }
}
