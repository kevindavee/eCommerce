using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class DeleteProductIdOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Product_ProductId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ProductId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "TransactionDetails",
                newName: "ProductInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_ProductInstanceId",
                table: "TransactionDetails",
                column: "ProductInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_ProductInstance_ProductInstanceId",
                table: "TransactionDetails",
                column: "ProductInstanceId",
                principalTable: "ProductInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_ProductInstance_ProductInstanceId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_ProductInstanceId",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "ProductInstanceId",
                table: "TransactionDetails",
                newName: "ProductId");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Options",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                table: "Options",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Product_ProductId",
                table: "Options",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
