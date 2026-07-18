using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Owner_OwnerUsserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Tenant_tentantUsserId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_tentantUsserId",
                table: "Messages",
                newName: "IX_Messages_tentantUsserId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_OwnerUsserId",
                table: "Messages",
                newName: "IX_Messages_OwnerUsserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Owner_OwnerUsserId",
                table: "Messages",
                column: "OwnerUsserId",
                principalTable: "Owner",
                principalColumn: "UsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Tenant_tentantUsserId",
                table: "Messages",
                column: "tentantUsserId",
                principalTable: "Tenant",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Owner_OwnerUsserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Tenant_tentantUsserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_tentantUsserId",
                table: "Message",
                newName: "IX_Message_tentantUsserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_OwnerUsserId",
                table: "Message",
                newName: "IX_Message_OwnerUsserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Owner_OwnerUsserId",
                table: "Message",
                column: "OwnerUsserId",
                principalTable: "Owner",
                principalColumn: "UsserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Tenant_tentantUsserId",
                table: "Message",
                column: "tentantUsserId",
                principalTable: "Tenant",
                principalColumn: "UsserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
