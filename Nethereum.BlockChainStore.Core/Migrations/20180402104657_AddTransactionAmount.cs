using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
  public partial class AddTransactionAmount : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<decimal>(
          name: "Amount",
          table: "NodeTransaction",
          nullable: false,
          type: "decimal(28,8)",
          defaultValue: 0m);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Amount",
          table: "NodeTransaction");
    }
  }
}
