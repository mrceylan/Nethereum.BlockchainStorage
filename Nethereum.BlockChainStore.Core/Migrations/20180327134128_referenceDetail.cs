using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class referenceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetUserID",
                table: "ReferenceDetail");

            migrationBuilder.AddColumn<string>(
                name: "NewUserID",
                table: "ReferenceDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewUserID",
                table: "ReferenceDetail");

            migrationBuilder.AddColumn<int>(
                name: "TargetUserID",
                table: "ReferenceDetail",
                nullable: false,
                defaultValue: 0);
        }
    }
}
