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
        private DispatcherTimer sensorTimer = new DispatcherTimer();
        private DispatcherTimer storeSensorValues = new DispatcherTimer();

        // DHT22 Comm variables //
        private ISensor _insideDht22Sensor;

        // elegoo Relay 4 channel
        private IOutput _relayOutput;

        // Unit info
        private const int UnitId = 1;

        // Client
        private DeviceClient _deviceClient;
        private AppConfig _config;

        // Data Points
        private double _temp;
        private double _humid;
        

        public MainPage()
        {
            this.InitializeComponent();

            _config = new AppConfig();
            _deviceClient = InitializeDeviceClient(_config.GetSection<AppConfig_DTO>("ConnectionStrings").deviceConnectionString); // get deviceConnection

            _insideDht22Sensor = new Dht22_Input(4);
            _relayOutput = new Elegoo4Ch_Output(5, 6, 13, 19);


            // setup sensorTimer
            sensorTimer.Interval = TimeSpan.FromSeconds(1);
            sensorTimer.Tick += sensorTimer_Tick;
            sensorTimer.Start();

            // setup storeSensorValues
            storeSensorValues.Interval = TimeSpan.FromSeconds(300);
            storeSensorValues.Tick += storeSensorValues_Tick;
            storeSensorValues.Start();

           

        }
        
        private async void storeSensorValues_Tick(object sender, object e)
        {
            await SendSensorTelemetryAsync();
        }

        private DeviceClient InitializeDeviceClient(string deviceConnectionString)
        {
            return DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Mqtt, new ClientOptions { });
        }

        private async void sensorTimer_Tick(object sender, object e)
        {
            Reading_DTO reading = await _insideDht22Sensor.Read();
            
            if(reading != null)
            {
                _temp = reading.Temperature;
                _humid = reading.Humidity;

                Debug.WriteLine($"temp: {_temp} C humidity {_humid}%");
            }
            
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


        private void TestRelay()
        {
            while (true) 
            {

                bool state = true;

                for (int i = 0; i == 4; i++)
                {
                    //change relay 
                    

                }



            }

        }


    }
}