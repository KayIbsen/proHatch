using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Sensors.Dht;
using Windows.Devices.Gpio;
using System.Diagnostics;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using System.Text;
using proHatchApp.Interfaces;
using proHatchApp.Input;
using proHatchApp.Models;
using proHatchApp.Output;

namespace proHatchApp
{
    public sealed partial class MainPage : Page
    {

        private AppConfig _config;



        // MAN btn
        private IButton _MAN_btn;
        public enum OperationMode { MANUAL = 0, AUTO = 1};
        OperationMode Mode;


        // Unit info
        private const int _unitId = 1;


        private IOperation _operation;

        public MainPage()
        {
            this.InitializeComponent();
            _config = new AppConfig();




            // initialize auto button + check if ON
            _MAN_btn = new On_Off(18);
            GpioPin MAN_gpioPin = _MAN_btn.ReadPin();
            MAN_gpioPin.ValueChanged += _btnPin_ValueChanged;
            Mode = (OperationMode)_MAN_btn.updatePinValue();

            InitializeOperation();

        }


        private void _btnPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            _MAN_btn.updatePinValue();
            Mode = (OperationMode)_MAN_btn.updatePinValue();
           
            InitializeOperation();
        }




        private void InitializeOperation()
        {

            

            if (Mode == OperationMode.AUTO)
            {
                // >>>>>>>>>   check for any 'IsActive'

                //IPlan plan = DefaultPlans.GetDefaultPlan();
                //plan.setInitialValues(_unitId, DateTime.Now);

                IPlan plan = new Plan(
                "5d72040c-03cc-4301-a2ff-0594b26916a3",
                _unitId,
                "Chicken",
                1,
                DateTime.Now,
                21,
                new List<PlanTemperature> { new PlanTemperature() {
                    Id = "0912cbdf-e05b-4584-81ce-562b71c46d96",
                    PlanId = "5d72040c-03cc-4301-a2ff-0594b26916a3",
                    Order = 1,
                    Days = 21,
                    Set = 38.00F,
                    HystHigh = 0.8F,
                    HystLow = 1.8F }
                },
                new List<PlanHumidity> { new PlanHumidity()
                {
                    Id = "a2045c57-ae77-49b8-8189-e09970bb552c",
                    PlanId = "5d72040c-03cc-4301-a2ff-0594b26916a3",
                    Order = 1,
                    Days = 17,
                    Set = 52.00F,
                    HystHigh = 3.00F,
                    HystLow = 2.00F
                },
                new PlanHumidity()
                {
                    Id = "6661dd0f-b6e0-48d8-9447-bed1d95fcdec",
                    PlanId = "5d72040c-03cc-4301-a2ff-0594b26916a3",
                    Order = 2,
                    Days = 4,
                    Set = 70.00F,
                    HystHigh = 2.00F,
                    HystLow = 2.00F
                }
                },
                new List<PlanTurn>()
                {
                    new PlanTurn()
                    {
                        Id = "a8ba27b7-6851-42eb-9a35-a97d060d7bc1",
                        PlanId = "5d72040c-03cc-4301-a2ff-0594b26916a3",
                        Order = 1,
                        Days = 17,
                        Set = 6
                    },
                    new PlanTurn()
                    {
                        Id = "2e6310c1-8f4f-421d-8c96-dfee368b06a2",
                        PlanId = "5d72040c-03cc-4301-a2ff-0594b26916a3",
                        Order = 1,
                        Days = 4,
                        Set = 0
                    }
                });



                _operation = new Operation(_config , plan);
                _operation.Start();
            }
            else if (Mode == OperationMode.MANUAL)
            {

            }

        }

       










    }
}