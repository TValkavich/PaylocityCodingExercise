using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaylocityCodingExercise.Model.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SalaryRaw = table.Column<int>(type: "INTEGER", nullable: false),
                    BenefitsPackage = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Obi-Wan Kenobi" },
                    { 2, 1, "Barbara Connors" },
                    { 3, 2, "Dave Mathews" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BenefitsPackage", "Name", "SalaryRaw" },
                values: new object[,]
                {
                    { 1, "Type A", "Andrew Miller", 52000 },
                    { 2, "Type B", "Bobby Flay", 52000 },
                    { 3, "Type C", "Luke Skywalker", 52000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
