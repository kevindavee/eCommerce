using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class UpdateStatusTransactionHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TransactionHeader",
                newName: "LastStatus");

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "TransactionHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "TransactionHeader");

            migrationBuilder.RenameColumn(
                name: "LastStatus",
                table: "TransactionHeader",
                newName: "Status");
        }
    }
}
