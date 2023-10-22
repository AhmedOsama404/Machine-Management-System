using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrGreiche.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentLocationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_locations_locations_ParentLocationID",
                        column: x => x.ParentLocationID,
                        principalTable: "locations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "machines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose_MTBF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose_MTTR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "machineLocation",
                columns: table => new
                {
                    MachineLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    WorkingHours = table.Column<double>(type: "float", nullable: false),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machineLocation", x => x.MachineLocationID);
                    table.ForeignKey(
                        name: "FK_machineLocation_locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_machineLocation_machines_MachineID",
                        column: x => x.MachineID,
                        principalTable: "machines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "machines",
                columns: new[] { "ID", "MachineCode", "MachineName", "Purpose_MTBF", "Purpose_MTTR" },
                values: new object[,]
                {
                    { 1, "M001", "Printing Machine A", "Purpose A", "Purpose B" },
                    { 2, "M002", "Cutting Machine B", "Purpose C", "Purpose D" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_locations_ParentLocationID",
                table: "locations",
                column: "ParentLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_machineLocation_LocationID",
                table: "machineLocation",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_machineLocation_MachineID",
                table: "machineLocation",
                column: "MachineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "machineLocation");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "machines");
        }
    }
}
