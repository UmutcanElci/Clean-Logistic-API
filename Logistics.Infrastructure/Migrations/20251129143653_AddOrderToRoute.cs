using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderToRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Routes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Routes_OrderId",
                table: "Routes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Orders_OrderId",
                table: "Routes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Orders_OrderId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_OrderId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Routes");
        }
    }
}
