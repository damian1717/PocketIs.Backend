using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class RegulationRepository : IRegulationRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public RegulationRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddRegulationAsync(Regulation regulation)
        {
            await _dbContext.Regulations.AddAsync(regulation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Regulation> GetRegulationByIdAsync(Guid id) => await _dbContext.Regulations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Regulation>> GetRegulationsAsync() => await _dbContext.Regulations.ToListAsync();

        public async Task UpdateRegulationAsync(Regulation regulation)
        {
            _dbContext.Regulations.Update(regulation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
