using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Blog.Migrations
{
    /// <inheritdoc />
    public partial class updatekeep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UseridUser",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UseridUser",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UseridUser",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_idUser",
                table: "Posts",
                column: "idUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_idUser",
                table: "Posts",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_idUser",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_idUser",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "UseridUser",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UseridUser",
                table: "Posts",
                column: "UseridUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UseridUser",
                table: "Posts",
                column: "UseridUser",
                principalTable: "Users",
                principalColumn: "idUser");
        }
    }
}
