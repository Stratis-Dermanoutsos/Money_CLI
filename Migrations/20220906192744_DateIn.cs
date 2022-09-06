using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Money_CLI.Migrations
{
    public partial class DateIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_in",
                table: "Incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_in",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_in",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Date_in",
                table: "Expenses");
        }
    }
}
