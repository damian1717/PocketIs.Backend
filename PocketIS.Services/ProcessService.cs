using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        public ProcessService(IProcessRepository processRepository)
        {
            _processRepository = processRepository;
        }

        public async Task AddProcessAsync(Process process) 
            => await _processRepository.AddProcessAsync(process);

        public async Task<List<Process>> GetBaseProcessesAsync() 
            => await _processRepository.GetBaseProcessesAsync();

        public async Task<Process> GetProcessByIdAsync(Guid id) 
            => await _processRepository.GetProcessByIdAsync(id);

        public async Task<List<Process>> GetProcessesAsync() 
            => await _processRepository.GetProcessesAsync();

        public async Task<List<Process>> GetProcessesForSuperAdmin() 
            => await _processRepository.GetProcessesForSuperAdmin();

        public async Task UpdateProcessAsync(Process process) 
            => await _processRepository.UpdateProcessAsync(process);
    }
}
