﻿using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IOrganizationChartRepository
    {
        Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson);
        Task<List<OrganizationChartPerson>> GetListOfPersonsAsync();
        Task<OrganizationChartPerson> GetPersonAsync(Guid id);
        Task DeleteOrganizationChartPersonAsync(Guid id);
        Task DeleteOrganizationChartPersonAndBelowPersonsAsync(List<OrganizationChartPerson> organizationCharts);
        Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id);
        Task<int> GetMaxLevelAsync();
    }
}
