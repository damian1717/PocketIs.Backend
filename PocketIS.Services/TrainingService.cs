using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }
        public async Task AddTrainingAsync(Training training) => await _trainingRepository.AddTrainingAsync(training);

        public async Task<Training> GetTrainingByIdAsync(Guid id) => await _trainingRepository.GetTrainingByIdAsync(id);

        public async Task<List<Training>> GetTrainingsAsync(Guid companyId) => await _trainingRepository.GetTrainingsAsync(companyId);

        public async Task UpdateTrainingAsync(Training training) => await _trainingRepository.UpdateTrainingAsync(training);
    }
}
