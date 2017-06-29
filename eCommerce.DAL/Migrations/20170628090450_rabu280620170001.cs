using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class rabu280620170001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_TransactionHeaderId",
                table: "ShippingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_TransactionHeaderId",
                table: "ShippingDetails",
                column: "TransactionHeaderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_TransactionHeaderId",
                table: "ShippingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_TransactionHeaderId",
                table: "ShippingDetails",
                column: "TransactionHeaderId");
        }
    }
}
