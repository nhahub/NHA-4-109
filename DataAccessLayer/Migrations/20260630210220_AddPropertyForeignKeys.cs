using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Admins_ManageAdminUsserId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Owner_OwnerUsserId",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "OwnerUsserId",
                table: "Property",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "ManageAdminUsserId",
                table: "Property",
                newName: "ManageAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_OwnerUsserId",
                table: "Property",
                newName: "IX_Property_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ManageAdminUsserId",
                table: "Property",
                newName: "IX_Property_ManageAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Admins_ManageAdminId",
                table: "Property",
                column: "ManageAdminId",
                principalTable: "Admins",
                principalColumn: "UsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Owner_OwnerId",
                table: "Property",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Admins_ManageAdminId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Owner_OwnerId",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Property",
                newName: "OwnerUsserId");

            migrationBuilder.RenameColumn(
                name: "ManageAdminId",
                table: "Property",
                newName: "ManageAdminUsserId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_OwnerId",
                table: "Property",
                newName: "IX_Property_OwnerUsserId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ManageAdminId",
                table: "Property",
                newName: "IX_Property_ManageAdminUsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Admins_ManageAdminUsserId",
                table: "Property",
                column: "ManageAdminUsserId",
                principalTable: "Admins",
                principalColumn: "UsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Owner_OwnerUsserId",
                table: "Property",
                column: "OwnerUsserId",
                principalTable: "Owner",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
