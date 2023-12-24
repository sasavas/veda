using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    tc_kimlik_no = table.Column<string>(type: "text", nullable: false),
                    email_address = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    recipient_ids = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membership_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_name = table.Column<string>(type: "text", nullable: false),
                    digital_storage_capacity_in_bytes = table.Column<double>(type: "double precision", nullable: false),
                    recipient_limit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recipient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    tc_kimlik_no = table.Column<string>(type: "text", nullable: false),
                    e_mail_address = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipient", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipient_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_recipient_customer_customer_id1",
                        column: x => x.customer_id1,
                        principalTable: "customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "member_ship",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    membership_status_id = table.Column<int>(type: "integer", nullable: false),
                    start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_ship", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_ship_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_member_ship_membership_status_membership_status_id",
                        column: x => x.membership_status_id,
                        principalTable: "membership_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "folder",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    folder_name = table.Column<string>(type: "text", nullable: false),
                    size_occupied = table.Column<double>(type: "double precision", nullable: false),
                    recipient_id = table.Column<int>(type: "integer", nullable: false),
                    recipient_id1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_folder", x => x.id);
                    table.ForeignKey(
                        name: "fk_folder_recipient_recipient_id",
                        column: x => x.recipient_id,
                        principalTable: "recipient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_folder_recipient_recipient_id1",
                        column: x => x.recipient_id1,
                        principalTable: "recipient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipient_phone_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    country_code = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipient_phone_numbers", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipient_phone_numbers_recipient_id",
                        column: x => x.id,
                        principalTable: "recipient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "digital_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<double>(type: "double precision", nullable: false),
                    hash_code = table.Column<string>(type: "text", nullable: false),
                    folder_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_digital_content", x => x.id);
                    table.ForeignKey(
                        name: "fk_digital_content_folder_folder_id",
                        column: x => x.folder_id,
                        principalTable: "folder",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_digital_content_folder_id",
                table: "digital_content",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "ix_folder_recipient_id",
                table: "folder",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "ix_folder_recipient_id1",
                table: "folder",
                column: "recipient_id1");

            migrationBuilder.CreateIndex(
                name: "ix_member_ship_customer_id",
                table: "member_ship",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_ship_membership_status_id",
                table: "member_ship",
                column: "membership_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_recipient_customer_id",
                table: "recipient",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_recipient_customer_id1",
                table: "recipient",
                column: "customer_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "digital_content");

            migrationBuilder.DropTable(
                name: "member_ship");

            migrationBuilder.DropTable(
                name: "recipient_phone_numbers");

            migrationBuilder.DropTable(
                name: "folder");

            migrationBuilder.DropTable(
                name: "membership_status");

            migrationBuilder.DropTable(
                name: "recipient");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
