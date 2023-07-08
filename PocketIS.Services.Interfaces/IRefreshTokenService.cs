using PocketIS.Domain;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
