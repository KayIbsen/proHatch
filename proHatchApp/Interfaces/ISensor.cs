using proHatchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proHatchApp.Interfaces
{
    public interface ISensor
    {

        Task<Reading_DTO> Read();


    }
}
