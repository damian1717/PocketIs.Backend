using PocketIS.Common;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class ClaimsProviderService : IClaimsProviderService
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId, Guid companyId)
        {
             return await Task.FromResult(new Dictionary<string, string>
             {
                 [Constants.CompanyId] = companyId.ToString()
             });
        }
    }
}
