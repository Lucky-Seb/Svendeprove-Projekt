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
    public class PensumMap : Profile
    {
        public PensumMap()
        {
            CreateMap<Pensum, PensumDTO>();
            CreateMap<PensumDTO, Pensum>();

            CreateMap<Pensum, PensumDTO>().ReverseMap();
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Øvelse, ØvelseDTO>().ReverseMap();
            CreateMap<Teori, TeoriDTO>().ReverseMap();
            CreateMap<Teknik, TeknikDTO>().ReverseMap();
        }
    }
}
