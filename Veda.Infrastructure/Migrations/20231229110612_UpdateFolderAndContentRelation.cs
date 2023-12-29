using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFolderAndContentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_digital_content_folder_folder_id",
                table: "digital_content");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_date",
                table: "digital_content",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "upload_date",
                table: "digital_content",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "fk_digital_content_folder_folder_id",
                table: "digital_content",
                column: "folder_id",
                principalTable: "folder",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_digital_content_folder_folder_id",
                table: "digital_content");

            migrationBuilder.DropColumn(
                name: "deletion_date",
                table: "digital_content");

            migrationBuilder.DropColumn(
                name: "upload_date",
                table: "digital_content");

            migrationBuilder.AddForeignKey(
                name: "fk_digital_content_folder_folder_id",
                table: "digital_content",
                column: "folder_id",
                principalTable: "folder",
                principalColumn: "id");
        }
    }
}
