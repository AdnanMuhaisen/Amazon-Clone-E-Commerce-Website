using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Cascade_Order_Payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Payments_tbl_Orders_OrderID",
                table: "tbl_Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 2, 15, 40, 21, 824, DateTimeKind.Local).AddTicks(7806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 2, 15, 18, 32, 470, DateTimeKind.Local).AddTicks(1017));

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Payments_tbl_Orders_OrderID",
                table: "tbl_Payments",
                column: "OrderID",
                principalTable: "tbl_Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Payments_tbl_Orders_OrderID",
                table: "tbl_Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 2, 15, 18, 32, 470, DateTimeKind.Local).AddTicks(1017),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 2, 15, 40, 21, 824, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Payments_tbl_Orders_OrderID",
                table: "tbl_Payments",
                column: "OrderID",
                principalTable: "tbl_Orders",
                principalColumn: "OrderID");
        }
    }
}
