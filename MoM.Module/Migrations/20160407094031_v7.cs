using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MoM.Module.Migrations
{
    public partial class v7 : Migration
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
                name: "NavigationMenuNavigationMenuItem",
                schema: "Core",
                columns: table => new
                {
                    NavigationMenuId = table.Column<int>(nullable: false),
                    NavigationMenuItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationMenuNavigationMenuItem", x => new { x.NavigationMenuId, x.NavigationMenuItemId });
                    table.ForeignKey(
                        name: "FK_NavigationMenuNavigationMenuItem_NavigationMenu_NavigationMenuId",
                        column: x => x.NavigationMenuId,
                        principalSchema: "Core",
                        principalTable: "NavigationMenu",
                        principalColumn: "NavigationMenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NavigationMenuNavigationMenuItem_NavigationMenuItem_NavigationMenuItemId",
                        column: x => x.NavigationMenuItemId,
                        principalSchema: "Core",
                        principalTable: "NavigationMenuItem",
                        principalColumn: "NavigationMenuItemId",
                        onDelete: ReferentialAction.Cascade);
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
            migrationBuilder.DropTable(name: "NavigationMenuNavigationMenuItem", schema: "Core");
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
