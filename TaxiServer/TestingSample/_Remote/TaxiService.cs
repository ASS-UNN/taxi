using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using TestingSample.Controller;

namespace TestingSample
{
    /// <summary>
    /// A general purpose controller class to handle ALL requests from client instances.
    /// Please, refrain from creating additional methods in this class.
    /// Use dedicated controllers and model classes instead whenever possible.
    /// </summary>
    class TaxiService : ScsService, ITaxiService
    {
        OrderManagerCtrl orderManager = null;

        public int CreateOrder(string Name, string Phone, string StartAddress, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishAddress, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<string> Extra)
        {
            string name = OrderCreationCtrl.ValidateName(Name);
            string phone = OrderCreationCtrl.ValidatePhoneNumber(Phone);
            List<string> options = OrderCreationCtrl.ValidateExtraOptions(Extra);

            if (name == OrderCreationCtrl.INVALID_NAME ||
                phone == OrderCreationCtrl.INVALID_PHONE ||
                options == OrderCreationCtrl.INVALID_OPTIONS)
                return -1;

            int serviceID = OrderCreationCtrl.CreateOrder(name, phone, StartGeographicalLongitude, StartGeographicalLatitude, 
                FinishGeographicalLongitude, FinishGeographicalLatitude, StartAddress, FinishAddress, options);
            orderManager = new OrderManagerCtrl(serviceID);
            return serviceID;
        }

        public decimal GetPrice(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get price for non-existent order");
            return orderManager.GetPrice();
        }

        public string GetOperatorPhone()
        {
            if (orderManager == null)
                throw new Exception("Trying to get operator phone number from non-existent order");
            return orderManager.GetOperatorPhone();
        }

        public int GetOrderStatus(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get order status for non-existent order");
            return orderManager.GetStatus();
        }

        public string GetDriverPhone(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get driver phone number from non-existent order");
            return orderManager.GetDriverPhone();
        }

        public string GetDriverName(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get driver name from non-existent order");
            return orderManager.GetDriverName();
        }

        public Tuple<float, float> GetDriverPosition(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get driver position from non-existent order");
            return orderManager.GetDriverPosition();
        }

        public void AbortOrder(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to abort non-existent order");
            orderManager.AbortOrder();
            orderManager = null;
        }
    }
}
