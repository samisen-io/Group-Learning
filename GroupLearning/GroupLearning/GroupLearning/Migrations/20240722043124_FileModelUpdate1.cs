using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupLearning.Migrations
{
    /// <inheritdoc />
    public partial class FileModelUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UploadedById",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UploadedById",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Files",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UploadedById",
                table: "Files",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UploadedById",
                table: "Files",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
