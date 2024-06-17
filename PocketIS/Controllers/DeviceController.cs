using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Device;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : BaseController
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
        [Route("adddevice")]
        public async Task<IActionResult> AddDevice(AddDevice model)
        {
            var device = new Device
            {
                Name = model.Name,
                Number = model.Number,
                DeviceType = model.DeviceType,
                FirstOverviewDate = model.FirstOverviewDate,
                NextOverviewDate = model.NextOverviewDate,
                CompanyId = CompanyId
            };

            await _deviceService.AddDeviceAsync(device);
            return Ok();
        }

        [HttpPost]
        [Route("updatedevice")]
        public async Task<IActionResult> UpdateDevice(UpdateDevice model)
        {
            if (model?.Id is null) return BadRequest();

            var device = new Device
            {
                Id = model.Id,
                Name = model.Name,
                Number = model.Number,
                DeviceType = model.DeviceType,
                FirstOverviewDate = model.FirstOverviewDate,
                NextOverviewDate = model.NextOverviewDate,
                CompanyId = CompanyId
            };

            await _deviceService.UpdateDeviceAsync(device);
            return Ok();
        }

        [HttpGet]
        [Route("getdevices")]
        public async Task<IActionResult> Get()
            => Ok(await _deviceService.GetDevicesAsync());

        [HttpGet]
        [Route("getdevice/{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _deviceService.GetDeviceByIdAsync(id));

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return Ok();
        }
    }
}
