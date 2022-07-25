using proHatchApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace proHatchApp.Input
{
    public class On_Off : IButton
    {
        private readonly GpioPin _btnPin = null;
        private int _btnPinValue;


        public On_Off(int pin)
        {
            _btnPin = GpioController.GetDefault().OpenPin(pin, GpioSharingMode.Exclusive);

            // Check if input pull-up resistors are supported
            if (_btnPin.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                _btnPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                _btnPin.SetDriveMode(GpioPinDriveMode.Input);

            // Set a debounce timeout to filter out switch bounce noise from a button press
            _btnPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);


            updatePinValue();

        }

        

        public int updatePinValue()
        {

            GpioPinValue pinValue = _btnPin.Read();

            //  Low = Manuel / High = Auto
            if (pinValue == GpioPinValue.Low) 
            {
                _btnPinValue = 0;

            }
            else if (pinValue == GpioPinValue.High)
            {
                _btnPinValue = 1;
            }

            return _btnPinValue;

        }



        public GpioPin ReadPin()
        {
            return _btnPin;
        }
    }
}
