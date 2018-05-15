using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class AddNodeTransactionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultToken",
                table: "TokenContract");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "NodeTransaction");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "NodeTransaction");

            migrationBuilder.RenameColumn(
                name: "TokenContractID",
                table: "NodeTransaction",
                newName: "BlockOrderNo");

            migrationBuilder.RenameColumn(
                name: "AddressTo",
                table: "NodeTransaction",
                newName: "WalletAddress");

            migrationBuilder.RenameColumn(
                name: "AddressFrom",
                table: "NodeTransaction",
                newName: "ContractAddress");

            migrationBuilder.CreateTable(
                name: "NodeTransactionDetail",
                columns: table => new
                {
                    NodeTransactionDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false, type: "decimal(28,8)"),
                    NodeTransactionId = table.Column<int>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    TokenContractID = table.Column<int>(nullable: false),
                    TransactionDirection = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTransactionDetail", x => x.NodeTransactionDetailId);
                    table.ForeignKey(
                        name: "FK_NodeTransactionDetail_NodeTransaction_NodeTransactionId",
                        column: x => x.NodeTransactionId,
                        principalTable: "NodeTransaction",
                        principalColumn: "NodeTransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeTransactionDetail_NodeTransactionId",
                table: "NodeTransactionDetail",
                column: "NodeTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeTransactionDetail");

            migrationBuilder.RenameColumn(
                name: "WalletAddress",
                table: "NodeTransaction",
                newName: "AddressTo");

            migrationBuilder.RenameColumn(
                name: "ContractAddress",
                table: "NodeTransaction",
                newName: "AddressFrom");

            migrationBuilder.RenameColumn(
                name: "BlockOrderNo",
                table: "NodeTransaction",
                newName: "TokenContractID");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultToken",
                table: "TokenContract",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "NodeTransaction",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "NodeTransaction",
                nullable: false,
                defaultValue: 0);
        }
    }
}
