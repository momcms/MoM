using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MoM.Module.Models;

namespace MoM.Module.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160407063314_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("Relational:ColumnName", "RoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "Role");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "UserRole");
                });

            modelBuilder.Entity("MoM.Blog.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UrlSlug");

                    b.HasKey("CategoryId");

                    b.HasAnnotation("Relational:Schema", "Blog");

                    b.HasAnnotation("Relational:TableName", "Category");
                });

            modelBuilder.Entity("MoM.Blog.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryCategoryId");

                    b.Property<string>("Content");

                    b.Property<int>("IsPublished");

                    b.Property<string>("Meta");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<DateTime>("PostedDate");

                    b.Property<string>("Teaser");

                    b.Property<string>("Title");

                    b.Property<string>("UrlSlug");

                    b.HasKey("PostId");

                    b.HasAnnotation("Relational:Schema", "Blog");

                    b.HasAnnotation("Relational:TableName", "Post");
                });

            modelBuilder.Entity("MoM.Blog.Models.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasAnnotation("Relational:Schema", "Blog");

                    b.HasAnnotation("Relational:TableName", "PostTag");
                });

            modelBuilder.Entity("MoM.Blog.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UrlSlug");

                    b.HasKey("TagId");

                    b.HasAnnotation("Relational:Schema", "Blog");

                    b.HasAnnotation("Relational:TableName", "Tag");
                });

            modelBuilder.Entity("MoM.Module.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("Relational:ColumnName", "UserId");

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
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:Schema", "Identity");

                    b.HasAnnotation("Relational:TableName", "User");
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

                    b.HasAnnotation("Relational:Schema", "Core");

                    b.HasAnnotation("Relational:TableName", "ClientRouteConfig");
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

                    b.HasAnnotation("Relational:Schema", "Core");

                    b.HasAnnotation("Relational:TableName", "Module");
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

                    b.HasAnnotation("Relational:Schema", "Tutorial");

                    b.HasAnnotation("Relational:TableName", "HelloPlanet");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("MoM.Module.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MoM.Blog.Models.Post", b =>
                {
                    b.HasOne("MoM.Blog.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryCategoryId");
                });

            modelBuilder.Entity("MoM.Blog.Models.PostTag", b =>
                {
                    b.HasOne("MoM.Blog.Models.Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.HasOne("MoM.Blog.Models.Tag")
                        .WithMany()
                        .HasForeignKey("TagId");
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
