using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrGreiche.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_machines_MachineCode",
                table: "machines");

            migrationBuilder.DeleteData(
                table: "machines",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "machines",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "MachineCode",
                table: "machines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MachineCode",
                table: "machines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "machines",
                columns: new[] { "ID", "MachineCode", "MachineName", "Purpose_MTBF", "Purpose_MTTR" },
                values: new object[,]
                {
                    { 1, "M001", "Printing Machine A", "Purpose A", "Purpose B" },
                    { 2, "M002", "Cutting Machine B", "Purpose C", "Purpose D" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_machines_MachineCode",
                table: "machines",
                column: "MachineCode",
                unique: true);
        }
    }
}
