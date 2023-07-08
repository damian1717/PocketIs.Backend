using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task AddAsync(RefreshToken token) => await _refreshTokenRepository.AddAsync(token);

        public async Task<RefreshToken> GetAsync(string token) => await _refreshTokenRepository.GetAsync(token);

        public async Task UpdateAsync(RefreshToken token) => await _refreshTokenRepository.UpdateAsync(token);
    }
}
