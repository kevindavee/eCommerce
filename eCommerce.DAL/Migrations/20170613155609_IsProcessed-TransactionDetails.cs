using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class IsProcessedTransactionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "TransactionDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "TransactionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "TransactionDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "TransactionDetails",
                nullable: false,
                defaultValue: false);
        }
    }
}
