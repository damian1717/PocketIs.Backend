using PocketIS.Application.Common.Codes;
using PocketIS.Application.Common.Types;
using PocketIS.Domain;
using PocketIS.Domain.BusinessEntity;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class OrganizationChartService : IOrganizationChartService
    {
        private readonly IOrganizationChartRepository _organizationChartRepository;
        public OrganizationChartService(IOrganizationChartRepository organizationChartRepository)
        {
            _organizationChartRepository = organizationChartRepository;
        }

        public async Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson) => await _organizationChartRepository.AddOrganizationChartPersonAsync(organizationChartPerson);

        public async Task<OrganizationChartPerson> GetListOfPersonAsync(Guid id) => await _organizationChartRepository.GetListOfPersonAsync(id);

        public async Task<List<OrganizationChartPersonModel>> GetListOfPersonsAsync()
        {
            var newList = new List<OrganizationChartPersonModel>();
            var organizationChartPersons = await _organizationChartRepository.GetListOfPersonsAsync();

            var namesOfPersons = organizationChartPersons.ToDictionary(x => x.Id, z => $"{z.Name} {z.LastName}");

            foreach (var person in organizationChartPersons)
            {
                var belowPersonName = string.Empty;
                
                if (person.BelowPersonId is not null)
                {
                    var guid = (Guid)person.BelowPersonId;
                    belowPersonName = namesOfPersons.ContainsKey(guid) ? namesOfPersons[guid] : string.Empty;
                }

                var item = new OrganizationChartPersonModel
                {
                    Id = person.Id,
                    Name = person.Name,
                    BelowPersonId = person.BelowPersonId,
                    LastName = person.LastName,
                    Position = person.Position,
                    BelowPersonName = belowPersonName,
                    Level = person.Level
                };
                newList.Add(item);
            }

            return newList;
        }

        public async Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson) => await _organizationChartRepository.UpdateOrganizationChartPersonAsync(organizationChartPerson);

        public async Task DeleteOrganizationChartPersonAsync(Guid id)
        {
            var allPersonsBelow = await GetListOfPersonsBelowIdAsync(id);

            if (allPersonsBelow is not null && allPersonsBelow.Any())
            {
                throw new PocketISException(OrganizationChartCodes.BelowPersonExists,
                    "Nie można usunąc danej osoby. Dana osoba jest w osobach poniżej.");
            }
            await _organizationChartRepository.DeleteOrganizationChartPersonAsync(id);
        }

        public async Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id) => await _organizationChartRepository.GetListOfPersonsBelowIdAsync(id);
    }
}
