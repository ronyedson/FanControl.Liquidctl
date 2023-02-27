using FanControl.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanControl.Liquidctl
{
    public class NzxtFanControllerDevice
    {
        public FanSpeed Speed;

        public FanDuty Duty;

        public NzxtFanControllerDevice(LiquidctlStatusJSON output, int fanNumber)
        {
            Speed = new FanSpeed(output, fanNumber);
            Duty = new FanDuty(output, fanNumber);
        }

        public void UpdateFromJSON(LiquidctlStatusJSON output)
        {
            Speed.UpdateFromJSON(output);
            Duty.UpdateFromJSON(output);
        }
    }

    public class FanSpeed : IPluginSensor
    {
        private readonly string _id;
        private readonly string _name;
        private float? _value;
        private readonly int _fanNumber;

        public FanSpeed(LiquidctlStatusJSON output, int fanNumber)
        {
            _id = $"{output.address.ToLower()}-fan{fanNumber}speed";
            _name = $"Fan {fanNumber} - {output.description}";
            _fanNumber = fanNumber;
            UpdateFromJSON(output);
        }

        public string Id => _id;

        public string Name => _name;

        public float? Value => _value;

        public void UpdateFromJSON(LiquidctlStatusJSON output)
        {
            _value = null;
            if (float.TryParse(output.status.Single(entry => entry.key == $"Fan {_fanNumber} speed").value, out var result))
            {
                _value = result;
            }
        }

        public void Update()
        {
        }
    }

    public class FanDuty : IPluginControlSensor
    {
        private readonly string _id;
        private readonly string _name;
        private float? _value;
        private readonly int _fanNumber;
        private string _address;

        public string Id => _id;
        public string Name => _name;
        public float? Value => _value;

        public FanDuty(LiquidctlStatusJSON output, int fanNumber)
        {
            _address = output.address;
            _id = $"{output.address.ToLower()}-fan{fanNumber}duty";
            _name = $"Fan {fanNumber} - {output.description}";
            _fanNumber = fanNumber;
            UpdateFromJSON(output);
        }

        public void Reset()
        {
            Set(40);
        }

        public void Set(float val)
        {
            LiquidctlCLIWrapper.SetFanSpeed(_address, _fanNumber, (int)val);
        }

        public void Update()
        {
        }

        public void UpdateFromJSON(LiquidctlStatusJSON output)
        {
            _value = null;
            if (float.TryParse(output.status.Single(entry => entry.key == $"Fan {_fanNumber} duty").value, out var result))
            {
                _value = result;
            }
        }
    }
}
