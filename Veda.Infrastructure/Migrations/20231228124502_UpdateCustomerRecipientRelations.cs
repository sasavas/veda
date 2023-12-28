using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerRecipientRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recipient_ids",
                table: "customer");

            // migrationBuilder.AlterColumn<long>(
            //     name: "number",
            //     table: "recipient_phone_numbers",
            //     type: "bigint",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "text");
            
            migrationBuilder.Sql("ALTER TABLE recipient_phone_numbers " +
                                 "ALTER COLUMN number TYPE bigint USING number::bigint;");

            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                table: "recipient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_recipient_customer_id",
                table: "recipient",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient");

            migrationBuilder.DropIndex(
                name: "ix_recipient_customer_id",
                table: "recipient");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "recipient");

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "recipient_phone_numbers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<List<int>>(
                name: "recipient_ids",
                table: "customer",
                type: "integer[]",
                nullable: false);
        }
    }
}
