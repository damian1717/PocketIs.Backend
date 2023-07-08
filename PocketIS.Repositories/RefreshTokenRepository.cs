using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public RefreshTokenRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(RefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetAsync(string token) => await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);

        public async Task UpdateAsync(RefreshToken token)
        {
            _dbContext.RefreshTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }
    }
}
