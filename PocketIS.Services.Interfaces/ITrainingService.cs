using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<List<Training>> GetTrainingsAsync(Guid companyId);
        Task<List<Training>> GetTrainingsForLevelAsync(Guid companyId, int level);
        Task<Training> GetTrainingByIdAsync(Guid id);
        Task AddTrainingAsync(Training training);
        Task UpdateTrainingAsync(Training training);
        Task DeleteTrainingAsync(Guid id);
    }
}
