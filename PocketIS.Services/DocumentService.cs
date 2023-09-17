using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Document>> GetAllDocumentsAsync(Guid companyId) => await _repository.GetAllDocumentsAsync(companyId);

        public async Task<List<Document>> GetAllDocumentsByCodeAsync(string code, Guid companyId) => await _repository.GetAllDocumentsByCodeAsync(code, companyId);

        public async Task<Document> GetDocumnetAsync(Guid id) => await _repository.GetDocumnetAsync(id);

        public async Task<int> SaveDocumentAsync(Document document) => await _repository.SaveDocumentAsync(document);
    }
}
