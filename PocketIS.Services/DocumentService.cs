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

        public async Task<List<Document>> GetAllDocumentsAsync() => await _repository.GetAllDocumentsAsync();

        public async Task<List<Document>> GetAllDocumentsByCodeAsync(string code) => await _repository.GetAllDocumentsByCodeAsync(code);

        public async Task<Document> GetDocumnetAsync(Guid id) => await _repository.GetDocumnetAsync(id);

        public async Task<int> SaveDocumentAsync(Document document) => await _repository.SaveDocumentAsync(document);
    }
}
