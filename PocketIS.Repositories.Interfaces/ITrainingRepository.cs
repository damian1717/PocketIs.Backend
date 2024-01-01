using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task<List<Training>> GetTrainingsAsync(Guid companyId);
        Task<List<Training>> GetTrainingsForLevelAsync(Guid companyId, int level);
        Task<Training> GetTrainingByIdAsync(Guid id);
        Task AddTrainingAsync(Training training);
        Task UpdateTrainingAsync(Training training);
        Task DeleteTrainingAsync(Guid id);
    }
}
