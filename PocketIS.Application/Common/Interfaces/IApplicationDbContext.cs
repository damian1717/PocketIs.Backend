using Microsoft.EntityFrameworkCore;
using PocketIS.Domain;

namespace PocketIS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Document> Documents { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<QualityPolicy> QualityPolicies { get; set; }
        DbSet<Regulation> Regulations { get; set; }
        Task<int> SaveChangesAsync();
    }
}
