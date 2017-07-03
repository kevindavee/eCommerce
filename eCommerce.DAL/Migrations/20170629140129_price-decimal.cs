using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class pricedecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductInstance",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductInstance",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
