using AutoMapper;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using static TaekwondoApp.Shared.Pages.Register;

namespace TaekwondoApp.Shared.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Bruger and BrugerDTO Mapping
            CreateMap<Bruger, BrugerDTO>()
                //.ForMember(dest => dest.Klub, opt => opt.MapFrom(src => src.BrugerKlubber.FirstOrDefault() != null ? src.BrugerKlubber.FirstOrDefault().Klub : null));
                //.ForMember(dest => dest.Token, opt => opt.Ignore()); 
                .ForMember(dest => dest.Brugerkode, opt => opt.Ignore());
            CreateMap<BrugerDTO, Bruger>(); // Reverse Mapping
            CreateMap<BrugerUpdateDTO, Bruger>();
            CreateMap<Bruger, BrugerUpdateDTO>();


            // Klub and KlubDTO Mapping
            CreateMap<Klub, KlubDTO>();
            CreateMap<KlubDTO, Klub>(); // Reverse Mapping

            // Ordbog and OrdbogDTO Mapping
            CreateMap<Ordbog, OrdbogDTO>();
            CreateMap<OrdbogDTO, Ordbog>(); // Reverse Mapping

            // Øvelse and ØvelseDTO Mapping
            CreateMap<Øvelse, ØvelseDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));
            CreateMap<ØvelseDTO, Øvelse>(); // Reverse Mapping

            // Pensum and PensumDTO Mapping
            CreateMap<Pensum, PensumDTO>()
                .ForMember(dest => dest.Teknik, opt => opt.MapFrom(src => src.Teknikker))
                .ForMember(dest => dest.Teori, opt => opt.MapFrom(src => src.Teorier));
            CreateMap<PensumDTO, Pensum>(); // Reverse Mapping

            // Teknik and TeknikDTO Mapping
            CreateMap<Teknik, TeknikDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));
            CreateMap<TeknikDTO, Teknik>(); // Reverse Mapping

            // Teori and TeoriDTO Mapping
            CreateMap<Teori, TeoriDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));
            CreateMap<TeoriDTO, Teori>(); // Reverse Mapping

            // ProgramPlan and ProgramPlanDTO Mapping
            CreateMap<ProgramPlan, ProgramPlanDTO>()
                .ForMember(dest => dest.Træninger, opt => opt.MapFrom(src => src.Træninger));
            CreateMap<ProgramPlanDTO, ProgramPlan>(); // Reverse Mapping

            // Quiz and QuizDTO Mapping
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.Spørgsmål, opt => opt.MapFrom(src => src.Spørgsmåls));
            CreateMap<QuizDTO, Quiz>(); // Reverse Mapping

            // Spørgsmål and SpørgsmålDTO Mapping
            CreateMap<Spørgsmål, SpørgsmålDTO>()
                .ForMember(dest => dest.Teknik, opt => opt.MapFrom(src => src.Teknik))
                .ForMember(dest => dest.Teori, opt => opt.MapFrom(src => src.Teori))
                .ForMember(dest => dest.Øvelse, opt => opt.MapFrom(src => src.Øvelse));
            CreateMap<SpørgsmålDTO, Spørgsmål>(); // Reverse Mapping

            // Træning and TræningDTO Mapping
            CreateMap<Træning, TræningDTO>()
                .ForMember(dest => dest.Quiz, opt => opt.MapFrom(src => src.Quiz))
                .ForMember(dest => dest.Teori, opt => opt.MapFrom(src => src.Teori))
                .ForMember(dest => dest.Teknik, opt => opt.MapFrom(src => src.Teknik))
                .ForMember(dest => dest.Øvelse, opt => opt.MapFrom(src => src.Øvelse));
            CreateMap<TræningDTO, Træning>(); // Reverse Mapping

            // BrugerKlub, BrugerProgram, BrugerØvelse, BrugerQuiz
            CreateMap<BrugerKlub, BrugerKlubDTO>();
            CreateMap<BrugerKlubDTO, BrugerKlub>(); // Reverse Mapping
            CreateMap<BrugerProgram, BrugerProgramDTO>();
            CreateMap<BrugerProgramDTO, BrugerProgram>(); // Reverse Mapping
            CreateMap<BrugerØvelse, BrugerØvelseDTO>();
            CreateMap<BrugerØvelseDTO, BrugerØvelse>(); // Reverse Mapping
            CreateMap<BrugerQuiz, BrugerQuizDTO>();
            CreateMap<BrugerQuizDTO, BrugerQuiz>(); // Reverse Mapping

            // KlubProgram, KlubQuiz, KlubØvelse
            CreateMap<KlubProgram, KlubProgramDTO>();
            CreateMap<KlubProgramDTO, KlubProgram>(); // Reverse Mapping
            CreateMap<KlubQuiz, KlubQuizDTO>();
            CreateMap<KlubQuizDTO, KlubQuiz>(); // Reverse Mapping
            CreateMap<KlubØvelse, KlubØvelseDTO>();
            CreateMap<KlubØvelseDTO, KlubØvelse>(); // Reverse Mapping


            CreateMap<RegisterModel, BrugerDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Brugernavn, opt => opt.MapFrom(src => src.Brugernavn))
                .ForMember(dest => dest.Fornavn, opt => opt.MapFrom(src => src.Fornavn))
                .ForMember(dest => dest.Efternavn, opt => opt.MapFrom(src => src.Efternavn))
                .ForMember(dest => dest.Bæltegrad, opt => opt.MapFrom(src => src.Bæltegrad))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));



            // Bruger -> BrugerDTO
            CreateMap<Bruger, BrugerDTO>()
                .ForMember(dest => dest.Klubber, opt => opt.MapFrom(src => src.BrugerKlubber.Select(bk => bk.Klub)))
                .ForMember(dest => dest.Programmer, opt => opt.MapFrom(src => src.BrugerProgrammer.Select(bp => bp.Plan)))
                .ForMember(dest => dest.Quizzer, opt => opt.MapFrom(src => src.BrugerQuizzer.Select(bq => bq.Quiz)))
                .ForMember(dest => dest.Øvelser, opt => opt.MapFrom(src => src.BrugerØvelser.Select(bø => bø.Øvelse)));

            // Klub -> KlubDTO
            CreateMap<Klub, KlubDTO>();

            // ProgramPlan -> ProgramPlanDTO
            CreateMap<ProgramPlan, ProgramPlanDTO>()
                .ForMember(dest => dest.Træninger, opt => opt.MapFrom(src => src.Træninger));

            // Træning -> TræningDTO
            CreateMap<Træning, TræningDTO>();

            // Quizzer -> QuizDTO
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.Spørgsmål, opt => opt.MapFrom(src => src.Spørgsmåls));

            // Spørgsmål -> SpørgsmålDTO
            CreateMap<Spørgsmål, SpørgsmålDTO>()
                .ForMember(dest => dest.Teknik, opt => opt.MapFrom(src => src.Teknik))
                .ForMember(dest => dest.Teori, opt => opt.MapFrom(src => src.Teori))
                .ForMember(dest => dest.Øvelse, opt => opt.MapFrom(src => src.Øvelse));

            // Øvelse -> ØvelseDTO
            CreateMap<Øvelse, ØvelseDTO>();

            // Teknik -> TeknikDTO
            CreateMap<Teknik, TeknikDTO>();

            // Teori -> TeoriDTO
            CreateMap<Teori, TeoriDTO>();

            CreateMap<Klub, KlubDTO>();
            CreateMap<KlubProgram, ProgramPlanDTO>().ForMember(dest => dest.ProgramNavn, opt => opt.MapFrom(src => src.Plan.ProgramNavn));
            CreateMap<KlubQuiz, QuizDTO>().ForMember(dest => dest.QuizNavn, opt => opt.MapFrom(src => src.Quiz.QuizNavn));
            CreateMap<KlubØvelse, ØvelseDTO>().ForMember(dest => dest.ØvelseNavn, opt => opt.MapFrom(src => src.Øvelse.ØvelseNavn));
            CreateMap<BrugerKlub, BrugerDTO>().ForMember(dest => dest.Brugernavn, opt => opt.MapFrom(src => src.Bruger.Brugernavn));

        }
    }
}
