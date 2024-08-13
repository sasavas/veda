using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEncryptionKeyEncryptorFieldToDigitalContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "encrypted_file_encryption_key",
                table: "digital_content",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "encrypted_file_encryption_key",
                table: "digital_content");
        }
    }
}
