using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task AddDeviceAsync(Device device);
        Task<List<Device>> GetDevicesAsync();
        Task<Device> GetDeviceByIdAsync(Guid id);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Guid id);
    }
}
