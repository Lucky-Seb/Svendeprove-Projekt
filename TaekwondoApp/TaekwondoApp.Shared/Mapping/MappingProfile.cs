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
            // Grouping user-related mappings
            CreateUserMappings();

            // Grouping club-related mappings
            CreateClubMappings();

            // Grouping program-related mappings
            CreateProgramMappings();

            // Grouping exercise-related mappings
            CreateExerciseMappings();

            // Grouping quiz-related mappings
            CreateQuizMappings();

            // Grouping dictionary-related mappings
            CreateDictionaryMappings();

            // Grouping other relationship mappings
            CreateRelationshipMappings();
        }

        #region User Mappings
        private void CreateUserMappings()
        {
            // Bruger and BrugerDTO Mapping
            CreateMap<Bruger, BrugerDTO>()
                .ForMember(dest => dest.Brugerkode, opt => opt.Ignore())
                .ForMember(dest => dest.Klubber, opt => opt.MapFrom(src => src.BrugerKlubber.Select(bk => bk.Klub)))
                .ForMember(dest => dest.Programmer, opt => opt.MapFrom(src => src.BrugerProgrammer.Select(bp => bp.Plan)))
                .ForMember(dest => dest.Quizzer, opt => opt.MapFrom(src => src.BrugerQuizzer.Select(bq => bq.Quiz)))
                .ForMember(dest => dest.Øvelser, opt => opt.MapFrom(src => src.BrugerØvelser.Select(bø => bø.Øvelse)));

            CreateMap<BrugerDTO, Bruger>(); // Reverse Mapping

            // Other user-related mappings
            CreateMap<BrugerUpdateDTO, Bruger>();
            CreateMap<Bruger, BrugerUpdateDTO>();

            CreateMap<RegisterModel, BrugerDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Brugernavn, opt => opt.MapFrom(src => src.Brugernavn))
                .ForMember(dest => dest.Fornavn, opt => opt.MapFrom(src => src.Fornavn))
                .ForMember(dest => dest.Efternavn, opt => opt.MapFrom(src => src.Efternavn))
                .ForMember(dest => dest.Bæltegrad, opt => opt.MapFrom(src => src.Bæltegrad))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
        #endregion

        #region Club Mappings
        private void CreateClubMappings()
        {
            CreateMap<Klub, KlubDTO>()
                .ForMember(dest => dest.Programmer, opt => opt.MapFrom(src => src.KlubProgrammer.Select(kp => kp.Plan).ToList()))
                .ForMember(dest => dest.Quizzer, opt => opt.MapFrom(src => src.KlubQuizzer.Select(kq => kq.Quiz).ToList()))
                .ForMember(dest => dest.Øvelser, opt => opt.MapFrom(src => src.KlubØvelser.Select(kø => kø.Øvelse).ToList()))
                .ForMember(dest => dest.Bruger, opt => opt.MapFrom(src => src.BrugerKlubber.Select(bk => bk.Bruger).ToList()));

            CreateMap<KlubDTO, Klub>(); // Reverse Mapping
        }
        #endregion

        #region Program Mappings
        private void CreateProgramMappings()
        {
            CreateMap<ProgramPlan, ProgramPlanDTO>()
                .ForMember(dest => dest.Træninger, opt => opt.MapFrom(src => src.Træninger));

            CreateMap<ProgramPlanDTO, ProgramPlan>(); // Reverse Mapping
        }
        #endregion

        #region Exercise Mappings
        private void CreateExerciseMappings()
        {
            CreateMap<Øvelse, ØvelseDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));

            CreateMap<ØvelseDTO, Øvelse>(); // Reverse Mapping
        }
        #endregion

        #region Quiz Mappings
        private void CreateQuizMappings()
        {
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.Spørgsmål, opt => opt.MapFrom(src => src.Spørgsmåls));

            CreateMap<QuizDTO, Quiz>(); // Reverse Mapping

            CreateMap<Spørgsmål, SpørgsmålDTO>()
                .ForMember(dest => dest.Teknik, opt => opt.MapFrom(src => src.Teknik))
                .ForMember(dest => dest.Teori, opt => opt.MapFrom(src => src.Teori))
                .ForMember(dest => dest.Øvelse, opt => opt.MapFrom(src => src.Øvelse));

            CreateMap<SpørgsmålDTO, Spørgsmål>(); // Reverse Mapping
        }
        #endregion

        #region Dictionary Mappings
        private void CreateDictionaryMappings()
        {
            CreateMap<Ordbog, OrdbogDTO>();
            CreateMap<OrdbogDTO, Ordbog>(); // Reverse Mapping

            CreateMap<Teknik, TeknikDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));

            CreateMap<TeknikDTO, Teknik>(); // Reverse Mapping

            CreateMap<Teori, TeoriDTO>()
                .ForMember(dest => dest.PensumID, opt => opt.MapFrom(src => src.Pensum.PensumID));

            CreateMap<TeoriDTO, Teori>(); // Reverse Mapping
        }
        #endregion

        #region Other Relationship Mappings
        private void CreateRelationshipMappings()
        {
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
        }
        #endregion
    }
}
