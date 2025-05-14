using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10254164_LukeC_GR2_PROG7311_A2.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_161 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "employees",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
