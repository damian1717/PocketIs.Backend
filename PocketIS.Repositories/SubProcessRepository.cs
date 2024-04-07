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
    public class SubProcessRepository : BaseRepository, ISubProcessRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public SubProcessRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubProcessAsync(SubProcess subProcess)
        {
            subProcess.InsertedDate = DateTime.Now;
            subProcess.InsertedUserId = UserId;
            subProcess.UpdatedDate = DateTime.Now;
            subProcess.UpdatedUserId = UserId;

            await _dbContext.SubProcesses.AddAsync(subProcess);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSubProcessAsync(Guid id)
        {
            var subProcess = new SubProcess()
            {
                Id = id
            };

            _dbContext.SubProcesses.Remove(subProcess);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SubProcess> GetSubProcessByIdAsync(Guid id)
            => await _dbContext.SubProcesses
                .FirstOrDefaultAsync(x => x.Id == id
                && x.CompanyId == CompanyId);

        public async Task<List<SubProcess>> GetSubProcessesAsync(int subProcessType)
            => await _dbContext.SubProcesses
                .Where(x => x.CompanyId == CompanyId
                && x.SubProcessType == subProcessType)
                .ToListAsync();

        public async Task UpdateSubProcessAsync(SubProcess subProcess)
        {
            var currentSubProcess = await _dbContext.SubProcesses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == subProcess.Id);

            if (currentSubProcess is not null)
            {
                currentSubProcess.HtmlContent = subProcess.HtmlContent;
                currentSubProcess.SubProcessType = subProcess.SubProcessType;
                currentSubProcess.Description = subProcess.Description;
                currentSubProcess.IsArchive = subProcess.IsArchive;
                currentSubProcess.UpdatedDate = DateTime.Now;
                currentSubProcess.UpdatedUserId = UserId;

                _dbContext.SubProcesses.Update(currentSubProcess);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetLastVersionAsync(int subProcessType)
        {
            return await _dbContext.SubProcesses
                            .Where(x => x.CompanyId == CompanyId
                            && x.SubProcessType == subProcessType)
                            .MaxAsync(x => (int?)x.Version)
                            ?? 0;
        }
    }
}
