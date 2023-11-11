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
        Task<List<Process>> GetBaseProcessesAsync(Guid companyId);
        Task<List<Process>> GetProcessesAsync(Guid companyId);
        Task<Process> GetProcessByIdAsync(Guid id);
    }
}
