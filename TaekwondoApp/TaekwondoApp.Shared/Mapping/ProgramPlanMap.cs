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
    public class ProgramPlanMap : Profile
    {
        public ProgramPlanMap()
        {
            CreateMap<ProgramPlan, ProgramPlanDTO>();
            CreateMap<ProgramPlanDTO, ProgramPlan>();

            CreateMap<ProgramPlan, ProgramPlanDTO>().ReverseMap();
            CreateMap<KlubProgram, KlubProgramDTO>().ReverseMap();
            CreateMap<BrugerProgram, BrugerProgramDTO>().ReverseMap();
            CreateMap<Træning, TræningDTO>().ReverseMap();
        }
    }
}
