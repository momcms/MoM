using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace MoM.Module.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", schema: "Identity", table: "RoleClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", schema: "Identity", table: "UserClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", schema: "Identity", table: "UserLogin");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", schema: "Identity", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", schema: "Identity", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_PostTag_Post_PostId", schema: "Blog", table: "PostTag");
            migrationBuilder.DropForeignKey(name: "FK_PostTag_Tag_TagId", schema: "Blog", table: "PostTag");
            migrationBuilder.CreateTable(
                name: "NavigationMenu",
                schema: "Core",
                columns: table => new
                {
                    NavigationMenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationMenu", x => x.NavigationMenuId);
                });
            migrationBuilder.CreateTable(
                name: "NavigationMenuItem",
                schema: "Core",
                columns: table => new
                {
                    NavigationMenuItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentNavigationMenuItemId = table.Column<int>(nullable: true),
                    RouterLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationMenuItem", x => x.NavigationMenuItemId);
                    table.ForeignKey(
                        name: "FK_NavigationMenuItem_NavigationMenuItem_ParentNavigationMenuItemId",
                        column: x => x.ParentNavigationMenuItemId,
                        principalSchema: "Core",
                        principalTable: "NavigationMenuItem",
                        principalColumn: "NavigationMenuItemId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                schema: "Identity",
                table: "RoleClaim",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Post_PostId",
                schema: "Blog",
                table: "PostTag",
                column: "PostId",
                principalSchema: "Blog",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                schema: "Blog",
                table: "PostTag",
                column: "TagId",
                principalSchema: "Blog",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", schema: "Identity", table: "RoleClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", schema: "Identity", table: "UserClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", schema: "Identity", table: "UserLogin");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", schema: "Identity", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", schema: "Identity", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_PostTag_Post_PostId", schema: "Blog", table: "PostTag");
            migrationBuilder.DropForeignKey(name: "FK_PostTag_Tag_TagId", schema: "Blog", table: "PostTag");
            migrationBuilder.DropTable(name: "NavigationMenu", schema: "Core");
            migrationBuilder.DropTable(name: "NavigationMenuItem", schema: "Core");
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                schema: "Identity",
                table: "RoleClaim",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                schema: "Identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Post_PostId",
                schema: "Blog",
                table: "PostTag",
                column: "PostId",
                principalSchema: "Blog",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                schema: "Blog",
                table: "PostTag",
                column: "TagId",
                principalSchema: "Blog",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
