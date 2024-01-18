using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 1, 18, 11, 55, 29, 925, DateTimeKind.Local).AddTicks(465)),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Payments_tbl_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "tbl_Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Payments_CustomerID",
                table: "tbl_Payments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Payments_OrderID",
                table: "tbl_Payments",
                column: "OrderID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Payments");
        }
    }
}
