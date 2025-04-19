using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class updatedtableswithsync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("13eeede6-81ae-4c79-a25e-9226d7a20316"));

            migrationBuilder.DeleteData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("ff1f1f02-2a9f-4281-9569-7bf33ec6f457"));

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Træninger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Træninger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Træninger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Træninger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Træninger",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Træninger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Træninger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Træninger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Træninger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Teorier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Teorier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teorier",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Teorier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teorier",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Teorier",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Teorier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Teorier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Teorier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Teknikker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Teknikker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teknikker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Teknikker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teknikker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Teknikker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Teknikker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Teknikker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Teknikker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Spørgsmål",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Spørgsmål",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Spørgsmål",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Spørgsmål",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Spørgsmål",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Spørgsmål",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Spørgsmål",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Spørgsmål",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Spørgsmål",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Quizzer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Quizzer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Quizzer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Quizzer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Quizzer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Quizzer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Quizzer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Quizzer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Quizzer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "ProgramPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "ProgramPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProgramPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "ProgramPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProgramPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProgramPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "ProgramPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ProgramPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProgramPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Pensum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Pensum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pensum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Pensum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pensum",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Pensum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Pensum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Pensum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pensum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Øvelser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Øvelser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Øvelser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Øvelser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Øvelser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Øvelser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Øvelser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Øvelser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Øvelser",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("e4ef6612-011c-453a-894f-858dff3937d4"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("0fd1ec97-6cee-4e0f-a032-0a3f3020d5be"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("10efa19d-6353-4373-b455-414131376826"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("2d189ccb-a481-4ea2-8bf3-8014d3fe5825"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3a2ba1b6-34c7-4be1-af4f-13bd66db3079"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3e459839-3c17-43a2-b141-6140eeae07d9"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("4bae52b7-970b-41ce-938b-690a44c29795"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("73d3dea1-d21b-4f45-bb0a-2eaca4c7aa04"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("7c83305d-527c-4eb7-bb39-28280ad42a2d"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("965a856f-eb8f-4910-a6a3-661ff0c4a78a"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("a61b1f2a-3236-4af5-90aa-3483b96a5666"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: new Guid("67db5817-3c5a-4604-ba74-8076578528c3"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: new Guid("9c3cabef-0731-4243-a5e8-d837c77ee523"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: new Guid("11741c42-315e-42fe-a0a8-337afd6d511f"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: new Guid("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: new Guid("8c98eb00-1efb-4361-99b7-974c1aed66e8"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Quizzer",
                keyColumn: "QuizID",
                keyValue: new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Quizzer",
                keyColumn: "QuizID",
                keyValue: new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Quizzer",
                keyColumn: "QuizID",
                keyValue: new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("0a4b9bd3-1fc3-498c-9574-51d1db67cce4"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("2f02f12d-3c9a-4b63-b7f0-8df70d9ed799"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("3e5a25d1-a1a2-411d-b5ab-db0aead404c3"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("464d80b9-28b4-4e16-949a-bddbafc4c6f1"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("49da5e18-a087-40aa-8ea8-0a67e13d249b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("4ba89cd8-b931-4071-a057-1c9740dac086"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("600a1b8b-6eed-434d-9a1f-1337124da834"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("67f31858-40f0-481d-844a-7dcc7c4e0b48"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("6e6089f3-1f29-412e-b7ba-9cfe652fecce"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("85c9d413-5148-488c-a667-f971171d2d78"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("8fe43b9a-f64d-4701-911a-764204b423ad"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("94dac4ff-3ef1-41dd-8593-de3736627b98"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("a79c2d74-bccf-4a9f-ba03-ac02c906f6c7"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("d1797c4a-4378-49a9-84d5-375efcff0d88"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("d1a60bed-fd96-47dd-b86a-a944230d53bb"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("42e75d1b-d44e-416c-8a6a-0dd9b2803d9c"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("49425f68-4bcf-412e-8ac3-0f87b3b117ca"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("49e7612b-36ff-4f40-9224-5d126945e3e2"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("4d608ca7-227c-4dc5-b7b9-f8f2315233ec"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("72385206-876c-4f9d-bfc6-dc8aa02ef587"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("ca16ac6c-3697-49b2-891b-dd9e632790c1"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("f4a14256-bf7c-4910-a4c4-13fc063a455a"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("fe2b1123-5434-4c83-ab16-e8372bd99fef"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("26abad43-d433-416e-ba59-6ddb20a64093"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("340c59b4-9f76-46af-8abb-49a200eb4671"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("3ed65b45-89f7-4209-bc51-9de0b3c6b6d3"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("9e4df6a6-af38-445f-a4d5-2f2f5e01b029"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("a845c529-8362-4fac-b7d0-df079db04860"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("aae05942-0586-4000-aca2-b02525c0f1ea"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("bf4135c9-db91-4354-956f-f1606bddccd8"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("d93a6ee0-f753-4f04-ac35-7fbb1cd09803"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("e10b50fa-9224-4469-9f4c-7553db8328b6"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("ecac9146-b9b2-492c-9561-032c39b1436b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("2faf5b1b-40a2-4599-99c9-c0e948a31dc7"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("3949e642-5f71-40b7-8c4f-7bdaee0686c9"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("4a91f6e6-3b23-4de7-85a1-1f173a90a27f"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("58d29731-5915-496c-9f52-b1c64199fdd7"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("84a19dcc-6f70-49ce-9629-d04b0cb0c7dd"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("cf9ce0e3-f9d0-41a0-8486-7b9d41aa43be"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("0335fde9-e05e-4a72-b08c-0c076803b395"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("29433916-f5aa-4d34-9bf3-6e0eb09aa010"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("a36f91d7-c05b-48c8-97a3-11f86a2eae69"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("bdb87fa8-e777-42e6-ad08-7187707fe1c3"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("d5a762e1-b401-4118-955d-bbb3d26f370e"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("d9f50fe9-11d5-46ee-8836-a57727dc424b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("f1c414cb-8ce7-4583-b65e-510bc0f2fd8b"),
                columns: new[] { "ChangeHistoryJson", "ConflictStatus", "CreatedAt", "ETag", "IsDeleted", "LastModified", "LastSyncedVersion", "ModifiedBy", "Status" },
                values: new object[] { "[]", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12346", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Træninger");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Teorier");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Teknikker");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Spørgsmål");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quizzer");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProgramPlans");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pensum");

            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Øvelser");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Øvelser");

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

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("0fd1ec97-6cee-4e0f-a032-0a3f3020d5be"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("10efa19d-6353-4373-b455-414131376826"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("2d189ccb-a481-4ea2-8bf3-8014d3fe5825"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3a2ba1b6-34c7-4be1-af4f-13bd66db3079"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3e459839-3c17-43a2-b141-6140eeae07d9"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("4bae52b7-970b-41ce-938b-690a44c29795"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("73d3dea1-d21b-4f45-bb0a-2eaca4c7aa04"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("7c83305d-527c-4eb7-bb39-28280ad42a2d"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("965a856f-eb8f-4910-a6a3-661ff0c4a78a"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("a61b1f2a-3236-4af5-90aa-3483b96a5666"),
                column: "Status",
                value: 0);

            migrationBuilder.InsertData(
                table: "Teknikker",
                columns: new[] { "TeknikID", "PensumID", "TeknikBeskrivelse", "TeknikBillede", "TeknikLyd", "TeknikNavn", "TeknikVideo" },
                values: new object[,]
                {
                    { new Guid("13eeede6-81ae-4c79-a25e-9226d7a20316"), new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"), "A strike using the edge of the hand, aimed at vulnerable areas.", "knife_hand_strike_image_url", "knife_hand_strike_sound_url", "Knife Hand Strike", "knife_hand_strike_video_url" },
                    { new Guid("ff1f1f02-2a9f-4281-9569-7bf33ec6f457"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "A spinning kick where you turn around and strike with a powerful back kick.", "spinning_back_kick_image_url", "spinning_back_kick_sound_url", "Spinning Back Kick", "spinning_back_kick_video_url" }
                });
        }
    }
}
