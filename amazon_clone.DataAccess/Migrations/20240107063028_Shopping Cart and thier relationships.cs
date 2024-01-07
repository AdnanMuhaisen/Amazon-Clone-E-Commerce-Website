using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartandthierrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PromoCodes",
                columns: table => new
                {
                    CodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PromoCodes", x => x.CodeID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShoppingCarts", x => x.ShoppingCartID);
                    table.ForeignKey(
                        name: "FK_tbl_ShoppingCarts_tbl_PromoCodes_PromoCodeID",
                        column: x => x.PromoCodeID,
                        principalTable: "tbl_PromoCodes",
                        principalColumn: "CodeID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_ShoppingCartProduct",
                columns: table => new
                {
                    ShoppingCartID = table.Column<int>(type: "int", nullable: false),
                    CustomerProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShoppingCartProduct", x => new { x.CustomerProductID, x.ShoppingCartID });
                    table.ForeignKey(
                        name: "FK_tbl_ShoppingCartProduct_tbl_CustomerProducts_CustomerProductID",
                        column: x => x.CustomerProductID,
                        principalTable: "tbl_CustomerProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ShoppingCartProduct_tbl_ShoppingCarts_ShoppingCartID",
                        column: x => x.ShoppingCartID,
                        principalTable: "tbl_ShoppingCarts",
                        principalColumn: "ShoppingCartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCartProduct_ShoppingCartID",
                table: "tbl_ShoppingCartProduct",
                column: "ShoppingCartID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCarts_PromoCodeID",
                table: "tbl_ShoppingCarts",
                column: "PromoCodeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ShoppingCartProduct");

            migrationBuilder.DropTable(
                name: "tbl_ShoppingCarts");

            migrationBuilder.DropTable(
                name: "tbl_PromoCodes");
        }
    }
}
