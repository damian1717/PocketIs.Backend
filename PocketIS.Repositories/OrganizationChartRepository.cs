﻿using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class OrganizationChartRepository : IOrganizationChartRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public OrganizationChartRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrganizationChartPerson> GetListOfPersonAsync(Guid id) => await _dbContext.OrganizationChartPersons.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<OrganizationChartPerson>> GetListOfPersonsAsync() => await _dbContext.OrganizationChartPersons.ToListAsync();

        public async Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson)
        {
            await _dbContext.OrganizationChartPersons.AddAsync(organizationChartPerson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson)
        {
            _dbContext.OrganizationChartPersons.Update(organizationChartPerson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrganizationChartPersonAsync(Guid id)
        {
            var organizationChartPerson = new OrganizationChartPerson()
            {
                Id = id
            };

            _dbContext.OrganizationChartPersons.Remove(organizationChartPerson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id) => await _dbContext.OrganizationChartPersons.Where(x => x.BelowPersonId == id).ToListAsync();

    }
}
