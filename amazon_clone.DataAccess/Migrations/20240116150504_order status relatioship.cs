using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class orderstatusrelatioship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_OrderStatuses_OrderStatusStatusID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_OrderStatusStatusID",
                table: "tbl_Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusStatusID",
                table: "tbl_Orders");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_StatusID",
                table: "tbl_Orders",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_OrderStatuses_StatusID",
                table: "tbl_Orders",
                column: "StatusID",
                principalTable: "tbl_OrderStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_OrderStatuses_StatusID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_StatusID",
                table: "tbl_Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusStatusID",
                table: "tbl_Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_OrderStatusStatusID",
                table: "tbl_Orders",
                column: "OrderStatusStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_OrderStatuses_OrderStatusStatusID",
                table: "tbl_Orders",
                column: "OrderStatusStatusID",
                principalTable: "tbl_OrderStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
