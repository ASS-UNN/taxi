using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSample.Model;
using TestingSample.Base;

namespace TestingSample.Controller
{
    class OrderManagerCtrl
    {
        private OrderInstanceModel model;

        public OrderManagerCtrl(int orderID)
        {
            model = OrderPoolModel.GetInstance().GetOrderByID(orderID);
        }
        public OrderInstanceModel Model()
        {
            return model;
        }
        public decimal GetPrice()
        {
            return model.CalculatePrice();
        }
        public string GetOperatorPhone()
        {
            return model.OperatorPhone();
        }

        public int GetStatus()
        {
            return model.GetStatus();
        }

        public string GetDriverPhone()
        {
            return model.DriverPhone();
        }

        public string GetDriverName()
        {
            return model.DriverName();
        }

        public Tuple<double, double> GetDriverPosition()
        {
            return model.DriverPosition();
        }

        public void AbortOrder()
        {
            OrderPoolModel.GetInstance().UpdateOrderStatus(model.orderID, CONST.ORDER_STATUS_ABORTED);
        }

        public static decimal GetPriceByAddress(string lonA, string latA, string lonZ, string latZ)
        {
            Random rnd = new Random();
            return (decimal)(Math.Abs((lonA.Length + 1) * (latA.Length + 1) * (lonZ.Length + 1) * (latZ.Length + 1)) * rnd.NextDouble() * 100);
        }

        internal int GetDriverRating()
        {
            return model.GetDriverRating();
        }

        internal int UpdateDriverRating(int amount)
        {
            if (amount == 0)
                return -1;
            if (amount < 0)
                return model.DecreaseDriverRating();
            else
                return model.IncreaseDriverRating();
        }
    }
}
