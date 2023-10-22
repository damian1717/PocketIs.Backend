using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserForIdentityAsync(Guid id);
        Task<UserInfo> GetAsync(Guid id);
        Task<List<UserInfo>> GetAllUsersAsync(Guid companyId, bool isSuperAdmin);
        Task<UserInfo> GetAsync(string email);
        Task<User> GetUserForIdentityAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
