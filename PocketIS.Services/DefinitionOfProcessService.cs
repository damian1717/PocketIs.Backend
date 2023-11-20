using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class DefinitionOfProcessService : IDefinitionOfProcessService
    {
        private readonly IDefinitionOfProcessRepository _definitionOfProcessRepository;
        public DefinitionOfProcessService(IDefinitionOfProcessRepository definitionOfProcessRepository)
        {
            _definitionOfProcessRepository = definitionOfProcessRepository;
        }

        public async Task AddDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess) 
            => await _definitionOfProcessRepository.AddDefinitionOfProcessAsync(definitionProcess);
                
        public async Task<List<DefinitionOfProcess>> GetBaseDefinitionOfProcessAsync(Guid processId)
            => await _definitionOfProcessRepository.GetBaseDefinitionOfProcessAsync(processId);

        public async Task<List<DefinitionOfProcess>> GetDefinitionOfProcessAsync(Guid processId)
            => await _definitionOfProcessRepository.GetDefinitionOfProcessAsync(processId);

        public async Task<DefinitionOfProcess> GetDefinitionOfProcessByIdAsync(Guid id)
            => await _definitionOfProcessRepository.GetDefinitionOfProcessByIdAsync(id);

        public async Task UpdateDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess)
            => await _definitionOfProcessRepository.UpdateDefinitionOfProcessAsync(definitionProcess);

        public async Task DeleteDefinitionOfProcessAsync(Guid id) 
            => await _definitionOfProcessRepository.DeleteDefinitionOfProcessAsync(id);
    }
}
