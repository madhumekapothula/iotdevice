using System;
using DotNetIot.Repository.IotDeviceProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Shared;
using PropertiesDto;

namespace MyCoreApi.IotDeviceController;

[ApiController]
[Route("[controller]")]
public class IotDevicePropertiesController:ControllerBase
{
   [HttpPut("UpdateDeviceReportedProperties")]
   
    public async Task<string> UpdateDeviceReportedProperties(string deviceName,ReportedProperties properties)
    {
        await IotDeviceProperties.AddReportedPropertiesAsync(deviceName,properties);
        return null;
    }
   [HttpPut("UpdateDeviceDesiredProperties")]
   public async Task<string> UpdateDeviceDesiredProperties(string deviceName)
   {
     await IotDeviceProperties.DesiredPropertiesUpdate(deviceName);
     return null;
   }
   [HttpPut("UpdateIotDeviceTagProperties")]
   public async Task<string> UpdateIotDeviceTagProperties(string deviceName)
   {
     await IotDeviceProperties.UpdateDeviceTagProperties(deviceName);
     return null;
   }
   [HttpGet("GetIotDeviceProperties")]
   public async Task<Twin> GetIotDevice(string deviceName)
   {
     var device=await IotDeviceProperties.GetDevicePropertiesAsync(deviceName);
     return device;
   }
}