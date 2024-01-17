using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpromocodefeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualSubTotal",
                table: "tbl_ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPromoCodeApplied",
                table: "tbl_ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotalAfterApplyingPromoCode",
                table: "tbl_ShoppingCarts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HomeAddress",
                table: "tbl_ShippingDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSubTotal",
                table: "tbl_ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "IsPromoCodeApplied",
                table: "tbl_ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "SubTotalAfterApplyingPromoCode",
                table: "tbl_ShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "HomeAddress",
                table: "tbl_ShippingDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
