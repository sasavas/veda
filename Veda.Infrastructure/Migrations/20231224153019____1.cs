using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ___1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_folder_recipient_recipient_id1",
                table: "folder");

            migrationBuilder.DropIndex(
                name: "ix_folder_recipient_id1",
                table: "folder");

            migrationBuilder.DropColumn(
                name: "recipient_id1",
                table: "folder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "recipient_id1",
                table: "folder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_folder_recipient_id1",
                table: "folder",
                column: "recipient_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_folder_recipient_recipient_id1",
                table: "folder",
                column: "recipient_id1",
                principalTable: "recipient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
