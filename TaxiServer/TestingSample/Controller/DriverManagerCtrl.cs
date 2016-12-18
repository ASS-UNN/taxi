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
            return (model = DriverInstanceModel.GetDriverByLogin(login, pwd)).driverID;
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

        internal void UpdatePosition(double lon, double lat)
        {
            if (model.driverID > 0)
                if (Math.Abs(lon - model.coord1) > CONST.POSITION_EPSILON ||
                    Math.Abs(lat - model.coord2) > CONST.POSITION_EPSILON)
                {
                    OrderPoolModel.GetInstance().UpdateDriverPosition(model.driverID, lon, lat);
                }
        }

        internal List<Tuple<int, double, double, double, double>> GetAvailableOrders()
        {
            if (model.driverID < 0)
                return null;

            return OrderPoolModel.GetInstance().GetAvailableOrders();
        }
    }
}
