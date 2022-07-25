using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Models
{
    public class PlanInfo
    {
        public string Id { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public DateTime LaunchTime { get; set; }
        public int TotalDays { get; set; }
        public List<PlanTemperature> planTemperatures { get; set; }
        public List<PlanHumidity> planHumidities { get; set; }
        public List<PlanTurn> planTurns { get; set; }

    }
}
