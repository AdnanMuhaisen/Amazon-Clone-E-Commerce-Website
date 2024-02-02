using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Relationships_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                table: "tbl_Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 2, 15, 18, 32, 470, DateTimeKind.Local).AddTicks(1017),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 28, 15, 9, 30, 485, DateTimeKind.Local).AddTicks(2378));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                principalTable: "tbl_ShippingDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                table: "tbl_Payments",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                table: "tbl_Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDateTime",
                table: "tbl_Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 28, 15, 9, 30, 485, DateTimeKind.Local).AddTicks(2378),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 2, 15, 18, 32, 470, DateTimeKind.Local).AddTicks(1017));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_Payments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                principalTable: "tbl_ShippingDetails",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                table: "tbl_Payments",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
