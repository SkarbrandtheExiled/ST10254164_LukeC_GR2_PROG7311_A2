using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10254164_LukeC_GR2_PROG7311_A2.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_latest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productName = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    productCreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    farmerName = table.Column<string>(type: "TEXT", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
