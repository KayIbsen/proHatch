using proHatchApp.Interfaces;
using proHatchApp.Models;
using Sensors.Dht;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace proHatchApp.Input
{
    public class Dht22_Input : ISensor
    {
        private IDht _dht = null;
        private GpioPin _dhtPin = null;


        public Dht22_Input(int PinNumber)
        {

            _dhtPin = GpioController.GetDefault().OpenPin(PinNumber, GpioSharingMode.Exclusive);
            _dht = new Dht22(_dhtPin, GpioPinDriveMode.Input);

        }

        public async Task<Reading_DTO> Read()
        {
            DhtReading reading = await _dht.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                return new Reading_DTO { Temperature=reading.Temperature, Humidity=reading.Humidity };

            }
            else
            {
                return null;
            }

        }


    }
}
