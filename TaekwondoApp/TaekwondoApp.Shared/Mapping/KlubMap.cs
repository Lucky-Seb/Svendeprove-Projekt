using AutoMapper;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using static TaekwondoApp.Shared.Pages.Register;

namespace TaekwondoApp.Shared.Mapping
{
    public class KlubMap : Profile
    {
        public KlubMap()
        {
            CreateMap<Klub, KlubDTO>();
            CreateMap<KlubDTO, Klub>();

            CreateMap<Klub, KlubDTO>().ReverseMap();
            CreateMap<ProgramPlan, ProgramPlanDTO>().ReverseMap();
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Øvelse, ØvelseDTO>().ReverseMap();
            CreateMap<Bruger, BrugerDTO>().ReverseMap();
        }
    }
}
