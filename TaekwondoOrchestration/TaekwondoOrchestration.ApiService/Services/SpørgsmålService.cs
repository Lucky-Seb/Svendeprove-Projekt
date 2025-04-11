using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class SpørgsmålService
    {
        private readonly ISpørgsmålRepository _spørgsmålRepository;

        public SpørgsmålService(ISpørgsmålRepository spørgsmålRepository)
        {
            _spørgsmålRepository = spørgsmålRepository;
        }

        public async Task<List<SpørgsmålDTO>> GetAllSpørgsmålAsync()
        {
            var spørgsmålList = await _spørgsmålRepository.GetAllAsync();
            return spørgsmålList.Select(s => new SpørgsmålDTO
            {
                SpørgsmålID = s.SpørgsmålID,
                SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                SpørgsmålTid = s.SpørgsmålTid,
                TeoriID = s.TeoriID,
                TeknikID = s.TeknikID,
                ØvelseID = s.ØvelseID,
                QuizID = s.QuizID
            }).ToList();
        }

        public async Task<SpørgsmålDTO?> GetSpørgsmålByIdAsync(Guid id)
        {
            var spørgsmål = await _spørgsmålRepository.GetByIdAsync(id);
            if (spørgsmål == null) return null;

            return new SpørgsmålDTO
            {
                SpørgsmålID = spørgsmål.SpørgsmålID,
                SpørgsmålRækkefølge = spørgsmål.SpørgsmålRækkefølge,
                SpørgsmålTid = spørgsmål.SpørgsmålTid,
                TeoriID = spørgsmål.TeoriID,
                TeknikID = spørgsmål.TeknikID,
                ØvelseID = spørgsmål.ØvelseID,
                QuizID = spørgsmål.QuizID
            };
        }

        public async Task<SpørgsmålDTO> CreateSpørgsmålAsync(SpørgsmålDTO spørgsmålDto)
        {
            var newSpørgsmål = new Spørgsmål
            {
                SpørgsmålRækkefølge = spørgsmålDto.SpørgsmålRækkefølge,
                SpørgsmålTid = spørgsmålDto.SpørgsmålTid,
                TeoriID = spørgsmålDto.TeoriID,
                TeknikID = spørgsmålDto.TeknikID,
                ØvelseID = spørgsmålDto.ØvelseID,
                QuizID = spørgsmålDto.QuizID
            };

            var createdSpørgsmål = await _spørgsmålRepository.CreateAsync(newSpørgsmål);

            return new SpørgsmålDTO
            {
                SpørgsmålID = createdSpørgsmål.SpørgsmålID,
                SpørgsmålRækkefølge = createdSpørgsmål.SpørgsmålRækkefølge,
                SpørgsmålTid = createdSpørgsmål.SpørgsmålTid,
                TeoriID = createdSpørgsmål.TeoriID,
                TeknikID = createdSpørgsmål.TeknikID,
                ØvelseID = createdSpørgsmål.ØvelseID,
                QuizID = createdSpørgsmål.QuizID
            };
        }

        public async Task<bool> DeleteSpørgsmålAsync(Guid id)
        {
            return await _spørgsmålRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateSpørgsmålAsync(Guid id, SpørgsmålDTO spørgsmålDto)
        {
            if (spørgsmålDto == null || id != spørgsmålDto.SpørgsmålID)
                return false;

            var spørgsmål = await _spørgsmålRepository.GetByIdAsync(id);
            if (spørgsmål == null)
                return false;

            spørgsmål.SpørgsmålRækkefølge = spørgsmålDto.SpørgsmålRækkefølge;
            spørgsmål.SpørgsmålTid = spørgsmålDto.SpørgsmålTid;
            spørgsmål.TeoriID = spørgsmålDto.TeoriID;
            spørgsmål.TeknikID = spørgsmålDto.TeknikID;
            spørgsmål.ØvelseID = spørgsmålDto.ØvelseID;
            spørgsmål.QuizID = spørgsmålDto.QuizID;

            return await _spørgsmålRepository.UpdateAsync(spørgsmål);
        }
    }
}
