using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Migrations.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class FeaturesAndPermissionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Features_FeatureId",
                schema: "admin",
                table: "Permissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Features_FeatureId",
                schema: "admin",
                table: "Permissions",
                column: "FeatureId",
                principalSchema: "admin",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Features_FeatureId",
                schema: "admin",
                table: "Permissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Features_FeatureId",
                schema: "admin",
                table: "Permissions",
                column: "FeatureId",
                principalSchema: "admin",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
