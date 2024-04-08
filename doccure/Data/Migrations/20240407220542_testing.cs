using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billing_Bookings_BookingId",
                table: "Billing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Billing",
                table: "Billing");

            migrationBuilder.RenameTable(
                name: "Billing",
                newName: "Billings");

            migrationBuilder.RenameIndex(
                name: "IX_Billing_BookingId",
                table: "Billings",
                newName: "IX_Billings_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Billings",
                table: "Billings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Bookings_BookingId",
                table: "Billings",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billings_Bookings_BookingId",
                table: "Billings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Billings",
                table: "Billings");

            migrationBuilder.RenameTable(
                name: "Billings",
                newName: "Billing");

            migrationBuilder.RenameIndex(
                name: "IX_Billings_BookingId",
                table: "Billing",
                newName: "IX_Billing_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Billing",
                table: "Billing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_Bookings_BookingId",
                table: "Billing",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
