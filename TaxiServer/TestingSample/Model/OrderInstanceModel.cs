using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string locA { get; private set; }
        public string locZ { get; private set; }
        public List<String> options { get; private set; }

        public OrderInstanceModel(string name, string phone, string lonA, string latA, string lonZ, string latZ,
            string locA, string locZ, List<string> options)
        {
            this.name = name;
            this.phone = phone;
            this.lonA = lonA;
            this.lonZ = lonZ;
            this.latA = latA;
            this.latZ = latZ;
            this.locA = locA;
            this.locZ = locZ;
            this.options = options;
            OrderPoolModel orders = OrderPoolModel.GetInstance();
            orderID = orders.GenerateOrderID(this);
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

        public int Status()
        {
            return 1;
        }

        internal string DriverPhone()
        {
            return "+790248817612";
        }

        internal string DriverName()
        {
            return "Агазимов Айзегин Асагисянович";
        }

        internal Tuple<float, float> DriverPosition()
        {
            Random rnd = new Random();
            return new Tuple<float, float>((float)rnd.NextDouble() * 300.0f, (float)rnd.NextDouble() * 300.0f);
        }
    }
}
