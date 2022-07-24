using proHatchApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Models
{
    public class PlanTurn
    {
        public string Id { get; set; }
        public string PlanId { get; set; }
        public int Order { get; set; }
        public int Days { get; set; }
        public int Set { get; set; }
    }
}
