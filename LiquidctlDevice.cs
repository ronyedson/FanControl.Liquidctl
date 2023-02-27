using System;
using System.Linq;
using FanControl.Plugins;

namespace FanControl.Liquidctl
{
    internal class LiquidctlDevice
    {
        public class LiquidTemperature : IPluginSensor
        {
            public LiquidTemperature(LiquidctlStatusJSON output)
            {
                _id = $"{output.address.ToLower()}-liqtmp";
                _name = $"Liquid Temp. - {output.description}";
                UpdateFromJSON(output);
            }
            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = null;

                if(float.TryParse(output.status.Single(entry => entry.key == "Liquid temperature").value, out var result)){
                    _value = result;
                }
            }
            public string Id => _id;
            string _id;

            public string Name => _name;
            string _name;

            public float? Value => _value;
            float? _value;

            public void Update()
            { } // plugin updates sensors
        }
        public class PumpSpeed : IPluginSensor
        {
            public PumpSpeed(LiquidctlStatusJSON output)
            {
                _id = $"{output.address.ToLower()}-pumprpm";
                _name = $"Pump - {output.description}";
                UpdateFromJSON(output);
            }
            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = null; 
                if(float.TryParse(output.status.Single(entry => entry.key == "Pump speed").value, out var result)){
                    _value = result;
                }
            }
            public string Id => _id;
            readonly string _id;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float? _value;

            public void Update()
            { } // plugin updates sensors
        }
        public class PumpDuty : IPluginControlSensor
        {
            public PumpDuty(LiquidctlStatusJSON output)
            {
                _address = output.address;
                _id = $"{_address.ToLower()}-pumpduty";
                _name = $"Pump Control - {output.description}";
                UpdateFromJSON(output);
            }
            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = null;

                if(float.TryParse(output.status.Single(entry => entry.key == "Pump duty").value, out var result)){
                    _value = result;
                }
            }
            public string Id => _id;
            string _id;
            string _address;

            public string Name => _name;
            string _name;

            public float? Value => _value;
            float? _value;

            public void Reset()
            {
                Set(100.0f);
            }

            public void Set(float val)
            {
                LiquidctlCLIWrapper.SetPump(_address, (int) val);
            }

            public void Update()
            { } // plugin updates sensors

        }
        public LiquidctlDevice(LiquidctlStatusJSON output)
        {
            address = output.address;

            hasFan1 = output.status.Exists(entry => entry.key == "Fan 1 control mode" && !string.IsNullOrEmpty(entry.value));
            if (hasFan1)
                Fan1 = new NzxtFanControllerDevice(output, 1);

            hasFan2 = output.status.Exists(entry => entry.key == "Fan 2 control mode" && !string.IsNullOrEmpty(entry.value));
            if (hasFan2)
                Fan2 = new NzxtFanControllerDevice(output, 2);

            hasFan3 = output.status.Exists(entry => entry.key == "Fan 3 control mode" && !string.IsNullOrEmpty(entry.value));
            if (hasFan3)
                Fan3 = new NzxtFanControllerDevice(output, 3);

            hasPumpSpeed = output.status.Exists(entry => entry.key == "Pump speed" && !string.IsNullOrEmpty(entry.value));
            if (hasPumpSpeed)
                pumpSpeed = new PumpSpeed(output);

            hasPumpDuty = output.status.Exists(entry => entry.key == "Pump duty" && !string.IsNullOrEmpty(entry.value));
            if (hasPumpDuty)
                pumpDuty = new PumpDuty(output);

            hasLiquidTemperature = output.status.Exists(entry => entry.key == "Liquid temperature" && !string.IsNullOrEmpty(entry.value));
            if (hasLiquidTemperature)
                liquidTemperature = new LiquidTemperature(output);
        }

        public readonly bool hasPumpSpeed, hasPumpDuty, hasLiquidTemperature, hasFan1, hasFan2, hasFan3;

        public void UpdateFromJSON(LiquidctlStatusJSON output)
        {
            if (hasFan1) Fan1.UpdateFromJSON(output);
            if (hasFan2) Fan2.UpdateFromJSON(output);
            if (hasFan3) Fan3.UpdateFromJSON(output);
            if (hasLiquidTemperature) liquidTemperature.UpdateFromJSON(output);
            if (hasPumpSpeed) pumpSpeed.UpdateFromJSON(output);
            if (hasPumpDuty) pumpDuty.UpdateFromJSON(output);
        }

        public string address;
        public LiquidTemperature liquidTemperature;
        public PumpSpeed pumpSpeed;
        public PumpDuty pumpDuty;
        public NzxtFanControllerDevice Fan1;
        public NzxtFanControllerDevice Fan2;
        public NzxtFanControllerDevice Fan3;

        public void LoadJSON()
        {
            try
            {
                LiquidctlStatusJSON output = LiquidctlCLIWrapper.ReadStatus(address).First();
                UpdateFromJSON(output);
            }
            catch (InvalidOperationException)
            {
                throw new Exception($"Device {address} not showing up");
            }
        }
    }
}
