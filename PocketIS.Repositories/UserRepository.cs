using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public UserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
