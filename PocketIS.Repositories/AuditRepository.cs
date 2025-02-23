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
    public class AuditRepository : BaseRepository, IAuditRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public AuditRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddAuditAsync(Audit audit)
        {
            audit.InsertedDate = DateTime.Now;
            audit.InsertedUserId = UserId;
            audit.UpdatedDate = DateTime.Now;
            audit.UpdatedUserId = UserId;

            await _dbContext.Audits.AddAsync(audit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuditAsync(Guid id)
        {
            var audit = new Audit()
            {
                Id = id
            };

            _dbContext.Audits.Remove(audit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Audit> GetAuditByIdAsync(Guid id)
            => await _dbContext.Audits.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Audit>> GetAuditsAsync()
            => await _dbContext.Audits.Where(x => x.CompanyId == CompanyId).ToListAsync();

        public async Task UpdateAuditAsync(Audit audit)
        {
            var currentAudit = await _dbContext.Audits.AsNoTracking().FirstOrDefaultAsync(x => x.Id == audit.Id);

            if (currentAudit is not null)
            {
                currentAudit.Name = audit.Name;
                currentAudit.Type = audit.Type;
                currentAudit.Status = audit.Status;
                currentAudit.AuditDate = audit.AuditDate;
                currentAudit.Auditor = audit.Auditor;
                currentAudit.UpdatedDate = DateTime.Now;
                currentAudit.UpdatedUserId = UserId;

                _dbContext.Audits.Update(currentAudit);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
