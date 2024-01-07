using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class orderandthierrelatedentitiesanrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDetails_CreatedAt",
                table: "tbl_ShoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDetails_UpdatedAt",
                table: "tbl_ShoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDetails_CreatedAt",
                table: "tbl_PromoCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDetails_UpdatedAt",
                table: "tbl_PromoCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "tbl_OrderStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ShippingDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShippingDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDetailsID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    OrderStatusStatusID = table.Column<int>(type: "int", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_tbl_Orders_tbl_OrderStatuses_OrderStatusStatusID",
                        column: x => x.OrderStatusStatusID,
                        principalTable: "tbl_OrderStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                        column: x => x.ShippingDetailsID,
                        principalTable: "tbl_ShippingDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_OrderStatusStatusID",
                table: "tbl_Orders",
                column: "OrderStatusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Orders");

            migrationBuilder.DropTable(
                name: "tbl_OrderStatuses");

            migrationBuilder.DropTable(
                name: "tbl_ShippingDetails");

            migrationBuilder.DropColumn(
                name: "CreationDetails_CreatedAt",
                table: "tbl_ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CreationDetails_UpdatedAt",
                table: "tbl_ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CreationDetails_CreatedAt",
                table: "tbl_PromoCodes");

            migrationBuilder.DropColumn(
                name: "CreationDetails_UpdatedAt",
                table: "tbl_PromoCodes");
        }
    }
}
