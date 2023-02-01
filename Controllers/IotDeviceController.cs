using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using IotDevice.Repository;
namespace IotDevice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IotDeviceController:ControllerBase
    {
         [HttpPost("AddIotDevice")]
    public async Task<string> AddDevice(string deviceName)
    {
        await IotDevice.Repository.IotDevice.AddDeviceAsync(deviceName);
        return null;
    }
    [HttpGet("GetIotDevice")]
    public async Task<Device> GetIotDevice(string deviceName)
    {
        Device device;
        device=await IotDevice.Repository.IotDevice.GetDeviceAsync(deviceName);
        return device;
    }
    [HttpDelete("DeleteIotDevice")]
    public async Task<string> DeleteIotDevice(string deviceName)
    {
        await IotDevice.Repository.IotDevice.DeleteDeviceAsync(deviceName);
        return null;
    }
    [HttpPut("UpdatedIotDevice")]
    public async Task<Device> UpdateDeviceProperties(string deviceName)
    {
        Device device;
        device=await IotDevice.Repository.IotDevice.UpdateDeviceAsync(deviceName);
        return device;
    }
    }
}