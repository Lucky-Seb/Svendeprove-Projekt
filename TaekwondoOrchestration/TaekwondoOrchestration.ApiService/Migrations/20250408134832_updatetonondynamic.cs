using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class updatetonondynamic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: 1,
                column: "OprettelseDato",
                value: new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: 1,
                column: "OprettelseDato",
                value: new DateTime(2025, 4, 8, 13, 34, 38, 219, DateTimeKind.Utc).AddTicks(8293));
        }
    }
}
