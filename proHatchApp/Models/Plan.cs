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
        private string _id { get; set; }
        private int _unitId { get; set; }
        private string _name { get; set; }
        private byte _active { get; set; }
        private DateTime _beginDate { get; set; }
        public int Days { get; set; }
        private List<PlanTemperature> _planTemperatures { get; set; }
        private List<PlanHumidity> _planHumidities { get; set; }
        private List<PlanTurn> _planTurns { get; set; }

        private List<DailySetpoints> _dailySetpoints { get; set; }


        public Plan(string id, int unitId, string name, byte active, DateTime beginDate, int days, List<PlanTemperature> planTemperatures, List<PlanHumidity> planHumidities, List<PlanTurn> planTurns)
        {
            _id = id;
            _unitId = unitId;
            _name = name;
            _active = active;
            _beginDate = beginDate;
            

            // Order Lists 
            IOrderedEnumerable<PlanTemperature> planTemperaturesOrdered = planTemperatures.OrderBy(x => x.Order);
            IOrderedEnumerable<PlanHumidity> planHumiditiesOrdered = planHumidities.OrderBy(x => x.Order);
            IOrderedEnumerable<PlanTurn> planTurnsOrdered = planTurns.OrderBy(x => x.Order);

            
        
            // Adding daily setpoints of different types to arrays
            float[] _dailySetpointsTemperature = new float[days];
            float[] _dailySetpointsTemperatureHystHigh = new float[days];
            float[] _dailySetpointsTemperatureHystLow = new float[days];
            float[] _dailySetpointsHumidity = new float[days];
            float[] _dailySetpointsHumidityHystHigh = new float[days];
            float[] _dailySetpointsHumidityHystLow = new float[days];
            int[] _dailySetpointsTurn = new int[days];

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


        public DailySetpoints getDailySetpoints()
        {
            throw new NotImplementedException();
        }




    }


}
