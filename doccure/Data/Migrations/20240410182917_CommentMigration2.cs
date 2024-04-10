using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_doctorId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "Reviews",
                newName: "ApplicationuserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_doctorId",
                table: "Reviews",
                newName: "IX_Reviews_ApplicationuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationuserId",
                table: "Reviews",
                column: "ApplicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationuserId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ApplicationuserId",
                table: "Reviews",
                newName: "doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ApplicationuserId",
                table: "Reviews",
                newName: "IX_Reviews_doctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_doctorId",
                table: "Reviews",
                column: "doctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
