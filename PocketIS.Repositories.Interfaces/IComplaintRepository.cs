using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        Task<List<Complaint>> GetComplaintsAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<Complaint> GetComplaintByIdAsync(Guid id);
        Task AddComplaintAsync(Complaint complaint);
        Task UpdateComplaintAsync(Complaint complaint);
        Task DeleteComplaintAsync(Guid id);
    }
}
