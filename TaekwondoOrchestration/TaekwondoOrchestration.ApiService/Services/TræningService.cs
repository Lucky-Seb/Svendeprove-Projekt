using TaekwondoOrchestration.ApiService.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;


namespace TaekwondoOrchestration.ApiService.Services
{
    public class TræningService
    {
        private readonly ITræningRepository _træningRepository;

        public TræningService(ITræningRepository træningRepository)
        {
            _træningRepository = træningRepository;
        }

        // Get all Træning as DTO
        public async Task<List<TræningDTO>> GetAllTræningAsync()
        {
            var træningList = await _træningRepository.GetAllTræningAsync();
            if (træningList == null || !træningList.Any()) return new List<TræningDTO>();

            return træningList.Select(t => new TræningDTO
            {
                TræningID = t.TræningID,
                TræningRækkefølge = t.TræningRækkefølge,
                Tid = t.Tid,
                ProgramID = t.ProgramID,
                QuizID = t.QuizID,
                TeoriID = t.TeoriID,
                TeknikID = t.TeknikID,
                ØvelseID = t.ØvelseID,
                PensumID = t.PensumID
            }).ToList();
        }

        // Get Træning by ID
        public async Task<TræningDTO?> GetTræningByIdAsync(int id)
        {
            if (id <= 0) return null;

            var træning = await _træningRepository.GetTræningByIdAsync(id);
            if (træning == null) return null;

            return new TræningDTO
            {
                TræningID = træning.TræningID,
                TræningRækkefølge = træning.TræningRækkefølge,
                Tid = træning.Tid,
                ProgramID = træning.ProgramID,
                QuizID = træning.QuizID,
                TeoriID = træning.TeoriID,
                TeknikID = træning.TeknikID,
                ØvelseID = træning.ØvelseID,
                PensumID = træning.PensumID
            };
        }

        // Create Træning based on DTO
        public async Task<TræningDTO?> CreateTræningAsync(TræningDTO træningDto)
        {
            if (træningDto == null) return null;
            if (træningDto.ProgramID <= 0 || træningDto.Tid <= 0) return null;

            var newTræning = new Træning
            {
                TræningRækkefølge = træningDto.TræningRækkefølge,
                Tid = træningDto.Tid,
                ProgramID = træningDto.ProgramID,
                QuizID = træningDto.QuizID,
                TeoriID = træningDto.TeoriID,
                TeknikID = træningDto.TeknikID,
                ØvelseID = træningDto.ØvelseID,
                PensumID = træningDto.PensumID
            };

            await _træningRepository.CreateTræningAsync(newTræning);

            return new TræningDTO
            {
                TræningID = newTræning.TræningID,
                TræningRækkefølge = newTræning.TræningRækkefølge,
                Tid = newTræning.Tid,
                ProgramID = newTræning.ProgramID,
                QuizID = newTræning.QuizID,
                TeoriID = newTræning.TeoriID,
                TeknikID = newTræning.TeknikID,
                ØvelseID = newTræning.ØvelseID,
                PensumID = newTræning.PensumID
            };
        }

        // Delete Træning by ID
        public async Task<bool> DeleteTræningAsync(int id)
        {
            if (id <= 0) return false;
            return await _træningRepository.DeleteTræningAsync(id);
        }

        // Update Træning by ID and DTO
        public async Task<bool> UpdateTræningAsync(int id, TræningDTO træningDto)
        {
            if (id <= 0 || træningDto == null || id != træningDto.TræningID) return false;
            if (træningDto.ProgramID <= 0 || træningDto.Tid <= 0) return false;

            var existingTræning = await _træningRepository.GetTræningByIdAsync(id);
            if (existingTræning == null) return false;

            var updatedTræning = new Træning
            {
                TræningID = id,
                TræningRækkefølge = træningDto.TræningRækkefølge,
                Tid = træningDto.Tid,
                ProgramID = træningDto.ProgramID,
                QuizID = træningDto.QuizID,
                TeoriID = træningDto.TeoriID,
                TeknikID = træningDto.TeknikID,
                ØvelseID = træningDto.ØvelseID,
                PensumID = træningDto.PensumID
            };

            return await _træningRepository.UpdateTræningAsync(updatedTræning);
        }
    }
}
