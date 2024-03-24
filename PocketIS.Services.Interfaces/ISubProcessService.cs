using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface ISubProcessService
    {
        Task<List<SubProcess>> GetSubProcessesAsync(int subProcessType);
        Task<SubProcess> GetSubProcessByIdAsync(Guid id);
        Task AddSubProcessAsync(SubProcess subProcess);
        Task UpdateSubProcessAsync(SubProcess subProcess);
        Task DeleteSubProcessAsync(Guid id);
    }
}
