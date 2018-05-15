using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class Walletname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirdropAmount",
                table: "Campaign");

            migrationBuilder.AddColumn<string>(
                name: "WalletName",
                table: "UserWallet",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewUserID",
                table: "ReferenceDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OpsPerCurrency",
                table: "Pricing",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceDetail_NewUserID",
                table: "ReferenceDetail",
                column: "NewUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceDetail_AspNetUsers_NewUserID",
                table: "ReferenceDetail",
                column: "NewUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceDetail_AspNetUsers_NewUserID",
                table: "ReferenceDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReferenceDetail_NewUserID",
                table: "ReferenceDetail");

            migrationBuilder.DropColumn(
                name: "WalletName",
                table: "UserWallet");

            migrationBuilder.DropColumn(
                name: "OpsPerCurrency",
                table: "Pricing");

            migrationBuilder.AlterColumn<string>(
                name: "NewUserID",
                table: "ReferenceDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AirdropAmount",
                table: "Campaign",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
