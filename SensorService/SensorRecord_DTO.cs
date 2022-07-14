using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorService
{
    public class SensorRecord_DTO
    {

        public string Id { get; set; }
        public string UnitId { get; set; }
        public string TimeStamp { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int PPM { get; set; }

    }
}
