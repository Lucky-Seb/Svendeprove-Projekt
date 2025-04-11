using Microsoft.EntityFrameworkCore;
using TaekwondoApp.Shared.Models;

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
            var brugerID1 = Guid.Parse("8bb14e0c-04a3-4679-aa0b-48b9978eb220");
            var brugerID2 = Guid.Parse("446a5a83-a0bd-4633-b28f-a6526245eed7");
            var brugerID3 = Guid.Parse("98e78576-6bc4-408d-b4af-2ad9051f905b");
            var brugerID4 = Guid.Parse("e4ef6612-011c-453a-894f-858dff3937d4");
            var brugerID5 = Guid.Parse("4153884f-a1ce-44d0-970b-8898a11fdb81");

            var klubID1 = Guid.Parse("afa9ebbf-49bb-4737-9ab0-7d9d3153c993");
            var klubID2 = Guid.Parse("fed25ea9-7695-4945-a109-2900a24ff1ce");
            var klubID3 = Guid.Parse("6b25c814-a97b-41dc-9597-0864f08cb779");
            var klubID4 = Guid.Parse("2bed2d1b-b96e-427a-8451-3c43ea48ea5c");
            var klubID5 = Guid.Parse("9c3e5c16-5bb2-4076-a871-b2414bd782c2");

            var programID1 = Guid.Parse("11741c42-315e-42fe-a0a8-337afd6d511f");
            var programID2 = Guid.Parse("8c98eb00-1efb-4361-99b7-974c1aed66e8");
            var programID3 = Guid.Parse("3c3b50f0-e2d9-4b97-aa42-f15b5ecc7e47");

            // Define fixed GUIDs for seeding Øvelse
            var øvelseID1 = Guid.Parse("0335fde9-e05e-4a72-b08c-0c076803b395");
            var øvelseID2 = Guid.Parse("1ab7a999-d644-418a-b3ba-c3cd27a5dfd6");
            var øvelseID3 = Guid.Parse("1e86d0c9-1d34-4b46-a939-1326f7f9df42");
            var øvelseID4 = Guid.Parse("dcbcc571-4377-4fcc-91a8-fb83f07165f6");
            var øvelseID5 = Guid.Parse("d5a762e1-b401-4118-955d-bbb3d26f370e");
            var øvelseID6 = Guid.Parse("a36f91d7-c05b-48c8-97a3-11f86a2eae69");
            var øvelseID7 = Guid.Parse("29433916-f5aa-4d34-9bf3-6e0eb09aa010");
            var øvelseID8 = Guid.Parse("d9f50fe9-11d5-46ee-8836-a57727dc424b");
            var øvelseID9 = Guid.Parse("f1c414cb-8ce7-4583-b65e-510bc0f2fd8b");
            var øvelseID10 = Guid.Parse("bdb87fa8-e777-42e6-ad08-7187707fe1c3");

            // Fixed GUID values for Teknik
            var teknikID1 = Guid.Parse("4d608ca7-227c-4dc5-b7b9-f8f2315233ec");
            var teknikID2 = Guid.Parse("f4a14256-bf7c-4910-a4c4-13fc063a455a");
            var teknikID3 = Guid.Parse("72385206-876c-4f9d-bfc6-dc8aa02ef587");
            var teknikID4 = Guid.Parse("49425f68-4bcf-412e-8ac3-0f87b3b117ca");
            var teknikID5 = Guid.Parse("fe2b1123-5434-4c83-ab16-e8372bd99fef");
            var teknikID6 = Guid.Parse("49e7612b-36ff-4f40-9224-5d126945e3e2");
            var teknikID7 = Guid.Parse("ca16ac6c-3697-49b2-891b-dd9e632790c1");
            var teknikID8 = Guid.Parse("42e75d1b-d44e-416c-8a6a-0dd9b2803d9c");
            var teknikID9 = Guid.Parse("ff1f1f02-2a9f-4281-9569-7bf33ec6f457");
            var teknikID10 = Guid.Parse("13eeede6-81ae-4c79-a25e-9226d7a20316");
            // Fixed GUID values for Teori
            var teoriID1 = Guid.Parse("bf4135c9-db91-4354-956f-f1606bddccd8");
            var teoriID2 = Guid.Parse("aae05942-0586-4000-aca2-b02525c0f1ea");
            var teoriID3 = Guid.Parse("a845c529-8362-4fac-b7d0-df079db04860");
            var teoriID4 = Guid.Parse("ecac9146-b9b2-492c-9561-032c39b1436b");
            var teoriID5 = Guid.Parse("26abad43-d433-416e-ba59-6ddb20a64093");
            var teoriID6 = Guid.Parse("9e4df6a6-af38-445f-a4d5-2f2f5e01b029");
            var teoriID7 = Guid.Parse("d93a6ee0-f753-4f04-ac35-7fbb1cd09803");
            var teoriID8 = Guid.Parse("340c59b4-9f76-46af-8abb-49a200eb4671");
            var teoriID9 = Guid.Parse("e10b50fa-9224-4469-9f4c-7553db8328b6");
            var teoriID10 = Guid.Parse("3ed65b45-89f7-4209-bc51-9de0b3c6b6d3");

            // Fixed GUID values for Quiz and Pensum
            var quizID1 = Guid.Parse("69cde397-c39c-4172-8d95-56c9a5cdc099");
            var quizID2 = Guid.Parse("3b89bd9d-dd30-4ea4-8563-984dbfccb644");
            var quizID3 = Guid.Parse("f4c2ee66-c57f-4d0c-b4de-1a7741eb28b2");


            var pensumID1 = Guid.Parse("094cb97e-10b1-4b4b-bfa0-fb6cf18cb973");
            var pensumID2 = Guid.Parse("dcd90332-5e1b-4352-bf88-fb40e75932bd");
            var pensumID3 = Guid.Parse("67db5817-3c5a-4604-ba74-8076578528c3");
            var pensumID4 = Guid.Parse("9c3cabef-0731-4243-a5e8-d837c77ee523");
            var pensumID5 = Guid.Parse("362385ac-7c43-41b1-989c-b8d9ba6fce67");

            // Static GUIDs for Spørgsmål (Question IDs) - fixed GUIDs
            var spørgsmålID1 = Guid.Parse("4ba89cd8-b931-4071-a057-1c9740dac086");  // Generated GUID 51
            var spørgsmålID2 = Guid.Parse("8fe43b9a-f64d-4701-911a-764204b423ad");  // Generated GUID 52
            var spørgsmålID3 = Guid.Parse("94dac4ff-3ef1-41dd-8593-de3736627b98");  // Generated GUID 53
            var spørgsmålID4 = Guid.Parse("464d80b9-28b4-4e16-949a-bddbafc4c6f1");  // Generated GUID 54
            var spørgsmålID5 = Guid.Parse("85c9d413-5148-488c-a667-f971171d2d78");  // Generated GUID 55

            var spørgsmålID6 = Guid.Parse("d1797c4a-4378-49a9-84d5-375efcff0d88");  // Generated GUID 56
            var spørgsmålID7 = Guid.Parse("0a4b9bd3-1fc3-498c-9574-51d1db67cce4");  // Generated GUID 57
            var spørgsmålID8 = Guid.Parse("3e5a25d1-a1a2-411d-b5ab-db0aead404c3");  // Generated GUID 58
            var spørgsmålID9 = Guid.Parse("a79c2d74-bccf-4a9f-ba03-ac02c906f6c7");  // Generated GUID 59
            var spørgsmålID10 = Guid.Parse("49da5e18-a087-40aa-8ea8-0a67e13d249b");  // Generated GUID 60

            var spørgsmålID11 = Guid.Parse("2f02f12d-3c9a-4b63-b7f0-8df70d9ed799");  // Generated GUID 61
            var spørgsmålID12 = Guid.Parse("67f31858-40f0-481d-844a-7dcc7c4e0b48");  // Generated GUID 62
            var spørgsmålID13 = Guid.Parse("6e6089f3-1f29-412e-b7ba-9cfe652fecce");  // Generated GUID 63
            var spørgsmålID14 = Guid.Parse("600a1b8b-6eed-434d-9a1f-1337124da834");  // Generated GUID 64
            var spørgsmålID15 = Guid.Parse("d1a60bed-fd96-47dd-b86a-a944230d53bb");  // Generated GUID 65

            // Static GUIDs for Træning (Training IDs) and other related IDs
            var træningID1 = Guid.Parse("4a91f6e6-3b23-4de7-85a1-1f173a90a27f");  // Generated GUID 66
            var træningID2 = Guid.Parse("84a19dcc-6f70-49ce-9629-d04b0cb0c7dd");  // Generated GUID 67
            var træningID3 = Guid.Parse("2faf5b1b-40a2-4599-99c9-c0e948a31dc7");  // Generated GUID 68
            var træningID4 = Guid.Parse("cf9ce0e3-f9d0-41a0-8486-7b9d41aa43be");  // Generated GUID 69
            var træningID5 = Guid.Parse("58d29731-5915-496c-9f52-b1c64199fdd7");  // Generated GUID 70
            var træningID6 = Guid.Parse("3949e642-5f71-40b7-8c4f-7bdaee0686c9");  // Generated GUID 71


            // Define fixed GUIDs for seeding ordbog
            var ordbogID1 = Guid.Parse("2d189ccb-a481-4ea2-8bf3-8014d3fe5825");  // Generated GUID 72
            var ordbogID2 = Guid.Parse("3a2ba1b6-34c7-4be1-af4f-13bd66db3079");  // Generated GUID 73
            var ordbogID3 = Guid.Parse("3e459839-3c17-43a2-b141-6140eeae07d9");  // Generated GUID 74
            var ordbogID4 = Guid.Parse("4bae52b7-970b-41ce-938b-690a44c29795");  // Generated GUID 75
            var ordbogID5 = Guid.Parse("73d3dea1-d21b-4f45-bb0a-2eaca4c7aa04");  // Generated GUID 76
            var ordbogID6 = Guid.Parse("7c83305d-527c-4eb7-bb39-28280ad42a2d");  // Generated GUID 77
            var ordbogID7 = Guid.Parse("965a856f-eb8f-4910-a6a3-661ff0c4a78a");  // Generated GUID 78
            var ordbogID8 = Guid.Parse("10efa19d-6353-4373-b455-414131376826");  // Generated GUID 79
            var ordbogID9 = Guid.Parse("0fd1ec97-6cee-4e0f-a032-0a3f3020d5be");  // Generated GUID 80
            var ordbogID10 = Guid.Parse("a61b1f2a-3236-4af5-90aa-3483b96a5666");  // Generated GUID 81
            
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
                    OprettelseDato = new DateTime(2025, 4, 8),  // Static date
                    Længde = 4,
                    Beskrivelse = "A basic program to get started with Taekwondo."
                },
                new ProgramPlan
                {
                    ProgramID = programID2,
                    ProgramNavn = "Intermediate Taekwondo Program",
                    OprettelseDato = new DateTime(2025, 4, 9),  // Static date
                    Længde = 6,
                    Beskrivelse = "An intermediate program to enhance your Taekwondo skills."
                },
                new ProgramPlan
                {
                    ProgramID = programID3,
                    ProgramNavn = "Advanced Taekwondo Program",
                    OprettelseDato = new DateTime(2025, 4, 10),  // Static date
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
                    PensumID = pensumID1  // Example PensumID
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
                    PensumID = pensumID1  // Example PensumID
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
                    PensumID = pensumID2  // Example PensumID
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
                    PensumID = pensumID1  // Example PensumID
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
                    PensumID = pensumID3  // Example PensumID
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
                    PensumID = pensumID2  // Example PensumID
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
                    PensumID = pensumID1  // Example PensumID
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
                    PensumID = pensumID1  // Example PensumID
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
                    PensumID = pensumID2  // Example PensumID
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
                    PensumID = pensumID3  // Example PensumID
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
                    TræningID = træningID6,
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

