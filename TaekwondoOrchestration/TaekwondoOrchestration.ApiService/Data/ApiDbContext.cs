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

            var klubID1 = Guid.Parse("f1a4f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var klubID2 = Guid.Parse("a2e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var klubID3 = Guid.Parse("b3e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var klubID4 = Guid.Parse("c4e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var klubID5 = Guid.Parse("d5e7d72e-2d56-4c64-b536-ff2b742bfcdc");

            var programID1 = Guid.Parse("e1a4f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var programID2 = Guid.Parse("f2b7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var programID3 = Guid.Parse("g3c8d72e-2d56-4c64-b536-ff2b742bfcdc");

            // Define fixed GUIDs for seeding Øvelse
            var øvelseID1 = Guid.Parse("f1a7f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var øvelseID2 = Guid.Parse("c2b8f72e-2d56-4c64-b536-ff2b742bfcdc");
            var øvelseID3 = Guid.Parse("d3c9e72e-2d56-4c64-b536-ff2b742bfcdc");
            var øvelseID4 = Guid.Parse("e4d0f72e-2d56-4c64-b536-ff2b742bfcdc");
            var øvelseID5 = Guid.Parse("f5e1f72e-2d56-4c64-b536-ff2b742bfcdc");
            var øvelseID6 = Guid.Parse("f7e2f82e-3d56-5c64-c637-gg3b743bfcdc");
            var øvelseID7 = Guid.Parse("a8e3f92e-4e67-5d75-b548-hh4c824bfcdc");
            var øvelseID8 = Guid.Parse("b9e4f02e-5f78-6e86-c659-ii5d935bfcdc");
            var øvelseID9 = Guid.Parse("c0f5f12e-6g89-7f97-d760-jj6e046bfcdc");
            var øvelseID10 = Guid.Parse("d1f6f22e-7h90-8g08-e871-kk7f157bfcdc");

            var teknikID1 = Guid.Parse("a1d6f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var teknikID2 = Guid.Parse("b2e7d72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID3 = Guid.Parse("c3f8d72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID4 = Guid.Parse("d4g9e72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID5 = Guid.Parse("e5h0f72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID6 = Guid.Parse("f6i1g72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID7 = Guid.Parse("g7j2h72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID8 = Guid.Parse("h8k3i72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID9 = Guid.Parse("i9l4j72e-2d56-4c64-b536-ff2b742bfcdc");
            var teknikID10 = Guid.Parse("j0m5k72e-2d56-4c64-b536-ff2b742bfcdc");

            var teoriID1 = Guid.Parse("b1a7f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var teoriID2 = Guid.Parse("c2b8f72e-2d56-4c64-b536-ff2b742bfcdc");
            var teoriID3 = Guid.Parse("d3c9e72e-2d56-4c64-b536-ff2b742bfcdc");
            var teoriID4 = Guid.Parse("e4d0f72e-2d56-4c64-b536-ff2b742bfcdc");
            var teoriID5 = Guid.Parse("f5e1f72e-2d56-4c64-b536-ff2b742bfcdc");
            var teoriID6 = Guid.Parse("f7e2f82e-3d56-5c64-c637-gg3b743bfcdc");
            var teoriID7 = Guid.Parse("a8e3f92e-4e67-5d75-b548-hh4c824bfcdc");
            var teoriID8 = Guid.Parse("b9e4f02e-5f78-6e86-c659-ii5d935bfcdc");
            var teoriID9 = Guid.Parse("c0f5f12e-6g89-7f97-d760-jj6e046bfcdc");
            var teoriID10 = Guid.Parse("d1f6f22e-7h90-8g08-e871-kk7f157bfcdc");

            // Fixed GUID values for Quiz and Pensum
            var quizID1 = Guid.Parse("a743a0b1-75b6-49ad-9f8f-1bfbf2107fd3");
            var quizID2 = Guid.Parse("0b413fea-992d-4c3f-b072-c5294f4c2646");
            var quizID3 = Guid.Parse("4f71a72d-d94a-46d0-b917-bf7b17fd2191");

            var pensumID1 = Guid.Parse("b2a7f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var pensumID2 = Guid.Parse("c3b8f72e-2d56-4c64-b536-ff2b742bfcdc");
            var pensumID3 = Guid.Parse("d4c9e72e-2d56-4c64-b536-ff2b742bfcdc");
            var pensumID4 = Guid.Parse("e5d0f72e-2d56-4c64-b536-ff2b742bfcdc");
            var pensumID5 = Guid.Parse("f6e1g72e-2d56-4c64-b536-ff2b742bfcdc");

            // Static GUIDs for Spørgsmål (Question IDs) - fixed GUIDs
            var spørgsmålID1 = Guid.Parse("a93d7c1a-933f-44a7-b6c7-5f03a05214a7");
            var spørgsmålID2 = Guid.Parse("5a7633c4-b45b-4fbb-8d8b-59c5aeb1703e");
            var spørgsmålID3 = Guid.Parse("82e42e23-8f83-44b7-bd72-99c82e1c36ad");
            var spørgsmålID4 = Guid.Parse("6fd5a2f4-b970-4bb7-b870-dcf1b10c16be");
            var spørgsmålID5 = Guid.Parse("4a836fc1-8005-4a9d-b3a2-230d47ff2978");

            var spørgsmålID6 = Guid.Parse("89b0d93c-970e-4558-92e7-f6cb4090105e");
            var spørgsmålID7 = Guid.Parse("5fe63557-8ca8-47d9-954b-6f804ed404fe");
            var spørgsmålID8 = Guid.Parse("0dff6ed4-3ec7-4b61-b540-baa7c9ca92a4");
            var spørgsmålID9 = Guid.Parse("6f6d9a4d-722d-4594-b41c-758f27785a5d");
            var spørgsmålID10 = Guid.Parse("7fa5f8f9-cf02-4a0f-bc2f-5729c3d3f5a5");

            var spørgsmålID11 = Guid.Parse("55bc4a82-3cf6-4fbd-bd9c-4e9b6fe245bb");
            var spørgsmålID12 = Guid.Parse("ccdf344a-4fbc-46f6-b9c6-b906d73c0a4e");
            var spørgsmålID13 = Guid.Parse("9e8d3e8d-4ff9-4e74-a840-69ac02212b1f");
            var spørgsmålID14 = Guid.Parse("8a67c5a6-bd19-4e76-8fd6-e5be6c0d8c3f");
            var spørgsmålID15 = Guid.Parse("113fb830-ffba-4fd8-b264-82c6f4640571");

            // Static GUIDs for Træning (Training IDs) and other related IDs
            var træningID1 = Guid.Parse("bb33c7e1-50b5-4639-a314-18f7ad365c98");
            var træningID2 = Guid.Parse("d3b376cf-c3c7-48a5-8125-f199a5b7809b");
            var træningID3 = Guid.Parse("929ea3b7-b0d5-417b-9a9d-c8f29e56c77b");
            var træningID4 = Guid.Parse("99f49c4b-8b7f-4c62-9283-fc249e3d8bfb");
            var træningID5 = Guid.Parse("f4a7f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var træningID6 = Guid.Parse("c2b8f72e-2d56-4c64-b536-ff2b742bfcdc");

            // Define fixed GUIDs for seeding ordbog
            var ordbogID1 = Guid.Parse("f1a7f9c9-6d77-4a8d-a3f3-b0f5095df9fe");
            var ordbogID2 = Guid.Parse("c2b8f72e-2d56-4c64-b536-ff2b742bfcdc");
            var ordbogID3 = Guid.Parse("d3c9e72e-2d56-4c64-b536-ff2b742bfcdc");
            var ordbogID4 = Guid.Parse("e4d0f72e-2d56-4c64-b536-ff2b742bfcdc");
            var ordbogID5 = Guid.Parse("f5e1f72e-2d56-4c64-b536-ff2b742bfcdc");
            var ordbogID6 = Guid.Parse("f7e2f82e-3d56-5c64-c637-gg3b743bfcdc");
            var ordbogID7 = Guid.Parse("a8e3f92e-4e67-5d75-b548-hh4c824bfcdc");
            var ordbogID8 = Guid.Parse("b9e4f02e-5f78-6e86-c659-ii5d935bfcdc");
            var ordbogID9 = Guid.Parse("c0f5f12e-6g89-7f97-d760-jj6e046bfcdc");
            var ordbogID10 = Guid.Parse("d1f6f22e-7h90-8g08-e871-kk7f157bfcdc");

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
                    KlubID = klubID1,
                    KlubNavn = "Taekwondo Club A"
                },
                new Klub
                {
                    KlubID = klubID2,
                    KlubNavn = "Taekwondo Club B"
                },
                new Klub
                {
                    KlubID = klubID3,
                    KlubNavn = "Taekwondo Club C"
                },
                new Klub
                {
                    KlubID = klubID4,
                    KlubNavn = "Taekwondo Club D"
                },
                new Klub
                {
                    KlubID = klubID5,
                    KlubNavn = "Taekwondo Club E"
                }
            );

            // Seed for Pensum
            modelBuilder.Entity<Pensum>().HasData(
                new Pensum
                {
                    PensumID = pensumID1,
                    PensumGrad = "Hvid"
                },
                new Pensum
                {
                    PensumID = pensumID2,
                    PensumGrad = "Gult"
                },
                new Pensum
                {
                    PensumID = pensumID3,
                    PensumGrad = "Blåt"
                },
                new Pensum
                {
                    PensumID = pensumID4,
                    PensumGrad = "Rød"
                },
                new Pensum
                {
                    PensumID = pensumID5,
                    PensumGrad = "Sort"
                }
            );

            // Seed for ProgramPlan
            modelBuilder.Entity<ProgramPlan>().HasData(
                new ProgramPlan
                {
                    ProgramID = programID1,
                    ProgramNavn = "Basic Taekwondo Program",
                    OprettelseDato = DateTime.UtcNow,
                    Længde = 4,
                    Beskrivelse = "A basic program to get started with Taekwondo."
                },
                new ProgramPlan
                {
                    ProgramID = programID2,
                    ProgramNavn = "Intermediate Taekwondo Program",
                    OprettelseDato = DateTime.UtcNow,
                    Længde = 6,
                    Beskrivelse = "An intermediate program to enhance your Taekwondo skills."
                },
                new ProgramPlan
                {
                    ProgramID = programID3,
                    ProgramNavn = "Advanced Taekwondo Program",
                    OprettelseDato = DateTime.UtcNow,
                    Længde = 8,
                    Beskrivelse = "An advanced program for mastering Taekwondo techniques."
                }
            );

            // Seed for Teknik
            modelBuilder.Entity<Teknik>().HasData(
                new Teknik
                {
                    TeknikID = teknikID1,
                    TeknikNavn = "Roundhouse Kick",
                    TeknikBeskrivelse = "A powerful kick aimed at the opponent's head or torso.",
                    TeknikBillede = "roundhouse_kick_image_url",
                    TeknikVideo = "roundhouse_kick_video_url",
                    TeknikLyd = "roundhouse_kick_sound_url",
                    PensumID = pensumID1
                },
                new Teknik
                {
                    TeknikID = teknikID2,
                    TeknikNavn = "Front Kick",
                    TeknikBeskrivelse = "A quick kick aimed at the opponent's stomach or face.",
                    TeknikBillede = "front_kick_image_url",
                    TeknikVideo = "front_kick_video_url",
                    TeknikLyd = "front_kick_sound_url",
                    PensumID = pensumID1
                },
                new Teknik
                {
                    TeknikID = teknikID3,
                    TeknikNavn = "Back Kick",
                    TeknikBeskrivelse = "A kick delivered by turning your back to the opponent and kicking backward.",
                    TeknikBillede = "back_kick_image_url",
                    TeknikVideo = "back_kick_video_url",
                    TeknikLyd = "back_kick_sound_url",
                    PensumID = pensumID2
                },
                new Teknik
                {
                    TeknikID = teknikID4,
                    TeknikNavn = "Side Kick",
                    TeknikBeskrivelse = "A powerful kick aimed at the opponent's torso, delivered from the side.",
                    TeknikBillede = "side_kick_image_url",
                    TeknikVideo = "side_kick_video_url",
                    TeknikLyd = "side_kick_sound_url",
                    PensumID = pensumID2
                },
                new Teknik
                {
                    TeknikID = teknikID5,
                    TeknikNavn = "Hammerfist",
                    TeknikBeskrivelse = "A strike using the bottom of the fist, like a hammer blow.",
                    TeknikBillede = "hammerfist_image_url",
                    TeknikVideo = "hammerfist_video_url",
                    TeknikLyd = "hammerfist_sound_url",
                    PensumID = pensumID3
                },
                new Teknik
                {
                    TeknikID = teknikID6,
                    TeknikNavn = "Elbow Strike",
                    TeknikBeskrivelse = "A strike using the elbow to target the opponent's head or torso.",
                    TeknikBillede = "elbow_strike_image_url",
                    TeknikVideo = "elbow_strike_video_url",
                    TeknikLyd = "elbow_strike_sound_url",
                    PensumID = pensumID3
                },
                new Teknik
                {
                    TeknikID = teknikID7,
                    TeknikNavn = "Knee Strike",
                    TeknikBeskrivelse = "A strike using the knee to target the opponent's midsection or head.",
                    TeknikBillede = "knee_strike_image_url",
                    TeknikVideo = "knee_strike_video_url",
                    TeknikLyd = "knee_strike_sound_url",
                    PensumID = pensumID1
                },
                new Teknik
                {
                    TeknikID = teknikID8,
                    TeknikNavn = "Axe Kick",
                    TeknikBeskrivelse = "A kick where the foot comes down like an axe to strike the opponent.",
                    TeknikBillede = "axe_kick_image_url",
                    TeknikVideo = "axe_kick_video_url",
                    TeknikLyd = "axe_kick_sound_url",
                    PensumID = pensumID2
                },
                new Teknik
                {
                    TeknikID = teknikID9,
                    TeknikNavn = "Spinning Back Kick",
                    TeknikBeskrivelse = "A spinning kick where you turn around and strike with a powerful back kick.",
                    TeknikBillede = "spinning_back_kick_image_url",
                    TeknikVideo = "spinning_back_kick_video_url",
                    TeknikLyd = "spinning_back_kick_sound_url",
                    PensumID = pensumID3
                },
                new Teknik
                {
                    TeknikID = teknikID10,
                    TeknikNavn = "Knife Hand Strike",
                    TeknikBeskrivelse = "A strike using the edge of the hand, aimed at vulnerable areas.",
                    TeknikBillede = "knife_hand_strike_image_url",
                    TeknikVideo = "knife_hand_strike_video_url",
                    TeknikLyd = "knife_hand_strike_sound_url",
                    PensumID = pensumID5
                }
            );

            // Seed for Teori
            modelBuilder.Entity<Teori>().HasData(
                new Teori
                {
                    TeoriID = teoriID1,
                    TeoriNavn = "Taekwondo Etiquette",
                    TeoriBeskrivelse = "The formal etiquette and behavior expected in Taekwondo.",
                    TeoriBillede = "taekwondo_etiquette_image_url",
                    TeoriVideo = "taekwondo_etiquette_video_url",
                    TeoriLyd = "taekwondo_etiquette_sound_url",
                    PensumID = pensumID1
                },
                new Teori
                {
                    TeoriID = teoriID2,
                    TeoriNavn = "History of Taekwondo",
                    TeoriBeskrivelse = "A deep dive into the origins and history of Taekwondo.",
                    TeoriBillede = "history_of_taekwondo_image_url",
                    TeoriVideo = "history_of_taekwondo_video_url",
                    TeoriLyd = "history_of_taekwondo_sound_url",
                    PensumID = pensumID1
                },
                new Teori
                {
                    TeoriID = teoriID3,
                    TeoriNavn = "Taekwondo Stances",
                    TeoriBeskrivelse = "Overview of the basic stances used in Taekwondo.",
                    TeoriBillede = "taekwondo_stances_image_url",
                    TeoriVideo = "taekwondo_stances_video_url",
                    TeoriLyd = "taekwondo_stances_sound_url",
                    PensumID = pensumID2
                },
                new Teori
                {
                    TeoriID = teoriID4,
                    TeoriNavn = "The Dojang",
                    TeoriBeskrivelse = "The significance of the Taekwondo training hall, the Dojang.",
                    TeoriBillede = "dojang_image_url",
                    TeoriVideo = "dojang_video_url",
                    TeoriLyd = "dojang_sound_url",
                    PensumID = pensumID2
                },
                new Teori
                {
                    TeoriID = teoriID5,
                    TeoriNavn = "Taekwondo Belt System",
                    TeoriBeskrivelse = "Understanding the belt levels in Taekwondo and their meanings.",
                    TeoriBillede = "belt_system_image_url",
                    TeoriVideo = "belt_system_video_url",
                    TeoriLyd = "belt_system_sound_url",
                    PensumID = pensumID2
                },
                new Teori
                {
                    TeoriID = teoriID6,
                    TeoriNavn = "Basic Taekwondo Movements",
                    TeoriBeskrivelse = "The foundational movements in Taekwondo.",
                    TeoriBillede = "basic_movements_image_url",
                    TeoriVideo = "basic_movements_video_url",
                    TeoriLyd = "basic_movements_sound_url",
                    PensumID = pensumID3
                },
                new Teori
                {
                    TeoriID = teoriID7,
                    TeoriNavn = "Taekwondo Self-defense Techniques",
                    TeoriBeskrivelse = "Introduction to basic self-defense techniques in Taekwondo.",
                    TeoriBillede = "self_defense_image_url",
                    TeoriVideo = "self_defense_video_url",
                    TeoriLyd = "self_defense_sound_url",
                    PensumID = pensumID3
                },
                new Teori
                {
                    TeoriID = teoriID8,
                    TeoriNavn = "Taekwondo Tournaments",
                    TeoriBeskrivelse = "How Taekwondo tournaments are organized and what the rules are.",
                    TeoriBillede = "tournaments_image_url",
                    TeoriVideo = "tournaments_video_url",
                    TeoriLyd = "tournaments_sound_url",
                    PensumID = pensumID4
                },
                new Teori
                {
                    TeoriID = teoriID9,
                    TeoriNavn = "Taekwondo Forms",
                    TeoriBeskrivelse = "The different forms (patterns) performed in Taekwondo.",
                    TeoriBillede = "forms_image_url",
                    TeoriVideo = "forms_video_url",
                    TeoriLyd = "forms_sound_url",
                    PensumID = pensumID5
                },
                new Teori
                {
                    TeoriID = teoriID10,
                    TeoriNavn = "Taekwondo Philosophy",
                    TeoriBeskrivelse = "The philosophy behind Taekwondo and its martial arts principles.",
                    TeoriBillede = "philosophy_image_url",
                    TeoriVideo = "philosophy_video_url",
                    TeoriLyd = "philosophy_sound_url",
                    PensumID = pensumID5
                }
            );

            // Seed for Ordbog
            modelBuilder.Entity<Ordbog>().HasData(
                new Ordbog
                {
                    OrdbogId = ordbogID1,
                    DanskOrd = "Taekwondo",
                    KoranskOrd = "تايكوندو",
                    Beskrivelse = "A Korean martial art focusing on high kicks and hand techniques.",
                    BilledeLink = "taekwondo_image_url",
                    LydLink = "taekwondo_sound_url",
                    VideoLink = "taekwondo_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID2,
                    DanskOrd = "Kik",
                    KoranskOrd = "킥",
                    Beskrivelse = "A kick in Taekwondo, used for both offense and defense.",
                    BilledeLink = "kick_image_url",
                    LydLink = "kick_sound_url",
                    VideoLink = "kick_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID3,
                    DanskOrd = "Stance",
                    KoranskOrd = "자세",
                    Beskrivelse = "A fundamental position in Taekwondo used for balance and power.",
                    BilledeLink = "stance_image_url",
                    LydLink = "stance_sound_url",
                    VideoLink = "stance_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID4,
                    DanskOrd = "Form",
                    KoranskOrd = "품세",
                    Beskrivelse = "A series of movements and techniques in a specific sequence.",
                    BilledeLink = "form_image_url",
                    LydLink = "form_sound_url",
                    VideoLink = "form_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID5,
                    DanskOrd = "Master",
                    KoranskOrd = "사범",
                    Beskrivelse = "A highly skilled Taekwondo practitioner and instructor.",
                    BilledeLink = "master_image_url",
                    LydLink = "master_sound_url",
                    VideoLink = "master_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID6,
                    DanskOrd = "Sparring",
                    KoranskOrd = "겨루기",
                    Beskrivelse = "A practice of fighting against an opponent in Taekwondo.",
                    BilledeLink = "sparring_image_url",
                    LydLink = "sparring_sound_url",
                    VideoLink = "sparring_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID7,
                    DanskOrd = "Kickboard",
                    KoranskOrd = "발차기보드",
                    Beskrivelse = "A board used in training for practicing kicks.",
                    BilledeLink = "kickboard_image_url",
                    LydLink = "kickboard_sound_url",
                    VideoLink = "kickboard_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID8,
                    DanskOrd = "Breaking",
                    KoranskOrd = "격파",
                    Beskrivelse = "The act of breaking boards or other objects to test strength and technique.",
                    BilledeLink = "breaking_image_url",
                    LydLink = "breaking_sound_url",
                    VideoLink = "breaking_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID9,
                    DanskOrd = "Belt",
                    KoranskOrd = "띠",
                    Beskrivelse = "A symbol of rank in Taekwondo, representing the practitioner's level.",
                    BilledeLink = "belt_image_url",
                    LydLink = "belt_sound_url",
                    VideoLink = "belt_video_url"
                },
                new Ordbog
                {
                    OrdbogId = ordbogID10,
                    DanskOrd = "Charyeot",
                    KoranskOrd = "차렷",
                    Beskrivelse = "A command to stand at attention, often used during training or ceremonies.",
                    BilledeLink = "charyeot_image_url",
                    LydLink = "charyeot_sound_url",
                    VideoLink = "charyeot_video_url"
                }
            );

            modelBuilder.Entity<Øvelse>().HasData(
                new Øvelse
                {
                    ØvelseID = øvelseID1,
                    ØvelseNavn = "Push-ups",
                    ØvelseBeskrivelse = "A basic bodyweight exercise for strengthening the upper body and arms.",
                    ØvelseBillede = "push_up_image_url",
                    ØvelseVideo = "push_up_video_url",
                    ØvelseTid = 30,  // Time for each round of exercise in seconds
                    ØvelseSværhed = "Let",  // Difficulty: Easy
                },
                new Øvelse
                {
                    ØvelseID = øvelseID2,
                    ØvelseNavn = "Sit-ups",
                    ØvelseBeskrivelse = "A core exercise to strengthen the abdominal muscles.",
                    ØvelseBillede = "sit_up_image_url",
                    ØvelseVideo = "sit_up_video_url",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Let",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID3,
                    ØvelseNavn = "Squats",
                    ØvelseBeskrivelse = "A lower body exercise to strengthen the thighs, hips, and buttocks.",
                    ØvelseBillede = "squat_image_url",
                    ØvelseVideo = "squat_video_url",
                    ØvelseTid = 45,
                    ØvelseSværhed = "Mellem",  // Difficulty: Medium
                },
                new Øvelse
                {
                    ØvelseID = øvelseID4,
                    ØvelseNavn = "Burpees",
                    ØvelseBeskrivelse = "A full-body exercise that combines squats, push-ups, and jumps.",
                    ØvelseBillede = "burpee_image_url",
                    ØvelseVideo = "burpee_video_url",
                    ØvelseTid = 60,
                    ØvelseSværhed = "Svær",  // Difficulty: Hard
                },
                new Øvelse
                {
                    ØvelseID = øvelseID5,
                    ØvelseNavn = "Lunges",
                    ØvelseBeskrivelse = "A lower body exercise targeting the quads, hamstrings, and glutes.",
                    ØvelseBillede = "lunge_image_url",
                    ØvelseVideo = "lunge_video_url",
                    ØvelseTid = 40,
                    ØvelseSværhed = "Mellem",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID6,
                    ØvelseNavn = "Mountain Climbers",
                    ØvelseBeskrivelse = "A cardiovascular exercise that mimics climbing a mountain while on the ground.",
                    ØvelseBillede = "mountain_climber_image_url",
                    ØvelseVideo = "mountain_climber_video_url",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Mellem",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID7,
                    ØvelseNavn = "Plank",
                    ØvelseBeskrivelse = "A core stability exercise to strengthen the abdominals, back, and shoulders.",
                    ØvelseBillede = "plank_image_url",
                    ØvelseVideo = "plank_video_url",
                    ØvelseTid = 45,
                    ØvelseSværhed = "Mellem",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID8,
                    ØvelseNavn = "Jumping Jacks",
                    ØvelseBeskrivelse = "A full-body cardio exercise to improve endurance and agility.",
                    ØvelseBillede = "jumping_jacks_image_url",
                    ØvelseVideo = "jumping_jacks_video_url",
                    ØvelseTid = 30,
                    ØvelseSværhed = "Let",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID9,
                    ØvelseNavn = "High Knees",
                    ØvelseBeskrivelse = "A cardio exercise focusing on fast leg movement to increase heart rate.",
                    ØvelseBillede = "high_knees_image_url",
                    ØvelseVideo = "high_knees_video_url",
                    ØvelseTid = 40,
                    ØvelseSværhed = "Mellem",
                },
                new Øvelse
                {
                    ØvelseID = øvelseID10,
                    ØvelseNavn = "Tricep Dips",
                    ØvelseBeskrivelse = "An upper body exercise that targets the triceps, using parallel bars or a bench.",
                    ØvelseBillede = "tricep_dips_image_url",
                    ØvelseVideo = "tricep_dips_video_url",
                    ØvelseTid = 45,
                    ØvelseSværhed = "Mellem",
                }
            );

            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    QuizID = quizID1,
                    QuizNavn = "Taekwondo Basics Quiz",
                    QuizBeskrivelse = "A quiz to test your knowledge of basic Taekwondo concepts.",
                    PensumID = pensumID1  // Example PensumID
                },
                new Quiz
                {
                    QuizID = quizID2,
                    QuizNavn = "Taekwondo History Quiz",
                    QuizBeskrivelse = "Test your knowledge of the history and origins of Taekwondo.",
                    PensumID = pensumID2  // Example PensumID
                },
                new Quiz
                {
                    QuizID = quizID3,
                    QuizNavn = "Taekwondo Techniques Quiz",
                    QuizBeskrivelse = "A quiz to assess your understanding of various Taekwondo techniques and movements.",
                    PensumID = pensumID3  // Example PensumID
                }
            );

            // Seed for Spørgsmål (Questions) for each Quiz
            modelBuilder.Entity<Spørgsmål>().HasData(
                // Questions for Quiz 1 (Taekwondo Basics Quiz)
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID1,
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = quizID1,  // Reference the first quiz
                    TeoriID = teoriID1,  // Reference a theory
                    TeknikID = teknikID1,  // Reference a technique
                    ØvelseID = øvelseID1  // Reference an exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID2,
                    SpørgsmålRækkefølge = 2,
                    SpørgsmålTid = 30,
                    QuizID = quizID1,  // Reference the first quiz
                    TeoriID = teoriID2,  // Reference a different theory
                    TeknikID = teknikID2,  // Reference a different technique
                    ØvelseID = øvelseID2  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID3,
                    SpørgsmålRækkefølge = 3,
                    SpørgsmålTid = 40,
                    QuizID = quizID1,  // Reference the first quiz
                    TeoriID = teoriID3,  // Reference a different theory
                    TeknikID = teknikID3,  // Reference a different technique
                    ØvelseID = øvelseID3  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID4,
                    SpørgsmålRækkefølge = 4,
                    SpørgsmålTid = 45,
                    QuizID = quizID1,  // Reference the first quiz
                    TeoriID = teoriID4,  // Reference a different theory
                    TeknikID = teknikID4,  // Reference a different technique
                    ØvelseID = øvelseID4  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID5,
                    SpørgsmålRækkefølge = 5,
                    SpørgsmålTid = 60,
                    QuizID = quizID1,  // Reference the first quiz
                    TeoriID = teoriID5,  // Reference a different theory
                    TeknikID = teknikID5,  // Reference a different technique
                    ØvelseID = øvelseID5  // Reference a different exercise
                },

                // Questions for Quiz 2 (Taekwondo History Quiz)
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID6,
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = quizID2,  // Reference the second quiz
                    TeoriID = teoriID1,  // Reference a theory
                    TeknikID = teknikID2,  // Reference a technique
                    ØvelseID = øvelseID1  // Reference an exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID7,
                    SpørgsmålRækkefølge = 2,
                    SpørgsmålTid = 30,
                    QuizID = quizID2,  // Reference the second quiz
                    TeoriID = teoriID2,  // Reference a different theory
                    TeknikID = teknikID3,  // Reference a different technique
                    ØvelseID = øvelseID2  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID8,
                    SpørgsmålRækkefølge = 3,
                    SpørgsmålTid = 40,
                    QuizID = quizID2,  // Reference the second quiz
                    TeoriID = teoriID3,  // Reference a different theory
                    TeknikID = teknikID4,  // Reference a different technique
                    ØvelseID = øvelseID3  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID9,
                    SpørgsmålRækkefølge = 4,
                    SpørgsmålTid = 45,
                    QuizID = quizID2,  // Reference the second quiz
                    TeoriID = teoriID4,  // Reference a different theory
                    TeknikID = teknikID5,  // Reference a different technique
                    ØvelseID = øvelseID4  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID10,
                    SpørgsmålRækkefølge = 5,
                    SpørgsmålTid = 60,
                    QuizID = quizID2,  // Reference the second quiz
                    TeoriID = teoriID5,  // Reference a different theory
                    TeknikID = teknikID1,  // Reference a different technique
                    ØvelseID = øvelseID5  // Reference a different exercise
                },

                // Questions for Quiz 3 (Taekwondo Advanced Quiz)
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID11,
                    SpørgsmålRækkefølge = 1,
                    SpørgsmålTid = 30,
                    QuizID = quizID3,  // Reference the third quiz
                    TeoriID = teoriID1,  // Reference a theory
                    TeknikID = teknikID2,  // Reference a technique
                    ØvelseID = øvelseID1  // Reference an exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID12,
                    SpørgsmålRækkefølge = 2,
                    SpørgsmålTid = 30,
                    QuizID = quizID3,  // Reference the third quiz
                    TeoriID = teoriID2,  // Reference a different theory
                    TeknikID = teknikID3,  // Reference a different technique
                    ØvelseID = øvelseID2  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID13,
                    SpørgsmålRækkefølge = 3,
                    SpørgsmålTid = 40,
                    QuizID = quizID3,  // Reference the third quiz
                    TeoriID = teoriID3,  // Reference a different theory
                    TeknikID = teknikID4,  // Reference a different technique
                    ØvelseID = øvelseID3  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID14,
                    SpørgsmålRækkefølge = 4,
                    SpørgsmålTid = 45,
                    QuizID = quizID3,  // Reference the third quiz
                    TeoriID = teoriID4,  // Reference a different theory
                    TeknikID = teknikID5,  // Reference a different technique
                    ØvelseID = øvelseID4  // Reference a different exercise
                },
                new Spørgsmål
                {
                    SpørgsmålID = spørgsmålID15,
                    SpørgsmålRækkefølge = 5,
                    SpørgsmålTid = 60,
                    QuizID = quizID3,  // Reference the third quiz
                    TeoriID = teoriID5,  // Reference a different theory
                    TeknikID = teknikID1,  // Reference a different technique
                    ØvelseID = øvelseID5  // Reference a different exercise
                }
            );

            // Seed for Træning (Training) for each Program
            modelBuilder.Entity<Træning>().HasData(
                // Træning for Program 1
                new Træning
                {
                    TræningID = træningID1,
                    TræningRækkefølge = 1,
                    Tid = 60,
                    ProgramID = programID1,  // Reference the first program
                    PensumID = pensumID1  // Reference the first Pensum
                },
                new Træning
                {
                    TræningID = træningID2,
                    TræningRækkefølge = 2,
                    Tid = 75,
                    ProgramID = programID1,  // Reference the first program
                    PensumID = pensumID1  // Reference the first Pensum
                },

                // Træning for Program 2
                new Træning
                {
                    TræningID = træningID3,
                    TræningRækkefølge = 1,
                    Tid = 90,
                    ProgramID = programID2,  // Reference the second program
                    PensumID = pensumID2  // Reference the second Pensum
                },
                new Træning
                {
                    TræningID = træningID4,
                    TræningRækkefølge = 2,
                    Tid = 80,
                    ProgramID = programID2,  // Reference the second program
                    PensumID = pensumID2  // Reference the second Pensum
                },
                // Træning for Program 3
                new Træning
                {
                    TræningID = træningID5,
                    TræningRækkefølge = 1,
                    Tid = 90,
                    ProgramID = programID3,  // Reference the second program
                    PensumID = pensumID3  // Reference the second Pensum
                },
                new Træning
                {
                    TræningID = træningID5,
                    TræningRækkefølge = 2,
                    Tid = 80,
                    ProgramID = programID3,  // Reference the second program
                    PensumID = pensumID3  // Reference the second Pensum
                }
            );

            // Seed for BrugerKlub (Link Bruger and Klub)
            modelBuilder.Entity<BrugerKlub>().HasData(
                new BrugerKlub
                {
                    BrugerID = brugerID1,  // Link to Bruger 1
                    KlubID = klubID1       // Link to Klub 1
                },
                new BrugerKlub
                {
                    BrugerID = brugerID2,  // Link to Bruger 2
                    KlubID = klubID2       // Link to Klub 2
                }
            );

            // Seed for BrugerProgram (Link Bruger and ProgramPlan)
            modelBuilder.Entity<BrugerProgram>().HasData(
                new BrugerProgram
                {
                    BrugerID = brugerID1,      // Link to Bruger 1
                    ProgramID = programID3 // Link to Program 1
                }
            );

            // Seed for BrugerQuiz (Link Bruger and Quiz)
            modelBuilder.Entity<BrugerQuiz>().HasData(
                new BrugerQuiz
                {
                    BrugerID = brugerID1,  // Link to Bruger 1
                    QuizID = quizID3       // Link to Quiz 1
                }
            );

            // Seed for BrugerØvelse (Link Bruger and Øvelse)
            modelBuilder.Entity<BrugerØvelse>().HasData(
                new BrugerØvelse
                {
                    BrugerID = brugerID1,      // Link to Bruger 1
                    ØvelseID = øvelseID3   // Link to Øvelse 3
                },
                new BrugerØvelse
                {
                    BrugerID = brugerID2,      // Link to Bruger 2
                    ØvelseID = øvelseID4   // Link to Øvelse 4
                }
            );

            // Seed for KlubProgram (Link Klub and ProgramPlan)
            modelBuilder.Entity<KlubProgram>().HasData(
                new KlubProgram
                {
                    KlubID = klubID1,      // Link to Klub 1
                    ProgramID = programID1 // Link to Program 1
                },
                new KlubProgram
                {
                    KlubID = klubID2,      // Link to Klub 2
                    ProgramID = programID2 // Link to Program 2
                }
            );

            // Seed for KlubQuiz (Link Klub and Quiz)
            modelBuilder.Entity<KlubQuiz>().HasData(
                new KlubQuiz
                {
                    KlubID = klubID1,      // Link to Klub 1
                    QuizID = quizID1       // Link to Quiz 1
                },
                new KlubQuiz
                {
                    KlubID = klubID2,      // Link to Klub 2
                    QuizID = quizID2       // Link to Quiz 2
                }
            );

            // Seed for KlubØvelse (Link Klub and Øvelse)
            modelBuilder.Entity<KlubØvelse>().HasData(
                new KlubØvelse
                {
                    KlubID = klubID1,      // Link to Klub 1
                    ØvelseID = øvelseID1   // Link to Øvelse 1
                },
                new KlubØvelse
                {
                    KlubID = klubID2,      // Link to Klub 2
                    ØvelseID = øvelseID2   // Link to Øvelse 2
                }
            );
        }
    }
}

