using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<List<Training>> GetTrainingsAsync();
        Task<List<Training>> GetTrainingsForLevelAsync(int level);
        Task<Training> GetTrainingByIdAsync(Guid id);
        Task AddTrainingAsync(Training training);
        Task UpdateTrainingAsync(Training training);
        Task DeleteTrainingAsync(Guid id);
        Task<LastModifiedRecordInfo> GetLastModifiedRecordDataAsync();
    }
}
