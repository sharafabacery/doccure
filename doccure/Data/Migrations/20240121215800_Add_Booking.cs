using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    videocall = table.Column<double>(type: "float", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false),
                    payment = table.Column<int>(type: "int", nullable: false),
                    scheduletimingId = table.Column<int>(type: "int", nullable: false),
                    patientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    doctorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_doctorId",
                        column: x => x.doctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_patientId",
                        column: x => x.patientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_ScheduleTiming_scheduletimingId",
                        column: x => x.scheduletimingId,
                        principalTable: "ScheduleTiming",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_doctorId",
                table: "Bookings",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_patientId",
                table: "Bookings",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_scheduletimingId",
                table: "Bookings",
                column: "scheduletimingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
