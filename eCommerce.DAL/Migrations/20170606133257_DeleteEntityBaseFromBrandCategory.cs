using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.DAL.Migrations
{
    public partial class DeleteEntityBaseFromBrandCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandAndCategory",
                table: "BrandAndCategory");

            migrationBuilder.DropIndex(
                name: "IX_BrandAndCategory_BrandId",
                table: "BrandAndCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BrandAndCategory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BrandAndCategory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BrandAndCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BrandAndCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BrandAndCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandAndCategory",
                table: "BrandAndCategory",
                columns: new[] { "BrandId", "CategoryId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandAndCategory",
                table: "BrandAndCategory");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "BrandAndCategory",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BrandAndCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BrandAndCategory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "BrandAndCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BrandAndCategory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandAndCategory",
                table: "BrandAndCategory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAndCategory_BrandId",
                table: "BrandAndCategory",
                column: "BrandId");
        }
    }
}
