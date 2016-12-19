using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSample.Model;
using TestingSample.Base;

namespace TestingSample.Controller
{
    class DriverManagerCtrl
    {
        private DriverInstanceModel model;

        public int LogIn(String login, String pwd)
        {
            if (login == null || login.Equals("") || pwd == null || pwd.Equals(""))
            {
                return -1;
            }
            model = DriverInstanceModel.GetDriverByLogin(login, pwd);
            if (model == null)
                return -1;
            else return model.driverID;
        }

        public bool TakeOrder(int orderID)
        {
            if (model.driverID < 0)
                return false;
            return OrderPoolModel.GetInstance().TieDriverAndOrder(model.driverID, orderID);
        }

        internal bool CompleteOrder()
        {
            if (model.driverID < 0)
                return false;
            return OrderPoolModel.GetInstance().CompleteOrder(model.driverID);
        }

        internal void UpdatePosition(string lon, string lat)
        {
            if (model.driverID > 0)
                OrderPoolModel.GetInstance().UpdateDriverPosition(model.driverID, lon, lat);
        }

        internal List<Tuple<int, string, string, string, string>> GetAvailableOrders()
        {
            if (model.driverID < 0)
                return null;

            return OrderPoolModel.GetInstance().GetAvailableOrders();
        }
    }
}
