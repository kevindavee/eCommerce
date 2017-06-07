﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class EditFewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "TransactionHeader");

            migrationBuilder.AddColumn<string>(
                name: "NamaAlamat",
                table: "Alamat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ObjectId",
                table: "AspNetUsers",
                column: "ObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customer_ObjectId",
                table: "AspNetUsers",
                column: "ObjectId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customer_ObjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ObjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NamaAlamat",
                table: "Alamat");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "TransactionHeader",
                nullable: false,
                defaultValue: false);
        }
    }
}
