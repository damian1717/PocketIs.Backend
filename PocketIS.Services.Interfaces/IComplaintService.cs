using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<List<ComplaintInfo>> GetComplaintsAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<Complaint> GetComplaintByIdAsync(Guid id);
        Task AddComplaintAsync(Complaint complaint);
        Task UpdateComplaintAsync(Complaint complaint);
        Task DeleteComplaintAsync(Guid id);
    }
}
