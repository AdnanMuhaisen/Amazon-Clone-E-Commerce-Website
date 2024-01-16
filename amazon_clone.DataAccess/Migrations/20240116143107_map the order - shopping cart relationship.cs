using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class maptheordershoppingcartrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "tbl_Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShoppingCartID",
                table: "tbl_Orders",
                column: "ShoppingCartID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_tbl_ShoppingCarts_ShoppingCartID",
                table: "tbl_Orders",
                column: "ShoppingCartID",
                principalTable: "tbl_ShoppingCarts",
                principalColumn: "ShoppingCartID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_tbl_ShoppingCarts_ShoppingCartID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_ShoppingCartID",
                table: "tbl_Orders");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "tbl_Orders");
        }
    }
}
