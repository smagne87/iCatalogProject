using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iCatalogBB;

namespace iCatalogSite.Models
{
    public class DeviceModel
    {
        public int IdDevice { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceDescription { get; set; }
        public DateTime? LastSync { get; set; }
        private BBDevice _devicesContext;

        public DeviceModel()
        {
            _devicesContext = new BBDevice();
        }

        public List<Device> GetAllDevices()
        {
            return _devicesContext.GetAllDevices();
        }
    }
}
