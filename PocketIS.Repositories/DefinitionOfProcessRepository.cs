﻿using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class DefinitionOfProcessRepository : BaseRepository, IDefinitionOfProcessRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public DefinitionOfProcessRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess)
        {
            definitionProcess.InsertedDate = DateTime.Now;
            definitionProcess.InsertedUserId = UserId;
            definitionProcess.UpdatedDate = DateTime.Now;
            definitionProcess.UpdatedUserId = UserId;

            await _dbContext.DefinitionOfProcesses.AddAsync(definitionProcess);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DefinitionOfProcess>> GetBaseDefinitionOfProcessAsync(Guid processId)
            => await _dbContext.DefinitionOfProcesses
            .Where(x => x.ProcessId == processId
            && x.IsBase)
            .ToListAsync();

        public async Task<List<DefinitionOfProcess>> GetDefinitionOfProcessAsync(Guid processId)
            => await _dbContext.DefinitionOfProcesses
            .Where(x => x.ProcessId == processId)
            .ToListAsync();

        public async Task<DefinitionOfProcess> GetDefinitionOfProcessByIdAsync(Guid id) 
            => await _dbContext.DefinitionOfProcesses
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess)
        {
            var currentDefinitionProcess = _dbContext.DefinitionOfProcesses.Find(definitionProcess.Id);

            if (currentDefinitionProcess is not null)
            {
                currentDefinitionProcess.Name = definitionProcess.Name;
                currentDefinitionProcess.UpdatedDate = DateTime.Now;
                currentDefinitionProcess.UpdatedUserId = UserId;

                _dbContext.DefinitionOfProcesses.Update(currentDefinitionProcess);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteDefinitionOfProcessAsync(Guid id)
        {
            var definitionOfProcess = new DefinitionOfProcess()
            {
                Id = id
            };

            _dbContext.DefinitionOfProcesses.Remove(definitionOfProcess);
            await _dbContext.SaveChangesAsync();
        }
    }
}
