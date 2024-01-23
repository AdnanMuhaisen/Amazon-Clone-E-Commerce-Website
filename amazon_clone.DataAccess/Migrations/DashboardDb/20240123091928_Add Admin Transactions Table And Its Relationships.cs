using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.DataAccess.Migrations.DashboardDb
{
    /// <inheritdoc />
    public partial class AddAdminTransactionsTableAndItsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_AdministratorTransactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 1, 23, 9, 19, 26, 886, DateTimeKind.Utc).AddTicks(8040)),
                    TransactionLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministratorID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AdministratorTransactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_tbl_AdministratorTransactions_AspNetUsers_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AdministratorTransactions_AdministratorID",
                table: "tbl_AdministratorTransactions",
                column: "AdministratorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AdministratorTransactions");
        }
    }
}
