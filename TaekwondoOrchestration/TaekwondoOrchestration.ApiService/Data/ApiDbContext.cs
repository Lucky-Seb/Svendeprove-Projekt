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

            // Define fixed GUIDs for seeding relationships
            var brugerID1 = Guid.Parse("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var brugerID2 = Guid.Parse("b2e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var brugerID3 = Guid.Parse("c3e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var brugerID4 = Guid.Parse("d4e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var brugerID5 = Guid.Parse("e5e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var klubID = Guid.Parse("a2e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var programID = Guid.Parse("7f5d8424-0c8a-4532-b4f0-19508fda7f5c");
            var øvelseID = Guid.Parse("e8c0420b-8360-4a24-bb30-3eabb99462b1");
            var teknikID = Guid.Parse("6e8726d4-9e4d-4389-b8b7-09829c08c826");
            var teoriID = Guid.Parse("4f5ac028-30be-4c93-b7e9-21d95a4b1a97");
            var quizID = Guid.Parse("0b35a9fd-d575-4712-8e35-58d7a3ecb0be");
            var pensumID = Guid.Parse("a35b2394-16f2-4bc1-8aef-8c94b0a334b1");
            var træningID = Guid.Parse("d3b50b32-67d3-4fa7-9db8-32b6e850c409");
            var ordbogID = Guid.Parse("4d89d76d-f206-4018-bb7f-35dbf87c4f9e");

            modelBuilder.Entity<Bruger>().HasData(
                new Bruger
                {
                    BrugerID = brugerID1,
                    Email = "john.doe@example.com",
                    Brugernavn = "johndoe123",
                    Fornavn = "John",
                    Efternavn = "Doe",
                    Brugerkode = "hashed_password",  // This should be a hashed password
                    Address = "123 Taekwondo St.",
                    Bæltegrad = "Hvidt Bælte",
                    Role = "Bruger"
                },
                new Bruger
                {
                    BrugerID = brugerID2,
                    Email = "jane.doe@example.com",
                    Brugernavn = "janedoe456",
                    Fornavn = "Jane",
                    Efternavn = "Doe",
                    Brugerkode = "hashed_password2",  // This should be a hashed password
                    Address = "456 Taekwondo St.",
                    Bæltegrad = "Gult Bælte",
                    Role = "Bruger"
                },
                new Bruger
                {
                    BrugerID = brugerID3,
                    Email = "mark.smith@example.com",
                    Brugernavn = "marksmith789",
                    Fornavn = "Mark",
                    Efternavn = "Smith",
                    Brugerkode = "hashed_password3",  // This should be a hashed password
                    Address = "789 Taekwondo St.",
                    Bæltegrad = "Blåt Bælte",
                    Role = "Bruger"
                },
                new Bruger
                {
                    BrugerID = brugerID4,
                    Email = "lucy.jones@example.com",
                    Brugernavn = "lucyjones321",
                    Fornavn = "Lucy",
                    Efternavn = "Jones",
                    Brugerkode = "hashed_password4",  // This should be a hashed password
                    Address = "321 Taekwondo St.",
                    Bæltegrad = "Grønt Bælte",
                    Role = "Bruger"
                },
                new Bruger
                {
                    BrugerID = brugerID5,
                    Email = "robert.brown@example.com",
                    Brugernavn = "robertbrown654",
                    Fornavn = "Robert",
                    Efternavn = "Brown",
                    Brugerkode = "hashed_password5",  // This should be a hashed password
                    Address = "654 Taekwondo St.",
                    Bæltegrad = "Brunt Bælte",
                    Role = "Bruger"
                }
            );

            // Seed for Klub
            modelBuilder.Entity<Klub>().HasData(
            new Klub
            {
                KlubID = klubID,
                KlubNavn = "Taekwondo Club"
            }
            );

            // Seed for ProgramPlan
            modelBuilder.Entity<ProgramPlan>().HasData(
                new ProgramPlan
                {
                    ProgramID = programID,
                    ProgramNavn = "Basic Taekwondo Program",
                    OprettelseDato = DateTime.UtcNow,
                    Længde = 4,
                    Beskrivelse = "A basic program to get started with Taekwondo."
                }
            );

            // Seed for Øvelse
            modelBuilder.Entity<Øvelse>().HasData(
                new Øvelse
                {
                    ØvelseID = øvelseID,
                    ØvelseNavn = "Front Kick",
                    ØvelseBeskrivelse = "A basic front kick in Taekwondo.",
                    ØvelseBillede = "front_kick_image_url",
                    ØvelseVideo = "front_kick_video_url",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Let",
                    PensumID = pensumID  // Example PensumID
                }
            );

            // Seed for Teknik
            modelBuilder.Entity<Teknik>().HasData(
                new Teknik
                {
                    TeknikID = teknikID,
                    TeknikNavn = "Roundhouse Kick",
                    TeknikBeskrivelse = "A powerful kick aimed at the opponent's head or torso.",
                    TeknikBillede = "roundhouse_kick_image_url",
                    TeknikVideo = "roundhouse_kick_video_url",
                    TeknikLyd = "roundhouse_kick_sound_url",
                    PensumID = pensumID  // Example PensumID
                }
            );

            // Seed for Teori
            modelBuilder.Entity<Teori>().HasData(
                new Teori
                {
                    TeoriID = teoriID,
                    TeoriNavn = "Taekwondo Etiquette",
                    TeoriBeskrivelse = "The formal etiquette and behavior expected in Taekwondo.",
                    TeoriBillede = "taekwondo_etiquette_image_url",
                    TeoriVideo = "taekwondo_etiquette_video_url",
                    TeoriLyd = "taekwondo_etiquette_sound_url",
                    PensumID = pensumID  // Example PensumID
                }
            );

            // Seed for Quiz
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    QuizID = quizID,
                    QuizNavn = "Taekwondo Basics Quiz",
                    QuizBeskrivelse = "A quiz to test your knowledge of basic Taekwondo concepts.",
                    PensumID = pensumID  // Example PensumID
                }
            );

            // Seed for Spørgsmål
            modelBuilder.Entity<Spørgsmål>().HasData(
                new Spørgsmål
                {
                    SpørgsmålID = Guid.NewGuid(),
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = quizID,  // Reference the quiz
                    TeoriID = teoriID,  // Reference the theory
                    TeknikID = teknikID,  // Reference the technique
                    ØvelseID = øvelseID  // Reference the exercise
                }
            );

            // Seed for Ordbog
            modelBuilder.Entity<Ordbog>().HasData(
                new Ordbog
                {
                    OrdbogId = ordbogID,
                    DanskOrd = "Taekwondo",
                    KoranskOrd = "تايكوندو",
                    Beskrivelse = "A Korean martial art focusing on high kicks and hand techniques.",
                    BilledeLink = "taekwondo_image_url",
                    LydLink = "taekwondo_sound_url",
                    VideoLink = "taekwondo_video_url"
                }
            );

            // Seed for Pensum
            modelBuilder.Entity<Pensum>().HasData(
                new Pensum
                {
                    PensumID = pensumID,
                    PensumGrad = "Beginner"
                }
            );

            // Seed for Træning
            modelBuilder.Entity<Træning>().HasData(
                new Træning
                {
                    TræningID = træningID,
                    TræningRækkefølge = 1,
                    Tid = 60,
                    ProgramID = programID,  // Reference the program
                    PensumID = pensumID  // Reference the Pensum
                }
            );

            // Seed for BrugerKlub (Link Bruger and Klub)
            modelBuilder.Entity<BrugerKlub>().HasData(
                new BrugerKlub
                {
                    BrugerID = brugerID,  // Link to Bruger
                    KlubID = klubID           // Link to Klub
                }
            );

            // Seed for KlubProgram (Link Klub and ProgramPlan)
            modelBuilder.Entity<KlubProgram>().HasData(
                new KlubProgram
                {
                    KlubID = klubID,  // Link to Klub
                    ProgramID = programID  // Link to ProgramPlan
                }
            );

            // Seed for KlubQuiz (Link Klub and Quiz)
            modelBuilder.Entity<KlubQuiz>().HasData(
                new KlubQuiz
                {
                    KlubID = klubID,  // Link to Klub
                    QuizID = quizID  // Link to Quiz
                }
            );

            // Seed for KlubØvelse (Link Klub and Øvelse)
            modelBuilder.Entity<KlubØvelse>().HasData(
                new KlubØvelse
                {
                    KlubID = klubID,  // Link to Klub
                    ØvelseID = øvelseID  // Link to Øvelse
                }
            );
        }
    }
}

