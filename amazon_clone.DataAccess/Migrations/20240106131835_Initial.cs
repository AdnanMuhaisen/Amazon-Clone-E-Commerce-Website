using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClothesSizes",
                columns: table => new
                {
                    SizeID = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SizeCreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeCreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClothesSizes", x => x.SizeID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Genders",
                columns: table => new
                {
                    GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProductCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryCreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProductCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WishLists",
                columns: table => new
                {
                    WishListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WishLists", x => x.WishListID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleteDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductCreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tbl_Products_tbl_ProductCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "tbl_ProductCategories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_CustomerProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CustomerProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CustomerProducts", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tbl_CustomerProducts_tbl_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tbl_Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClothesProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ClothesProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetGenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClothesProducts", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tbl_ClothesProducts_tbl_CustomerProducts_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tbl_CustomerProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClothesProducts_tbl_Genders_TargetGenderID",
                        column: x => x.TargetGenderID,
                        principalTable: "tbl_Genders",
                        principalColumn: "GenderID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_WishListsProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WishListsProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_WishListsProducts_tbl_CustomerProducts_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tbl_CustomerProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WishListsProducts_tbl_WishLists_ListID",
                        column: x => x.ListID,
                        principalTable: "tbl_WishLists",
                        principalColumn: "WishListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothesSizes",
                columns: table => new
                {
                    ClothesProductID = table.Column<int>(type: "int", nullable: false),
                    ClothesSizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesSizes", x => new { x.ClothesProductID, x.ClothesSizeID });
                    table.ForeignKey(
                        name: "FK_ClothesSizes_tbl_ClothesProducts_ClothesProductID",
                        column: x => x.ClothesProductID,
                        principalTable: "tbl_ClothesProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_tbl_ClothesSizes_ClothesSizeID",
                        column: x => x.ClothesSizeID,
                        principalTable: "tbl_ClothesSizes",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_ClothesSizeID",
                table: "ClothesSizes",
                column: "ClothesSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClothesProducts_TargetGenderID",
                table: "tbl_ClothesProducts",
                column: "TargetGenderID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Products_CategoryID",
                table: "tbl_Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WishListsProducts_ListID",
                table: "tbl_WishListsProducts",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WishListsProducts_ProductID",
                table: "tbl_WishListsProducts",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothesSizes");

            migrationBuilder.DropTable(
                name: "tbl_WishListsProducts");

            migrationBuilder.DropTable(
                name: "tbl_ClothesProducts");

            migrationBuilder.DropTable(
                name: "tbl_ClothesSizes");

            migrationBuilder.DropTable(
                name: "tbl_WishLists");

            migrationBuilder.DropTable(
                name: "tbl_CustomerProducts");

            migrationBuilder.DropTable(
                name: "tbl_Genders");

            migrationBuilder.DropTable(
                name: "tbl_Products");

            migrationBuilder.DropTable(
                name: "tbl_ProductCategories");
        }
    }
}
