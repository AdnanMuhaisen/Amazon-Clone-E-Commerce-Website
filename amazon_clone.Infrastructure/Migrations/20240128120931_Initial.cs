using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                name: "tbl_OrderStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderStatuses", x => x.StatusID);
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
                name: "tbl_PromoCodes",
                columns: table => new
                {
                    CodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForQuantity = table.Column<int>(type: "int", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PromoCodes", x => x.CodeID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ShippingDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShippingDetails", x => x.ID);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "tbl_ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotalAfterApplyingPromoCode = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPromoCodeApplied = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActualSubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromoCodeID = table.Column<int>(type: "int", nullable: true),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WishListID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_tbl_WishLists_WishListID",
                        column: x => x.WishListID,
                        principalTable: "tbl_WishLists",
                        principalColumn: "WishListID");
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivery = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDetailsID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    CreationDetails_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDetails_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShoppingCartID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_tbl_Orders_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Orders_tbl_OrderStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "tbl_OrderStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Orders_tbl_ShippingDetails_ShippingDetailsID",
                        column: x => x.ShippingDetailsID,
                        principalTable: "tbl_ShippingDetails",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tbl_Orders_tbl_ShoppingCarts_ShoppingCartID",
                        column: x => x.ShoppingCartID,
                        principalTable: "tbl_ShoppingCarts",
                        principalColumn: "ShoppingCartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClothesProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ClothingProductID = table.Column<int>(type: "int", nullable: false)
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
                name: "tbl_Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 1, 28, 15, 9, 30, 485, DateTimeKind.Local).AddTicks(2378)),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_tbl_Payments_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Payments_tbl_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "tbl_Orders",
                        principalColumn: "OrderID");
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishListID",
                table: "AspNetUsers",
                column: "WishListID",
                unique: true,
                filter: "[WishListID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_ClothesSizeID",
                table: "ClothesSizes",
                column: "ClothesSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClothesProducts_TargetGenderID",
                table: "tbl_ClothesProducts",
                column: "TargetGenderID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_CustomerID",
                table: "tbl_Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShippingDetailsID",
                table: "tbl_Orders",
                column: "ShippingDetailsID",
                unique: true,
                filter: "[ShippingDetailsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_ShoppingCartID",
                table: "tbl_Orders",
                column: "ShoppingCartID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Orders_StatusID",
                table: "tbl_Orders",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Payments_CustomerID",
                table: "tbl_Payments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Payments_OrderID",
                table: "tbl_Payments",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Products_CategoryID",
                table: "tbl_Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCartProduct_ShoppingCartID",
                table: "tbl_ShoppingCartProduct",
                column: "ShoppingCartID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCarts_PromoCodeID",
                table: "tbl_ShoppingCarts",
                column: "PromoCodeID");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClothesSizes");

            migrationBuilder.DropTable(
                name: "tbl_Payments");

            migrationBuilder.DropTable(
                name: "tbl_ShoppingCartProduct");

            migrationBuilder.DropTable(
                name: "tbl_WishListsProducts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tbl_ClothesProducts");

            migrationBuilder.DropTable(
                name: "tbl_ClothesSizes");

            migrationBuilder.DropTable(
                name: "tbl_Orders");

            migrationBuilder.DropTable(
                name: "tbl_CustomerProducts");

            migrationBuilder.DropTable(
                name: "tbl_Genders");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbl_OrderStatuses");

            migrationBuilder.DropTable(
                name: "tbl_ShippingDetails");

            migrationBuilder.DropTable(
                name: "tbl_ShoppingCarts");

            migrationBuilder.DropTable(
                name: "tbl_Products");

            migrationBuilder.DropTable(
                name: "tbl_WishLists");

            migrationBuilder.DropTable(
                name: "tbl_PromoCodes");

            migrationBuilder.DropTable(
                name: "tbl_ProductCategories");
        }
    }
}
