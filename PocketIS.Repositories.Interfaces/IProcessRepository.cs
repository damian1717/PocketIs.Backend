using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IProcessRepository
    {
        Task AddProcessAsync(Process process);
        Task UpdateProcessAsync(Process process);
        Task<List<Process>> GetBaseProcessesAsync();
        Task<List<Process>> GetProcessesAsync();
        Task<List<Process>> GetProcessesForSuperAdmin();
        Task<Process> GetProcessByIdAsync(Guid id);
    }
}
