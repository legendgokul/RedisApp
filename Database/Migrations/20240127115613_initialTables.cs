using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedisApp.Database.Migrations
{
    public partial class initialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    itemCount = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderUid);
                });

            migrationBuilder.CreateTable(
                name: "Ordersitems",
                columns: table => new
                {
                    OrderitemUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    placedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: true),
                    OrderUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordersitems", x => x.OrderitemUid);
                    table.ForeignKey(
                        name: "FK_Ordersitems_Orders_OrderUid",
                        column: x => x.OrderUid,
                        principalTable: "Orders",
                        principalColumn: "OrderUid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordersitems_OrderUid",
                table: "Ordersitems",
                column: "OrderUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordersitems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
