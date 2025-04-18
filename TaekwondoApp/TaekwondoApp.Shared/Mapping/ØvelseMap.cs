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
    public class ØvelseMap : Profile
    {
        public ØvelseMap()
        {
            CreateMap<Øvelse, ØvelseDTO>();
            CreateMap<ØvelseDTO, Øvelse>();

            CreateMap<Øvelse, ØvelseDTO>().ReverseMap();
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Teori, TeoriDTO>().ReverseMap();
            CreateMap<Teknik, TeknikDTO>().ReverseMap();
        }
    }
}
