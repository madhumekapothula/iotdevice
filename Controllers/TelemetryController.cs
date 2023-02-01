using System;
using Microsoft.AspNetCore.Mvc;
using DotNetIot.Repository.SendTelemetryMessages;
namespace MyCoreApi.IotDeviceController;

[ApiController]
[Route("[controller]")]

public class TelemetryController:ControllerBase
{
    [HttpPost("SendTelemeteryMessage")]
    public async Task<string> SendMessage(string deviceName)
    {
        await SendTelemetryMessages.SendMessage(deviceName);
        return null;
    }
}
