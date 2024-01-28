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
    public class RegulationRepository : BaseRepository, IRegulationRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public RegulationRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddRegulationAsync(Regulation regulation)
        {
            regulation.InsertedDate = DateTime.Now;
            regulation.InsertedUserId = UserId;
            regulation.UpdatedDate = DateTime.Now;
            regulation.UpdatedUserId = UserId;

            await _dbContext.Regulations.AddAsync(regulation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Regulation> GetRegulationByIdAsync(Guid id) 
            => await _dbContext.Regulations
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Regulation>> GetRegulationsAsync() 
            => await _dbContext.Regulations
                .Where(x => x.CompanyId == CompanyId)
                .ToListAsync();

        public async Task UpdateRegulationAsync(Regulation regulation)
        {
            var currentRegulation = await _dbContext.Regulations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == regulation.Id);

            if (currentRegulation is not null)
            {
                currentRegulation.Link = regulation.Link;
                currentRegulation.Description = regulation.Description;
                currentRegulation.Name = regulation.Name;
                currentRegulation.UpdatedDate = DateTime.Now;
                currentRegulation.UpdatedUserId = UserId;

                _dbContext.Regulations.Update(currentRegulation);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
