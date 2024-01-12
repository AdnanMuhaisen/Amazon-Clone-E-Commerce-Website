using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class configurethecustomerapplicationuserandoredrwishlistandshoppingcartrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "tbl_WishLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "tbl_Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_CustomerID",
                table: "tbl_Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbl_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID",
                principalTable: "tbl_ShoppingCarts",
                principalColumn: "ShoppingCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbl_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                table: "tbl_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Orders_CustomerID",
                table: "tbl_Orders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "tbl_Orders");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "AspNetUsers");
        }
    }
}
