using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.DAL.Migrations
{
    public partial class InventoryJournalStockNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "TransactionDetails");

            migrationBuilder.CreateTable(
                name: "InventoryJournal",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    JournalCode = table.Column<string>(nullable: true),
                    JournalType = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    OnCart = table.Column<int>(nullable: false),
                    ProductInstanceId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_ProductInstance_ProductInstanceId",
                        column: x => x.ProductInstanceId,
                        principalTable: "ProductInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryJournalItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    InventoryJournalId = table.Column<long>(nullable: false),
                    ProductInstanceId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournalItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryJournalItem_InventoryJournal_InventoryJournalId",
                        column: x => x.InventoryJournalId,
                        principalTable: "InventoryJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryJournalItem_ProductInstance_ProductInstanceId",
                        column: x => x.ProductInstanceId,
                        principalTable: "ProductInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournalItem_InventoryJournalId",
                table: "InventoryJournalItem",
                column: "InventoryJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournalItem_ProductInstanceId",
                table: "InventoryJournalItem",
                column: "ProductInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductInstanceId",
                table: "Stock",
                column: "ProductInstanceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryJournalItem");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "InventoryJournal");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "TransactionDetails",
                nullable: true);
        }
    }
}
