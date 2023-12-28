using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer_phone_numbers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    country_code = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_phone_numbers", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_customer_phone_numbers_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_phone_numbers");
        }
    }
}
