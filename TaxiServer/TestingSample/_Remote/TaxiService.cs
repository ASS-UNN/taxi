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

        public int CreateOrder(string Name, string Phone, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<int> Extra)
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

        public decimal GetPrice()
        {
            if (orderManager == null)
                return -1;
            return orderManager.GetPrice();
        }

        public decimal GetPriceByCoords(string lonA, string latA, string lonZ, string latZ)
        {
            return OrderManagerCtrl.GetPriceByAddress(lonA, latA, lonZ, latZ);
        }

        public string GetOperatorPhone()
        {
            if (orderManager == null)
                return null;
            return orderManager.GetOperatorPhone();
        }

        public int GetOrderStatus()
        {
            if (orderManager == null)
                return -1;
            return orderManager.GetStatus();
        }

        public string GetDriverPhone()
        {
            if (orderManager == null)
                return null;
            return orderManager.GetDriverPhone();
        }

        public string GetDriverName()
        {
            if (orderManager == null)
                return null;
            return orderManager.GetDriverName();
        }

        public Tuple<double, double> GetDriverPosition()
        {
            if (orderManager == null)
                return null;
            return orderManager.GetDriverPosition();
        }

        public void AbortOrder()
        {
            if (orderManager == null)
                return;
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

        public bool MarkOrderComplete()
        {
            if (driverManager == null)
                return false;
            return driverManager.CompleteOrder();
        }

        public void UpdateDriverPosition(string lon, string lat)
        {
            if (driverManager == null)
                return;
            driverManager.UpdatePosition(lon, lat);
        }
        public List<Tuple<int, string, string, string, string>> GetAvailableOrders()
        {
            if (driverManager == null)
                return null;
            return driverManager.GetAvailableOrders();
        }
        public int GetDriverRating()
        {
            if (orderManager == null)
                return -1;
            return orderManager.GetDriverRating();
        }
        public int UpdateDriverRating(int amount)
        {
            if (orderManager == null)
                return -1;
            return orderManager.UpdateDriverRating(amount);
        }
        public int RegisterNewDriver(string name, string phone, string login, string password)
        {
            if (driverManager == null)
                driverManager = new DriverManagerCtrl();
            return driverManager.RegisterNewDriver(login, password, name, phone);
        }
    }
}
