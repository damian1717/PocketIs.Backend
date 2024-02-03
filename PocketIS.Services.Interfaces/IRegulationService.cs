using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IRegulationService
    {
        Task AddRegulationAsync(Regulation regulation);
        Task UpdateRegulationAsync(Regulation regulation);
        Task<List<Regulation>> GetRegulationsAsync();
        Task<Regulation> GetRegulationByIdAsync(Guid id);
        Task DeleteRegulationAsync(Guid id);
    }
}
