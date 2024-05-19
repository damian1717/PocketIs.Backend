using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IOrganizationalContextRepository
    {
        Task AddOrganizationalContextAsync(OrganizationalContext organizationalContext);
        Task UpdateOrganizationalContextAsync(OrganizationalContext organizationalContext);
        Task<OrganizationalContext> GetOrganizationalContextByUserIdAsync(Guid userId);
    }
}
