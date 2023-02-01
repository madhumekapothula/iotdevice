using System;
using Microsoft.Azure.Devices.Shared;
using PropertiesDto;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace DotNetIot.Repository.IotDeviceProperties
{
    public class IotDeviceProperties
    {
        private static string connectionString="HostName=madhu.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=a+6wPM2s63Ya0/QjdKXzaOXdbCbPgmpL0MgFy/aRyuM=";
        public static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(connectionString);
        public static DeviceClient client=null;
        public static string myDeviceConnection="HostName=madhu.azure-devices.net;DeviceId=mu;SharedAccessKey=c67u8I0pTnJPwy5XFwKigYk1kqswWU6lYk0F9wY7+KI=";
        public static async Task AddReportedPropertiesAsync(string deviceName,ReportedProperties properties)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Valid device name please");
            }
            else
            {
                client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties,connectivity;
                reportedProperties=new TwinCollection();
                connectivity=new TwinCollection();
                connectivity["type"]="cellular";
                reportedProperties["connectivity"]="connectivity";
                reportedProperties["temperature"]=properties.temperature;
                reportedProperties["pressure"]=properties.pressure;
                reportedProperties["drift"]=properties.drift;
                reportedProperties["accuracy"]=properties.accuracy;
                reportedProperties["supplyVoltageLevel"]=properties.supplyVoltageLevel;
                reportedProperties["fullScale"]=properties.fullScale;
                reportedProperties["frequency"]=properties.frequency;
                reportedProperties["resolution"]=properties.resolution;
                reportedProperties["sensorType"]=properties.sensorType;
                reportedProperties["dateTimeLastAppLaunch"]=properties.dateTimeLastAppLaunch;
                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;
            
            }
        }
        public static async Task DesiredPropertiesUpdate(string deviceName)
        {
            client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredproperties, telemetryconfig;
            desiredproperties=new TwinCollection();
            telemetryconfig=new TwinCollection();
            telemetryconfig["frequency"]="5Hz";
            desiredproperties["telemetryconfig"]=telemetryconfig;
            device.Properties.Desired["telemetryconfig"]=telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);
            return;
        }
        public static async Task<Twin> GetDevicePropertiesAsync(string deviceName)
        {
            var device=await registryManager.GetTwinAsync(deviceName);
            return device;
        }
        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Valid device name please");
            }
            else
            {
                var twin=await registryManager.GetTwinAsync(deviceName);
                var patchData=
                @"{
                    tags:{
                        location:{
                            region:'Canada',
                            plant:'IOTPro'
                            }
                        }
                    }";
                client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity=new TwinCollection();
                connectivity["type"]="cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId,patchData,twin.ETag);
                return;
            }
        }
    }
}