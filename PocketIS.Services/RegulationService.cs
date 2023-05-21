using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class RegulationService : IRegulationService
    {
        private readonly IRegulationRepository _regulationRepository;
        public RegulationService(IRegulationRepository regulationRepository)
        {
            _regulationRepository = regulationRepository;
        }
        public async Task AddRegulationAsync(Regulation regulation) => await _regulationRepository.AddRegulationAsync(regulation);

        public async Task<Regulation> GetRegulationByIdAsync(Guid id) => await _regulationRepository.GetRegulationByIdAsync(id);

        public async Task<List<Regulation>> GetRegulationsAsync() => await _regulationRepository.GetRegulationsAsync();

        public async Task UpdateRegulationAsync(Regulation regulation) => await _regulationRepository.UpdateRegulationAsync(regulation);
    }
}
