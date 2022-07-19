using proHatchApp.Models;
using Sensors.Dht;
using System;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace proHatchApp.Sensors
{
    public class Sensor_Dht22 : Isensor
    {
        private readonly IDht _dht = null;
        private readonly int _pinNumber;
        private readonly GpioPin _dhtPin = null;
        
        //private double temp;
        //private double humid;



        public Sensor_Dht22(int pinNumber)
        {
            _pinNumber = pinNumber;
            _dhtPin = GpioController.GetDefault().OpenPin(_pinNumber, GpioSharingMode.Exclusive);
            _dht = new Dht22(_dhtPin, GpioPinDriveMode.Input);

        }


        public Task<bool> IsConnected => throw new NotImplementedException();

        public async Task<SensorReading_DTO> Read()
        {

            DhtReading reading = await _dht.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                //temp = reading.Temperature;
                //humid = reading.Humidity;

                return new SensorReading_DTO { Temperature=reading.Temperature, Humidity=reading.Humidity};

            }
            else
            {
                return null;
            }



        }
    }
}
