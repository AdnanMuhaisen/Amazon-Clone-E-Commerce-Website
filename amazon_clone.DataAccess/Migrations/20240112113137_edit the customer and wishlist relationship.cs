using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editthecustomerandwishlistrelationship : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_WishLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.DropIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "tbl_WishLists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WishLists_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_WishLists_AspNetUsers_CustomerID",
                table: "tbl_WishLists",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
