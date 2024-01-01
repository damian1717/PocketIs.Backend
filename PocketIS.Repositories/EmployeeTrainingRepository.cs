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
    public class EmployeeTrainingRepository : IEmployeeTrainingRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public EmployeeTrainingRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining)
        {
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
            => await _dbContext.EmployeeTrainings.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<EmployeeTraining>> GetEmployeeTrainingsAsync(Guid companyId, Guid employeeId)
            => await _dbContext.EmployeeTrainings
            .Where(x => x.CompanyId == companyId
            && x.EmployeeId == employeeId)
            .ToListAsync();

        public async Task UpdateEmployeeTrainingAsync(EmployeeTraining employeeTraining)
        {
            var currentEmployeesTraining = await _dbContext.EmployeeTrainings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeTraining.Id);

            if (currentEmployeesTraining is not null)
            {
                currentEmployeesTraining.Required = employeeTraining.Required;
                currentEmployeesTraining.TrainingDate = employeeTraining.TrainingDate;
                currentEmployeesTraining.SkillLevel = employeeTraining.SkillLevel;

                _dbContext.EmployeeTrainings.Update(currentEmployeesTraining);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
