using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IOrganizationalContextService
    {
        Task AddOrganizationalContextAsync(OrganizationalContext organizationalContext);
        Task UpdateOrganizationalContextAsync(OrganizationalContext organizationalContext);
        Task<OrganizationalContext> GetOrganizationalContextByUserIdAsync(Guid userId);
    }
}
