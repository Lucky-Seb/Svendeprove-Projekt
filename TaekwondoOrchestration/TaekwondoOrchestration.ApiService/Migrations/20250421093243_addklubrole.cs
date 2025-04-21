using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class addklubrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KlubRole",
                table: "BrugerKlubber",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BrugerKlubber",
                keyColumns: new[] { "BrugerID", "KlubID" },
                keyValues: new object[] { new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"), new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce") },
                column: "KlubRole",
                value: "Member");

            migrationBuilder.UpdateData(
                table: "BrugerKlubber",
                keyColumns: new[] { "BrugerID", "KlubID" },
                keyValues: new object[] { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993") },
                column: "KlubRole",
                value: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KlubRole",
                table: "BrugerKlubber");
        }
    }
}
