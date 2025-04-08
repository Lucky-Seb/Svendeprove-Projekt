using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        { }

        // DbSet properties for each model
        public DbSet<Bruger> Brugere { get; set; }
        public DbSet<Klub> Klubber { get; set; }
        public DbSet<Ordbog> Ordboger { get; set; }
        public DbSet<Øvelse> Øvelser { get; set; }
        public DbSet<Pensum> Pensum { get; set; }
        public DbSet<ProgramPlan> ProgramPlans { get; set; }
        public DbSet<Quiz> Quizzer { get; set; }
        public DbSet<Spørgsmål> Spørgsmål { get; set; }
        public DbSet<Teknik> Teknikker { get; set; }
        public DbSet<Teori> Teorier { get; set; }
        public DbSet<Træning> Træninger { get; set; }
        public DbSet<KlubProgram> KlubProgrammer { get; set; }
        public DbSet<BrugerProgram> BrugerProgrammer { get; set; }
        public DbSet<BrugerØvelse> BrugerØvelser { get; set; }
        public DbSet<BrugerQuiz> BrugerQuizzer { get; set; }
        public DbSet<KlubQuiz> KlubQuizzer { get; set; }
        public DbSet<BrugerKlub> BrugerKlubber { get; set; }
        public DbSet<KlubØvelse> KlubØvelser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define primary keys
            modelBuilder.Entity<Bruger>().HasKey(b => b.BrugerID);
            modelBuilder.Entity<Klub>().HasKey(k => k.KlubID);
            modelBuilder.Entity<Ordbog>().HasKey(o => o.Id);
            modelBuilder.Entity<Øvelse>().HasKey(e => e.ØvelseID);
            modelBuilder.Entity<Pensum>().HasKey(p => p.PensumID);
            modelBuilder.Entity<ProgramPlan>().HasKey(pp => pp.ProgramID);
            modelBuilder.Entity<Quiz>().HasKey(q => q.QuizID);
            modelBuilder.Entity<Spørgsmål>().HasKey(s => s.SpørgsmålID);
            modelBuilder.Entity<Teknik>().HasKey(t => t.TeknikID);
            modelBuilder.Entity<Teori>().HasKey(t => t.TeoriID);
            modelBuilder.Entity<Træning>().HasKey(t => t.TræningID);

            // Many-to-Many relationships
            modelBuilder.Entity<KlubProgram>().HasKey(kp => new { kp.KlubID, kp.ProgramID });
            modelBuilder.Entity<BrugerProgram>().HasKey(bp => new { bp.BrugerID, bp.ProgramID });
            modelBuilder.Entity<BrugerØvelse>().HasKey(bo => new { bo.BrugerID, bo.ØvelseID });
            modelBuilder.Entity<BrugerQuiz>().HasKey(bq => new { bq.BrugerID, bq.QuizID });
            modelBuilder.Entity<KlubQuiz>().HasKey(kq => new { kq.KlubID, kq.QuizID });
            modelBuilder.Entity<BrugerKlub>().HasKey(bk => new { bk.BrugerID, bk.KlubID });
            modelBuilder.Entity<KlubØvelse>().HasKey(ko => new { ko.KlubID, ko.ØvelseID });

            // Define relationships
            modelBuilder.Entity<KlubProgram>()
                .HasOne(kp => kp.Klub)
                .WithMany(k => k.KlubProgrammer)
                .HasForeignKey(kp => kp.KlubID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KlubProgram>()
                .HasOne(kp => kp.Plan)
                .WithMany(pp => pp.KlubProgrammer)
                .HasForeignKey(kp => kp.ProgramID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrugerProgram>()
                .HasOne(bp => bp.Bruger)
                .WithMany(b => b.BrugerProgrammer)
                .HasForeignKey(bp => bp.BrugerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrugerProgram>()
                .HasOne(bp => bp.Plan)
                .WithMany(pp => pp.BrugerProgrammer)
                .HasForeignKey(bp => bp.ProgramID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrugerØvelse>()
                .HasOne(bo => bo.Bruger)
                .WithMany(b => b.BrugerØvelser)
                .HasForeignKey(bo => bo.BrugerID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for BrugerØvelse

            modelBuilder.Entity<BrugerØvelse>()
                .HasOne(bo => bo.Øvelse)
                .WithMany(e => e.BrugerØvelses)
                .HasForeignKey(bo => bo.ØvelseID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for BrugerØvelse

            modelBuilder.Entity<BrugerQuiz>()
                .HasOne(bq => bq.Bruger)
                .WithMany(b => b.BrugerQuizzer)
                .HasForeignKey(bq => bq.BrugerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrugerQuiz>()
                .HasOne(bq => bq.Quiz)
                .WithMany(q => q.BrugerQuizzer)
                .HasForeignKey(bq => bq.QuizID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KlubQuiz>()
                .HasOne(kq => kq.Klub)
                .WithMany(k => k.KlubQuizzer)
                .HasForeignKey(kq => kq.KlubID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KlubQuiz>()
                .HasOne(kq => kq.Quiz)
                .WithMany(q => q.KlubQuizzer)
                .HasForeignKey(kq => kq.QuizID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrugerKlub>()
                .HasOne(bk => bk.Bruger)
                .WithMany(b => b.BrugerKlubber)
                .HasForeignKey(bk => bk.BrugerID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for KlubØvelse



            modelBuilder.Entity<BrugerKlub>()
                .HasOne(bk => bk.Klub)
                .WithMany(k => k.BrugerKlubber)
                .HasForeignKey(bk => bk.KlubID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for KlubØvelse


            modelBuilder.Entity<KlubØvelse>()
                .HasOne(ko => ko.Klub)
                .WithMany(k => k.KlubØvelser)
                .HasForeignKey(ko => ko.KlubID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for KlubØvelse

            modelBuilder.Entity<KlubØvelse>()
                .HasOne(ko => ko.Øvelse)
                .WithMany(e => e.KlubØvelses)
                .HasForeignKey(ko => ko.ØvelseID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for KlubØvelse

            // Define relationships for entities that belong to Pensum
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Pensum)
                .WithMany(p => p.Quizzer)
                .HasForeignKey(q => q.PensumID);

            modelBuilder.Entity<Teori>()
                .HasOne(t => t.Pensum)
                .WithMany(p => p.Teorier)
                .HasForeignKey(t => t.PensumID);

            modelBuilder.Entity<Teknik>()
                .HasOne(t => t.Pensum)
                .WithMany(p => p.Teknikker)
                .HasForeignKey(t => t.PensumID);

            // Define relationships for Træning
            modelBuilder.Entity<Træning>()
                .HasOne(t => t.ProgramPlan)
                .WithMany(p => p.Træninger)
                .HasForeignKey(t => t.ProgramID);

            modelBuilder.Entity<Træning>()
                .HasOne(t => t.Quiz)
                .WithMany()
                .HasForeignKey(t => t.QuizID);

            modelBuilder.Entity<Træning>()
                .HasOne(t => t.Teori)
                .WithMany()
                .HasForeignKey(t => t.TeoriID);

            modelBuilder.Entity<Træning>()
                .HasOne(t => t.Teknik)
                .WithMany()
                .HasForeignKey(t => t.TeknikID);

            modelBuilder.Entity<Træning>()
                .HasOne(t => t.Øvelse)
                .WithMany()
                .HasForeignKey(t => t.ØvelseID);

            // Define relationships for Spørgsmål
            modelBuilder.Entity<Spørgsmål>()
                .HasOne(s => s.Quiz)
                .WithMany(q => q.Spørgsmåls)
                .HasForeignKey(s => s.QuizID);  // Explicitly specify the foreign key
                                                //.OnDelete(DeleteBehavior.Cascade);  // Add the delete behavior if needed


            modelBuilder.Entity<Spørgsmål>()
                .HasOne(t => t.Teori)
                .WithMany()
                .HasForeignKey(t => t.TeoriID);

            modelBuilder.Entity<Spørgsmål>()
                .HasOne(t => t.Teknik)
                .WithMany()
                .HasForeignKey(t => t.TeknikID);

            modelBuilder.Entity<Spørgsmål>()
                .HasOne(t => t.Øvelse)
                .WithMany()
                .HasForeignKey(t => t.ØvelseID);

            // --- Seed Pensum ---
            modelBuilder.Entity<Pensum>().HasData(
                new Pensum { PensumID = 1, PensumGrad = "Hvidt Bælte" },
                new Pensum { PensumID = 2, PensumGrad = "Gult Bælte" }
            );

            // --- Seed Klub ---
            modelBuilder.Entity<Klub>().HasData(
                new Klub { KlubID = 1, KlubNavn = "København Taekwondo Klub" },
                new Klub { KlubID = 2, KlubNavn = "Aarhus Kampkunstcenter" }
            );

            // --- Seed Bruger ---
            modelBuilder.Entity<Bruger>().HasData(
                new Bruger
                {
                    BrugerID = 1,
                    Email = "emma@dojo.dk",
                    Brugernavn = "emma123",
                    Fornavn = "Emma",
                    Efternavn = "Jensen",
                    Brugerkode = "123456", // hash in real app
                    Address = "Nørrebrogade 42",
                    Bæltegrad = "Gult Bælte",
                    Role = "Bruger"
                }
            );

            // --- BrugerKlub ---
            modelBuilder.Entity<BrugerKlub>().HasData(
                new BrugerKlub { BrugerID = 1, KlubID = 1 }
            );

            // --- Øvelse ---
            modelBuilder.Entity<Øvelse>().HasData(
                new Øvelse
                {
                    ØvelseID = 1,
                    ØvelseNavn = "Front Spark",
                    ØvelseBeskrivelse = "En simpel frontspark teknik.",
                    ØvelseBillede = "",
                    ØvelseVideo = "",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Begynder",
                    PensumID = 1
                }
            );

            // --- BrugerØvelse ---
            modelBuilder.Entity<BrugerØvelse>().HasData(
                new BrugerØvelse { BrugerID = 1, ØvelseID = 1 }
            );

            // --- KlubØvelse ---
            modelBuilder.Entity<KlubØvelse>().HasData(
                new KlubØvelse { KlubID = 1, ØvelseID = 1 }
            );

            // --- Teori ---
            modelBuilder.Entity<Teori>().HasData(
                new Teori
                {
                    TeoriID = 1,
                    TeoriNavn = "Respect",
                    TeoriBeskrivelse = "Respekt for dojo og lærere.",
                    TeoriBillede = "",
                    TeoriVideo = "",
                    TeoriLyd = "",
                    PensumID = 1
                }
            );

            // --- Teknik ---
            modelBuilder.Entity<Teknik>().HasData(
                new Teknik
                {
                    TeknikID = 1,
                    TeknikNavn = "Blokering",
                    TeknikBeskrivelse = "Forsvar mod angreb.",
                    TeknikBillede = "",
                    TeknikVideo = "",
                    TeknikLyd = "",
                    PensumID = 1
                }
            );

            // --- Quiz ---
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    QuizID = 1,
                    QuizNavn = "Begynder Quiz",
                    QuizBeskrivelse = "Spørgsmål for begyndere",
                    PensumID = 1
                }
            );

            // --- Spørgsmål ---
            modelBuilder.Entity<Spørgsmål>().HasData(
                new Spørgsmål
                {
                    SpørgsmålID = 1,
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = 1,
                    TeoriID = 1
                }
            );

            // --- BrugerQuiz ---
            modelBuilder.Entity<BrugerQuiz>().HasData(
                new BrugerQuiz { BrugerID = 1, QuizID = 1 }
            );

            // --- KlubQuiz ---
            modelBuilder.Entity<KlubQuiz>().HasData(
                new KlubQuiz { KlubID = 1, QuizID = 1 }
            );

            // --- ProgramPlan ---
            modelBuilder.Entity<ProgramPlan>().HasData(
                new ProgramPlan
                {
                    ProgramID = 1,
                    ProgramNavn = "Intro Program",
                    Beskrivelse = "2 ugers intro",
                    Længde = 14,
                    OprettelseDato = DateTime.UtcNow
                }
            );

            // --- KlubProgram ---
            modelBuilder.Entity<KlubProgram>().HasData(
                new KlubProgram { KlubID = 1, ProgramID = 1 }
            );

            // --- BrugerProgram ---
            modelBuilder.Entity<BrugerProgram>().HasData(
                new BrugerProgram { BrugerID = 1, ProgramID = 1 }
            );

            // --- Træning ---
            modelBuilder.Entity<Træning>().HasData(
                new Træning
                {
                    TræningID = 1,
                    TræningRækkefølge = 1,
                    Tid = 45,
                    ProgramID = 1,
                    QuizID = 1,
                    TeoriID = 1,
                    TeknikID = 1,
                    ØvelseID = 1,
                    PensumID = 1
                }
            );

            // --- Ordbog ---
            modelBuilder.Entity<Ordbog>().HasData(
                new Ordbog
                {
                    Id = 1,
                    DanskOrd = "Hilsen",
                    KoranskOrd = "Annyeonghaseyo",
                    Beskrivelse = "En typisk hilsen i kampkunst",
                    BilledeLink = "",
                    LydLink = "",
                    VideoLink = ""
                }
            );
        }
    }
}
