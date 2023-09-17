using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<int> SaveDocumentAsync(Document document);
        Task<List<Document>> GetAllDocumentsAsync(Guid companyId);
        Task<List<Document>> GetAllDocumentsByCodeAsync(string code, Guid companyId);
        Task<Document> GetDocumnetAsync(Guid id);
    }
}
