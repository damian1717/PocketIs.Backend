using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class ClaimsProviderService : IClaimsProviderService
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
        {
            // claims collection
            // return await Task.FromResult(new Dictionary<string, string>
            // {
            //     ["custom_claim_1"] = "value1, value2, value3",
            //     ["custom_claim_2"] = "value1, value2, value3",
            // });
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}
