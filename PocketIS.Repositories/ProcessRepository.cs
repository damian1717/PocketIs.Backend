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
    public class ProcessRepository : IProcessRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public ProcessRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProcessAsync(Process process)
        {
            await _dbContext.Processes.AddAsync(process);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Process> GetProcessByIdAsync(Guid id) => await _dbContext.Processes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Process>> GetBaseProcessesAsync(Guid companyId) 
            => await _dbContext.Processes
            .Where(x => x.CompanyId == companyId
                     && x.IsBaseProcess)
            .ToListAsync();

        public async Task<List<Process>> GetProcessesAsync(Guid companyId) 
            => await _dbContext.Processes
            .Where(x => x.CompanyId == companyId)
            .ToListAsync();

        public async Task UpdateProcessAsync(Process process)
        {
            var currentProcess = await _dbContext.Processes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == process.Id);

            if (currentProcess is not null)
            {
                currentProcess.Name = process.Name;

                _dbContext.Processes.Update(currentProcess);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Process>> GetProcessesForSuperAdmin()
        => await _dbContext.Processes
            .Include(x => x.Company)
            .ToListAsync();
    }
}
