using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateandreconfigureofthewishlistcustomerrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.AddColumn<int>(
                name: "WishListID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishListID",
                table: "AspNetUsers",
                column: "WishListID",
                unique: true,
                filter: "[WishListID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbl_WishLists_WishListID",
                table: "AspNetUsers",
                column: "WishListID",
                principalTable: "tbl_WishLists",
                principalColumn: "WishListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbl_WishLists_WishListID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WishListID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WishListID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "tbl_WishLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
