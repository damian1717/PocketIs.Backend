﻿using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IDefinitionOfProcessService
    {
        Task AddDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess);
        Task UpdateDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess);
        Task<List<DefinitionOfProcess>> GetBaseDefinitionOfProcessAsync(Guid processId);
        Task<List<DefinitionOfProcess>> GetDefinitionOfProcessAsync(Guid processId);
        Task<DefinitionOfProcess> GetDefinitionOfProcessByIdAsync(Guid id);
        Task DeleteDefinitionOfProcessAsync(Guid id);
        string GenerateDefinitionOfProcessImage64String(string processName);
    }
}
