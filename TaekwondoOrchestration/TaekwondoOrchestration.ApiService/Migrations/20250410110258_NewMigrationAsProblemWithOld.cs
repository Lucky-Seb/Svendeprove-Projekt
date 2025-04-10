using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationAsProblemWithOld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brugere",
                columns: table => new
                {
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brugernavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brugerkode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bæltegrad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brugere", x => x.BrugerID);
                });

            migrationBuilder.CreateTable(
                name: "Klubber",
                columns: table => new
                {
                    KlubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    KlubNavn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klubber", x => x.KlubID);
                });

            migrationBuilder.CreateTable(
                name: "Ordboger",
                columns: table => new
                {
                    OrdbogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DanskOrd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KoranskOrd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BilledeLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LydLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordboger", x => x.OrdbogId);
                });

            migrationBuilder.CreateTable(
                name: "Pensum",
                columns: table => new
                {
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PensumGrad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensum", x => x.PensumID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPlans",
                columns: table => new
                {
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProgramNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OprettelseDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Længde = table.Column<int>(type: "int", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPlans", x => x.ProgramID);
                });

            migrationBuilder.CreateTable(
                name: "BrugerKlubber",
                columns: table => new
                {
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrugerKlubber", x => new { x.BrugerID, x.KlubID });
                    table.ForeignKey(
                        name: "FK_BrugerKlubber_Brugere_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrugerKlubber_Klubber_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klubber",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Øvelser",
                columns: table => new
                {
                    ØvelseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ØvelseNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ØvelseBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ØvelseBillede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ØvelseVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ØvelseTid = table.Column<int>(type: "int", nullable: false),
                    ØvelseSværhed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Øvelser", x => x.ØvelseID);
                    table.ForeignKey(
                        name: "FK_Øvelser_Pensum_PensumID",
                        column: x => x.PensumID,
                        principalTable: "Pensum",
                        principalColumn: "PensumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzer",
                columns: table => new
                {
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    QuizNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzer", x => x.QuizID);
                    table.ForeignKey(
                        name: "FK_Quizzer_Pensum_PensumID",
                        column: x => x.PensumID,
                        principalTable: "Pensum",
                        principalColumn: "PensumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teknikker",
                columns: table => new
                {
                    TeknikID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TeknikNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeknikBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeknikBillede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeknikVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeknikLyd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teknikker", x => x.TeknikID);
                    table.ForeignKey(
                        name: "FK_Teknikker_Pensum_PensumID",
                        column: x => x.PensumID,
                        principalTable: "Pensum",
                        principalColumn: "PensumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teorier",
                columns: table => new
                {
                    TeoriID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TeoriNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeoriBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeoriBillede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeoriVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeoriLyd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teorier", x => x.TeoriID);
                    table.ForeignKey(
                        name: "FK_Teorier_Pensum_PensumID",
                        column: x => x.PensumID,
                        principalTable: "Pensum",
                        principalColumn: "PensumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrugerProgrammer",
                columns: table => new
                {
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrugerProgrammer", x => new { x.BrugerID, x.ProgramID });
                    table.ForeignKey(
                        name: "FK_BrugerProgrammer_Brugere_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrugerProgrammer_ProgramPlans_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "ProgramPlans",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlubProgrammer",
                columns: table => new
                {
                    KlubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlubProgrammer", x => new { x.KlubID, x.ProgramID });
                    table.ForeignKey(
                        name: "FK_KlubProgrammer_Klubber_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klubber",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlubProgrammer_ProgramPlans_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "ProgramPlans",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrugerØvelser",
                columns: table => new
                {
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ØvelseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrugerØvelser", x => new { x.BrugerID, x.ØvelseID });
                    table.ForeignKey(
                        name: "FK_BrugerØvelser_Brugere_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrugerØvelser_Øvelser_ØvelseID",
                        column: x => x.ØvelseID,
                        principalTable: "Øvelser",
                        principalColumn: "ØvelseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlubØvelser",
                columns: table => new
                {
                    KlubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ØvelseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlubØvelser", x => new { x.KlubID, x.ØvelseID });
                    table.ForeignKey(
                        name: "FK_KlubØvelser_Klubber_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klubber",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlubØvelser_Øvelser_ØvelseID",
                        column: x => x.ØvelseID,
                        principalTable: "Øvelser",
                        principalColumn: "ØvelseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrugerQuizzer",
                columns: table => new
                {
                    BrugerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrugerQuizzer", x => new { x.BrugerID, x.QuizID });
                    table.ForeignKey(
                        name: "FK_BrugerQuizzer_Brugere_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Brugere",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrugerQuizzer_Quizzer_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzer",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlubQuizzer",
                columns: table => new
                {
                    KlubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlubQuizzer", x => new { x.KlubID, x.QuizID });
                    table.ForeignKey(
                        name: "FK_KlubQuizzer_Klubber_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klubber",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlubQuizzer_Quizzer_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzer",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spørgsmål",
                columns: table => new
                {
                    SpørgsmålID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SpørgsmålRækkefølge = table.Column<int>(type: "int", nullable: false),
                    SpørgsmålTid = table.Column<int>(type: "int", nullable: false),
                    TeoriID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeknikID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ØvelseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spørgsmål", x => x.SpørgsmålID);
                    table.ForeignKey(
                        name: "FK_Spørgsmål_Quizzer_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzer",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spørgsmål_Teknikker_TeknikID",
                        column: x => x.TeknikID,
                        principalTable: "Teknikker",
                        principalColumn: "TeknikID");
                    table.ForeignKey(
                        name: "FK_Spørgsmål_Teorier_TeoriID",
                        column: x => x.TeoriID,
                        principalTable: "Teorier",
                        principalColumn: "TeoriID");
                    table.ForeignKey(
                        name: "FK_Spørgsmål_Øvelser_ØvelseID",
                        column: x => x.ØvelseID,
                        principalTable: "Øvelser",
                        principalColumn: "ØvelseID");
                });

            migrationBuilder.CreateTable(
                name: "Træninger",
                columns: table => new
                {
                    TræningID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TræningRækkefølge = table.Column<int>(type: "int", nullable: false),
                    Tid = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeoriID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeknikID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ØvelseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PensumID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Træninger", x => x.TræningID);
                    table.ForeignKey(
                        name: "FK_Træninger_Pensum_PensumID",
                        column: x => x.PensumID,
                        principalTable: "Pensum",
                        principalColumn: "PensumID");
                    table.ForeignKey(
                        name: "FK_Træninger_ProgramPlans_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "ProgramPlans",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Træninger_Quizzer_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzer",
                        principalColumn: "QuizID");
                    table.ForeignKey(
                        name: "FK_Træninger_Teknikker_TeknikID",
                        column: x => x.TeknikID,
                        principalTable: "Teknikker",
                        principalColumn: "TeknikID");
                    table.ForeignKey(
                        name: "FK_Træninger_Teorier_TeoriID",
                        column: x => x.TeoriID,
                        principalTable: "Teorier",
                        principalColumn: "TeoriID");
                    table.ForeignKey(
                        name: "FK_Træninger_Øvelser_ØvelseID",
                        column: x => x.ØvelseID,
                        principalTable: "Øvelser",
                        principalColumn: "ØvelseID");
                });

            migrationBuilder.InsertData(
                table: "Brugere",
                columns: new[] { "BrugerID", "Address", "Brugerkode", "Brugernavn", "Bæltegrad", "Efternavn", "Email", "Fornavn", "Role" },
                values: new object[] { new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), "Nørrebrogade 42", "123456", "emma123", "Gult Bælte", "Jensen", "emma@dojo.dk", "Emma", "Bruger" });

            migrationBuilder.InsertData(
                table: "ProgramPlans",
                columns: new[] { "ProgramID", "Beskrivelse", "Længde", "OprettelseDato", "ProgramNavn" },
                values: new object[] { new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe"), "2 ugers intro", 14, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intro Program" });

            migrationBuilder.InsertData(
                table: "Quizzer",
                columns: new[] { "QuizID", "PensumID", "QuizBeskrivelse", "QuizNavn" },
                values: new object[] { new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"), new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"), "Spørgsmål for begyndere", "Begynder Quiz" });

            migrationBuilder.InsertData(
                table: "Teknikker",
                columns: new[] { "TeknikID", "PensumID", "TeknikBeskrivelse", "TeknikBillede", "TeknikLyd", "TeknikNavn", "TeknikVideo" },
                values: new object[] { new Guid("81c54d38-cb85-420b-b697-13f1f2a8c1cd"), new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"), "Forsvar mod angreb.", "", "", "Blokering", "" });

            migrationBuilder.InsertData(
                table: "Teorier",
                columns: new[] { "TeoriID", "PensumID", "TeoriBeskrivelse", "TeoriBillede", "TeoriLyd", "TeoriNavn", "TeoriVideo" },
                values: new object[] { new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547"), new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"), "Respekt for dojo og lærere.", "", "", "Respect", "" });

            migrationBuilder.InsertData(
                table: "Øvelser",
                columns: new[] { "ØvelseID", "PensumID", "ØvelseBeskrivelse", "ØvelseBillede", "ØvelseNavn", "ØvelseSværhed", "ØvelseTid", "ØvelseVideo" },
                values: new object[] { new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2"), new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"), "En simpel frontspark teknik.", "", "Front Spark", "Begynder", 30, "" });

            migrationBuilder.InsertData(
                table: "BrugerKlubber",
                columns: new[] { "BrugerID", "KlubID" },
                values: new object[] { new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d") });

            migrationBuilder.InsertData(
                table: "BrugerProgrammer",
                columns: new[] { "BrugerID", "ProgramID" },
                values: new object[] { new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe") });

            migrationBuilder.InsertData(
                table: "BrugerQuizzer",
                columns: new[] { "BrugerID", "QuizID" },
                values: new object[] { new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730") });

            migrationBuilder.InsertData(
                table: "BrugerØvelser",
                columns: new[] { "BrugerID", "ØvelseID" },
                values: new object[] { new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2") });

            migrationBuilder.InsertData(
                table: "KlubProgrammer",
                columns: new[] { "KlubID", "ProgramID" },
                values: new object[] { new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"), new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe") });

            migrationBuilder.InsertData(
                table: "KlubQuizzer",
                columns: new[] { "KlubID", "QuizID" },
                values: new object[] { new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"), new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730") });

            migrationBuilder.InsertData(
                table: "KlubØvelser",
                columns: new[] { "KlubID", "ØvelseID" },
                values: new object[] { new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"), new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2") });

            migrationBuilder.InsertData(
                table: "Spørgsmål",
                columns: new[] { "SpørgsmålID", "QuizID", "SpørgsmålRækkefølge", "SpørgsmålTid", "TeknikID", "TeoriID", "ØvelseID" },
                values: new object[] { new Guid("f2563f57-92c7-4388-b920-bf38e47e9d12"), new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"), 1, 30, null, new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547"), null });

            migrationBuilder.InsertData(
                table: "Træninger",
                columns: new[] { "TræningID", "PensumID", "ProgramID", "QuizID", "TeknikID", "TeoriID", "Tid", "TræningRækkefølge", "ØvelseID" },
                values: new object[] { new Guid("a3e2121b-b256-4564-bff1-2f2c94ed00de"), new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"), new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe"), new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"), new Guid("81c54d38-cb85-420b-b697-13f1f2a8c1cd"), new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547"), 45, 1, new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2") });

            migrationBuilder.CreateIndex(
                name: "IX_BrugerKlubber_KlubID",
                table: "BrugerKlubber",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_BrugerØvelser_ØvelseID",
                table: "BrugerØvelser",
                column: "ØvelseID");

            migrationBuilder.CreateIndex(
                name: "IX_BrugerProgrammer_ProgramID",
                table: "BrugerProgrammer",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_BrugerQuizzer_QuizID",
                table: "BrugerQuizzer",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_KlubØvelser_ØvelseID",
                table: "KlubØvelser",
                column: "ØvelseID");

            migrationBuilder.CreateIndex(
                name: "IX_KlubProgrammer_ProgramID",
                table: "KlubProgrammer",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_KlubQuizzer_QuizID",
                table: "KlubQuizzer",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Øvelser_PensumID",
                table: "Øvelser",
                column: "PensumID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzer_PensumID",
                table: "Quizzer",
                column: "PensumID");

            migrationBuilder.CreateIndex(
                name: "IX_Spørgsmål_ØvelseID",
                table: "Spørgsmål",
                column: "ØvelseID");

            migrationBuilder.CreateIndex(
                name: "IX_Spørgsmål_QuizID",
                table: "Spørgsmål",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Spørgsmål_TeknikID",
                table: "Spørgsmål",
                column: "TeknikID");

            migrationBuilder.CreateIndex(
                name: "IX_Spørgsmål_TeoriID",
                table: "Spørgsmål",
                column: "TeoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Teknikker_PensumID",
                table: "Teknikker",
                column: "PensumID");

            migrationBuilder.CreateIndex(
                name: "IX_Teorier_PensumID",
                table: "Teorier",
                column: "PensumID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_ØvelseID",
                table: "Træninger",
                column: "ØvelseID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_PensumID",
                table: "Træninger",
                column: "PensumID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_ProgramID",
                table: "Træninger",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_QuizID",
                table: "Træninger",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_TeknikID",
                table: "Træninger",
                column: "TeknikID");

            migrationBuilder.CreateIndex(
                name: "IX_Træninger_TeoriID",
                table: "Træninger",
                column: "TeoriID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrugerKlubber");

            migrationBuilder.DropTable(
                name: "BrugerØvelser");

            migrationBuilder.DropTable(
                name: "BrugerProgrammer");

            migrationBuilder.DropTable(
                name: "BrugerQuizzer");

            migrationBuilder.DropTable(
                name: "KlubØvelser");

            migrationBuilder.DropTable(
                name: "KlubProgrammer");

            migrationBuilder.DropTable(
                name: "KlubQuizzer");

            migrationBuilder.DropTable(
                name: "Ordboger");

            migrationBuilder.DropTable(
                name: "Spørgsmål");

            migrationBuilder.DropTable(
                name: "Træninger");

            migrationBuilder.DropTable(
                name: "Brugere");

            migrationBuilder.DropTable(
                name: "Klubber");

            migrationBuilder.DropTable(
                name: "ProgramPlans");

            migrationBuilder.DropTable(
                name: "Quizzer");

            migrationBuilder.DropTable(
                name: "Teknikker");

            migrationBuilder.DropTable(
                name: "Teorier");

            migrationBuilder.DropTable(
                name: "Øvelser");

            migrationBuilder.DropTable(
                name: "Pensum");
        }
    }
}
