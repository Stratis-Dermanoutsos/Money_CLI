using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Money_CLI.Migrations
{
    public partial class SeparatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Incomes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Incomes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Incomes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Expenses");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
