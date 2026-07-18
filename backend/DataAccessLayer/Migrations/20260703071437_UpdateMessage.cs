using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimewStamp",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Messages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadDate",
                table: "Messages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "TimeStamp",
                table: "Review",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "DATEDIFF(SECOND, [ReciveDate], [ReadDate]) / 3600.0",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Review");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimewStamp",
                table: "Review",
                type: "time",
                nullable: false,
                computedColumnSql: "DATEDIFF(SECOND, [ReciveDate], [ReadDate]) / 3600.0",
                stored: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeStamp",
                table: "Messages",
                type: "time",
                nullable: false,
                computedColumnSql: "DATEDIFF(SECOND, [ReciveDate], [ReadDate]) / 3600.0",
                stored: true);
        }
    }
}
