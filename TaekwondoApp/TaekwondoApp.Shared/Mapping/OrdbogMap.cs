using AutoMapper;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Mapping
{
    public class KlubMap : Profile
    {
        public KlubMap()
        {
            CreateMap<Klub, KlubDTO>();
            CreateMap<KlubDTO, Klub>();
        }
    }
}
