using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Data;
public static class DbInitializer
{
    public static void Seed(ApiDbContext context)
    {
        context.Database.EnsureCreated();

        // ---- Avoid reseeding ----
        if (context.Brugere.Any()) return;

        // --- Seed Klub ---
        var klub1 = new Klub { KlubNavn = "København Taekwondo Klub" };
        var klub2 = new Klub { KlubNavn = "Aarhus Kampkunstcenter" };
        context.Klubber.AddRange(klub1, klub2);

        // --- Seed Pensum ---
        var pensum1 = new Pensum { PensumGrad = "Hvidt Bælte" };
        var pensum2 = new Pensum { PensumGrad = "Gult Bælte" };
        context.Pensum.AddRange(pensum1, pensum2);

        // --- Seed Bruger ---
        var bruger1 = new Bruger
        {
            Email = "emma@dojo.dk",
            Brugernavn = "emma123",
            Fornavn = "Emma",
            Efternavn = "Jensen",
            Brugerkode = "123456", // ❗ hash it later
            Address = "Nørrebrogade 42",
            Bæltegrad = "Gult Bælte",
            Role = "Bruger"
        };
        context.Brugere.Add(bruger1);

        // --- BrugerKlub ---
        var brugerKlub = new BrugerKlub
        {
            Bruger = bruger1,
            Klub = klub1
        };
        context.BrugerKlubber.Add(brugerKlub);

        // --- Seed Øvelse ---
        var øvelse = new Øvelse
        {
            ØvelseNavn = "Front Spark",
            ØvelseBeskrivelse = "En simpel frontspark teknik.",
            ØvelseBillede = "https://example.com/front.jpg",
            ØvelseVideo = "https://example.com/front.mp4",
            ØvelseTid = 30,
            ØvelseSværhed = "Begynder",
            Pensum = pensum1
        };
        context.Øvelser.Add(øvelse);

        context.BrugerØvelser.Add(new BrugerØvelse
        {
            Bruger = bruger1,
            Øvelse = øvelse
        });

        // --- KlubØvelse ---
        context.KlubØvelser.Add(new KlubØvelse
        {
            Klub = klub1,
            Øvelse = øvelse
        });

        // --- Seed Teknik ---
        var teknik = new Teknik
        {
            TeknikNavn = "Blokering",
            TeknikBeskrivelse = "Forsvar mod angreb.",
            TeknikBillede = "",
            TeknikVideo = "",
            TeknikLyd = "",
            Pensum = pensum1
        };
        context.Teknikker.Add(teknik);

        // --- Seed Teori ---
        var teori = new Teori
        {
            TeoriNavn = "Respect",
            TeoriBeskrivelse = "Grundlæggende respekt for dojoen og instruktøren.",
            TeoriBillede = "",
            TeoriVideo = "",
            TeoriLyd = "",
            Pensum = pensum1
        };
        context.Teorier.Add(teori);

        // --- Seed Quiz ---
        var quiz = new Quiz
        {
            QuizNavn = "Begynder Quiz",
            QuizBeskrivelse = "Spørgsmål for begyndere",
            Pensum = pensum1
        };
        context.Quizzer.Add(quiz);

        // --- Seed Spørgsmål ---
        var spg = new Spørgsmål
        {
            SpørgsmålRækkefølge = 1,
            SpørgsmålTid = 30,
            Quiz = quiz,
            Teori = teori
        };
        context.Spørgsmål.Add(spg);

        // --- Seed BrugerQuiz ---
        context.BrugerQuizzer.Add(new BrugerQuiz
        {
            Bruger = bruger1,
            Quiz = quiz
        });

        // --- Seed KlubQuiz ---
        context.KlubQuizzer.Add(new KlubQuiz
        {
            Klub = klub1,
            Quiz = quiz
        });

        // --- ProgramPlan ---
        var plan = new ProgramPlan
        {
            ProgramNavn = "Intro Program",
            Beskrivelse = "2 ugers intro",
            Længde = 14,
            OprettelseDato = DateTime.UtcNow
        };
        context.ProgramPlans.Add(plan);

        // --- KlubProgram ---
        context.KlubProgrammer.Add(new KlubProgram
        {
            Klub = klub1,
            Plan = plan
        });

        // --- BrugerProgram ---
        context.BrugerProgrammer.Add(new BrugerProgram
        {
            Bruger = bruger1,
            Plan = plan
        });

        // --- Træning ---
        var træning = new Træning
        {
            TræningRækkefølge = 1,
            Tid = 45,
            Quiz = quiz,
            Teori = teori,
            Teknik = teknik,
            Øvelse = øvelse,
            Pensum = pensum1,
            ProgramPlan = plan
        };
        context.Træninger.Add(træning);

        // --- Ordbog ---
        context.Ordboger.Add(new Ordbog
        {
            DanskOrd = "Hilsen",
            KoranskOrd = "Annyeonghaseyo",
            Beskrivelse = "En typisk hilsen i kampkunst",
            BilledeLink = "",
            LydLink = "",
            VideoLink = ""
        });

        context.SaveChanges();
    }
}
