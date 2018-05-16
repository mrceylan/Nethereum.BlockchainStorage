using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Nethereum.BlockChainStore.Core.Migrations
{
    public partial class TokenContractAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenContractAddress",
                table: "NodeTokenTransfer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenContractAddress",
                table: "NodeTokenTransfer");
        }
    }
}
