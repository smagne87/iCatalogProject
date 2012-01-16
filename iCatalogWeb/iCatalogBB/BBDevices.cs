using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository = iCatalogData.iCatalogdbDataContext;
using RepositoryDevice = iCatalogData.Device;

namespace iCatalogBB
{
    public class BBDevice
    {
        public void DeleteDevice(int idDevice)
        {
            try
            {
                using (Repository r = new Repository())
                {
                    RepositoryDevice c = r.Devices.Where<RepositoryDevice>(co => co.IdDevice.Equals(idDevice)).SingleOrDefault();
                    c.IsAssociated = false;
                    r.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Device> GetAllDevices()
        {
            try
            {
                using (Repository r = new Repository())
                {
                    List<Device> lst = new List<Device>();
                    foreach (RepositoryDevice aDevice in r.Devices.ToList())
                    {
                        lst.Add(new Device() { DeviceCode = aDevice.DeviceCode, DeviceDescription = aDevice.DeviceDescription, IdDevice = (int)aDevice.IdDevice, LastSync = aDevice.LastSync });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Device
    {
        public int IdDevice { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceDescription { get; set; }
        public DateTime? LastSync { get; set; }
    }
}
