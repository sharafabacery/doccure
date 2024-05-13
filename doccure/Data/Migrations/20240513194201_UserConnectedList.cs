using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doccure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserConnectedList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConnected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    applicationuserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConnected_AspNetUsers_applicationuserId",
                        column: x => x.applicationuserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConnected_applicationuserId",
                table: "UserConnected",
                column: "applicationuserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConnected");
        }
    }
}
