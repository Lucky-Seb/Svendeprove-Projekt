using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class addSyncableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangeHistoryJson",
                table: "Ordboger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ordboger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Ordboger",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ordboger",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Ordboger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastSyncedVersion",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Ordboger",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("0fd1ec97-6cee-4e0f-a032-0a3f3020d5be"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8532), "b7978429-39b3-42d3-aef1-39c570eef34f", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8533), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("10efa19d-6353-4373-b455-414131376826"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8527), "17261337-ad04-48f1-b2cd-f3cc31c4ac3a", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8528), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("2d189ccb-a481-4ea2-8bf3-8014d3fe5825"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 527, DateTimeKind.Local).AddTicks(5814), "f153e933-b245-478c-a3f6-ea32f284989e", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(7798), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3a2ba1b6-34c7-4be1-af4f-13bd66db3079"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8463), "4591dbb7-986c-4133-b960-f33543048331", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8469), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("3e459839-3c17-43a2-b141-6140eeae07d9"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8474), "9b3c75e8-497d-4792-bcde-6cf8c7b2a89f", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8475), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("4bae52b7-970b-41ce-938b-690a44c29795"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8479), "ba3ca9e3-12f0-47a5-8765-0f3c0d822d97", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8480), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("73d3dea1-d21b-4f45-bb0a-2eaca4c7aa04"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8483), "a90e8fb8-03b4-406a-9a10-cf408bb61f5c", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8484), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("7c83305d-527c-4eb7-bb39-28280ad42a2d"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8487), "780f6321-ab42-4064-8ed9-cfd5c14cfd6b", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8488), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("965a856f-eb8f-4910-a6a3-661ff0c4a78a"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8518), "52ee0f18-f5af-4977-86e6-8c519fd11cae", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8519), "Admin" });

            migrationBuilder.UpdateData(
                table: "Ordboger",
                keyColumn: "OrdbogId",
                keyValue: new Guid("a61b1f2a-3236-4af5-90aa-3483b96a5666"),
                columns: new[] { "ChangeHistoryJson", "CreatedAt", "ETag", "LastModified", "ModifiedBy" },
                values: new object[] { "[]", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8536), "28208bef-05fc-4e0a-9c8c-cbaa91e9f566", new DateTime(2025, 4, 11, 10, 13, 0, 528, DateTimeKind.Local).AddTicks(8537), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeHistoryJson",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "LastSyncedVersion",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Ordboger");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ordboger");
        }
    }
}
