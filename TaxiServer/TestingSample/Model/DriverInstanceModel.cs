using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingSample.Base;

namespace TestingSample.Model
{
    class DriverInstanceModel
    {
        public int driverID { get; private set; }
        public String name { get; private set; }
        public String login { get; private set; }
        public String password { get; private set; }
        public string coord1 { get; set; }
        public string coord2 { get; set; }
        public int status { get; set; }
        public String phone { get; private set; }
        public int rating { get; private set; }

        public DriverInstanceModel(int id, String Name, String Login, String Password, String Phone, string c1, string c2, int Status, int Rating)
        {
            driverID = id;
            name = Name;
            login = Login;
            password = Password;
            coord1 = c1;
            coord2 = c2;
            status = Status;
            phone = Phone;
            rating = Rating;
        }

        public Dictionary<String, String> Context()
        {
            return new Dictionary<String, String>()
            {
                {CONST.DRIVER_NAME, name},
                {CONST.PHONE,     phone},
                {CONST.COORD1,    Convert.ToString(coord1)},
                {CONST.COORD2,    Convert.ToString(coord2)},
                {CONST.PASSWORD,  password},
                {CONST.LOGIN,     login},
                {CONST.STATUS,    status.ToString()},
                {CONST.RATING,    rating.ToString()}
            };
        }

        public static DriverInstanceModel GetDriverByLogin(String username, String pwd)
        {
            return OrderPoolModel.GetInstance().GetDriverByLogin(username, pwd);
        }

        public void ForceSetDriverID(int id)
        {
            driverID = id;
        }
    }
}
