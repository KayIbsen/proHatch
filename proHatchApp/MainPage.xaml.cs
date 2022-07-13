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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace proHatchApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer sensorTimer = new DispatcherTimer();

        // DHT22 Comm variables //
        private const int DHTPIN = 4;
        private IDht dht = null;
        private GpioPin dhtPin = null;


        public MainPage()
        {
            this.InitializeComponent();

            dhtPin = GpioController.GetDefault().OpenPin(DHTPIN, GpioSharingMode.Exclusive);
            dht = new Dht22(dhtPin, GpioPinDriveMode.Input);


            sensorTimer.Interval = TimeSpan.FromSeconds(1);
            sensorTimer.Tick += sensorTimer_Tick;
            sensorTimer.Start();
        }


        private void sensorTimer_Tick(object sender, object e)
        {
            readSensor();
        }


        private async void readSensor()
        {
            double temp = 0;
            double humidity = 0;


            DhtReading reading = await dht.GetReadingAsync().AsTask();

            if (reading.IsValid)
            {
                temp = reading.Temperature;
                humidity = reading.Humidity;

                Debug.WriteLine($"temp: {temp} C humidity {humidity}%");
            }

        }

    }
}