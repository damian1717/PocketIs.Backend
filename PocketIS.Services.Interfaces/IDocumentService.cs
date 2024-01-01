using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<int> SaveDocumentAsync(Document document);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<List<Document>> GetAllDocumentsByCodeAsync(string code);
        Task<Document> GetDocumnetAsync(Guid id);
    }
}
