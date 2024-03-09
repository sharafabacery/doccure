using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Prescriptionupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Prescriptions");

            migrationBuilder.AddColumn<bool>(
                name: "Afternoon",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Evening",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Morning",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Night",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Afternoon",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Evening",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Morning",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Night",
                table: "Prescriptions");

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
