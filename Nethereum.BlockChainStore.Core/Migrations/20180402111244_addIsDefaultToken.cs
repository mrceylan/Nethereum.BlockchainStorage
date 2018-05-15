using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class addIsDefaultToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultToken",
                table: "TokenContract",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_IcoContract_CampaignID",
                table: "IcoContract",
                column: "CampaignID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IcoContract_Campaign_CampaignID",
                table: "IcoContract",
                column: "CampaignID",
                principalTable: "Campaign",
                principalColumn: "CampaignID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IcoContract_Campaign_CampaignID",
                table: "IcoContract");

            migrationBuilder.DropIndex(
                name: "IX_IcoContract_CampaignID",
                table: "IcoContract");

            migrationBuilder.DropColumn(
                name: "IsDefaultToken",
                table: "TokenContract");
        }
    }
}
