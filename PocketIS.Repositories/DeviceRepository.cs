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
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public DeviceRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddDeviceAsync(Device device)
        {
            device.InsertedDate = DateTime.Now;
            device.InsertedUserId = UserId;
            device.UpdatedDate = DateTime.Now;
            device.UpdatedUserId = UserId;

            await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(Guid id)
        {
            var device = new Device()
            {
                Id = id
            };

            _dbContext.Devices.Remove(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(Guid id)
            => await _dbContext.Devices
                    .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Device>> GetDevicesAsync()
            => await _dbContext.Devices
                    .Where(x => x.CompanyId == CompanyId)
                    .ToListAsync();

        public async Task UpdateDeviceAsync(Device device)
        {
            var currentDevice = await _dbContext.Devices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == device.Id);

            if (currentDevice is not null)
            {
                currentDevice.Name = device.Name;
                currentDevice.Number = device.Number;
                currentDevice.DeviceType = device.DeviceType;
                currentDevice.FirstOverviewDate = device.FirstOverviewDate;
                currentDevice.NextOverviewDate = device.NextOverviewDate;
                currentDevice.UpdatedDate = DateTime.Now;
                currentDevice.UpdatedUserId = UserId;

                _dbContext.Devices.Update(currentDevice);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
