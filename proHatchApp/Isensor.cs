using proHatchApp.Models;
using System.Threading.Tasks;

namespace proHatchApp
{
    public interface Isensor
    {

        Task<bool> IsConnected { get; }

        Task<SensorReading_DTO> Read();

     

    }
}
