using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class createdUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "WhiteListMember",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "Pricing",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "CampaignUpdate",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "CampaignDetail",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "Campaign",
                newName: "CreatedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Campaign",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_CreatedUserId",
                table: "Campaign",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_AspNetUsers_CreatedUserId",
                table: "Campaign",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_AspNetUsers_CreatedUserId",
                table: "Campaign");

            migrationBuilder.DropIndex(
                name: "IX_Campaign_CreatedUserId",
                table: "Campaign");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "WhiteListMember",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Pricing",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "CampaignUpdate",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "CampaignDetail",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Campaign",
                newName: "CreatedUser");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUser",
                table: "Campaign",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
