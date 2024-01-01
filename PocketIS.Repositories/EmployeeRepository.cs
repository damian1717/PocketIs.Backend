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
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public EmployeeRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddEmployeeAsync(Employee employees)
        {
            await _dbContext.Employees.AddAsync(employees);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employees = new Employee()
            {
                Id = id
            };

            _dbContext.Employees.Remove(employees);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
            => await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Employee>> GetEmployeesAsync()
            => await _dbContext.Employees
            .Where(x => x.CompanyId == CompanyId)
            .ToListAsync();

        public async Task UpdateEmployeeAsync(Employee employees)
        {
            var currentEmployees = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employees.Id);

            if (currentEmployees is not null)
            {
                currentEmployees.Name = employees.Name;
                currentEmployees.Level = employees.Level;
                currentEmployees.LastName = employees.LastName;
                currentEmployees.Position = employees.Position;
                currentEmployees.Email = employees.Email;

                _dbContext.Employees.Update(currentEmployees);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
