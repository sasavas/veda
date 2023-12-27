using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecipientFolderForeignKeyDef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipient_folder_folder_id",
                table: "recipient");

            migrationBuilder.DropIndex(
                name: "ix_recipient_folder_id",
                table: "recipient");

            migrationBuilder.DropColumn(
                name: "folder_id",
                table: "recipient");

            migrationBuilder.CreateIndex(
                name: "ix_folder_recipient_id",
                table: "folder",
                column: "recipient_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_folder_recipient_recipient_id",
                table: "folder",
                column: "recipient_id",
                principalTable: "recipient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_folder_recipient_recipient_id",
                table: "folder");

            migrationBuilder.DropIndex(
                name: "ix_folder_recipient_id",
                table: "folder");

            migrationBuilder.AddColumn<int>(
                name: "folder_id",
                table: "recipient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_recipient_folder_id",
                table: "recipient",
                column: "folder_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_folder_folder_id",
                table: "recipient",
                column: "folder_id",
                principalTable: "folder",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
