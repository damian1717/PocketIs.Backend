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
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<OrganizationChartPerson> OrganizationChartPersons { get; set; }
        DbSet<SavedQualityPolicy> SavedQualityPolicies { get; set; }
        DbSet<Process> Processes { get; set; }
        Task<int> SaveChangesAsync();
    }
}
