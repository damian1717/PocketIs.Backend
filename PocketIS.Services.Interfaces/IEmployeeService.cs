using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task AddEmployeeAsync(Employee employees);
        Task UpdateEmployeeAsync(Employee employees);
        Task DeleteEmployeeAsync(Guid id);
    }
}
