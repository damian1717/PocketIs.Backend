using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IClaimsProviderService
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId, Guid companyId);
    }
}
