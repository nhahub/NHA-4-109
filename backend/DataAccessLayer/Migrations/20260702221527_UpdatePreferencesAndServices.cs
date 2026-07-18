using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePreferencesAndServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Tenant_TenantID",
                table: "Preferences");

            migrationBuilder.RenameColumn(
                name: "TenantID",
                table: "Preferences",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Preferences_TenantID",
                table: "Preferences",
                newName: "IX_Preferences_TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Tenant_TenantId",
                table: "Preferences",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Tenant_TenantId",
                table: "Preferences");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Preferences",
                newName: "TenantID");

            migrationBuilder.RenameIndex(
                name: "IX_Preferences_TenantId",
                table: "Preferences",
                newName: "IX_Preferences_TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Tenant_TenantID",
                table: "Preferences",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
