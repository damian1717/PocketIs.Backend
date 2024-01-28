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
        private readonly IUserService _userService;
        public TrainingService(ITrainingRepository trainingRepository, IUserService userService)
        {
            _trainingRepository = trainingRepository;
            _userService = userService;
        }
        public async Task AddTrainingAsync(Training training) 
            => await _trainingRepository.AddTrainingAsync(training);

        public async Task DeleteTrainingAsync(Guid id) 
            => await _trainingRepository.DeleteTrainingAsync(id);

        public async Task<LastModifiedRecordInfo> GetLastModifiedRecordDataAsync()
        {
            var item = new LastModifiedRecordInfo();
            var training = await _trainingRepository.GetLastModifiedRecordAsync();

            item.ModifiedDate = training?.UpdatedDate ?? null;

            if (training is not null)
            {
                var user = await _userService.GetAsync(training.UpdatedUserId ?? Guid.Empty);

                if (user is not null)
                {
                    item.Name = $"{user.FirstName} {user.LastName}";
                }
            }

            return item;
        }

        public async Task<Training> GetTrainingByIdAsync(Guid id) 
            => await _trainingRepository.GetTrainingByIdAsync(id);

        public async Task<List<Training>> GetTrainingsAsync() 
            => await _trainingRepository.GetTrainingsAsync();

        public async Task<List<Training>> GetTrainingsForLevelAsync(int level) 
            => await _trainingRepository.GetTrainingsForLevelAsync(level);

        public async Task UpdateTrainingAsync(Training training) 
            => await _trainingRepository.UpdateTrainingAsync(training);
    }
}
