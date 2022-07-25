using proHatchApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Models
{
    public class Plan : IPlan
    {
        private PlanInfo _planInfo { get; set; }
        private List<DailySetpoints> _dailySetpoints { get; }


        public Plan(string id, int? unitId, string name, byte isActive, DateTime? launchTime,int totalDays, List<PlanTemperature> planTemperatures, List<PlanHumidity> planHumidities, List<PlanTurn> planTurns)
        {
            _planInfo = new PlanInfo()
            {
                Id = id,
                UnitId = (int)unitId,    
                Name = name,
                IsActive = isActive,
                LaunchTime = (DateTime)launchTime,
                TotalDays = totalDays,
                planTemperatures = planTemperatures,
                planHumidities = planHumidities,
                planTurns = planTurns
            };


            // Order Lists 
            IOrderedEnumerable<PlanTemperature> planTemperaturesOrdered = planTemperatures.OrderBy(x => x.Order);
            IOrderedEnumerable<PlanHumidity> planHumiditiesOrdered = planHumidities.OrderBy(x => x.Order);
            IOrderedEnumerable<PlanTurn> planTurnsOrdered = planTurns.OrderBy(x => x.Order);

            
        
            // Adding daily setpoints of different types to arrays
            float[] _dailySetpointsTemperature = new float[_planInfo.TotalDays];
            float[] _dailySetpointsTemperatureHystHigh = new float[_planInfo.TotalDays];
            float[] _dailySetpointsTemperatureHystLow = new float[_planInfo.TotalDays];
            float[] _dailySetpointsHumidity = new float[_planInfo.TotalDays];
            float[] _dailySetpointsHumidityHystHigh = new float[_planInfo.TotalDays];
            float[] _dailySetpointsHumidityHystLow = new float[_planInfo.TotalDays];
            int[] _dailySetpointsTurn = new int[_planInfo.TotalDays];

            int index = 0;
            foreach (PlanTemperature planTemperature in planTemperaturesOrdered)
            {

                for (int i = 0; i < planTemperature.Days; i++, index++)
                {
                    _dailySetpointsTemperature[index] = planTemperature.Set;
                    _dailySetpointsTemperatureHystHigh[index] = planTemperature.HystHigh;
                    _dailySetpointsTemperatureHystLow[index] = planTemperature.HystLow;
                }
                index = 0;
            }
            foreach (PlanHumidity planHumidity in planHumiditiesOrdered)
            {

                for (int i = 0; i < planHumidity.Days; i++, index++)
                {
                    _dailySetpointsHumidity[index] = planHumidity.Set;
                    _dailySetpointsHumidityHystHigh[index] = planHumidity.HystHigh;
                    _dailySetpointsHumidityHystLow[index] = planHumidity.HystLow;
                }
                index = 0;
            }
            foreach (PlanTurn planTurn in planTurnsOrdered)
            {

                for (int i = 0; i < planTurn.Days; i++, index++)
                {
                    _dailySetpointsTurn[index] = planTurn.Set;
                }
                index = 0;
            }



            // Generating complete DailySetpoints object, and adding them to _dailySetpoints list
            for (int i = 1; i<=index; i++, index++)
            {
                DailySetpoints dailySetpoints = new DailySetpoints
                {
                    Day = i,
                    TemperatureSet = _dailySetpointsTemperature[index],
                    TemperatureHystHighSet = _dailySetpointsHumidityHystHigh[index],
                    TemperatureHystLowSet = _dailySetpointsHumidityHystLow[index],
                    HumiditySet = _dailySetpointsHumidity[index],
                    HumidityHystHighSet = _dailySetpointsHumidityHystHigh[index],
                    HumidityHystLowSet = _dailySetpointsHumidityHystLow[index]
                };
                _dailySetpoints.Add(dailySetpoints);
            }

        }


        public DailySetpoints getDailySetpoints(int currentDay)
        {
            return _dailySetpoints.FirstOrDefault(x => x.Day == currentDay);
        }

        // If return 'DailySetpoints' are null, it means the 'plan' is finished
        public DailySetpoints getNextDailySetpoints(int currentDay)
        {
            return _dailySetpoints.FirstOrDefault(x => x.Day == currentDay+1);
        }

        public PlanInfo getPlanInfo()
        {
            return _planInfo;
        }

        public void setInitialValues(int unitId, DateTime launchTime)
        {
             
            _planInfo.UnitId = unitId;
               
            _planInfo.LaunchTime = launchTime;  
            
        }
    }


}
