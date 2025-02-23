using Microsoft.AspNetCore.Identity;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Infrastucture.Persistence;
using PocketIS.Repositories;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services;
using PocketIS.Services.Interfaces;

namespace PocketIS.Framwork
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IQualityPoliciesService, QualityPoliciesService>();
            services.AddTransient<IRegulationService, RegulationService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IClaimsProviderService, ClaimsProviderService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IRenderRazorToStringService, RenderRazorToStringService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IOrganizationChartService, OrganizationChartService>();
            services.AddTransient<ISavedQualityPoliciesService, SavedQualityPoliciesService>();
            services.AddTransient<IProcessService, ProcessService>();
            services.AddTransient<IDefinitionOfProcessService, DefinitionOfProcessService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeTrainingService, EmployeeTrainingService>();
            services.AddTransient<ISubProcessService, SubProcessService>();
            services.AddTransient<IOrganizationalContextService, OrganizationalContextService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IRiskAnalysisService, RiskAnalysisService>();
            services.AddTransient<IComplaintService, ComplaintService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IAuditService, AuditService>();
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IQualityPoliciesRepository, QualityPoliciesRepository>();
            services.AddTransient<IRegulationRepository, RegulationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IOrganizationChartRepository, OrganizationChartRepository>();
            services.AddTransient<ISavedQualityPoliciesRepository, SavedQualityPoliciesRepository>();
            services.AddTransient<IProcessRepository, ProcessRepository>();
            services.AddTransient<IDefinitionOfProcessRepository, DefinitionOfProcessRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeTrainingRepository, EmployeeTrainingRepository>();
            services.AddTransient<ISubProcessRepository, SubProcessRepository>();
            services.AddTransient<IOrganizationalContextRepository, OrganizationalContextRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IRiskAnalysisRepository, RiskAnalysisRepository>();
            services.AddTransient<IComplaintRepository, ComplaintRepository>();
            services.AddTransient<IAuditRepository, AuditRepository>();

            return services;
        }
    }
}
