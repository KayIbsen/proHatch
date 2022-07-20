﻿using proHatchApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace proHatchApp.Output
{
    public class Elegoo4Ch_Output : IOutput
    {
        private int[] _relayPins = null;

        private GpioPin _relay1;
        private GpioPin _relay2;
        private GpioPin _relay3;
        private GpioPin _relay4;

        public Elegoo4Ch_Output(int gpioPinRelay1, int gpioPinRelay2, int gpioPinRelay3, int gpioPinRelay4)
        {
            _relayPins = new int[4] { gpioPinRelay1, gpioPinRelay2, gpioPinRelay3, gpioPinRelay4 }; 

            var gpioController = GpioController.GetDefault();

            _relay1 = GpioController.GetDefault().OpenPin(gpioPinRelay1, GpioSharingMode.Exclusive);
            _relay1.SetDriveMode(GpioPinDriveMode.Output);
            _relay2 = GpioController.GetDefault().OpenPin(gpioPinRelay2, GpioSharingMode.Exclusive);
            _relay2.SetDriveMode(GpioPinDriveMode.Output);
            _relay3 = GpioController.GetDefault().OpenPin(gpioPinRelay3, GpioSharingMode.Exclusive);
            _relay3.SetDriveMode(GpioPinDriveMode.Output);
            _relay4 = GpioController.GetDefault().OpenPin(gpioPinRelay4, GpioSharingMode.Exclusive);
            _relay4.SetDriveMode(GpioPinDriveMode.Output);

        }


        public void ChangeState(int relayNumber, bool state)
        {

            GpioPinValue gpioPinValue = (state ? GpioPinValue.Low : GpioPinValue.High);


            //_relayPins[relayNumber + 1].Write(gpioPinValue);

        }



    }
}
