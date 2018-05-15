using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class CommitmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TxID",
                table: "Investment",
                newName: "TransactionID");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionTime",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Commitment",
                columns: table => new
                {
                    CommitmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    CampaignID = table.Column<int>(nullable: false),
                    CommitmentTime = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    WalletID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commitment", x => x.CommitmentID);
                    table.ForeignKey(
                        name: "FK_Commitment_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commitment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commitment_UserWallet_WalletID",
                        column: x => x.WalletID,
                        principalTable: "UserWallet",
                        principalColumn: "UserWalletID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_CampaignID",
                table: "Commitment",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_UserID",
                table: "Commitment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_WalletID",
                table: "Commitment",
                column: "WalletID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commitment");

            migrationBuilder.DropColumn(
                name: "TransactionTime",
                table: "Investment");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Investment",
                newName: "TxID");
        }
    }
}
