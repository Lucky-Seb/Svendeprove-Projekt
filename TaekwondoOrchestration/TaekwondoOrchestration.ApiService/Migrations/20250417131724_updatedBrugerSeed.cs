using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class updatedBrugerSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Ordboger",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Ordboger",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "LastSyncedVersion",
                table: "Ordboger",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Ordboger",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ETag",
                table: "Ordboger",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ConflictStatus",
                table: "Ordboger",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Brugere",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Brugere",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Brugere",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Brugere",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brugere",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Brugere",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Brugere",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Brugere",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Brugere",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("4153884f-a1ce-44d0-970b-8898a11fdb81"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "etag_005", false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "etag_002", false, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "etag_001", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "etag_003", false, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("e4ef6612-011c-453a-894f-858dff3937d4"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "etag_004", false, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Brugere");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Brugere");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Ordboger",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LastSyncedVersion",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Ordboger",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ETag",
                table: "Ordboger",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ConflictStatus",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
