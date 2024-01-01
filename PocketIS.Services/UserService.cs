using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User user) 
            => await _userRepository.AddAsync(user);

        public async Task<List<UserInfo>> GetAllUsersAsync(bool isSuperAdmin)
        {
            var newUsers = new List<UserInfo>();

            List<User> users = null;

            if (isSuperAdmin)
            {
                users = await _userRepository.GetAllUsersAsync();
            }
            else
            {
                users = await _userRepository.GetAllUsersForCompanyAsync();
            }
                        
            if (users is null || users.Count == 0) return newUsers;

            foreach (var user in users)
            {
                var newUser = new UserInfo
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };

                newUsers.Add(newUser);
            }

            return newUsers;
        }

        public async Task<UserInfo> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user is null) return null;

            return new UserInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CompanyId = user.CompanyId,
                CompanyName = user.Company?.Name ?? string.Empty
            };
        }

        public async Task<UserInfo> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            if (user is null) return null;

            return new UserInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CompanyId = user.CompanyId
            };
        }

        public async Task UpdateAsync(User user) 
            => await _userRepository.UpdateAsync(user);

        public async Task DeleteAsync(Guid id) 
            => await _userRepository.DeleteAsync(id);

        public async Task<User> GetUserForIdentityAsync(Guid id) 
            => await _userRepository.GetAsync(id);

        public async Task<User> GetUserForIdentityAsync(string email) 
            => await _userRepository.GetAsync(email);
    }
}
