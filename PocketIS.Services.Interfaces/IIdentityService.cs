using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
