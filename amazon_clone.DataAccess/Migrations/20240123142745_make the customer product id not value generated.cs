using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class makethecustomerproductidnotvaluegenerated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 27, 43, 871, DateTimeKind.Local).AddTicks(9702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 18, 11, 55, 29, 925, DateTimeKind.Local).AddTicks(465));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 18, 11, 55, 29, 925, DateTimeKind.Local).AddTicks(465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 27, 43, 871, DateTimeKind.Local).AddTicks(9702));
        }
    }
}
