using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addawards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "specialization",
                table: "Doctor",
                newName: "Specialization");

            migrationBuilder.RenameColumn(
                name: "services",
                table: "Doctor",
                newName: "Services");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Doctor",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VideoCall",
                table: "Doctor",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    award = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Awards_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Awards_DoctorId",
                table: "Awards",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VideoCall",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Doctor",
                newName: "specialization");

            migrationBuilder.RenameColumn(
                name: "Services",
                table: "Doctor",
                newName: "services");
        }
    }
}
