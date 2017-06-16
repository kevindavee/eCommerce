using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class isValidKonfirmasiPembayaran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "KonfirmasiPembayaran");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "KonfirmasiPembayaran",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "KonfirmasiPembayaran");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KonfirmasiPembayaran",
                nullable: true);
        }
    }
}
