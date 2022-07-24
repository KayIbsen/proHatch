using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Models
{
    public class DailySetpoints
    {
        public int Day { get; set; }
        public float TemperatureSet { get; set; }
        public float TemperatureHystHighSet { get; set; }
        public float TemperatureHystLowSet { get; set; }
        public float HumiditySet { get; set; }
        public float HumidityHystHighSet { get; set; }
        public float HumidityHystLowSet { get; set; }
        public int TurnsSet { get; set; }
    }
}
