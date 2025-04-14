using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class NewBrugerLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brugerkode",
                table: "Brugere",
                newName: "TwoFactorSecret");

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Brugere",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BrugereLogins",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrugerID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrugereLogins", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_BrugereLogins_Brugere_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrugereLogins_Brugere_BrugerID1",
                        column: x => x.BrugerID1,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID");
                });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("4153884f-a1ce-44d0-970b-8898a11fdb81"),
                columns: new[] { "TwoFactorEnabled", "TwoFactorSecret" },
                values: new object[] { false, "TestSecret5" });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"),
                columns: new[] { "TwoFactorEnabled", "TwoFactorSecret" },
                values: new object[] { false, "TestSecret2" });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"),
                columns: new[] { "TwoFactorEnabled", "TwoFactorSecret" },
                values: new object[] { false, "TestSecret1" });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"),
                columns: new[] { "TwoFactorEnabled", "TwoFactorSecret" },
                values: new object[] { false, "TestSecret3" });

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("e4ef6612-011c-453a-894f-858dff3937d4"),
                columns: new[] { "TwoFactorEnabled", "TwoFactorSecret" },
                values: new object[] { false, "TestSecret4" });

            migrationBuilder.CreateIndex(
                name: "IX_BrugereLogins_BrugerID",
                table: "BrugereLogins",
                column: "BrugerID");

            migrationBuilder.CreateIndex(
                name: "IX_BrugereLogins_BrugerID1",
                table: "BrugereLogins",
                column: "BrugerID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrugereLogins");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Brugere");

            migrationBuilder.RenameColumn(
                name: "TwoFactorSecret",
                table: "Brugere",
                newName: "Brugerkode");

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("4153884f-a1ce-44d0-970b-8898a11fdb81"),
                column: "Brugerkode",
                value: "hashed_password5");

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"),
                column: "Brugerkode",
                value: "hashed_password2");

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"),
                column: "Brugerkode",
                value: "hashed_password");

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"),
                column: "Brugerkode",
                value: "hashed_password3");

            migrationBuilder.UpdateData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("e4ef6612-011c-453a-894f-858dff3937d4"),
                column: "Brugerkode",
                value: "hashed_password4");
        }
    }
}
