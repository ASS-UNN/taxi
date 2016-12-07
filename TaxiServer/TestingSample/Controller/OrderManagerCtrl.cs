using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSample.Model;

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
            return model.Status();
        }

        public string GetDriverPhone()
        {
            return model.DriverPhone();
        }

        public string GetDriverName()
        {
            return model.DriverName();
        }

        public Tuple<float, float> GetDriverPosition()
        {
            return model.DriverPosition();
        }

        public void AbortOrder()
        {
            OrderPoolModel.GetInstance().RemoveOrder(model);
        }
    }
}
