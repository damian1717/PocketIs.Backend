﻿using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task<List<Audit>> GetAuditsAsync();
        Task<Audit> GetAuditByIdAsync(Guid id);
        Task AddAuditAsync(Audit audit);
        Task UpdateAuditAsync(Audit audit);
        Task DeleteAuditAsync(Guid id);
    }
}
