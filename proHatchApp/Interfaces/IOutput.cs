using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Interfaces
{
    public interface IOutput
    {
        void ChangeState(int relayNumber, bool state);
    }
}
