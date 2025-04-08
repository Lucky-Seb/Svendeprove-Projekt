using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.DTO;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class TeoriService
    {
        private readonly ITeoriRepository _teoriRepository;

        public TeoriService(ITeoriRepository teoriRepository)
        {
            _teoriRepository = teoriRepository;
        }

        // Private method to map Teori model to TeoriDTO
        private TeoriDTO MapToDTO(Teori teori)
        {
            return new TeoriDTO
            {
                TeoriID = teori.TeoriID,
                TeoriNavn = teori.TeoriNavn,
                TeoriBeskrivelse = teori.TeoriBeskrivelse,
                TeoriBillede = teori.TeoriBillede,
                TeoriVideo = teori.TeoriVideo,
                TeoriLyd = teori.TeoriLyd,
                PensumID = teori.PensumID
            };
        }

        // Get all Teori as DTO
        public async Task<List<TeoriDTO>> GetAllTeoriAsync()
        {
            var teoriList = await _teoriRepository.GetAllTeoriAsync();
            if (teoriList == null || !teoriList.Any()) return new List<TeoriDTO>();

            return teoriList.Select(MapToDTO).ToList();
        }

        // Get Teori by ID
        public async Task<TeoriDTO?> GetTeoriByIdAsync(int id)
        {
            if (id <= 0) return null;

            var teori = await _teoriRepository.GetTeoriByIdAsync(id);
            if (teori == null) return null;

            return MapToDTO(teori);
        }

        // Get all Teori by PensumID
        public async Task<List<TeoriDTO>> GetTeoriByPensumAsync(int pensumId)
        {
            if (pensumId <= 0) return new List<TeoriDTO>();

            var teoriList = await _teoriRepository.GetTeoriByPensumAsync(pensumId);
            if (teoriList == null || !teoriList.Any()) return new List<TeoriDTO>();

            return teoriList.Select(MapToDTO).ToList();
        }

        // Get Teori by TeoriNavn
        public async Task<TeoriDTO?> GetTeoriByTeoriNavnAsync(string teoriNavn)
        {
            if (string.IsNullOrWhiteSpace(teoriNavn)) return null;

            var teori = await _teoriRepository.GetTeoriByTeoriNavnAsync(teoriNavn);
            if (teori == null) return null;

            return MapToDTO(teori);
        }

        // Create Teori based on DTO
        public async Task<TeoriDTO?> CreateTeoriAsync(TeoriDTO teoriDto)
        {
            if (teoriDto == null) return null;
            if (string.IsNullOrWhiteSpace(teoriDto.TeoriNavn) || string.IsNullOrWhiteSpace(teoriDto.TeoriBeskrivelse)) return null;

            var newTeori = new Teori
            {
                TeoriNavn = teoriDto.TeoriNavn,
                TeoriBeskrivelse = teoriDto.TeoriBeskrivelse,
                TeoriBillede = teoriDto.TeoriBillede,
                TeoriVideo = teoriDto.TeoriVideo,
                TeoriLyd = teoriDto.TeoriLyd,
                PensumID = teoriDto.PensumID
            };

            await _teoriRepository.CreateTeoriAsync(newTeori);

            return MapToDTO(newTeori);
        }

        // Delete Teori by ID
        public async Task<bool> DeleteTeoriAsync(int id)
        {
            if (id <= 0) return false;
            return await _teoriRepository.DeleteTeoriAsync(id);
        }

        // Update Teori by ID and DTO
        public async Task<bool> UpdateTeoriAsync(int id, TeoriDTO teoriDto)
        {
            if (id <= 0 || teoriDto == null || id != teoriDto.TeoriID) return false;
            if (string.IsNullOrWhiteSpace(teoriDto.TeoriNavn) || string.IsNullOrWhiteSpace(teoriDto.TeoriBeskrivelse)) return false;

            var existingTeori = await _teoriRepository.GetTeoriByIdAsync(id);
            if (existingTeori == null) return false;

            var updatedTeori = new Teori
            {
                TeoriID = id,
                TeoriNavn = teoriDto.TeoriNavn,
                TeoriBeskrivelse = teoriDto.TeoriBeskrivelse,
                TeoriBillede = teoriDto.TeoriBillede,
                TeoriVideo = teoriDto.TeoriVideo,
                TeoriLyd = teoriDto.TeoriLyd,
                PensumID = teoriDto.PensumID
            };

            return await _teoriRepository.UpdateTeoriAsync(updatedTeori);
        }
    }
}
