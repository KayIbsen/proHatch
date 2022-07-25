using Microsoft.Azure.Devices.Client;
using proHatchApp.Input;
using proHatchApp.Interfaces;
using proHatchApp.Models;
using proHatchApp.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace proHatchApp
{
    public class Operation : IOperation
    {


        private DispatcherTimer sensorTimer;
        private DispatcherTimer storeSensorValues;


        // DHT22 Comm variables //
        private ISensor _insideDht22Sensor;

        // elegoo Relay 4 channel
        private IOutput _relayOutput;


        // Data Points
        private double _temp;
        private double _humid;

        // Client
        private DeviceClient _deviceClient;

        //Config
        private readonly AppConfig _config;

        private IPlan _plan;

        public Operation(AppConfig config, IPlan plan)
        {
            _config = config;

            _plan = plan;

            InitializeTimers();



            _insideDht22Sensor = new Dht22_Input(4);
            _relayOutput = new Elegoo4Ch_Output(5, 6, 13, 19);


            _deviceClient = InitializeDeviceClient(_config.GetSection<AppConfig_DTO>("ConnectionStrings").deviceConnectionString); // get deviceConnection

          
        }



        public void Start()
        {
            this.sensorTimer.Start();
            this.storeSensorValues.Start();
        }


        public void Stop()
        {
            this.sensorTimer.Stop();
            this.storeSensorValues.Stop();
        }


        private void InitializeTimers()
        {
            // setup sensorTimers
            sensorTimer = new DispatcherTimer();
            sensorTimer.Interval = TimeSpan.FromSeconds(1);
            sensorTimer.Tick += sensorTimer_Tick;
            

            // setup storeSensorValues
            storeSensorValues = new DispatcherTimer();
            storeSensorValues.Interval = TimeSpan.FromSeconds(300);
            storeSensorValues.Tick += storeSensorValues_Tick;
            

        }

        private async void sensorTimer_Tick(object sender, object e)
        {
            Reading_DTO reading = await _insideDht22Sensor.Read();

            if (reading != null)
            {
                _temp = reading.Temperature;
                _humid = reading.Humidity;

                Debug.WriteLine($"temp: {_temp} C humidity {_humid}%");
            }

        }

        private async void storeSensorValues_Tick(object sender, object e)
        {
            await SendSensorTelemetryAsync();
        }


        private async Task SendSensorTelemetryAsync()
        {
            const string telemetryName1 = "temperature";
            //const string telemetryName2 = "humidity";

            string telemetryPayload = $"{telemetryName1} : {_temp}";

            var message = new Message(Encoding.UTF8.GetBytes(telemetryPayload))
            {
                ContentEncoding = "utf-8",
                ContentType = "application/json",
            };

            await _deviceClient.SendEventAsync(message);
            Debug.WriteLine("Telemetry has been send");

        }


        private DeviceClient InitializeDeviceClient(string deviceConnectionString)
        {
            return DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Mqtt, new ClientOptions { });
        }

     
    }
}
