using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class TrainingRepository : BaseRepository, ITrainingRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public TrainingRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddTrainingAsync(Training training)
        {
            await _dbContext.Trainings.AddAsync(training);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Training> GetTrainingByIdAsync(Guid id)
            => await _dbContext.Trainings.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Training>> GetTrainingsAsync()
            => await _dbContext.Trainings.Where(x => x.CompanyId == CompanyId).ToListAsync();

        public async Task UpdateTrainingAsync(Training training)
        {
            var currentTraining = await _dbContext.Trainings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == training.Id);

            if (currentTraining is not null)
            {
                currentTraining.Name = training.Name;
                currentTraining.Level = training.Level;
                currentTraining.ForHowManyMonths = training.ForHowManyMonths;

                _dbContext.Trainings.Update(currentTraining);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteTrainingAsync(Guid id)
        {
            var training = new Training()
            {
                Id = id
            };

            _dbContext.Trainings.Remove(training);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Training>> GetTrainingsForLevelAsync(int level)
            => await _dbContext.Trainings
                .Where(x => x.CompanyId == CompanyId
                && x.Level == level).ToListAsync();
    }
}
