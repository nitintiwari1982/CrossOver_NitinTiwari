using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossExchange.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Symbol = table.Column<string>(nullable: true),
                    NoOfShares = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PortfolioId = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "John Doe" });

            migrationBuilder.InsertData(
                table: "Shares",
                columns: new[] { "Id", "Rate", "Symbol", "TimeStamp" },
                values: new object[,]
                {
                    { 15, 95m, "CBI", new DateTime(2018, 8, 13, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 98m, "CBI", new DateTime(2018, 8, 13, 6, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 100m, "CBI", new DateTime(2018, 8, 13, 5, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 87m, "CBI", new DateTime(2018, 8, 13, 4, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 105m, "CBI", new DateTime(2018, 8, 13, 3, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 96m, "CBI", new DateTime(2018, 8, 13, 2, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 92m, "CBI", new DateTime(2018, 8, 13, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 91m, "CBI", new DateTime(2018, 8, 13, 1, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 97m, "REL", new DateTime(2018, 8, 13, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 96m, "REL", new DateTime(2018, 8, 13, 6, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 110m, "REL", new DateTime(2018, 8, 13, 5, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 89m, "REL", new DateTime(2018, 8, 13, 4, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 100m, "REL", new DateTime(2018, 8, 13, 3, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 95m, "REL", new DateTime(2018, 8, 13, 2, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 99m, "REL", new DateTime(2018, 8, 13, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 90m, "REL", new DateTime(2018, 8, 13, 1, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Action", "NoOfShares", "PortfolioId", "Price", "Symbol" },
                values: new object[,]
                {
                    { 1, "BUY", 50, 1, 5000.0m, "REL" },
                    { 2, "BUY", 100, 1, 10000.0m, "REL" },
                    { 3, "BUY", 150, 1, 14250.0m, "CBI" },
                    { 4, "SELL", 70, 1, 6790.0m, "CBI" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_PortfolioId",
                table: "Trades",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
