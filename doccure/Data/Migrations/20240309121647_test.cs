using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Bookings_BookingId",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_BookingId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Bookings_BookingId",
                table: "Prescriptions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Bookings_BookingId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_BookingId",
                table: "Prescription",
                newName: "IX_Prescription_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Bookings_BookingId",
                table: "Prescription",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
