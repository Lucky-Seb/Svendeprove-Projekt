using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.Services
{
    public interface ISQLiteService
    {
        void InitializeDatabase();
        Task<int> AddEntryAsync(OrdbogDTO entry);
        Task<List<OrdbogDTO>> GetAllEntriesAsync();
    }
}