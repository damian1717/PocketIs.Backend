using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<List<User>> GetAllUsersAsync(Guid companyId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
