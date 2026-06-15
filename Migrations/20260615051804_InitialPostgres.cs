using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    CorrectOptionType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestName = table.Column<string>(type: "text", nullable: false),
                    TestDescription = table.Column<string>(type: "text", nullable: true),
                    IsSectionTest = table.Column<bool>(type: "boolean", nullable: false),
                    Section = table.Column<int>(type: "integer", nullable: true),
                    TotalTimeInMins = table.Column<int>(type: "integer", nullable: true),
                    QnIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Section = table.Column<int>(type: "integer", nullable: false),
                    GKUnitName = table.Column<int>(type: "integer", nullable: false),
                    TamilUnitName = table.Column<int>(type: "integer", nullable: false),
                    MathsUnitName = table.Column<int>(type: "integer", nullable: false),
                    SclBookStandard = table.Column<int>(type: "integer", nullable: false),
                    QnId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Quiz_QnId",
                        column: x => x.QnId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectOption",
                columns: table => new
                {
                    CorrectOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CorrectOptionValue = table.Column<string>(type: "text", nullable: false),
                    CorrOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QnId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectOption", x => x.CorrectOptionId);
                    table.ForeignKey(
                        name: "FK_CorrectOption_Quiz_QnId",
                        column: x => x.QnId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    OptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    OptionValue = table.Column<string>(type: "text", nullable: false),
                    QuizId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Option_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_QnId",
                table: "Category",
                column: "QnId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectOption_QnId",
                table: "CorrectOption",
                column: "QnId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Option_QuizId",
                table: "Option",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CorrectOption");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "TestData");

            migrationBuilder.DropTable(
                name: "Quiz");
        }
    }
}
