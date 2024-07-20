using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupLearning.Migrations
{
    /// <inheritdoc />
    public partial class userTableUpdatesForDatesAndGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "User",
                newName: "Uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Uuid",
                table: "User",
                newName: "CreatedDate");
        }
    }
}
