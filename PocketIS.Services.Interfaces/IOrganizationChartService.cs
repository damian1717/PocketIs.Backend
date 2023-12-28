using PocketIS.Domain;
using PocketIS.Domain.BusinessEntity;
using PocketIS.Domain.ChartOrg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IOrganizationChartService
    {
        Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task<List<OrganizationChartPersonModel>> GetListOfPersonsAsync(Guid companyId);
        Task<OrganizationChartPerson> GetPersonAsync(Guid id);
        Task DeleteOrganizationChartPersonAsync(Guid id);
        Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id);
        string GenerateOrganizationChartImage64String(ChartNode[] chartNodes);
        Task<int> GetMaxLevelAsync(Guid companyId);
    }
}
