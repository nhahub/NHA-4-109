using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Tenant_ReiewerUsserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReiewerUsserId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReiewerUsserId",
                table: "Review");

            migrationBuilder.CreateIndex(
                name: "IX_Review_TenantId",
                table: "Review",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Tenant_TenantId",
                table: "Review",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "UsserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Tenant_TenantId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_TenantId",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "ReiewerUsserId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReiewerUsserId",
                table: "Review",
                column: "ReiewerUsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Tenant_ReiewerUsserId",
                table: "Review",
                column: "ReiewerUsserId",
                principalTable: "Tenant",
                principalColumn: "UsserId");
        }
    }
}
