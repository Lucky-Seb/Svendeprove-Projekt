﻿using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerService
    {
        // CRUD
        Task<Result<IEnumerable<BrugerDTO>>> GetAllBrugereAsync();
        Task<Result<BrugerDTO>> GetBrugerByIdAsync(Guid id);
        Task<Result<BrugerDTO>> CreateBrugerAsync(BrugerDTO brugerDto);
        Task<Result<bool>> UpdateBrugerAsync(Guid id, BrugerDTO brugerDto);
        Task<Result<bool>> DeleteBrugerAsync(Guid id);
        Task<Result<bool>> RestoreBrugerAsync(Guid id, BrugerDTO dto);
        Task<Result<BrugerDTO>> AuthenticateBrugerAsync(LoginDTO loginDto);

        // Search
        Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByRoleAsync(string role);
        Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByBælteAsync(string bæltegrad);
        Task<Result<IEnumerable<BrugerDTO>>> GetBrugereByKlubAsync(Guid klubId);
        Task<Result<IEnumerable<BrugerDTO>>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad);
        Task<Result<BrugerDTO>> GetBrugerByBrugernavnAsync(string brugernavn);
        Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn);
    }
}
