using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace proHatchApp.Interfaces
{
    public interface IButton
    {

        int updatePinValue();

        GpioPin ReadPin();

    }
}
