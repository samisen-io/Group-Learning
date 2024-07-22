using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupLearning.Migrations
{
    /// <inheritdoc />
    public partial class FileModelUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileObject",
                table: "Files",
                newName: "ContentType");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Files",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Files",
                newName: "FileObject");
        }
    }
}
