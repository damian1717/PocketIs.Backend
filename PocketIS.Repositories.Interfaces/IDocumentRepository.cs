using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<int> SaveDocumentAsync(Document document);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<List<Document>> GetAllDocumentsByCodeAsync(string code);
        Task<Document> GetDocumnetAsync(Guid id);
    }
}
