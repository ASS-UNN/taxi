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

        public static decimal GetPriceByAddress(double lonA, double latA, double lonZ, double latZ)
        {
            Random rnd = new Random();
            return (decimal)(Math.Abs((lonA + 1) * (latA + 1) * (lonZ + 1) * (latZ + 1)) * rnd.NextDouble() * 100);
        }
    }
}
