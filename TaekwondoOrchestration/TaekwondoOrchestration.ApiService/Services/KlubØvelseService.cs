using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubØvelseService : IKlubØvelseService
    {
        private readonly IKlubØvelseRepository _klubØvelseRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public KlubØvelseService(IKlubØvelseRepository klubØvelseRepository, IMapper mapper)
        {
            _klubØvelseRepository = klubØvelseRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All KlubØvelser
        public async Task<Result<IEnumerable<KlubØvelseDTO>>> GetAllKlubØvelserAsync()
        {
            var klubØvelser = await _klubØvelseRepository.GetAllKlubØvelserAsync();
            var mapped = _mapper.Map<IEnumerable<KlubØvelseDTO>>(klubØvelser);
            return Result<IEnumerable<KlubØvelseDTO>>.Ok(mapped);
        }

        // Get KlubØvelse by ID
        public async Task<Result<KlubØvelseDTO>> GetKlubØvelseByIdAsync(Guid klubId, Guid øvelseId)
        {
            var klubØvelse = await _klubØvelseRepository.GetKlubØvelseByIdAsync(klubId, øvelseId);
            if (klubØvelse == null)
                return Result<KlubØvelseDTO>.Fail("KlubØvelse not found.");

            var mapped = _mapper.Map<KlubØvelseDTO>(klubØvelse);
            return Result<KlubØvelseDTO>.Ok(mapped);
        }

        // Create New KlubØvelse
        public async Task<Result<KlubØvelseDTO>> CreateKlubØvelseAsync(KlubØvelseDTO klubØvelseDto)
        {
            if (klubØvelseDto == null)
                return Result<KlubØvelseDTO>.Fail("Invalid KlubØvelse data.");

            var newKlubØvelse = _mapper.Map<KlubØvelse>(klubØvelseDto);
            var createdKlubØvelse = await _klubØvelseRepository.CreateKlubØvelseAsync(newKlubØvelse);
            var mapped = _mapper.Map<KlubØvelseDTO>(createdKlubØvelse);
            return Result<KlubØvelseDTO>.Ok(mapped);
        }

        // Delete KlubØvelse
        public async Task<Result<bool>> DeleteKlubØvelseAsync(Guid klubId, Guid øvelseId)
        {
            var klubØvelse = await _klubØvelseRepository.GetKlubØvelseByIdAsync(klubId, øvelseId);
            if (klubØvelse == null)
                return Result<bool>.Fail("KlubØvelse not found.");

            var success = await _klubØvelseRepository.DeleteKlubØvelseAsync(klubId, øvelseId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete KlubØvelse.");
        }

        #endregion
    }
}
