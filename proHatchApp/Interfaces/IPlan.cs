using proHatchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Interfaces
{
    public interface IPlan
    {
        DailySetpoints getDailySetpoints(int currentDay);

        DailySetpoints getNextDailySetpoints(int currentDay);

        PlanInfo getPlanInfo();


    }
}
