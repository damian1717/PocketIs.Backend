using Microsoft.EntityFrameworkCore;
using PocketIS.Domain;
using PocketIS.Domain.EmployeeTrainingModel;

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
        DbSet<DefinitionOfProcess> DefinitionOfProcesses { get; set; }
        DbSet<Training> Trainings { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        DbSet<SubProcess> SubProcesses { get; set; }
        DbSet<OrganizationalContext> OrganizationalContexts { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<RiskAnalys> RiskAnalyses { get; set; }
        Task<int> SaveChangesAsync();
    }
}
