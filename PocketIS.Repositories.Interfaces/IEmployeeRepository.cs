using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task AddEmployeeAsync(Employee employees);
        Task UpdateEmployeeAsync(Employee employees);
        Task DeleteEmployeeAsync(Guid id);
    }
}
