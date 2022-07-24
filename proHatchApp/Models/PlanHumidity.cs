using proHatchApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Models
{
    public class PlanHumidity
    {
        public string Id { get; set; }
        public string PlanId { get; set; }
        public int Order { get; set; }
        public int Days { get; set; }
        public float Set { get; set; }
        public float HystHigh { get; set; }
        public float HystLow { get; set; }
    }
}
