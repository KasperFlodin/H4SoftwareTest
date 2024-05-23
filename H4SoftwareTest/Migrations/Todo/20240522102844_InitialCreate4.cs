using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H4SoftwareTest.Migrations.Todo
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cpr",
                newName: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Cpr",
                newName: "UserId");
        }
    }
}
