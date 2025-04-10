﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaekwondoOrchestration.ApiService.Data;

#nullable disable

namespace TaekwondoOrchestration.ApiService.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Bruger", b =>
                {
                    b.Property<Guid>("BrugerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brugerkode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brugernavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bæltegrad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Efternavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrugerID");

                    b.ToTable("Brugere");

                    b.HasData(
                        new
                        {
                            BrugerID = new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"),
                            Address = "Nørrebrogade 42",
                            Brugerkode = "123456",
                            Brugernavn = "emma123",
                            Bæltegrad = "Gult Bælte",
                            Efternavn = "Jensen",
                            Email = "emma@dojo.dk",
                            Fornavn = "Emma",
                            Role = "Bruger"
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerKlub", b =>
                {
                    b.Property<Guid>("BrugerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KlubID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BrugerID", "KlubID");

                    b.HasIndex("KlubID");

                    b.ToTable("BrugerKlubber");

                    b.HasData(
                        new
                        {
                            BrugerID = new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"),
                            KlubID = new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerProgram", b =>
                {
                    b.Property<Guid>("BrugerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BrugerID", "ProgramID");

                    b.HasIndex("ProgramID");

                    b.ToTable("BrugerProgrammer");

                    b.HasData(
                        new
                        {
                            BrugerID = new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"),
                            ProgramID = new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerQuiz", b =>
                {
                    b.Property<Guid>("BrugerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuizID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BrugerID", "QuizID");

                    b.HasIndex("QuizID");

                    b.ToTable("BrugerQuizzer");

                    b.HasData(
                        new
                        {
                            BrugerID = new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"),
                            QuizID = new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerØvelse", b =>
                {
                    b.Property<Guid>("BrugerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ØvelseID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BrugerID", "ØvelseID");

                    b.HasIndex("ØvelseID");

                    b.ToTable("BrugerØvelser");

                    b.HasData(
                        new
                        {
                            BrugerID = new Guid("a7b6372f-35a2-47e2-8bfb-e418e2337b8f"),
                            ØvelseID = new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Klub", b =>
                {
                    b.Property<Guid>("KlubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("KlubNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlubID");

                    b.ToTable("Klubber");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubProgram", b =>
                {
                    b.Property<Guid>("KlubID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KlubID", "ProgramID");

                    b.HasIndex("ProgramID");

                    b.ToTable("KlubProgrammer");

                    b.HasData(
                        new
                        {
                            KlubID = new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"),
                            ProgramID = new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubQuiz", b =>
                {
                    b.Property<Guid>("KlubID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuizID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KlubID", "QuizID");

                    b.HasIndex("QuizID");

                    b.ToTable("KlubQuizzer");

                    b.HasData(
                        new
                        {
                            KlubID = new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"),
                            QuizID = new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubØvelse", b =>
                {
                    b.Property<Guid>("KlubID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ØvelseID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KlubID", "ØvelseID");

                    b.HasIndex("ØvelseID");

                    b.ToTable("KlubØvelser");

                    b.HasData(
                        new
                        {
                            KlubID = new Guid("c2b62e9a-38da-43ab-9731-f3641cd3121d"),
                            ØvelseID = new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Ordbog", b =>
                {
                    b.Property<Guid>("OrdbogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Beskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BilledeLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DanskOrd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KoranskOrd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LydLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrdbogId");

                    b.ToTable("Ordboger");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Pensum", b =>
                {
                    b.Property<Guid>("PensumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("PensumGrad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PensumID");

                    b.ToTable("Pensum");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.ProgramPlan", b =>
                {
                    b.Property<Guid>("ProgramID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Beskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Længde")
                        .HasColumnType("int");

                    b.Property<DateTime>("OprettelseDato")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProgramNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProgramID");

                    b.ToTable("ProgramPlans");

                    b.HasData(
                        new
                        {
                            ProgramID = new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe"),
                            Beskrivelse = "2 ugers intro",
                            Længde = 14,
                            OprettelseDato = new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgramNavn = "Intro Program"
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Quiz", b =>
                {
                    b.Property<Guid>("QuizID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("PensumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QuizBeskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuizID");

                    b.HasIndex("PensumID");

                    b.ToTable("Quizzer");

                    b.HasData(
                        new
                        {
                            QuizID = new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"),
                            PensumID = new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"),
                            QuizBeskrivelse = "Spørgsmål for begyndere",
                            QuizNavn = "Begynder Quiz"
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Spørgsmål", b =>
                {
                    b.Property<Guid>("SpørgsmålID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("QuizID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SpørgsmålRækkefølge")
                        .HasColumnType("int");

                    b.Property<int>("SpørgsmålTid")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeknikID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeoriID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ØvelseID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SpørgsmålID");

                    b.HasIndex("QuizID");

                    b.HasIndex("TeknikID");

                    b.HasIndex("TeoriID");

                    b.HasIndex("ØvelseID");

                    b.ToTable("Spørgsmål");

                    b.HasData(
                        new
                        {
                            SpørgsmålID = new Guid("f2563f57-92c7-4388-b920-bf38e47e9d12"),
                            QuizID = new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"),
                            SpørgsmålRækkefølge = 1,
                            SpørgsmålTid = 30,
                            TeoriID = new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Teknik", b =>
                {
                    b.Property<Guid>("TeknikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("PensumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeknikBeskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeknikBillede")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeknikLyd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeknikNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeknikVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeknikID");

                    b.HasIndex("PensumID");

                    b.ToTable("Teknikker");

                    b.HasData(
                        new
                        {
                            TeknikID = new Guid("81c54d38-cb85-420b-b697-13f1f2a8c1cd"),
                            PensumID = new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"),
                            TeknikBeskrivelse = "Forsvar mod angreb.",
                            TeknikBillede = "",
                            TeknikLyd = "",
                            TeknikNavn = "Blokering",
                            TeknikVideo = ""
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Teori", b =>
                {
                    b.Property<Guid>("TeoriID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("PensumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeoriBeskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeoriBillede")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeoriLyd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeoriNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeoriVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeoriID");

                    b.HasIndex("PensumID");

                    b.ToTable("Teorier");

                    b.HasData(
                        new
                        {
                            TeoriID = new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547"),
                            PensumID = new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"),
                            TeoriBeskrivelse = "Respekt for dojo og lærere.",
                            TeoriBillede = "",
                            TeoriLyd = "",
                            TeoriNavn = "Respect",
                            TeoriVideo = ""
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Træning", b =>
                {
                    b.Property<Guid>("TræningID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid?>("PensumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuizID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeknikID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeoriID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tid")
                        .HasColumnType("int");

                    b.Property<int>("TræningRækkefølge")
                        .HasColumnType("int");

                    b.Property<Guid?>("ØvelseID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TræningID");

                    b.HasIndex("PensumID");

                    b.HasIndex("ProgramID");

                    b.HasIndex("QuizID");

                    b.HasIndex("TeknikID");

                    b.HasIndex("TeoriID");

                    b.HasIndex("ØvelseID");

                    b.ToTable("Træninger");

                    b.HasData(
                        new
                        {
                            TræningID = new Guid("a3e2121b-b256-4564-bff1-2f2c94ed00de"),
                            PensumID = new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"),
                            ProgramID = new Guid("f1f4f9c9-6d77-4a8d-a3f3-b0f5095df9fe"),
                            QuizID = new Guid("29f8a1b3-62f0-4d92-b7ad-4079239a9730"),
                            TeknikID = new Guid("81c54d38-cb85-420b-b697-13f1f2a8c1cd"),
                            TeoriID = new Guid("cd428c33-d8d7-46f3-8e8a-3a82e8b5f547"),
                            Tid = 45,
                            TræningRækkefølge = 1,
                            ØvelseID = new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2")
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Øvelse", b =>
                {
                    b.Property<Guid>("ØvelseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("PensumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ØvelseBeskrivelse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ØvelseBillede")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ØvelseNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ØvelseSværhed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ØvelseTid")
                        .HasColumnType("int");

                    b.Property<string>("ØvelseVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ØvelseID");

                    b.HasIndex("PensumID");

                    b.ToTable("Øvelser");

                    b.HasData(
                        new
                        {
                            ØvelseID = new Guid("431e1b6b-3b4f-442b-b97f-11b238f660b2"),
                            PensumID = new Guid("08ff7a3e-0627-493b-92c9-36c26f6ad7fa"),
                            ØvelseBeskrivelse = "En simpel frontspark teknik.",
                            ØvelseBillede = "",
                            ØvelseNavn = "Front Spark",
                            ØvelseSværhed = "Begynder",
                            ØvelseTid = 30,
                            ØvelseVideo = ""
                        });
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerKlub", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Bruger", "Bruger")
                        .WithMany("BrugerKlubber")
                        .HasForeignKey("BrugerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Klub", "Klub")
                        .WithMany("BrugerKlubber")
                        .HasForeignKey("KlubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bruger");

                    b.Navigation("Klub");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerProgram", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Bruger", "Bruger")
                        .WithMany("BrugerProgrammer")
                        .HasForeignKey("BrugerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.ProgramPlan", "Plan")
                        .WithMany("BrugerProgrammer")
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bruger");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerQuiz", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Bruger", "Bruger")
                        .WithMany("BrugerQuizzer")
                        .HasForeignKey("BrugerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Quiz", "Quiz")
                        .WithMany("BrugerQuizzer")
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bruger");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.BrugerØvelse", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Bruger", "Bruger")
                        .WithMany("BrugerØvelser")
                        .HasForeignKey("BrugerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Øvelse", "Øvelse")
                        .WithMany("BrugerØvelses")
                        .HasForeignKey("ØvelseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bruger");

                    b.Navigation("Øvelse");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubProgram", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Klub", "Klub")
                        .WithMany("KlubProgrammer")
                        .HasForeignKey("KlubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.ProgramPlan", "Plan")
                        .WithMany("KlubProgrammer")
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klub");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubQuiz", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Klub", "Klub")
                        .WithMany("KlubQuizzer")
                        .HasForeignKey("KlubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Quiz", "Quiz")
                        .WithMany("KlubQuizzer")
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klub");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.KlubØvelse", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Klub", "Klub")
                        .WithMany("KlubØvelser")
                        .HasForeignKey("KlubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Øvelse", "Øvelse")
                        .WithMany("KlubØvelses")
                        .HasForeignKey("ØvelseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klub");

                    b.Navigation("Øvelse");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Quiz", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Pensum", "Pensum")
                        .WithMany("Quizzer")
                        .HasForeignKey("PensumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pensum");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Spørgsmål", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Quiz", "Quiz")
                        .WithMany("Spørgsmåls")
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Teknik", "Teknik")
                        .WithMany()
                        .HasForeignKey("TeknikID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Teori", "Teori")
                        .WithMany()
                        .HasForeignKey("TeoriID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Øvelse", "Øvelse")
                        .WithMany()
                        .HasForeignKey("ØvelseID");

                    b.Navigation("Quiz");

                    b.Navigation("Teknik");

                    b.Navigation("Teori");

                    b.Navigation("Øvelse");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Teknik", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Pensum", "Pensum")
                        .WithMany("Teknikker")
                        .HasForeignKey("PensumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pensum");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Teori", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Pensum", "Pensum")
                        .WithMany("Teorier")
                        .HasForeignKey("PensumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pensum");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Træning", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Pensum", "Pensum")
                        .WithMany()
                        .HasForeignKey("PensumID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.ProgramPlan", "ProgramPlan")
                        .WithMany("Træninger")
                        .HasForeignKey("ProgramID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Teknik", "Teknik")
                        .WithMany()
                        .HasForeignKey("TeknikID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Teori", "Teori")
                        .WithMany()
                        .HasForeignKey("TeoriID");

                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Øvelse", "Øvelse")
                        .WithMany()
                        .HasForeignKey("ØvelseID");

                    b.Navigation("Pensum");

                    b.Navigation("ProgramPlan");

                    b.Navigation("Quiz");

                    b.Navigation("Teknik");

                    b.Navigation("Teori");

                    b.Navigation("Øvelse");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Øvelse", b =>
                {
                    b.HasOne("TaekwondoOrchestration.ApiService.Models.Pensum", "Pensum")
                        .WithMany("Øvelser")
                        .HasForeignKey("PensumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pensum");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Bruger", b =>
                {
                    b.Navigation("BrugerKlubber");

                    b.Navigation("BrugerProgrammer");

                    b.Navigation("BrugerQuizzer");

                    b.Navigation("BrugerØvelser");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Klub", b =>
                {
                    b.Navigation("BrugerKlubber");

                    b.Navigation("KlubProgrammer");

                    b.Navigation("KlubQuizzer");

                    b.Navigation("KlubØvelser");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Pensum", b =>
                {
                    b.Navigation("Quizzer");

                    b.Navigation("Teknikker");

                    b.Navigation("Teorier");

                    b.Navigation("Øvelser");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.ProgramPlan", b =>
                {
                    b.Navigation("BrugerProgrammer");

                    b.Navigation("KlubProgrammer");

                    b.Navigation("Træninger");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Quiz", b =>
                {
                    b.Navigation("BrugerQuizzer");

                    b.Navigation("KlubQuizzer");

                    b.Navigation("Spørgsmåls");
                });

            modelBuilder.Entity("TaekwondoOrchestration.ApiService.Models.Øvelse", b =>
                {
                    b.Navigation("BrugerØvelses");

                    b.Navigation("KlubØvelses");
                });
#pragma warning restore 612, 618
        }
    }
}
