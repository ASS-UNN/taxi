using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TestingSample.Base;

namespace TestingSample.Model
{
    public class OrderInstanceModel
    {
        public int orderID { get; private set; }
        public string name { get; private set; }
        public string phone { get; private set; }
        public string lonA { get; private set; }
        public string latA { get; private set; }
        public string lonZ { get; private set; }
        public string latZ { get; private set; }
        public int status { get; private set; }
        public List<int> options { get; private set; }

        public OrderInstanceModel(string name, string phone, string lonA, string latA, string lonZ, string latZ,
            List<int> options)
        {
            this.name = name;
            this.phone = phone;
            this.lonA = lonA;
            this.lonZ = lonZ;
            this.latA = latA;
            this.latZ = latZ;
            this.options = options;
        }

        public Dictionary<String, String> Context()
        {
            return new Dictionary<String, String>()
            {
                {CONST.CUST_NAME, name},
                {CONST.PHONE,     phone},
                {CONST.COORD1,    Convert.ToString(lonA)},
                {CONST.COORD2,    Convert.ToString(latA)},
                {CONST.COORD3,    Convert.ToString(lonZ)},
                {CONST.COORD4,    Convert.ToString(latZ)},
                {CONST.STATUS,    status.ToString()}
            };
        }

        public decimal CalculatePrice()
        {
            Random rnd = new Random();
            return (decimal)(30 + (int)(rnd.NextDouble()*10));
        }

        public string OperatorPhone()
        {
            return "+79114229617";
        }

        public void SetStatus(int st)
        {
            status = st;
        }

        public void ForceSetOrderID(int _id)
        {
            orderID = _id;
        }

        public string DriverPhone()
        {
            return (string)OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.PHONE, orderID);
        }

        public string DriverName()
        {
            return (string)OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.DRIVER_NAME, orderID);
        }

        public Tuple<double, double> DriverPosition()
        {
            Object coord1 = OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.COORD1, orderID);
            Object coord2 = OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.COORD2, orderID);
            double c1 = Convert.ToDouble(coord1, CultureInfo.InvariantCulture);
            double c2 = Convert.ToDouble(coord2, CultureInfo.InvariantCulture);
            return new Tuple<double, double>(c1, c2);
        }

        internal int GetStatus()
        {
            return OrderPoolModel.GetInstance().GetOrderStatus(orderID);
        }

        internal int GetDriverRating()
        {
            return Convert.ToInt32(OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.RATING, orderID));
        }

        internal int DecreaseDriverRating()
        {
            return OrderPoolModel.GetInstance().DecreaseDriverStatus(orderID);
        }

        internal int IncreaseDriverRating()
        {
            return OrderPoolModel.GetInstance().IncreaseDriverStatus(orderID);
        }
    }
}
