using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.DAL.Migrations
{
    public partial class EditOptionValueField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValue_Options_OptionsId",
                table: "OptionValue");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "OptionValue");

            migrationBuilder.AlterColumn<long>(
                name: "OptionsId",
                table: "OptionValue",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValue_Options_OptionsId",
                table: "OptionValue",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValue_Options_OptionsId",
                table: "OptionValue");

            migrationBuilder.AlterColumn<long>(
                name: "OptionsId",
                table: "OptionValue",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "OptionId",
                table: "OptionValue",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValue_Options_OptionsId",
                table: "OptionValue",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
