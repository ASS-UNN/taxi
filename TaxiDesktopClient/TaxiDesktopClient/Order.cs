using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxiDesktopClient
{
    public class Order
    {
        public int OrderID { get; set; }
        public int OrderStatus { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string orderStartAddress{ get; set; }
        public string orderFinishAddress { get; set; }
        public string orderStartGeographicalLatitude { get; set; }
        public string orderStartGeographicalLongitude { get; set; }
        public string orderFinishGeographicalLatitude { get; set; }
        public string orderFinishGeographicalLongitude { get; set; }
        public List<string> orderExtraProperty { get; set; }
        public float costOfOrder { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string OperatorPhone { get; set; }
        public Tuple<float,float> DriverPosition {get; set;}

        public Order(string customerName = "", string customerPhone = "", string orderStartAddress="", string orderStartGeographicalLatitude = "", string orderStartGeographicalLongitude = "",
            string orderFinishAddress="", string orderFinishGeographicalLatitude = "", string orderFinishGeographicalLongitude = "", List<string> orderExtraProperty = null)
        {     
            this.customerName = customerName;
            this.customerPhone = customerPhone;
            this.orderStartAddress=orderStartAddress;
            this.orderStartGeographicalLatitude = orderStartGeographicalLatitude;
            this.orderStartGeographicalLongitude = orderStartGeographicalLongitude;
            this.orderFinishAddress=orderFinishAddress;
            this.orderFinishGeographicalLatitude = orderFinishGeographicalLatitude;
            this.orderFinishGeographicalLongitude = orderFinishGeographicalLongitude;
            this.orderExtraProperty = orderExtraProperty;
            this.OrderID=-1;
            this.OrderStatus = -1;
        }

        /*
        public void SetID(int ID)
        {
            this.OrderID = ID;
        }
         */
    }
}
