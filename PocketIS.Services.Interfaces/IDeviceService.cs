using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IDeviceService
    {
        Task AddDeviceAsync(Device device);
        Task<List<DeviceInfo>> GetDevicesAsync();
        Task<Device> GetDeviceByIdAsync(Guid id);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Guid id);
    }
}
