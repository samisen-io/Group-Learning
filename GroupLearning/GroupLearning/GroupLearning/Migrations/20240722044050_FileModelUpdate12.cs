using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupLearning.Migrations
{
    /// <inheritdoc />
    public partial class FileModelUpdate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Files_FileObjectId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileObjectId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileObjectId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileObject",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileObject",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "FileObjectId",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileObjectId",
                table: "Files",
                column: "FileObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Files_FileObjectId",
                table: "Files",
                column: "FileObjectId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
