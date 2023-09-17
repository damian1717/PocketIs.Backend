using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public DocumentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Document>> GetAllDocumentsAsync(Guid companyId) => await _dbContext.Documents.Where(x => x.CompanyId == companyId).ToListAsync();

        public async Task<List<Document>> GetAllDocumentsByCodeAsync(string code, Guid companyId) =>
            await _dbContext.Documents.Where(x => x.Code == code && x.CompanyId == companyId).OrderByDescending(x => x.InsertedDate).ToListAsync();
        public async Task<Document> GetDocumnetAsync(Guid id) => await _dbContext.Documents.FindAsync(id);

        public async Task<int> SaveDocumentAsync(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
