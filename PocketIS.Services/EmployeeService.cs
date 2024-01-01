using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task AddEmployeeAsync(Employee employees)
            => await _employeeRepository.AddEmployeeAsync(employees);

        public async Task DeleteEmployeeAsync(Guid id)
            => await _employeeRepository.DeleteEmployeeAsync(id);

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
            => await _employeeRepository.GetEmployeeByIdAsync(id);

        public async Task<List<Employee>> GetEmployeesAsync(Guid companyId)
            => await _employeeRepository.GetEmployeesAsync(companyId);

        public async Task UpdateEmployeeAsync(Employee employees)
            => await _employeeRepository.UpdateEmployeeAsync(employees);
    }
}
