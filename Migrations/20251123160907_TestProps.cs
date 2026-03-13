using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class TestProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSectionTest",
                table: "TestData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "TestData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestDescription",
                table: "TestData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "TestData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalTimeInMins",
                table: "TestData",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSectionTest",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "TestDescription",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "TestName",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "TotalTimeInMins",
                table: "TestData");
        }
    }
}
