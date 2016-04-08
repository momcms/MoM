using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace MoM.Module.Migrations
{
    public partial class v4 : Migration
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
            migrationBuilder.DropTable(name: "SiteSetting", schema: "Core");
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "Core",
                table: "ClientRouteConfig",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "IconClass",
                schema: "Core",
                table: "ClientRouteConfig",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "ParentClientRouteConfigId",
                schema: "Core",
                table: "ClientRouteConfig",
                nullable: true);
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
            migrationBuilder.AddForeignKey(
                name: "FK_ClientRouteConfig_ClientRouteConfig_ParentClientRouteConfigId",
                schema: "Core",
                table: "ClientRouteConfig",
                column: "ParentClientRouteConfigId",
                principalSchema: "Core",
                principalTable: "ClientRouteConfig",
                principalColumn: "ClientRouteConfigId",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.DropForeignKey(name: "FK_ClientRouteConfig_ClientRouteConfig_ParentClientRouteConfigId", schema: "Core", table: "ClientRouteConfig");
            migrationBuilder.DropColumn(name: "DisplayName", schema: "Core", table: "ClientRouteConfig");
            migrationBuilder.DropColumn(name: "IconClass", schema: "Core", table: "ClientRouteConfig");
            migrationBuilder.DropColumn(name: "ParentClientRouteConfigId", schema: "Core", table: "ClientRouteConfig");
            migrationBuilder.CreateTable(
                name: "SiteSetting",
                schema: "Core",
                columns: table => new
                {
                    SiteSettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogoHeight = table.Column<int>(nullable: false),
                    LogoImagePath = table.Column<string>(nullable: true),
                    LogoSvgPath = table.Column<string>(nullable: true),
                    LogoWidth = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SelectedThemeInModule = table.Column<string>(nullable: true),
                    SelectedThemeModuleModuleId = table.Column<int>(nullable: true),
                    UseLogo = table.Column<bool>(nullable: false),
                    UseSvgLogo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSetting", x => x.SiteSettingId);
                    table.ForeignKey(
                        name: "FK_SiteSetting_Module_SelectedThemeModuleModuleId",
                        column: x => x.SelectedThemeModuleModuleId,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModuleId",
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
