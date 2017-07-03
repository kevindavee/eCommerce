using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class BankAccountNumberLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AccountNumber",
                table: "Bank",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Bank",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
