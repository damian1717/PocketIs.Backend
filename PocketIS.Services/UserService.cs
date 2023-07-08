using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
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
        public async Task AddAsync(User user) => await _userRepository.AddAsync(user);

        public async Task<User> GetAsync(Guid id) => await _userRepository.GetAsync(id);

        public async Task<User> GetAsync(string email) => await _userRepository.GetAsync(email);

        public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
    }
}
