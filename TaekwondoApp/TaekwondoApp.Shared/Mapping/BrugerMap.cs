using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using static TaekwondoApp.Shared.Pages.Register;

namespace TaekwondoApp.Shared.Mapping
{
    public class BrugerMap : Profile
    {
        public BrugerMap()
        {
            CreateMap<Bruger, BrugerDTO>();
            CreateMap<BrugerDTO, Bruger>();
            CreateMap<RegisterModel, BrugerDTO>();
            // Mapping from BrugerDTO to Bruger
            //CreateMap<BrugerDTO, Bruger>()
            //    .ForMember(dest => dest.BrugerID, opt => opt.MapFrom(src => src.BrugerID))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.Brugernavn, opt => opt.MapFrom(src => src.Brugernavn))
            //    .ForMember(dest => dest.Fornavn, opt => opt.MapFrom(src => src.Fornavn))
            //    .ForMember(dest => dest.Efternavn, opt => opt.MapFrom(src => src.Efternavn))
            //    .ForMember(dest => dest.Brugerkode, opt => opt.MapFrom(src => src.Brugerkode))
            //    .ForMember(dest => dest.Bæltegrad, opt => opt.MapFrom(src => src.Bæltegrad))
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            //// Optional: You can create reverse mapping as well
            //CreateMap<Bruger, BrugerDTO>()
            //    .ForMember(dest => dest.BrugerID, opt => opt.MapFrom(src => src.BrugerID))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.Brugernavn, opt => opt.MapFrom(src => src.Brugernavn))
            //    .ForMember(dest => dest.Fornavn, opt => opt.MapFrom(src => src.Fornavn))
            //    .ForMember(dest => dest.Efternavn, opt => opt.MapFrom(src => src.Efternavn))
            //    .ForMember(dest => dest.Brugerkode, opt => opt.MapFrom(src => src.Brugerkode))
            //    .ForMember(dest => dest.Bæltegrad, opt => opt.MapFrom(src => src.Bæltegrad))
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
