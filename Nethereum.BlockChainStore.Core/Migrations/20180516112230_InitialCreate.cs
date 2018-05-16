using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Nethereum.BlockChainStore.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeBlock",
                columns: table => new
                {
                    NodeBlockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockHash = table.Column<string>(nullable: true),
                    BlockNumber = table.Column<int>(nullable: false),
                    BlockTime = table.Column<DateTime>(nullable: false),
                    Nonce = table.Column<string>(nullable: true),
                    ParentHash = table.Column<string>(nullable: true),
                    TransactionCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeBlock", x => x.NodeBlockID);
                });

            migrationBuilder.CreateTable(
                name: "NodeTransaction",
                columns: table => new
                {
                    NodeTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    From = table.Column<string>(nullable: true),
                    NodeBlockID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    To = table.Column<string>(nullable: true),
                    TxHash = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false, type:"decimal(28,10)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTransaction", x => x.NodeTransactionID);
                    table.ForeignKey(
                        name: "FK_NodeTransaction_NodeBlock_NodeBlockID",
                        column: x => x.NodeBlockID,
                        principalTable: "NodeBlock",
                        principalColumn: "NodeBlockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeTokenTransfer",
                columns: table => new
                {
                    NodeTokenTransferID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    NodeTransactionID = table.Column<int>(nullable: false),
                    To = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTokenTransfer", x => x.NodeTokenTransferID);
                    table.ForeignKey(
                        name: "FK_NodeTokenTransfer_NodeTransaction_NodeTransactionID",
                        column: x => x.NodeTransactionID,
                        principalTable: "NodeTransaction",
                        principalColumn: "NodeTransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeTokenTransfer_NodeTransactionID",
                table: "NodeTokenTransfer",
                column: "NodeTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_NodeTransaction_NodeBlockID",
                table: "NodeTransaction",
                column: "NodeBlockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeTokenTransfer");

            migrationBuilder.DropTable(
                name: "NodeTransaction");

            migrationBuilder.DropTable(
                name: "NodeBlock");
        }
    }
}
