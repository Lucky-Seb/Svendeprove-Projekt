using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Services
{
    public interface ISQLiteService
    {
        Task<int> AddEntryAsync(OrdbogDTO entry);
        Task<List<OrdbogDTO>> GetAllEntriesAsync();
        Task<int> DeleteEntryAsync(int id); // Add this line to the interface
        Task<int> UpdateEntryAsync(OrdbogDTO entry);
        void InitializeDatabase(); // Add this line to the interface
    }
}