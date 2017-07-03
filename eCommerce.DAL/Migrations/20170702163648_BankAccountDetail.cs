using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class BankAccountDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountHolder",
                table: "Bank",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "Bank",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountHolder",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Bank");
        }
    }
}
