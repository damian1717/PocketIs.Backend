using Microsoft.AspNetCore.Identity;
using PocketIS.Domain;
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

            return services;
        }
    }
}
