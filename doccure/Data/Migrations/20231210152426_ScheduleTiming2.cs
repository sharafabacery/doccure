using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleTiming2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "ScheduleTiming",
                newName: "StartTime");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "ScheduleTiming",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ScheduleTiming");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "ScheduleTiming",
                newName: "Time");
        }
    }
}
