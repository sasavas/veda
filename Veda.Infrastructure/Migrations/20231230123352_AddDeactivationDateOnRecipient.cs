using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeactivationDateOnRecipient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deactivation_date",
                table: "recipient",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deactivation_date",
                table: "recipient");
        }
    }
}
