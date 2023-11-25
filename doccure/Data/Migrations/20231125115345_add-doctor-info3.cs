using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddoctorinfo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_AspNetUsers_applicationuserId",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_speciality",
                table: "speciality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address",
                table: "address");

            migrationBuilder.RenameTable(
                name: "speciality",
                newName: "Speciality");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_address_applicationuserId",
                table: "Address",
                newName: "IX_Address_applicationuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AspNetUsers_applicationuserId",
                table: "Address",
                column: "applicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AspNetUsers_applicationuserId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Speciality",
                newName: "speciality");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "address");

            migrationBuilder.RenameIndex(
                name: "IX_Address_applicationuserId",
                table: "address",
                newName: "IX_address_applicationuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_speciality",
                table: "speciality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address",
                table: "address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_address_AspNetUsers_applicationuserId",
                table: "address",
                column: "applicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
