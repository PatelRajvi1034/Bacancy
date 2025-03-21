using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfCoreTask5.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Software Development" },
                    { 2, "Marketing" },
                    { 3, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "ProjectName", "StartDate" },
                values: new object[,]
                {
                    { 1, "E-Commerce Website", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Mobile Banking App", new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "SEO Campaign", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Cloud Migration", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Cybersecurity Upgrade", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 1, "alice@example.com", "Alice Johnson" },
                    { 2, 1, "bob@example.com", "Bob Smith" },
                    { 3, 2, "charlie@example.com", "Charlie Brown" },
                    { 4, 3, "diana@example.com", "Diana Adams" },
                    { 5, 1, "ethan@example.com", "Ethan White" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId", "Role" },
                values: new object[,]
                {
                    { 1, 1, "Frontend Developer" },
                    { 1, 2, "Backend Developer" },
                    { 2, 1, "Full Stack Developer" },
                    { 2, 4, "Cloud Architect" },
                    { 3, 3, "Marketing Specialist" },
                    { 4, 5, "Security Analyst" },
                    { 5, 2, "Mobile App Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
