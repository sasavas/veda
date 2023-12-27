using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DigitalContentSizeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "digital_content");

            migrationBuilder.AddColumn<long>(
                name: "size_in_bytes",
                table: "digital_content",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size_in_bytes",
                table: "digital_content");

            migrationBuilder.AddColumn<double>(
                name: "size",
                table: "digital_content",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
