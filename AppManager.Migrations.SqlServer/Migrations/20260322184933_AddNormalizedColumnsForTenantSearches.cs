using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedColumnsForTenantSearches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedNameAndSlug",
                schema: "admin",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "UPPER(COALESCE([Name], '') + ' ' + COALESCE([Slug], ''))",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_NormalizedNameAndSlug",
                schema: "admin",
                table: "Tenants",
                column: "NormalizedNameAndSlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_NormalizedNameAndSlug",
                schema: "admin",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "NormalizedNameAndSlug",
                schema: "admin",
                table: "Tenants");
        }
    }
}
