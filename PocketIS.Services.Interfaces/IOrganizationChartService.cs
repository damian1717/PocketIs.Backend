using PocketIS.Domain;
using PocketIS.Domain.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IOrganizationChartService
    {
        Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task<List<OrganizationChartPersonModel>> GetListOfPersonsAsync();
        Task<OrganizationChartPerson> GetListOfPersonAsync(Guid id);
        Task DeleteOrganizationChartPersonAsync(Guid id);
        Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id);
    }
}
