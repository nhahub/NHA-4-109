using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Property_ReviewdPropertyProperyID",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReviewdPropertyProperyID",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "ReviewdPropertyProperyID",
                table: "Review",
                newName: "TenantId");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_PropertyId",
                table: "Review",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Property_PropertyId",
                table: "Review",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "ProperyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Property_PropertyId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_PropertyId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Review",
                newName: "ReviewdPropertyProperyID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewdPropertyProperyID",
                table: "Review",
                column: "ReviewdPropertyProperyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Property_ReviewdPropertyProperyID",
                table: "Review",
                column: "ReviewdPropertyProperyID",
                principalTable: "Property",
                principalColumn: "ProperyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
