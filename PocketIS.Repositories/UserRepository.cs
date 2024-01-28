using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public UserRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            :base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersForCompanyAsync() 
            => await _dbContext.Users
                .Where(x => x.CompanyId == CompanyId)
                .ToListAsync();

        public async Task<List<User>> GetAllUsersAsync() 
            => await _dbContext.Users.ToListAsync();

        public async Task<User> GetAsync(Guid id) 
            => await _dbContext.Users
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email) 
            => await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);

        public async Task UpdateAsync(User user)
        {
            var currentUser = _dbContext.Users.Find(user.Id);

            if (currentUser is not null)
            {
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Email = user.Email;
                currentUser.Role = user.Role;
                currentUser.UpdatedDate = DateTime.Now;

                _dbContext.Users.Update(currentUser);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = new User(id);

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
