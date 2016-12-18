using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using TestingSample.Controller;
using TestingSample.Base;

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
        DriverManagerCtrl driverManager = null;

        public int CreateOrder(string Name, string Phone, double StartGeographicalLatitude, double StartGeographicalLongitude, double FinishGeographicalLatitude, double FinishGeographicalLongitude, List<int> Extra)
        {
            string name = OrderCreationCtrl.ValidateName(Name);
            string phone = OrderCreationCtrl.ValidatePhoneNumber(Phone);
            List<int> options = OrderCreationCtrl.ValidateExtraOptions(Extra);

            if (name == OrderCreationCtrl.INVALID_NAME ||
                phone == OrderCreationCtrl.INVALID_PHONE ||
                options == OrderCreationCtrl.INVALID_OPTIONS)
                return -1;

            int serviceID = OrderCreationCtrl.CreateOrder(name, phone, StartGeographicalLongitude, StartGeographicalLatitude, 
                FinishGeographicalLongitude, FinishGeographicalLatitude, options);
            orderManager = new OrderManagerCtrl(serviceID);
            return serviceID;
        }

        public decimal GetPrice(int OrderID)
        {
            if (orderManager == null)
                throw new Exception("Trying to get price for non-existent order");
            return orderManager.GetPrice();
        }

        public decimal GetPriceByCoords(double lonA, double latA, double lonZ, double latZ)
        {
            return OrderManagerCtrl.GetPriceByAddress(lonA, latA, lonZ, latZ);
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

        public Tuple<double, double> GetDriverPosition(int OrderID)
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

        public int LogDriverIn(String login, String password)
        {
            if (driverManager == null)
                driverManager = new DriverManagerCtrl();
            return driverManager.LogIn(login, password);
        }

        public bool TakeOrder(int orderID)
        {
            if (driverManager == null)
                return false;

            return driverManager.TakeOrder(orderID);
        }

        public bool MarkOrderComplete(int orderID)
        {
            if (driverManager == null)
                return false;
            return driverManager.CompleteOrder();
        }

        public void UpdateDriverPosition(double lon, double lat)
        {
            if (driverManager == null)
                return;
            driverManager.UpdatePosition(lon, lat);
        }
        public List<Tuple<int, double, double, double, double>> GetAvailableOrders()
        {
            if (driverManager == null)
                return null;
            return driverManager.GetAvailableOrders();
        }
    }
}
