using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Devices
{
    /// <summary>
    /// Удаляет устройство.
    /// </summary>
    public class DeleteDeviceRequest
    {
        public string Name { get; set; }
        
    }
}
