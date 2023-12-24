﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ___2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient");

            migrationBuilder.DropForeignKey(
                name: "fk_recipient_customer_customer_id1",
                table: "recipient");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id1",
                table: "recipient",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "recipient",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id1",
                table: "recipient",
                column: "customer_id1",
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

            migrationBuilder.DropForeignKey(
                name: "fk_recipient_customer_customer_id1",
                table: "recipient");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id1",
                table: "recipient",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "recipient",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id",
                table: "recipient",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipient_customer_customer_id1",
                table: "recipient",
                column: "customer_id1",
                principalTable: "customer",
                principalColumn: "id");
        }
    }
}
