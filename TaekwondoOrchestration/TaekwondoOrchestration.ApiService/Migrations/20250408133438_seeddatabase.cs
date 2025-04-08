using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class seeddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { 1, "2 ugers intro", 14, new DateTime(2025, 4, 8, 13, 34, 38, 219, DateTimeKind.Utc).AddTicks(8293), "Intro Program" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
