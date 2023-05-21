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
            services.AddScoped<IRenderRazorToStringService, RenderRazorToStringService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IQualityPoliciesRepository, QualityPoliciesRepository>();
            services.AddTransient<IRegulationRepository, RegulationRepository>();

            return services;
        }
    }
}
