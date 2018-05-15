using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class modelRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Message",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Message",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserID",
                table: "Message",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserID",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Message",
                newName: "To");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Message",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Message",
                nullable: true);
        }
    }
}
