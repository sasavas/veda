using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsePostgresNativeArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                table: "recipient",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipient_customer_id",
                table: "recipient",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id");
        }
    }
}
