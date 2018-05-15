using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class IcoTokenRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenID",
                table: "NodeTransaction",
                newName: "TokenContractID");

            migrationBuilder.CreateTable(
                name: "IcoTokenRelation",
                columns: table => new
                {
                    IcoTokenRelationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IcoContractID = table.Column<int>(nullable: false),
                    TokenContractID = table.Column<int>(nullable: false),
                    TokenType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcoTokenRelation", x => x.IcoTokenRelationID);
                    table.ForeignKey(
                        name: "FK_IcoTokenRelation_IcoContract_IcoContractID",
                        column: x => x.IcoContractID,
                        principalTable: "IcoContract",
                        principalColumn: "IcoContractID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IcoTokenRelation_TokenContract_TokenContractID",
                        column: x => x.TokenContractID,
                        principalTable: "TokenContract",
                        principalColumn: "TokenContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IcoTokenRelation_IcoContractID",
                table: "IcoTokenRelation",
                column: "IcoContractID");

            migrationBuilder.CreateIndex(
                name: "IX_IcoTokenRelation_TokenContractID",
                table: "IcoTokenRelation",
                column: "TokenContractID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcoTokenRelation");

            migrationBuilder.RenameColumn(
                name: "TokenContractID",
                table: "NodeTransaction",
                newName: "TokenID");
        }
    }
}
