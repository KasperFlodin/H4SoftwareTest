using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H4SoftwareTest.Migrations.Todo
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todolist");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Todolist",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todolist");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todolist",
                type: "int",
                maxLength: 500,
                nullable: false,
                defaultValue: 0);
        }
    }
}
