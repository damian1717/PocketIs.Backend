using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IEmployeeTrainingService
    {
        Task<List<EmployeeTrainingInfo>> GetEmployeeTrainingsAsync(Guid employeeId, int level);
        Task<EmployeeTraining> GetEmployeeTrainingByIdAsync(Guid id);
        Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining);
        Task UpdateEmployeeTrainingAsync(EmployeeTraining employeeTraining);
        Task DeleteEmployeeTrainingAsync(Guid id);
    }
}
