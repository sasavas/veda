using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EachRecipientHasOneFolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_folder_recipient_recipient_id",
                table: "folder");

            migrationBuilder.DropForeignKey(
                name: "fk_member_ship_customer_customer_id",
                table: "member_ship");

            migrationBuilder.DropForeignKey(
                name: "fk_member_ship_membership_status_membership_status_id",
                table: "member_ship");

            migrationBuilder.DropIndex(
                name: "ix_folder_recipient_id",
                table: "folder");

            migrationBuilder.DropPrimaryKey(
                name: "pk_member_ship",
                table: "member_ship");

            migrationBuilder.RenameTable(
                name: "member_ship",
                newName: "membership");

            migrationBuilder.RenameIndex(
                name: "ix_member_ship_membership_status_id",
                table: "membership",
                newName: "ix_membership_membership_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_member_ship_customer_id",
                table: "membership",
                newName: "ix_membership_customer_id");

            migrationBuilder.AddColumn<int>(
                name: "folders_id",
                table: "recipient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_membership",
                table: "membership",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_recipient_folders_id",
                table: "recipient",
                column: "folders_id");

            migrationBuilder.AddForeignKey(
                name: "fk_membership_customer_customer_id",
                table: "membership",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_membership_membership_status_membership_status_id",
                table: "membership",
                column: "membership_status_id",
                principalTable: "membership_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_folder_folders_id",
                table: "recipient",
                column: "folders_id",
                principalTable: "folder",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membership_customer_customer_id",
                table: "membership");

            migrationBuilder.DropForeignKey(
                name: "fk_membership_membership_status_membership_status_id",
                table: "membership");

            migrationBuilder.DropForeignKey(
                name: "fk_recipient_folder_folders_id",
                table: "recipient");

            migrationBuilder.DropIndex(
                name: "ix_recipient_folders_id",
                table: "recipient");

            migrationBuilder.DropPrimaryKey(
                name: "pk_membership",
                table: "membership");

            migrationBuilder.DropColumn(
                name: "folders_id",
                table: "recipient");

            migrationBuilder.RenameTable(
                name: "membership",
                newName: "member_ship");

            migrationBuilder.RenameIndex(
                name: "ix_membership_membership_status_id",
                table: "member_ship",
                newName: "ix_member_ship_membership_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_membership_customer_id",
                table: "member_ship",
                newName: "ix_member_ship_customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_member_ship",
                table: "member_ship",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_folder_recipient_id",
                table: "folder",
                column: "recipient_id");

            migrationBuilder.AddForeignKey(
                name: "fk_folder_recipient_recipient_id",
                table: "folder",
                column: "recipient_id",
                principalTable: "recipient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_member_ship_customer_customer_id",
                table: "member_ship",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_ship_membership_status_membership_status_id",
                table: "member_ship",
                column: "membership_status_id",
                principalTable: "membership_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
