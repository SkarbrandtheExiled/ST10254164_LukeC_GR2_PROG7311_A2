using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10254164_LukeC_GR2_PROG7311_A2.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_894 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    farmerName = table.Column<string>(type: "TEXT", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeID);
                });

            migrationBuilder.CreateTable(
                name: "farmers",
                columns: table => new
                {
                    farmerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productName = table.Column<string>(type: "TEXT", nullable: false),
                    farmerName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    productCreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    employeeModelemployeeID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_farmers", x => x.farmerID);
                    table.ForeignKey(
                        name: "FK_farmers_employees_employeeModelemployeeID",
                        column: x => x.employeeModelemployeeID,
                        principalTable: "employees",
                        principalColumn: "employeeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_farmers_employeeModelemployeeID",
                table: "farmers",
                column: "employeeModelemployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "farmers");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
