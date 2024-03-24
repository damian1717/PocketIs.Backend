using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class SubProcessService : ISubProcessService
    {
        private readonly ISubProcessRepository _subProcessRepository;
        public SubProcessService(ISubProcessRepository subProcessRepository)
        {
            _subProcessRepository = subProcessRepository;
        }
        public async Task AddSubProcessAsync(SubProcess subProcess)
        {
            var lastVersion = await _subProcessRepository.GetLastVersionAsync(subProcess.SubProcessType);

            var number = lastVersion + 1 >= 10 ? $"{lastVersion + 1}" : $"0{lastVersion + 1}";
            subProcess.Name = $"{GetSubProcessName(subProcess.SubProcessType)}{number}";
            subProcess.Version = lastVersion + 1;

            await _subProcessRepository.AddSubProcessAsync(subProcess);
        }
          
        public async Task DeleteSubProcessAsync(Guid id)
            => await _subProcessRepository.DeleteSubProcessAsync(id);

        public async Task<SubProcess> GetSubProcessByIdAsync(Guid id)
            => await _subProcessRepository.GetSubProcessByIdAsync(id);
            
        public async Task<List<SubProcess>> GetSubProcessesAsync(int subProcessType)
            => await _subProcessRepository.GetSubProcessesAsync(subProcessType);

        public async Task UpdateSubProcessAsync(SubProcess subProcess)
            => await _subProcessRepository.UpdateSubProcessAsync(subProcess);

        private string GetSubProcessName(int subProcessType)
        {
            if (subProcessType == 1) return "PS";

            if (subProcessType == 2) return "PIS";

            return string.Empty;
        }
    }
}
