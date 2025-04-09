using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class PensumService
    {
        private readonly IPensumRepository _pensumRepository;

        public PensumService(IPensumRepository pensumRepository)
        {
            _pensumRepository = pensumRepository;
        }

        public async Task<List<PensumDTO>> GetAllPensumAsync()
        {
            var pensumList = await _pensumRepository.GetAllAsync();
            return pensumList.Select(p => new PensumDTO
            {
                PensumID = p.PensumID,
                PensumGrad = p.PensumGrad
            }).ToList();
        }

        public async Task<PensumDTO?> GetPensumByIdAsync(int id)
        {
            var pensum = await _pensumRepository.GetByIdAsync(id);
            if (pensum == null)
                return null;

            return new PensumDTO
            {
                PensumID = pensum.PensumID,
                PensumGrad = pensum.PensumGrad,

                // Ensure Technik and Teori are correctly mapped from the loaded related entities
                Teknik = pensum.Teknikker?.Select(t => new TeknikDTO
                {
                    TeknikID = t.TeknikID,
                    TeknikNavn = t.TeknikNavn,
                    TeknikBeskrivelse = t.TeknikBeskrivelse,
                    TeknikBillede = t.TeknikBillede,
                    TeknikVideo = t.TeknikVideo,
                    TeknikLyd = t.TeknikLyd,
                    PensumID = t.PensumID
                }).ToList(),

                Teori = pensum.Teorier?.Select(te => new TeoriDTO
                {
                    TeoriID = te.TeoriID,
                    TeoriNavn = te.TeoriNavn,
                    TeoriBeskrivelse = te.TeoriBeskrivelse,
                    TeoriBillede = te.TeoriBillede,
                    TeoriVideo = te.TeoriVideo,
                    TeoriLyd = te.TeoriLyd,
                    PensumID = te.PensumID
                }).ToList()
            };
        }


        public async Task<PensumDTO> CreatePensumAsync(PensumDTO pensumDTO)
        {
            // Validate required field PensumGrad
            if (string.IsNullOrEmpty(pensumDTO.PensumGrad))
            {
                return null; // Return null or a custom error DTO if needed
            }

            var pensum = new Pensum
            {
                PensumGrad = pensumDTO.PensumGrad
            };

            var createdPensum = await _pensumRepository.CreateAsync(pensum);

            return new PensumDTO
            {
                PensumID = createdPensum.PensumID,
                PensumGrad = createdPensum.PensumGrad
            };
        }


        public async Task<bool> UpdatePensumAsync(int id, PensumDTO pensumDTO)
        {
            var pensum = await _pensumRepository.GetByIdAsync(id);
            if (pensum == null) return false;

            pensum.PensumGrad = pensumDTO.PensumGrad;

            return await _pensumRepository.UpdateAsync(pensum);
        }

        public async Task<bool> DeletePensumAsync(int id)
        {
            return await _pensumRepository.DeleteAsync(id);
        }
    }
}
