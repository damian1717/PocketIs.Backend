using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string firstName, string lastName, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(string email, string currentPassword, string newPassword);
    }
}
