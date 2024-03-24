using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface ISubProcessRepository
    {
        Task<List<SubProcess>> GetSubProcessesAsync(int subProcessType);
        Task<SubProcess> GetSubProcessByIdAsync(Guid id);
        Task AddSubProcessAsync(SubProcess subProcess);
        Task UpdateSubProcessAsync(SubProcess subProcess);
        Task DeleteSubProcessAsync(Guid id);
        Task<int> GetLastVersionAsync(int subProcessType);
    }
}
