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

        internal int RegisterNewDriver(string login, string password, string name, string phone)
        {
            if (login == null || password == null || name == null || phone == null)
                return -1;
            if (login == "" || password == "" || name == "" || phone == "")
                return -1;
            phone = OrderCreationCtrl.ValidatePhoneNumber(phone);
            if (phone == OrderCreationCtrl.INVALID_PHONE)
                return -1;
            
            //check if login is unique
            model = OrderPoolModel.GetInstance().GetDriverByLogin(login);
            if (model != null)
                return -1; //driver with that login already exists
            model = new DriverInstanceModel(-1, name, login, password, phone, "00.000000", "00.000000", 1, 0);
            int driverID = OrderPoolModel.GetInstance().CreateDriver(model);
            if (driverID > 0)
                model.ForceSetDriverID(driverID);
            return driverID;
        }
    }
}
