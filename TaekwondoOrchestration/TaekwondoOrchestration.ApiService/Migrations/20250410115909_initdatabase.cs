using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaekwondoOrchestration.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class initdatabase : Migration
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
                values: new object[,]
                {
                    { new Guid("4153884f-a1ce-44d0-970b-8898a11fdb81"), "654 Taekwondo St.", "hashed_password5", "robertbrown654", "Brunt Bælte", "Brown", "robert.brown@example.com", "Robert", "Bruger" },
                    { new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"), "456 Taekwondo St.", "hashed_password2", "janedoe456", "Gult Bælte", "Doe", "jane.doe@example.com", "Jane", "Bruger" },
                    { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), "123 Taekwondo St.", "hashed_password", "johndoe123", "Hvidt Bælte", "Doe", "john.doe@example.com", "John", "Bruger" },
                    { new Guid("98e78576-6bc4-408d-b4af-2ad9051f905b"), "789 Taekwondo St.", "hashed_password3", "marksmith789", "Blåt Bælte", "Smith", "mark.smith@example.com", "Mark", "Bruger" },
                    { new Guid("e4ef6612-011c-453a-894f-858dff3937d4"), "321 Taekwondo St.", "hashed_password4", "lucyjones321", "Grønt Bælte", "Jones", "lucy.jones@example.com", "Lucy", "Bruger" }
                });

            migrationBuilder.InsertData(
                table: "Klubber",
                columns: new[] { "KlubID", "KlubNavn" },
                values: new object[,]
                {
                    { new Guid("2bed2d1b-b96e-427a-8451-3c43ea48ea5c"), "Taekwondo Club D" },
                    { new Guid("6b25c814-a97b-41dc-9597-0864f08cb779"), "Taekwondo Club C" },
                    { new Guid("9c3e5c16-5bb2-4076-a871-b2414bd782c2"), "Taekwondo Club E" },
                    { new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993"), "Taekwondo Club A" },
                    { new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce"), "Taekwondo Club B" }
                });

            migrationBuilder.InsertData(
                table: "Ordboger",
                columns: new[] { "OrdbogId", "Beskrivelse", "BilledeLink", "DanskOrd", "KoranskOrd", "LydLink", "VideoLink" },
                values: new object[,]
                {
                    { new Guid("0fd1ec97-6cee-4e0f-a032-0a3f3020d5be"), "A symbol of rank in Taekwondo, representing the practitioner's level.", "belt_image_url", "Belt", "띠", "belt_sound_url", "belt_video_url" },
                    { new Guid("10efa19d-6353-4373-b455-414131376826"), "The act of breaking boards or other objects to test strength and technique.", "breaking_image_url", "Breaking", "격파", "breaking_sound_url", "breaking_video_url" },
                    { new Guid("2d189ccb-a481-4ea2-8bf3-8014d3fe5825"), "A Korean martial art focusing on high kicks and hand techniques.", "taekwondo_image_url", "Taekwondo", "تايكوندو", "taekwondo_sound_url", "taekwondo_video_url" },
                    { new Guid("3a2ba1b6-34c7-4be1-af4f-13bd66db3079"), "A kick in Taekwondo, used for both offense and defense.", "kick_image_url", "Kik", "킥", "kick_sound_url", "kick_video_url" },
                    { new Guid("3e459839-3c17-43a2-b141-6140eeae07d9"), "A fundamental position in Taekwondo used for balance and power.", "stance_image_url", "Stance", "자세", "stance_sound_url", "stance_video_url" },
                    { new Guid("4bae52b7-970b-41ce-938b-690a44c29795"), "A series of movements and techniques in a specific sequence.", "form_image_url", "Form", "품세", "form_sound_url", "form_video_url" },
                    { new Guid("73d3dea1-d21b-4f45-bb0a-2eaca4c7aa04"), "A highly skilled Taekwondo practitioner and instructor.", "master_image_url", "Master", "사범", "master_sound_url", "master_video_url" },
                    { new Guid("7c83305d-527c-4eb7-bb39-28280ad42a2d"), "A practice of fighting against an opponent in Taekwondo.", "sparring_image_url", "Sparring", "겨루기", "sparring_sound_url", "sparring_video_url" },
                    { new Guid("965a856f-eb8f-4910-a6a3-661ff0c4a78a"), "A board used in training for practicing kicks.", "kickboard_image_url", "Kickboard", "발차기보드", "kickboard_sound_url", "kickboard_video_url" },
                    { new Guid("a61b1f2a-3236-4af5-90aa-3483b96a5666"), "A command to stand at attention, often used during training or ceremonies.", "charyeot_image_url", "Charyeot", "차렷", "charyeot_sound_url", "charyeot_video_url" }
                });

            migrationBuilder.InsertData(
                table: "Pensum",
                columns: new[] { "PensumID", "PensumGrad" },
                values: new object[,]
                {
                    { new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "Hvid" },
                    { new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"), "Sort" },
                    { new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "Blåt" },
                    { new Guid("9c3cabef-0731-4243-a5e8-d837c77ee523"), "Rød" },
                    { new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "Gult" }
                });

            migrationBuilder.InsertData(
                table: "ProgramPlans",
                columns: new[] { "ProgramID", "Beskrivelse", "Længde", "OprettelseDato", "ProgramNavn" },
                values: new object[,]
                {
                    { new Guid("11741c42-315e-42fe-a0a8-337afd6d511f"), "A basic program to get started with Taekwondo.", 4, new DateTime(2025, 4, 10, 11, 59, 8, 696, DateTimeKind.Utc).AddTicks(4047), "Basic Taekwondo Program" },
                    { new Guid("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47"), "An advanced program for mastering Taekwondo techniques.", 8, new DateTime(2025, 4, 10, 11, 59, 8, 696, DateTimeKind.Utc).AddTicks(4283), "Advanced Taekwondo Program" },
                    { new Guid("8c98eb00-1efb-4361-99b7-974c1aed66e8"), "An intermediate program to enhance your Taekwondo skills.", 6, new DateTime(2025, 4, 10, 11, 59, 8, 696, DateTimeKind.Utc).AddTicks(4282), "Intermediate Taekwondo Program" }
                });

            migrationBuilder.InsertData(
                table: "Øvelser",
                columns: new[] { "ØvelseID", "PensumID", "ØvelseBeskrivelse", "ØvelseBillede", "ØvelseNavn", "ØvelseSværhed", "ØvelseTid", "ØvelseVideo" },
                values: new object[,]
                {
                    { new Guid("0335fde9-e05e-4a72-b08c-0c076803b395"), new Guid("00000000-0000-0000-0000-000000000000"), "A basic bodyweight exercise for strengthening the upper body and arms.", "push_up_image_url", "Push-ups", "Let", 30, "push_up_video_url" },
                    { new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6"), new Guid("00000000-0000-0000-0000-000000000000"), "A core exercise to strengthen the abdominal muscles.", "sit_up_image_url", "Sit-ups", "Let", 30, "sit_up_video_url" },
                    { new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42"), new Guid("00000000-0000-0000-0000-000000000000"), "A lower body exercise to strengthen the thighs, hips, and buttocks.", "squat_image_url", "Squats", "Mellem", 45, "squat_video_url" },
                    { new Guid("29433916-f5aa-4d34-9bf3-6e0eb09aa010"), new Guid("00000000-0000-0000-0000-000000000000"), "A core stability exercise to strengthen the abdominals, back, and shoulders.", "plank_image_url", "Plank", "Mellem", 45, "plank_video_url" },
                    { new Guid("a36f91d7-c05b-48c8-97a3-11f86a2eae69"), new Guid("00000000-0000-0000-0000-000000000000"), "A cardiovascular exercise that mimics climbing a mountain while on the ground.", "mountain_climber_image_url", "Mountain Climbers", "Mellem", 30, "mountain_climber_video_url" },
                    { new Guid("bdb87fa8-e777-42e6-ad08-7187707fe1c3"), new Guid("00000000-0000-0000-0000-000000000000"), "An upper body exercise that targets the triceps, using parallel bars or a bench.", "tricep_dips_image_url", "Tricep Dips", "Mellem", 45, "tricep_dips_video_url" },
                    { new Guid("d5a762e1-b401-4118-955d-bbb3d26f370e"), new Guid("00000000-0000-0000-0000-000000000000"), "A lower body exercise targeting the quads, hamstrings, and glutes.", "lunge_image_url", "Lunges", "Mellem", 40, "lunge_video_url" },
                    { new Guid("d9f50fe9-11d5-46ee-8836-a57727dc424b"), new Guid("00000000-0000-0000-0000-000000000000"), "A full-body cardio exercise to improve endurance and agility.", "jumping_jacks_image_url", "Jumping Jacks", "Let", 30, "jumping_jacks_video_url" },
                    { new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6"), new Guid("00000000-0000-0000-0000-000000000000"), "A full-body exercise that combines squats, push-ups, and jumps.", "burpee_image_url", "Burpees", "Svær", 60, "burpee_video_url" },
                    { new Guid("f1c414cb-8ce7-4583-b65e-510bc0f2fd8b"), new Guid("00000000-0000-0000-0000-000000000000"), "A cardio exercise focusing on fast leg movement to increase heart rate.", "high_knees_image_url", "High Knees", "Mellem", 40, "high_knees_video_url" }
                });

            migrationBuilder.InsertData(
                table: "BrugerKlubber",
                columns: new[] { "BrugerID", "KlubID" },
                values: new object[,]
                {
                    { new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"), new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce") },
                    { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993") }
                });

            migrationBuilder.InsertData(
                table: "BrugerProgrammer",
                columns: new[] { "BrugerID", "ProgramID" },
                values: new object[] { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), new Guid("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47") });

            migrationBuilder.InsertData(
                table: "BrugerØvelser",
                columns: new[] { "BrugerID", "ØvelseID" },
                values: new object[,]
                {
                    { new Guid("446a5a83-a0bd-4633-b28f-a6526245eed7"), new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6") },
                    { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42") }
                });

            migrationBuilder.InsertData(
                table: "KlubProgrammer",
                columns: new[] { "KlubID", "ProgramID" },
                values: new object[,]
                {
                    { new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993"), new Guid("11741c42-315e-42fe-a0a8-337afd6d511f") },
                    { new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce"), new Guid("8c98eb00-1efb-4361-99b7-974c1aed66e8") }
                });

            migrationBuilder.InsertData(
                table: "KlubØvelser",
                columns: new[] { "KlubID", "ØvelseID" },
                values: new object[,]
                {
                    { new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993"), new Guid("0335fde9-e05e-4a72-b08c-0c076803b395") },
                    { new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce"), new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6") }
                });

            migrationBuilder.InsertData(
                table: "Quizzer",
                columns: new[] { "QuizID", "PensumID", "QuizBeskrivelse", "QuizNavn" },
                values: new object[,]
                {
                    { new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "Test your knowledge of the history and origins of Taekwondo.", "Taekwondo History Quiz" },
                    { new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "A quiz to test your knowledge of basic Taekwondo concepts.", "Taekwondo Basics Quiz" },
                    { new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "A quiz to assess your understanding of various Taekwondo techniques and movements.", "Taekwondo Techniques Quiz" }
                });

            migrationBuilder.InsertData(
                table: "Teknikker",
                columns: new[] { "TeknikID", "PensumID", "TeknikBeskrivelse", "TeknikBillede", "TeknikLyd", "TeknikNavn", "TeknikVideo" },
                values: new object[,]
                {
                    { new Guid("13eeede6-81ae-4c79-a25e-9226d7a20316"), new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"), "A strike using the edge of the hand, aimed at vulnerable areas.", "knife_hand_strike_image_url", "knife_hand_strike_sound_url", "Knife Hand Strike", "knife_hand_strike_video_url" },
                    { new Guid("42e75d1b-d44e-416c-8a6a-0dd9b2803d9c"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "A kick where the foot comes down like an axe to strike the opponent.", "axe_kick_image_url", "axe_kick_sound_url", "Axe Kick", "axe_kick_video_url" },
                    { new Guid("49425f68-4bcf-412e-8ac3-0f87b3b117ca"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "A powerful kick aimed at the opponent's torso, delivered from the side.", "side_kick_image_url", "side_kick_sound_url", "Side Kick", "side_kick_video_url" },
                    { new Guid("49e7612b-36ff-4f40-9224-5d126945e3e2"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "A strike using the elbow to target the opponent's head or torso.", "elbow_strike_image_url", "elbow_strike_sound_url", "Elbow Strike", "elbow_strike_video_url" },
                    { new Guid("4d608ca7-227c-4dc5-b7b9-f8f2315233ec"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "A powerful kick aimed at the opponent's head or torso.", "roundhouse_kick_image_url", "roundhouse_kick_sound_url", "Roundhouse Kick", "roundhouse_kick_video_url" },
                    { new Guid("72385206-876c-4f9d-bfc6-dc8aa02ef587"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "A kick delivered by turning your back to the opponent and kicking backward.", "back_kick_image_url", "back_kick_sound_url", "Back Kick", "back_kick_video_url" },
                    { new Guid("ca16ac6c-3697-49b2-891b-dd9e632790c1"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "A strike using the knee to target the opponent's midsection or head.", "knee_strike_image_url", "knee_strike_sound_url", "Knee Strike", "knee_strike_video_url" },
                    { new Guid("f4a14256-bf7c-4910-a4c4-13fc063a455a"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "A quick kick aimed at the opponent's stomach or face.", "front_kick_image_url", "front_kick_sound_url", "Front Kick", "front_kick_video_url" },
                    { new Guid("fe2b1123-5434-4c83-ab16-e8372bd99fef"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "A strike using the bottom of the fist, like a hammer blow.", "hammerfist_image_url", "hammerfist_sound_url", "Hammerfist", "hammerfist_video_url" },
                    { new Guid("ff1f1f02-2a9f-4281-9569-7bf33ec6f457"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "A spinning kick where you turn around and strike with a powerful back kick.", "spinning_back_kick_image_url", "spinning_back_kick_sound_url", "Spinning Back Kick", "spinning_back_kick_video_url" }
                });

            migrationBuilder.InsertData(
                table: "Teorier",
                columns: new[] { "TeoriID", "PensumID", "TeoriBeskrivelse", "TeoriBillede", "TeoriLyd", "TeoriNavn", "TeoriVideo" },
                values: new object[,]
                {
                    { new Guid("26abad43-d433-416e-ba59-6ddb20a64093"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "Understanding the belt levels in Taekwondo and their meanings.", "belt_system_image_url", "belt_system_sound_url", "Taekwondo Belt System", "belt_system_video_url" },
                    { new Guid("340c59b4-9f76-46af-8abb-49a200eb4671"), new Guid("9c3cabef-0731-4243-a5e8-d837c77ee523"), "How Taekwondo tournaments are organized and what the rules are.", "tournaments_image_url", "tournaments_sound_url", "Taekwondo Tournaments", "tournaments_video_url" },
                    { new Guid("3ed65b45-89f7-4209-bc51-9de0b3c6b6d3"), new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"), "The philosophy behind Taekwondo and its martial arts principles.", "philosophy_image_url", "philosophy_sound_url", "Taekwondo Philosophy", "philosophy_video_url" },
                    { new Guid("9e4df6a6-af38-445f-a4d5-2f2f5e01b029"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "The foundational movements in Taekwondo.", "basic_movements_image_url", "basic_movements_sound_url", "Basic Taekwondo Movements", "basic_movements_video_url" },
                    { new Guid("a845c529-8362-4fac-b7d0-df079db04860"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "Overview of the basic stances used in Taekwondo.", "taekwondo_stances_image_url", "taekwondo_stances_sound_url", "Taekwondo Stances", "taekwondo_stances_video_url" },
                    { new Guid("aae05942-0586-4000-aca2-b02525c0f1ea"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "A deep dive into the origins and history of Taekwondo.", "history_of_taekwondo_image_url", "history_of_taekwondo_sound_url", "History of Taekwondo", "history_of_taekwondo_video_url" },
                    { new Guid("bf4135c9-db91-4354-956f-f1606bddccd8"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), "The formal etiquette and behavior expected in Taekwondo.", "taekwondo_etiquette_image_url", "taekwondo_etiquette_sound_url", "Taekwondo Etiquette", "taekwondo_etiquette_video_url" },
                    { new Guid("d93a6ee0-f753-4f04-ac35-7fbb1cd09803"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), "Introduction to basic self-defense techniques in Taekwondo.", "self_defense_image_url", "self_defense_sound_url", "Taekwondo Self-defense Techniques", "self_defense_video_url" },
                    { new Guid("e10b50fa-9224-4469-9f4c-7553db8328b6"), new Guid("362385ac-7c43-41b1-989c-b8d9ba6fce67"), "The different forms (patterns) performed in Taekwondo.", "forms_image_url", "forms_sound_url", "Taekwondo Forms", "forms_video_url" },
                    { new Guid("ecac9146-b9b2-492c-9561-032c39b1436b"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), "The significance of the Taekwondo training hall, the Dojang.", "dojang_image_url", "dojang_sound_url", "The Dojang", "dojang_video_url" }
                });

            migrationBuilder.InsertData(
                table: "Træninger",
                columns: new[] { "TræningID", "PensumID", "ProgramID", "QuizID", "TeknikID", "TeoriID", "Tid", "TræningRækkefølge", "ØvelseID" },
                values: new object[,]
                {
                    { new Guid("2faf5b1b-40a2-4599-99c9-c0e948a31dc7"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), new Guid("8c98eb00-1efb-4361-99b7-974c1aed66e8"), null, null, null, 90, 1, null },
                    { new Guid("3949e642-5f71-40b7-8c4f-7bdaee0686c9"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), new Guid("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47"), null, null, null, 80, 2, null },
                    { new Guid("4a91f6e6-3b23-4de7-85a1-1f173a90a27f"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), new Guid("11741c42-315e-42fe-a0a8-337afd6d511f"), null, null, null, 60, 1, null },
                    { new Guid("58d29731-5915-496c-9f52-b1c64199fdd7"), new Guid("67db5817-3c5a-4604-ba74-8076578528c3"), new Guid("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47"), null, null, null, 90, 1, null },
                    { new Guid("84a19dcc-6f70-49ce-9629-d04b0cb0c7dd"), new Guid("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973"), new Guid("11741c42-315e-42fe-a0a8-337afd6d511f"), null, null, null, 75, 2, null },
                    { new Guid("cf9ce0e3-f9d0-41a0-8486-7b9d41aa43be"), new Guid("dcd90332-5e1b-4352-bf88-fb40e75932bd"), new Guid("8c98eb00-1efb-4361-99b7-974c1aed66e8"), null, null, null, 80, 2, null }
                });

            migrationBuilder.InsertData(
                table: "BrugerQuizzer",
                columns: new[] { "BrugerID", "QuizID" },
                values: new object[] { new Guid("8bb14e0c-04a3-4679-aa0b-48b9978eb220"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2") });

            migrationBuilder.InsertData(
                table: "KlubQuizzer",
                columns: new[] { "KlubID", "QuizID" },
                values: new object[,]
                {
                    { new Guid("afa9ebbf-49bb-4737-9ab0-7d9d3153c993"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099") },
                    { new Guid("fed25ea9-7695-4945-a109-2900a24ff1ce"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644") }
                });

            migrationBuilder.InsertData(
                table: "Spørgsmål",
                columns: new[] { "SpørgsmålID", "QuizID", "SpørgsmålRækkefølge", "SpørgsmålTid", "TeknikID", "TeoriID", "ØvelseID" },
                values: new object[,]
                {
                    { new Guid("0a4b9bd3-1fc3-498c-9574-51d1db67cce4"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), 2, 30, new Guid("72385206-876c-4f9d-bfc6-dc8aa02ef587"), new Guid("aae05942-0586-4000-aca2-b02525c0f1ea"), new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6") },
                    { new Guid("2f02f12d-3c9a-4b63-b7f0-8df70d9ed799"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), 1, 30, new Guid("f4a14256-bf7c-4910-a4c4-13fc063a455a"), new Guid("bf4135c9-db91-4354-956f-f1606bddccd8"), new Guid("0335fde9-e05e-4a72-b08c-0c076803b395") },
                    { new Guid("3e5a25d1-a1a2-411d-b5ab-db0aead404c3"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), 3, 40, new Guid("49425f68-4bcf-412e-8ac3-0f87b3b117ca"), new Guid("a845c529-8362-4fac-b7d0-df079db04860"), new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42") },
                    { new Guid("464d80b9-28b4-4e16-949a-bddbafc4c6f1"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), 4, 45, new Guid("49425f68-4bcf-412e-8ac3-0f87b3b117ca"), new Guid("ecac9146-b9b2-492c-9561-032c39b1436b"), new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6") },
                    { new Guid("49da5e18-a087-40aa-8ea8-0a67e13d249b"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), 5, 60, new Guid("4d608ca7-227c-4dc5-b7b9-f8f2315233ec"), new Guid("26abad43-d433-416e-ba59-6ddb20a64093"), new Guid("d5a762e1-b401-4118-955d-bbb3d26f370e") },
                    { new Guid("4ba89cd8-b931-4071-a057-1c9740dac086"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), 1, 30, new Guid("4d608ca7-227c-4dc5-b7b9-f8f2315233ec"), new Guid("bf4135c9-db91-4354-956f-f1606bddccd8"), new Guid("0335fde9-e05e-4a72-b08c-0c076803b395") },
                    { new Guid("600a1b8b-6eed-434d-9a1f-1337124da834"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), 4, 45, new Guid("fe2b1123-5434-4c83-ab16-e8372bd99fef"), new Guid("ecac9146-b9b2-492c-9561-032c39b1436b"), new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6") },
                    { new Guid("67f31858-40f0-481d-844a-7dcc7c4e0b48"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), 2, 30, new Guid("72385206-876c-4f9d-bfc6-dc8aa02ef587"), new Guid("aae05942-0586-4000-aca2-b02525c0f1ea"), new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6") },
                    { new Guid("6e6089f3-1f29-412e-b7ba-9cfe652fecce"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), 3, 40, new Guid("49425f68-4bcf-412e-8ac3-0f87b3b117ca"), new Guid("a845c529-8362-4fac-b7d0-df079db04860"), new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42") },
                    { new Guid("85c9d413-5148-488c-a667-f971171d2d78"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), 5, 60, new Guid("fe2b1123-5434-4c83-ab16-e8372bd99fef"), new Guid("26abad43-d433-416e-ba59-6ddb20a64093"), new Guid("d5a762e1-b401-4118-955d-bbb3d26f370e") },
                    { new Guid("8fe43b9a-f64d-4701-911a-764204b423ad"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), 2, 30, new Guid("f4a14256-bf7c-4910-a4c4-13fc063a455a"), new Guid("aae05942-0586-4000-aca2-b02525c0f1ea"), new Guid("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6") },
                    { new Guid("94dac4ff-3ef1-41dd-8593-de3736627b98"), new Guid("69cde397-c39c-4172-8d95-56c9a5cdc099"), 3, 40, new Guid("72385206-876c-4f9d-bfc6-dc8aa02ef587"), new Guid("a845c529-8362-4fac-b7d0-df079db04860"), new Guid("1e86d0c9-1d34-4b46-a939-1326f7f9df42") },
                    { new Guid("a79c2d74-bccf-4a9f-ba03-ac02c906f6c7"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), 4, 45, new Guid("fe2b1123-5434-4c83-ab16-e8372bd99fef"), new Guid("ecac9146-b9b2-492c-9561-032c39b1436b"), new Guid("dcbcc571-4377-4fcc-91a8-fb83f07165f6") },
                    { new Guid("d1797c4a-4378-49a9-84d5-375efcff0d88"), new Guid("3b89bd9d-dd30-4ea4-8563-984dbfccb644"), 1, 30, new Guid("f4a14256-bf7c-4910-a4c4-13fc063a455a"), new Guid("bf4135c9-db91-4354-956f-f1606bddccd8"), new Guid("0335fde9-e05e-4a72-b08c-0c076803b395") },
                    { new Guid("d1a60bed-fd96-47dd-b86a-a944230d53bb"), new Guid("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2"), 5, 60, new Guid("4d608ca7-227c-4dc5-b7b9-f8f2315233ec"), new Guid("26abad43-d433-416e-ba59-6ddb20a64093"), new Guid("d5a762e1-b401-4118-955d-bbb3d26f370e") }
                });

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
