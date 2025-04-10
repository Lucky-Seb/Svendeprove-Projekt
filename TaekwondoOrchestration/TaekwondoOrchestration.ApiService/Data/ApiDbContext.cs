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

            // Define primary keys and sequential GUID generation
            modelBuilder.Entity<Bruger>().HasKey(b => b.BrugerID);
            modelBuilder.Entity<Bruger>().Property(b => b.BrugerID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Klub>().HasKey(k => k.KlubID);
            modelBuilder.Entity<Klub>().Property(k => k.KlubID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Ordbog>().HasKey(o => o.OrdbogId);
            modelBuilder.Entity<Ordbog>().Property(o => o.OrdbogId).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Øvelse>().HasKey(e => e.ØvelseID);
            modelBuilder.Entity<Øvelse>().Property(e => e.ØvelseID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Pensum>().HasKey(p => p.PensumID);
            modelBuilder.Entity<Pensum>().Property(p => p.PensumID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ProgramPlan>().HasKey(pp => pp.ProgramID);
            modelBuilder.Entity<ProgramPlan>().Property(pp => pp.ProgramID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Quiz>().HasKey(q => q.QuizID);
            modelBuilder.Entity<Quiz>().Property(q => q.QuizID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Spørgsmål>().HasKey(s => s.SpørgsmålID);
            modelBuilder.Entity<Spørgsmål>().Property(s => s.SpørgsmålID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Teknik>().HasKey(t => t.TeknikID);
            modelBuilder.Entity<Teknik>().Property(t => t.TeknikID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Teori>().HasKey(t => t.TeoriID);
            modelBuilder.Entity<Teori>().Property(t => t.TeoriID).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Træning>().HasKey(t => t.TræningID);
            modelBuilder.Entity<Træning>().Property(t => t.TræningID).HasDefaultValueSql("NEWSEQUENTIALID()");

            // Many-to-Many relationships (with GUID composite keys)
            modelBuilder.Entity<KlubProgram>().HasKey(kp => new { kp.KlubID, kp.ProgramID });
            modelBuilder.Entity<BrugerProgram>().HasKey(bp => new { bp.BrugerID, bp.ProgramID });
            modelBuilder.Entity<BrugerØvelse>().HasKey(bo => new { bo.BrugerID, bo.ØvelseID });
            modelBuilder.Entity<BrugerQuiz>().HasKey(bq => new { bq.BrugerID, bq.QuizID });
            modelBuilder.Entity<KlubQuiz>().HasKey(kq => new { kq.KlubID, kq.QuizID });
            modelBuilder.Entity<BrugerKlub>().HasKey(bk => new { bk.BrugerID, bk.KlubID });
            modelBuilder.Entity<KlubØvelse>().HasKey(ko => new { ko.KlubID, ko.ØvelseID });

            // Define relationships and cascade deletes
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
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for BrugerKlub

            modelBuilder.Entity<BrugerKlub>()
                .HasOne(bk => bk.Klub)
                .WithMany(k => k.BrugerKlubber)
                .HasForeignKey(bk => bk.KlubID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for BrugerKlub

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

            // Generate static GUIDs for the entities
            var programId = Guid.Parse("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe");  // Replace with your fixed GUID
            var quizId = Guid.Parse("29f8a1b3-62f0-4d92-b7ad-4079239a9730");      // Replace with your fixed GUID
            var teoriId = Guid.Parse("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547");    // Replace with your fixed GUID
            var teknikId = Guid.Parse("81c54d38-cb85-420b-b697-13f1f2a8c1cd");   // Replace with your fixed GUID
            var øvelseId = Guid.Parse("431e1b6b-3b4f-442b-b97f-11b238f660b2");  // Replace with your fixed GUID
            var pensumId = Guid.Parse("08ff7a3e-0627-493b-92c9-36c26f6ad7fa");  // Replace with your fixed GUID

            // Seed data for entities
            modelBuilder.Entity<Bruger>().HasData(
                new Bruger
                {
                    BrugerID = Guid.Parse("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), // Replace with your fixed GUID
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

            modelBuilder.Entity<BrugerKlub>().HasData(
                new BrugerKlub { BrugerID = Guid.Parse("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), KlubID = Guid.Parse("c2b62e9a-38da-43ab-9731-f3641cd3121d") } // Replace with your fixed GUIDs
            );

            modelBuilder.Entity<Øvelse>().HasData(
                new Øvelse
                {
                    ØvelseID = øvelseId,
                    ØvelseNavn = "Front Spark",
                    ØvelseBeskrivelse = "En simpel frontspark teknik.",
                    ØvelseBillede = "",
                    ØvelseVideo = "",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Begynder",
                    PensumID = pensumId
                }
            );

            modelBuilder.Entity<BrugerØvelse>().HasData(
                new BrugerØvelse { BrugerID = Guid.Parse("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), ØvelseID = øvelseId }
            );

            modelBuilder.Entity<KlubØvelse>().HasData(
                new KlubØvelse { KlubID = Guid.Parse("c2b62e9a-38da-43ab-9731-f3641cd3121d"), ØvelseID = øvelseId }
            );

            modelBuilder.Entity<Teori>().HasData(
                new Teori
                {
                    TeoriID = teoriId,
                    TeoriNavn = "Respect",
                    TeoriBeskrivelse = "Respekt for dojo og lærere.",
                    TeoriBillede = "",
                    TeoriVideo = "",
                    TeoriLyd = "",
                    PensumID = pensumId
                }
            );

            modelBuilder.Entity<Teknik>().HasData(
                new Teknik
                {
                    TeknikID = teknikId,
                    TeknikNavn = "Blokering",
                    TeknikBeskrivelse = "Forsvar mod angreb.",
                    TeknikBillede = "",
                    TeknikVideo = "",
                    TeknikLyd = "",
                    PensumID = pensumId
                }
            );

            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    QuizID = quizId,
                    QuizNavn = "Begynder Quiz",
                    QuizBeskrivelse = "Spørgsmål for begyndere",
                    PensumID = pensumId
                }
            );

            modelBuilder.Entity<Spørgsmål>().HasData(
                new Spørgsmål
                {
                    SpørgsmålID = Guid.Parse("f2563f57-92c7-4388-b920-bf38e47e9d12"), // Replace with your fixed GUID
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = quizId,
                    TeoriID = teoriId
                }
            );

            modelBuilder.Entity<BrugerQuiz>().HasData(
                new BrugerQuiz { BrugerID = Guid.Parse("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), QuizID = quizId }
            );

            modelBuilder.Entity<KlubQuiz>().HasData(
                new KlubQuiz { KlubID = Guid.Parse("c2b62e9a-38da-43ab-9731-f3641cd3121d"), QuizID = quizId }
            );

            // Seed data for ProgramPlan
            modelBuilder.Entity<ProgramPlan>().HasData(
                new ProgramPlan
                {
                    ProgramID = programId,
                    ProgramNavn = "Intro Program",
                    Beskrivelse = "2 ugers intro",
                    Længde = 14,
                    OprettelseDato = new DateTime(2025, 4, 8)  // Fixed date instead of DateTime.UtcNow
                }
            );

            // Link ProgramPlan to KlubProgram and BrugerProgram using their fixed GUIDs
            modelBuilder.Entity<KlubProgram>().HasData(
                new KlubProgram { KlubID = Guid.Parse("c2b62e9a-38da-43ab-9731-f3641cd3121d"), ProgramID = programId }
            );

            modelBuilder.Entity<BrugerProgram>().HasData(
                new BrugerProgram { BrugerID = Guid.Parse("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"), ProgramID = programId }
            );

            // Seed data for Træning and link it to ProgramPlan using the fixed GUID
            modelBuilder.Entity<Træning>().HasData(
                new Træning
                {
                    TræningID = Guid.Parse("a3e2121b-b256-4564-bff1-2f2c94ed00de"), // Replace with your fixed GUID
                    TræningRækkefølge = 1,
                    Tid = 45,
                    ProgramID = programId, // Using the fixed ProgramPlan GUID
                    QuizID = quizId,
                    TeoriID = teoriId,
                    TeknikID = teknikId,
                    ØvelseID = øvelseId,
                    PensumID = pensumId
                }
            );
        }
    }
}
