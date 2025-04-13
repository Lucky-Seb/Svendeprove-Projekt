using AutoMapper;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Mapping
{
    public class OrdbogMap : Profile
    {
        public OrdbogMap()
        {
            CreateMap<Ordbog, OrdbogDTO>();
            CreateMap<OrdbogDTO, Ordbog>();

            //// Mapping from Ordbog model to OrdbogDTO
            //CreateMap<Ordbog, OrdbogDTO>()
            //    .ForMember(dest => dest.OrdbogId, opt => opt.MapFrom(src => src.OrdbogId))
            //    .ForMember(dest => dest.DanskOrd, opt => opt.MapFrom(src => src.DanskOrd))
            //    .ForMember(dest => dest.KoranskOrd, opt => opt.MapFrom(src => src.KoranskOrd))
            //    .ForMember(dest => dest.Beskrivelse, opt => opt.MapFrom(src => src.Beskrivelse))
            //    .ForMember(dest => dest.BilledeLink, opt => opt.MapFrom(src => src.BilledeLink))
            //    .ForMember(dest => dest.LydLink, opt => opt.MapFrom(src => src.LydLink))
            //    .ForMember(dest => dest.VideoLink, opt => opt.MapFrom(src => src.VideoLink))
            //    .ForMember(dest => dest.LastSyncedVersion, opt => opt.MapFrom(src => src.LastSyncedVersion))
            //    .ForMember(dest => dest.ETag, opt => opt.MapFrom(src => src.ETag))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //    .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified));

            //// Mapping from OrdbogDTO to Ordbog model
            //CreateMap<OrdbogDTO, Ordbog>()
            //    .ForMember(dest => dest.OrdbogId, opt => opt.MapFrom(src => src.OrdbogId))
            //    .ForMember(dest => dest.DanskOrd, opt => opt.MapFrom(src => src.DanskOrd))
            //    .ForMember(dest => dest.KoranskOrd, opt => opt.MapFrom(src => src.KoranskOrd))
            //    .ForMember(dest => dest.Beskrivelse, opt => opt.MapFrom(src => src.Beskrivelse))
            //    .ForMember(dest => dest.BilledeLink, opt => opt.MapFrom(src => src.BilledeLink))
            //    .ForMember(dest => dest.LydLink, opt => opt.MapFrom(src => src.LydLink))
            //    .ForMember(dest => dest.VideoLink, opt => opt.MapFrom(src => src.VideoLink))
            //    .ForMember(dest => dest.LastSyncedVersion, opt => opt.MapFrom(src => src.LastSyncedVersion))
            //    .ForMember(dest => dest.ETag, opt => opt.MapFrom(src => src.ETag))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //    .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified));
        }
    }
}
