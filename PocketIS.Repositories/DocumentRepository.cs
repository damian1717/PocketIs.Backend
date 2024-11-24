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
    public class DocumentRepository : BaseRepository, IDocumentRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public DocumentRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Document>> GetAllDocumentsAsync() 
            => await _dbContext.Documents
                    .Where(x => x.CompanyId == CompanyId)
                    .ToListAsync();

        public async Task<List<Document>> GetAllDocumentsByCodeAsync(string code) 
            => await _dbContext.Documents
                .Where(x => x.Code == code 
                && x.CompanyId == CompanyId)
                .OrderByDescending(x => x.InsertedDate)
                .ToListAsync();

        public async Task<List<Document>> GetAllDocumentsByCodesAsync(List<string> codes)
            => await _dbContext.Documents
                .Where(x => codes.Contains(x.Code)
                && x.CompanyId == CompanyId)
                .ToListAsync();

        public async Task<List<Document>> GetAllDocumentsByCodeAndUserIdAsync(string code, Guid userId)
            => await _dbContext.Documents
                .Where(x => x.Code == code
                && x.CompanyId == CompanyId
                && x.InsertedUserId == userId)
                .OrderByDescending(x => x.InsertedDate)
                .ToListAsync();
        public async Task<Document> GetDocumnetAsync(Guid id)
            => await _dbContext.Documents
                    .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> SaveDocumentAsync(Document document)
        {
            document.InsertedDate = DateTime.Now;
            document.InsertedUserId = UserId;
            document.UpdatedDate = DateTime.Now;
            document.UpdatedUserId = UserId;

            await _dbContext.Documents.AddAsync(document);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var document = new Document()
            {
                Id = id
            };

            _dbContext.Documents.Remove(document);
            await _dbContext.SaveChangesAsync();
        }
    }
}
