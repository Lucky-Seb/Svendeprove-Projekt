using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class JWTaddTObruger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Brugere",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("4153884f-a1ce-44d0-970b-8898a11fdb81"),
                column: "Token",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"),
                column: "Token",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"),
                column: "Token",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"),
                column: "Token",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("e4ef6612-011c-453a-894f-858dff3937d4"),
                column: "Token",
                value: null);
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropColumn(
        //        name: "Token",
        //        table: "Brugere");
        //}
    }
}
