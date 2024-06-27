using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task AddDeviceAsync(Device device)
            => await _deviceRepository.AddDeviceAsync(device);

        public async Task<List<DeviceInfo>> GetDevicesAsync()
        {
            var deviceInfos = new List<DeviceInfo>();
            var devices = await _deviceRepository.GetDevicesAsync();

            foreach(var device in devices)
            {
                var dev = new DeviceInfo
                {
                    Id = device.Id,
                    Name = device.Name,
                    Number = device.Number,
                    FirstOverviewDate = device.FirstOverviewDate,
                    NextOverviewDate = device.NextOverviewDate,
                    DeviceType = device.DeviceType
                };

                if (device.NextOverviewDate < DateTime.Now)
                {
                    dev.CssClass = "after_term";
                }
                else if (device.NextOverviewDate.AddMonths(-1) < DateTime.Now)
                {
                    dev.CssClass = "before_term"; 
                }
                
                deviceInfos.Add(dev);
            }

            return deviceInfos;
        }
            

        public async Task<Device> GetDeviceByIdAsync(Guid id)
            => await _deviceRepository.GetDeviceByIdAsync(id);

        public async Task UpdateDeviceAsync(Device device)
            => await _deviceRepository.UpdateDeviceAsync(device);

        public async Task DeleteDeviceAsync(Guid id)
            => await _deviceRepository.DeleteDeviceAsync(id);
    }
}
