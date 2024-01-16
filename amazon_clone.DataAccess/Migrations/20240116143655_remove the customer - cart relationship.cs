using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removethecustomercartrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbl_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
