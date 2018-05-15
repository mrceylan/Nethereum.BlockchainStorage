using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class NodeRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IcoContract",
                columns: table => new
                {
                    IcoContractID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ABI = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ApprovalStatus = table.Column<int>(nullable: false),
                    CampaignID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcoContract", x => x.IcoContractID);
                });

            migrationBuilder.CreateTable(
                name: "NodeBlock",
                columns: table => new
                {
                    NodeBlockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockNumber = table.Column<int>(nullable: false),
                    BlockTime = table.Column<DateTime>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
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
                    AddressFrom = table.Column<string>(nullable: true),
                    AddressTo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    BlockNumber = table.Column<int>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
                    TokenID = table.Column<int>(nullable: false),
                    TxTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTransaction", x => x.NodeTransactionID);
                });

            migrationBuilder.CreateTable(
                name: "TokenContract",
                columns: table => new
                {
                    TokenContractID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ABI = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ApprovalStatus = table.Column<int>(nullable: false),
                    DecimalCount = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenContract", x => x.TokenContractID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcoContract");

            migrationBuilder.DropTable(
                name: "NodeBlock");

            migrationBuilder.DropTable(
                name: "NodeTransaction");

            migrationBuilder.DropTable(
                name: "TokenContract");
        }
    }
}
