using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Domain.EmployeeTrainingModel;

namespace PocketIS.Infrastucture.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<QualityPolicy> QualityPolicies { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OrganizationChartPerson> OrganizationChartPersons { get; set; }
        public DbSet<SavedQualityPolicy> SavedQualityPolicies { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<DefinitionOfProcess> DefinitionOfProcesses { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        public DbSet<SubProcess> SubProcesses { get; set; }
        public DbSet<OrganizationalContext> OrganizationalContexts { get; set; }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
