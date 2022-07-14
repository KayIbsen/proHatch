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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace proHatchApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer sensorTimer = new DispatcherTimer();
        private DispatcherTimer storeSensorValues = new DispatcherTimer();

        // DHT22 Comm variables //
        private const int DHTPIN = 4;
        private IDht dht = null;
        private GpioPin dhtPin = null;

        // Unit info
        private const int UnitId = 1;

        // Client
        private DeviceClient _deviceClient;
        private AppConfig _config;


        // Datapoints
        private double temperature;
        private double humidity;

        

        public MainPage()
        {
            this.InitializeComponent();

            _config = new AppConfig();
            _deviceClient = InitializeDeviceClient(_config.GetSection<AppConfig_DTO>("ConnectionStrings").deviceConnectionString); // get deviceConnection
            

            dhtPin = GpioController.GetDefault().OpenPin(DHTPIN, GpioSharingMode.Exclusive);
            dht = new Dht22(dhtPin, GpioPinDriveMode.Input);

            // setup sensorTimer
            sensorTimer.Interval = TimeSpan.FromSeconds(10);
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
            readSensor();
        }


        private async void readSensor()
        {

            DhtReading reading = await dht.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                temperature = reading.Temperature;
                humidity = reading.Humidity;

                Debug.WriteLine($"temp: {reading.Temperature} C humidity {reading.Humidity}%");
            }

        }

        private async Task SendSensorTelemetryAsync()
        {
            const string telemetryName1 = "temperature";
            //const string telemetryName2 = "humidity";

            string telemetryPayload = $"{telemetryName1} : {temperature}";
            
            var message = new Message(Encoding.UTF8.GetBytes(telemetryPayload))
            {
                ContentEncoding = "utf-8",
                ContentType = "application/json",
            };

            await _deviceClient.SendEventAsync(message);
            Debug.WriteLine("Telemetry has been send");

        }


    }
}