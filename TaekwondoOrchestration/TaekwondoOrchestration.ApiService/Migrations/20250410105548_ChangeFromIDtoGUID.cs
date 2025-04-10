using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFromIDtoGUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ordboger",
                table: "Ordboger");

            migrationBuilder.DeleteData(
                table: "BrugerKlubber",
                keyColumns: new[] { "BrugerID", "KlubID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BrugerProgrammer",
                keyColumns: new[] { "BrugerID", "ProgramID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BrugerQuizzer",
                keyColumns: new[] { "BrugerID", "QuizID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BrugerØvelser",
                keyColumns: new[] { "BrugerID", "ØvelseID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "KlubProgrammer",
                keyColumns: new[] { "KlubID", "ProgramID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "KlubQuizzer",
                keyColumns: new[] { "KlubID", "QuizID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Klubber",
                keyColumn: "KlubID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KlubØvelser",
                keyColumns: new[] { "KlubID", "ØvelseID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Ordboger",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klubber",
                keyColumn: "KlubID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Quizzer",
                keyColumn: "QuizID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pensum",
                keyColumn: "PensumID",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ordboger");

            migrationBuilder.AlterColumn<Guid>(
                name: "ØvelseID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeoriID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeknikID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TræningID",
                table: "Træninger",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Teorier",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeoriID",
                table: "Teorier",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Teknikker",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeknikID",
                table: "Teknikker",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "ØvelseID",
                table: "Spørgsmål",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeoriID",
                table: "Spørgsmål",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeknikID",
                table: "Spørgsmål",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizID",
                table: "Spørgsmål",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpørgsmålID",
                table: "Spørgsmål",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Quizzer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizID",
                table: "Quizzer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramID",
                table: "ProgramPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Pensum",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "PensumID",
                table: "Øvelser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ØvelseID",
                table: "Øvelser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "OrdbogId",
                table: "Ordboger",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizID",
                table: "KlubQuizzer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "KlubID",
                table: "KlubQuizzer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramID",
                table: "KlubProgrammer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "KlubID",
                table: "KlubProgrammer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ØvelseID",
                table: "KlubØvelser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "KlubID",
                table: "KlubØvelser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "KlubID",
                table: "Klubber",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizID",
                table: "BrugerQuizzer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrugerID",
                table: "BrugerQuizzer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramID",
                table: "BrugerProgrammer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrugerID",
                table: "BrugerProgrammer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ØvelseID",
                table: "BrugerØvelser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrugerID",
                table: "BrugerØvelser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "KlubID",
                table: "BrugerKlubber",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrugerID",
                table: "BrugerKlubber",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrugerID",
                table: "Brugere",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ordboger",
                table: "Ordboger",
                column: "OrdbogId");

            migrationBuilder.InsertData(
                table: "BrugerKlubber",
                columns: new[] { "BrugerID", "KlubID" },
                values: new object[] { new Guid("c022a2da-a6c0-433c-9221-d1e72a9702e0"), new Guid("ab8af1d4-df2b-4be1-b1ae-2f111230d016") });

            migrationBuilder.InsertData(
                table: "Brugere",
                columns: new[] { "BrugerID", "Address", "Brugerkode", "Brugernavn", "Bæltegrad", "Efternavn", "Email", "Fornavn", "Role" },
                values: new object[] { new Guid("3634af44-883b-4392-b155-a6538059b6f7"), "Nørrebrogade 42", "123456", "emma123", "Gult Bælte", "Jensen", "emma@dojo.dk", "Emma", "Bruger" });

            migrationBuilder.InsertData(
                table: "ProgramPlans",
                columns: new[] { "ProgramID", "Beskrivelse", "Længde", "OprettelseDato", "ProgramNavn" },
                values: new object[] { new Guid("605b7306-724e-4f71-a3a0-a958907971bd"), "2 ugers intro", 14, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intro Program" });

            migrationBuilder.InsertData(
                table: "Quizzer",
                columns: new[] { "QuizID", "PensumID", "QuizBeskrivelse", "QuizNavn" },
                values: new object[] { new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252"), new Guid("322a94f0-288e-4b61-841b-12eacac43d5a"), "Spørgsmål for begyndere", "Begynder Quiz" });

            migrationBuilder.InsertData(
                table: "Teknikker",
                columns: new[] { "TeknikID", "PensumID", "TeknikBeskrivelse", "TeknikBillede", "TeknikLyd", "TeknikNavn", "TeknikVideo" },
                values: new object[] { new Guid("b29366af-4152-431a-9592-e54361d11e85"), new Guid("322a94f0-288e-4b61-841b-12eacac43d5a"), "Forsvar mod angreb.", "", "", "Blokering", "" });

            migrationBuilder.InsertData(
                table: "Teorier",
                columns: new[] { "TeoriID", "PensumID", "TeoriBeskrivelse", "TeoriBillede", "TeoriLyd", "TeoriNavn", "TeoriVideo" },
                values: new object[] { new Guid("b9276019-f574-460c-87a3-f5aa46e0fbdc"), new Guid("322a94f0-288e-4b61-841b-12eacac43d5a"), "Respekt for dojo og lærere.", "", "", "Respect", "" });

            migrationBuilder.InsertData(
                table: "Øvelser",
                columns: new[] { "ØvelseID", "PensumID", "ØvelseBeskrivelse", "ØvelseBillede", "ØvelseNavn", "ØvelseSværhed", "ØvelseTid", "ØvelseVideo" },
                values: new object[] { new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501"), new Guid("322a94f0-288e-4b61-841b-12eacac43d5a"), "En simpel frontspark teknik.", "", "Front Spark", "Begynder", 30, "" });

            migrationBuilder.InsertData(
                table: "BrugerProgrammer",
                columns: new[] { "BrugerID", "ProgramID" },
                values: new object[] { new Guid("a7d6ff24-d9cc-46b4-9d48-3f80b21ba7f7"), new Guid("605b7306-724e-4f71-a3a0-a958907971bd") });

            migrationBuilder.InsertData(
                table: "BrugerQuizzer",
                columns: new[] { "BrugerID", "QuizID" },
                values: new object[] { new Guid("5a75a4f2-3ce1-41eb-9ce9-81b21df7eca2"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252") });

            migrationBuilder.InsertData(
                table: "BrugerØvelser",
                columns: new[] { "BrugerID", "ØvelseID" },
                values: new object[] { new Guid("e4afc319-9111-491b-abb6-3be8c5ba0dd3"), new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501") });

            migrationBuilder.InsertData(
                table: "KlubProgrammer",
                columns: new[] { "KlubID", "ProgramID" },
                values: new object[] { new Guid("3fc700ae-f930-4241-bdb3-f2c145c7c625"), new Guid("605b7306-724e-4f71-a3a0-a958907971bd") });

            migrationBuilder.InsertData(
                table: "KlubQuizzer",
                columns: new[] { "KlubID", "QuizID" },
                values: new object[] { new Guid("693f1474-8c6c-411a-923e-03b4b28b448f"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252") });

            migrationBuilder.InsertData(
                table: "KlubØvelser",
                columns: new[] { "KlubID", "ØvelseID" },
                values: new object[] { new Guid("02f809d3-8400-4c52-bdd3-32f6455802bf"), new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501") });

            migrationBuilder.InsertData(
                table: "Spørgsmål",
                columns: new[] { "SpørgsmålID", "QuizID", "SpørgsmålRækkefølge", "SpørgsmålTid", "TeknikID", "TeoriID", "ØvelseID" },
                values: new object[] { new Guid("a757ba97-b0c0-4f73-8bca-bc195e919b7f"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252"), 1, 30, null, new Guid("b9276019-f574-460c-87a3-f5aa46e0fbdc"), null });

            migrationBuilder.InsertData(
                table: "Træninger",
                columns: new[] { "TræningID", "PensumID", "ProgramID", "QuizID", "TeknikID", "TeoriID", "Tid", "TræningRækkefølge", "ØvelseID" },
                values: new object[] { new Guid("f21e3298-a556-41ab-b9b1-94327bc5c51d"), new Guid("322a94f0-288e-4b61-841b-12eacac43d5a"), new Guid("605b7306-724e-4f71-a3a0-a958907971bd"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252"), new Guid("b29366af-4152-431a-9592-e54361d11e85"), new Guid("b9276019-f574-460c-87a3-f5aa46e0fbdc"), 45, 1, new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ordboger",
                table: "Ordboger");

            migrationBuilder.DeleteData(
                table: "BrugerKlubber",
                keyColumns: new[] { "BrugerID", "KlubID" },
                keyValues: new object[] { new Guid("c022a2da-a6c0-433c-9221-d1e72a9702e0"), new Guid("ab8af1d4-df2b-4be1-b1ae-2f111230d016") });

            migrationBuilder.DeleteData(
                table: "BrugerProgrammer",
                keyColumns: new[] { "BrugerID", "ProgramID" },
                keyValues: new object[] { new Guid("a7d6ff24-d9cc-46b4-9d48-3f80b21ba7f7"), new Guid("605b7306-724e-4f71-a3a0-a958907971bd") });

            migrationBuilder.DeleteData(
                table: "BrugerQuizzer",
                keyColumns: new[] { "BrugerID", "QuizID" },
                keyValues: new object[] { new Guid("5a75a4f2-3ce1-41eb-9ce9-81b21df7eca2"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252") });

            migrationBuilder.DeleteData(
                table: "Brugere",
                keyColumn: "BrugerID",
                keyValue: new Guid("3634af44-883b-4392-b155-a6538059b6f7"));

            migrationBuilder.DeleteData(
                table: "BrugerØvelser",
                keyColumns: new[] { "BrugerID", "ØvelseID" },
                keyValues: new object[] { new Guid("e4afc319-9111-491b-abb6-3be8c5ba0dd3"), new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501") });

            migrationBuilder.DeleteData(
                table: "KlubProgrammer",
                keyColumns: new[] { "KlubID", "ProgramID" },
                keyValues: new object[] { new Guid("3fc700ae-f930-4241-bdb3-f2c145c7c625"), new Guid("605b7306-724e-4f71-a3a0-a958907971bd") });

            migrationBuilder.DeleteData(
                table: "KlubQuizzer",
                keyColumns: new[] { "KlubID", "QuizID" },
                keyValues: new object[] { new Guid("693f1474-8c6c-411a-923e-03b4b28b448f"), new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252") });

            migrationBuilder.DeleteData(
                table: "KlubØvelser",
                keyColumns: new[] { "KlubID", "ØvelseID" },
                keyValues: new object[] { new Guid("02f809d3-8400-4c52-bdd3-32f6455802bf"), new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501") });

            migrationBuilder.DeleteData(
                table: "Spørgsmål",
                keyColumn: "SpørgsmålID",
                keyValue: new Guid("a757ba97-b0c0-4f73-8bca-bc195e919b7f"));

            migrationBuilder.DeleteData(
                table: "Træninger",
                keyColumn: "TræningID",
                keyValue: new Guid("f21e3298-a556-41ab-b9b1-94327bc5c51d"));

            migrationBuilder.DeleteData(
                table: "ProgramPlans",
                keyColumn: "ProgramID",
                keyValue: new Guid("605b7306-724e-4f71-a3a0-a958907971bd"));

            migrationBuilder.DeleteData(
                table: "Quizzer",
                keyColumn: "QuizID",
                keyValue: new Guid("5d13d035-a873-4dcd-a5bb-e5fe782a5252"));

            migrationBuilder.DeleteData(
                table: "Teknikker",
                keyColumn: "TeknikID",
                keyValue: new Guid("b29366af-4152-431a-9592-e54361d11e85"));

            migrationBuilder.DeleteData(
                table: "Teorier",
                keyColumn: "TeoriID",
                keyValue: new Guid("b9276019-f574-460c-87a3-f5aa46e0fbdc"));

            migrationBuilder.DeleteData(
                table: "Øvelser",
                keyColumn: "ØvelseID",
                keyValue: new Guid("16bd0ccb-3711-4a4c-9d4e-123fe73ef501"));

            migrationBuilder.DropColumn(
                name: "OrdbogId",
                table: "Ordboger");

            migrationBuilder.AlterColumn<int>(
                name: "ØvelseID",
                table: "Træninger",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeoriID",
                table: "Træninger",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeknikID",
                table: "Træninger",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "Træninger",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                table: "Træninger",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Træninger",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TræningID",
                table: "Træninger",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Teorier",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "TeoriID",
                table: "Teorier",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Teknikker",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "TeknikID",
                table: "Teknikker",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ØvelseID",
                table: "Spørgsmål",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeoriID",
                table: "Spørgsmål",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeknikID",
                table: "Spørgsmål",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "Spørgsmål",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "SpørgsmålID",
                table: "Spørgsmål",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Quizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "Quizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                table: "ProgramPlans",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Pensum",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PensumID",
                table: "Øvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ØvelseID",
                table: "Øvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ordboger",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "KlubQuizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "KlubQuizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                table: "KlubProgrammer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "KlubProgrammer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ØvelseID",
                table: "KlubØvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "KlubØvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "Klubber",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "BrugerQuizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BrugerID",
                table: "BrugerQuizzer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                table: "BrugerProgrammer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BrugerID",
                table: "BrugerProgrammer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ØvelseID",
                table: "BrugerØvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BrugerID",
                table: "BrugerØvelser",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "BrugerKlubber",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BrugerID",
                table: "BrugerKlubber",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BrugerID",
                table: "Brugere",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ordboger",
                table: "Ordboger",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Brugere",
                columns: new[] { "BrugerID", "Address", "Brugerkode", "Brugernavn", "Bæltegrad", "Efternavn", "Email", "Fornavn", "Role" },
                values: new object[] { 1, "Nørrebrogade 42", "123456", "emma123", "Gult Bælte", "Jensen", "emma@dojo.dk", "Emma", "Bruger" });

            migrationBuilder.InsertData(
                table: "Klubber",
                columns: new[] { "KlubID", "KlubNavn" },
                values: new object[,]
                {
                    { 1, "København Taekwondo Klub" },
                    { 2, "Aarhus Kampkunstcenter" }
                });

            migrationBuilder.InsertData(
                table: "Ordboger",
                columns: new[] { "Id", "Beskrivelse", "BilledeLink", "DanskOrd", "KoranskOrd", "LydLink", "VideoLink" },
                values: new object[] { 1, "En typisk hilsen i kampkunst", "", "Hilsen", "Annyeonghaseyo", "", "" });

            migrationBuilder.InsertData(
                table: "Pensum",
                columns: new[] { "PensumID", "PensumGrad" },
                values: new object[,]
                {
                    { 1, "Hvidt Bælte" },
                    { 2, "Gult Bælte" }
                });

            migrationBuilder.InsertData(
                table: "ProgramPlans",
                columns: new[] { "ProgramID", "Beskrivelse", "Længde", "OprettelseDato", "ProgramNavn" },
                values: new object[] { 1, "2 ugers intro", 14, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intro Program" });

            migrationBuilder.InsertData(
                table: "BrugerKlubber",
                columns: new[] { "BrugerID", "KlubID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BrugerProgrammer",
                columns: new[] { "BrugerID", "ProgramID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "KlubProgrammer",
                columns: new[] { "KlubID", "ProgramID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Quizzer",
                columns: new[] { "QuizID", "PensumID", "QuizBeskrivelse", "QuizNavn" },
                values: new object[] { 1, 1, "Spørgsmål for begyndere", "Begynder Quiz" });

            migrationBuilder.InsertData(
                table: "Teknikker",
                columns: new[] { "TeknikID", "PensumID", "TeknikBeskrivelse", "TeknikBillede", "TeknikLyd", "TeknikNavn", "TeknikVideo" },
                values: new object[] { 1, 1, "Forsvar mod angreb.", "", "", "Blokering", "" });

            migrationBuilder.InsertData(
                table: "Teorier",
                columns: new[] { "TeoriID", "PensumID", "TeoriBeskrivelse", "TeoriBillede", "TeoriLyd", "TeoriNavn", "TeoriVideo" },
                values: new object[] { 1, 1, "Respekt for dojo og lærere.", "", "", "Respect", "" });

            migrationBuilder.InsertData(
                table: "Øvelser",
                columns: new[] { "ØvelseID", "PensumID", "ØvelseBeskrivelse", "ØvelseBillede", "ØvelseNavn", "ØvelseSværhed", "ØvelseTid", "ØvelseVideo" },
                values: new object[] { 1, 1, "En simpel frontspark teknik.", "", "Front Spark", "Begynder", 30, "" });

            migrationBuilder.InsertData(
                table: "BrugerQuizzer",
                columns: new[] { "BrugerID", "QuizID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BrugerØvelser",
                columns: new[] { "BrugerID", "ØvelseID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "KlubQuizzer",
                columns: new[] { "KlubID", "QuizID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "KlubØvelser",
                columns: new[] { "KlubID", "ØvelseID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Spørgsmål",
                columns: new[] { "SpørgsmålID", "QuizID", "SpørgsmålRækkefølge", "SpørgsmålTid", "TeknikID", "TeoriID", "ØvelseID" },
                values: new object[] { 1, 1, 1, 30, null, 1, null });

            migrationBuilder.InsertData(
                table: "Træninger",
                columns: new[] { "TræningID", "PensumID", "ProgramID", "QuizID", "TeknikID", "TeoriID", "Tid", "TræningRækkefølge", "ØvelseID" },
                values: new object[] { 1, 1, 1, 1, 1, 1, 45, 1, 1 });
        }
    }
}
