using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class OrganizationalContextService : IOrganizationalContextService
    {
        private readonly IOrganizationalContextRepository _organizationalContextRepository;

        public OrganizationalContextService(IOrganizationalContextRepository organizationalContextRepository)
        {
            _organizationalContextRepository = organizationalContextRepository;
        }
        public async Task AddOrganizationalContextAsync(OrganizationalContext organizationalContext) 
            => await _organizationalContextRepository.AddOrganizationalContextAsync(organizationalContext);

        public async Task<OrganizationalContext> GetOrganizationalContextByUserIdAsync(Guid userId) 
            => await _organizationalContextRepository.GetOrganizationalContextByUserIdAsync(userId);

        public async Task UpdateOrganizationalContextAsync(OrganizationalContext organizationalContext)
         => await _organizationalContextRepository.UpdateOrganizationalContextAsync(organizationalContext);
    }
}
