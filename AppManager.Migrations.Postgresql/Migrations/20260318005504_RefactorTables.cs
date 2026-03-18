using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppManager.Migrations.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                schema: "admin",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_UserId",
                schema: "admin",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                schema: "admin",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "admin",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "admin",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "admin",
                table: "RolePermissions");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "admin",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "admin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "admin",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                schema: "admin",
                table: "UserPermissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GuardName",
                schema: "admin",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "admin",
                table: "Roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "GrantedAt",
                schema: "admin",
                table: "RolePermissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "admin",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardName",
                schema: "admin",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "admin",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                schema: "admin",
                table: "UserPermissions",
                columns: new[] { "UserId", "PermissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                schema: "admin",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });
            
            
            migrationBuilder.Sql("""
                                     INSERT INTO admin."Tenants" ("Id","Name","Slug")
                                     VALUES (0, 'Default Tenant', 'default')
                                     ON CONFLICT DO NOTHING;
                                 """);


            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "admin",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "admin",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Resource = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "admin",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "admin",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantId",
                schema: "admin",
                table: "Roles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TenantId",
                schema: "admin",
                table: "AuditLogs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                schema: "admin",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "admin",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Tenants_TenantId",
                schema: "admin",
                table: "Roles",
                column: "TenantId",
                principalSchema: "admin",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                schema: "admin",
                table: "Users",
                column: "TenantId",
                principalSchema: "admin",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Tenants_TenantId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "admin");

            migrationBuilder.DropIndex(
                name: "IX_Users_TenantId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                schema: "admin",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_Roles_TenantId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                schema: "admin",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                schema: "admin",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "GuardName",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "GrantedAt",
                schema: "admin",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "admin",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "GuardName",
                schema: "admin",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "admin",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "admin",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "admin",
                table: "UserPermissions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "admin",
                table: "RolePermissions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                schema: "admin",
                table: "UserPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                schema: "admin",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "admin",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                schema: "admin",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "admin",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "admin",
                table: "Users",
                column: "RoleId",
                principalSchema: "admin",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
