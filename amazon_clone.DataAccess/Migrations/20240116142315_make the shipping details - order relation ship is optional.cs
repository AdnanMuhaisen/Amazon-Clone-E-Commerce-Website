using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class maketheshippingdetailsorderrelationshipisoptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingDetailsID",
                table: "tbl_Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                unique: true,
                filter: "[ShippingDetailsID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                principalTable: "tbl_ShippingDetails",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingDetailsID",
                table: "tbl_Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                principalTable: "tbl_ShippingDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
