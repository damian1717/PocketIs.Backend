using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IEmployeeTrainingRepository
    {
        Task<List<EmployeeTraining>> GetEmployeeTrainingsAsync(Guid employeeId);
        Task<EmployeeTraining> GetEmployeeTrainingByIdAsync(Guid id);
        Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining);
        Task UpdateEmployeeTrainingAsync(EmployeeTraining employeeTraining);
        Task DeleteEmployeeTrainingAsync(Guid id);
    }
}
