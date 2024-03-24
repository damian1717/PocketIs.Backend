using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain.EmployeeTrainingModel;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class EmployeeTrainingRepository : BaseRepository, IEmployeeTrainingRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public EmployeeTrainingRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining)
        {
            employeeTraining.InsertedDate = DateTime.Now;
            employeeTraining.InsertedUserId = UserId;
            employeeTraining.UpdatedDate = DateTime.Now;
            employeeTraining.UpdatedUserId = UserId;

            await _dbContext.EmployeeTrainings.AddAsync(employeeTraining);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeTrainingAsync(Guid id)
        {
            var employeeTraining = new EmployeeTraining()
            {
                Id = id
            };

            _dbContext.EmployeeTrainings.Remove(employeeTraining);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeeTraining> GetEmployeeTrainingByIdAsync(Guid id)
            => await _dbContext.EmployeeTrainings
            .FirstOrDefaultAsync(x => x.Id == id
            && x.CompanyId == CompanyId);

        public async Task<List<EmployeeTraining>> GetEmployeeTrainingsAsync(Guid employeeId)
            => await _dbContext.EmployeeTrainings
            .Where(x => x.CompanyId == CompanyId
            && x.EmployeeId == employeeId)
            .ToListAsync();

        public async Task<List<EmployeeTraining>> GetEmployeeTrainingsAsync()
            => await _dbContext.EmployeeTrainings
                .Where(x => x.CompanyId == CompanyId)
                .ToListAsync();

        public async Task<EmployeeTraining> GetLastModifiedRecordAsync()
        {
            return await _dbContext.EmployeeTrainings
                   .OrderByDescending(p => p.UpdatedDate)
                   .FirstOrDefaultAsync();
        }

        public async Task UpdateEmployeeTrainingAsync(EmployeeTraining employeeTraining)
        {
            var currentEmployeesTraining = await _dbContext.EmployeeTrainings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeTraining.Id);

            if (currentEmployeesTraining is not null)
            {
                currentEmployeesTraining.Required = employeeTraining.Required;
                currentEmployeesTraining.TrainingDate = employeeTraining.TrainingDate;
                currentEmployeesTraining.SkillLevel = employeeTraining.SkillLevel;
                currentEmployeesTraining.UpdatedDate = DateTime.Now;
                currentEmployeesTraining.UpdatedUserId = UserId;

                _dbContext.EmployeeTrainings.Update(currentEmployeesTraining);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
