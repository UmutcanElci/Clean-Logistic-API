using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hub", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    MailAddress = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PickUpLocation_StreetAddress = table.Column<string>(type: "text", nullable: false),
                    PickUpLocation_City = table.Column<string>(type: "text", nullable: false),
                    PickUpLocation_PostalCode = table.Column<int>(type: "integer", nullable: false),
                    PickUpLocation_Country = table.Column<string>(type: "text", nullable: false),
                    PickUpLocation_GpsCoordinates_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    PickUpLocation_GpsCoordinates_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    DestinationLocation_StreetAddress = table.Column<string>(type: "text", nullable: false),
                    DestinationLocation_City = table.Column<string>(type: "text", nullable: false),
                    DestinationLocation_PostalCode = table.Column<int>(type: "integer", nullable: false),
                    DestinationLocation_Country = table.Column<string>(type: "text", nullable: false),
                    DestinationLocation_GpsCoordinates_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    DestinationLocation_GpsCoordinates_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    MaxWeightInKg = table.Column<int>(type: "integer", nullable: false),
                    MaxVolumeInCubicMeters = table.Column<int>(type: "integer", nullable: false),
                    CanGoAbroad = table.Column<bool>(type: "boolean", nullable: false),
                    MaxSpeedInKph = table.Column<int>(type: "integer", nullable: false),
                    CurrentLocation_StreetAddress = table.Column<string>(type: "text", nullable: false),
                    CurrentLocation_City = table.Column<string>(type: "text", nullable: false),
                    CurrentLocation_PostalCode = table.Column<int>(type: "integer", nullable: false),
                    CurrentLocation_Country = table.Column<string>(type: "text", nullable: false),
                    CurrentLocation_GpsCoordinates_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    CurrentLocation_GpsCoordinates_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HubId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Hub_HubId",
                        column: x => x.HubId,
                        principalTable: "Hub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxCapacity = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Address_StreetAddress = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "text", nullable: false),
                    Address_PostalCode = table.Column<int>(type: "integer", nullable: false),
                    Address_Country = table.Column<string>(type: "text", nullable: false),
                    Address_GpsCoordinates_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Address_GpsCoordinates_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    HubId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Hub_HubId",
                        column: x => x.HubId,
                        principalTable: "Hub",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WeightInKg = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignVehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Vehicles_AssignVehicleId",
                        column: x => x.AssignVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes_Stops",
                columns: table => new
                {
                    RouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StreetAddress = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    GpsCoordinates_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    GpsCoordinates_Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes_Stops", x => new { x.RouteId, x.Id });
                    table.ForeignKey(
                        name: "FK_Routes_Stops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_AssignVehicleId",
                table: "Routes",
                column: "AssignVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_HubId",
                table: "Vehicles",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_HubId",
                table: "Warehouses",
                column: "HubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Routes_Stops");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Hub");
        }
    }
}
