using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
