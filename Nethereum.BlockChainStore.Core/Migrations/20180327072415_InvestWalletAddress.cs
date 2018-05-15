using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class InvestWalletAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WalletID",
                table: "Investment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Investment_WalletID",
                table: "Investment",
                column: "WalletID");

            migrationBuilder.AddForeignKey(
                name: "FK_Investment_UserWallet_WalletID",
                table: "Investment",
                column: "WalletID",
                principalTable: "UserWallet",
                principalColumn: "UserWalletID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investment_UserWallet_WalletID",
                table: "Investment");

            migrationBuilder.DropIndex(
                name: "IX_Investment_WalletID",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "WalletID",
                table: "Investment");
        }
    }
}
