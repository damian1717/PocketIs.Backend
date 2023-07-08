using PocketIS.Domain;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
