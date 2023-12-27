using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EachRecipientHasOneFolderNamingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipient_folder_folders_id",
                table: "recipient");

            migrationBuilder.RenameColumn(
                name: "folders_id",
                table: "recipient",
                newName: "folder_id");

            migrationBuilder.RenameIndex(
                name: "ix_recipient_folders_id",
                table: "recipient",
                newName: "ix_recipient_folder_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_folder_folder_id",
                table: "recipient",
                column: "folder_id",
                principalTable: "folder",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipient_folder_folder_id",
                table: "recipient");

            migrationBuilder.RenameColumn(
                name: "folder_id",
                table: "recipient",
                newName: "folders_id");

            migrationBuilder.RenameIndex(
                name: "ix_recipient_folder_id",
                table: "recipient",
                newName: "ix_recipient_folders_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_folder_folders_id",
                table: "recipient",
                column: "folders_id",
                principalTable: "folder",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
