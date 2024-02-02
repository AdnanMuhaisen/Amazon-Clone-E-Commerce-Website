using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazon_clone.Infrastructure.Migrations.DashboardDb
{
    /// <inheritdoc />
    public partial class Update_The_Administrator_Operation_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_AdministratorOperations",
                columns: table => new
                {
                    OperationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 2, 2, 8, 13, 22, 595, DateTimeKind.Utc).AddTicks(6655)),
                    OperationLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministratorID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AdministratorOperations", x => x.OperationID);
                    table.ForeignKey(
                        name: "FK_tbl_AdministratorOperations_AspNetUsers_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AdministratorOperations_AdministratorID",
                table: "tbl_AdministratorOperations",
                column: "AdministratorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AdministratorOperations");

        }
    }
}
