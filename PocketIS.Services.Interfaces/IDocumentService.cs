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
        Task<List<Document>> GetAllDocumentsByCodeAndUserIdAsync(string code, Guid userId);
        Task<Document> GetDocumnetAsync(Guid id);
    }
}
