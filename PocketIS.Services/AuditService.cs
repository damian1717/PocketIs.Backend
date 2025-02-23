using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        public async Task AddAuditAsync(Audit audit) => await _auditRepository.AddAuditAsync(audit);

        public async Task DeleteAuditAsync(Guid id) => await _auditRepository.DeleteAuditAsync(id);

        public async Task<Audit> GetAuditByIdAsync(Guid id) => await _auditRepository.GetAuditByIdAsync(id);

        public async Task<List<Audit>> GetAuditsAsync() => await _auditRepository.GetAuditsAsync();

        public async Task UpdateAuditAsync(Audit audit) => await _auditRepository.UpdateAuditAsync(audit);
    }
}
