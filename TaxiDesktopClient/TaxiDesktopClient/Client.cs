using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;

namespace TaxiDesktopClient
{
    class Client
    {
        private Order clientOrder { get; set; }
        private IScsServiceClient<ITaxiService> clientService { get; set; }

        public Client()
        {
            clientOrder=null;
            clientService=null;
        }

        public bool Connect(string IP, int Port)
        {
            clientService = ScsServiceClientBuilder.CreateClient<ITaxiService>(new ScsTcpEndPoint(IP, Port));
            try
            {
                clientService.Connect();
            }
            catch
            {
                clientService.Dispose();
                return false;
            }
            return true;
        }

        public bool CreateOrder(string customerName = "", string customerPhone = "", string orderStartAddress = "", string orderStartGeographicalLatitude = "", string orderStartGeographicalLongitude = "",string orderFinishAddress = "", string orderFinishGeographicalLatitude = "", string orderFinishGeographicalLongitude = "", List<string> orderExtraProperty = null)
        {
            int ID = clientService.ServiceProxy.CreateOrder(customerName, customerPhone, orderStartAddress, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishAddress, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude, orderExtraProperty);
            if (ID != (-1))
            {
                this.clientOrder = new Order(customerName, customerPhone, orderStartAddress, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishAddress, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude, orderExtraProperty);
                this.clientOrder.OrderID = ID;
                this.clientOrder.OperatorPhone = clientService.ServiceProxy.GetOperatorPhone();
                this.clientOrder.costOfOrder = clientService.ServiceProxy.GetPrice(orderStartAddress, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishAddress, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude);
                return true;
            }
            return false;
        }

        public bool IsTaken()
        {
            if (clientService.ServiceProxy.GetOrderStatus(this.clientOrder.OrderID) != (-1))
            {
                return true;
            }
            return false;
        }

        public void SetOrderDriverInfo()
        {
            this.clientOrder.DriverName = clientService.ServiceProxy.GetDriverName(this.clientOrder.OrderID);
            this.clientOrder.DriverPhone = clientService.ServiceProxy.GetDriverPhone(this.clientOrder.OrderID);
        }
        public decimal GetPrice(string orderStartAddress = "", string orderStartGeographicalLatitude = "", string orderStartGeographicalLongitude = "", string orderFinishAddress = "", string orderFinishGeographicalLatitude = "", string orderFinishGeographicalLongitude = "")
        {
            return clientService.ServiceProxy.GetPrice(orderStartAddress, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishAddress, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude);
        }

        public string GetDriverName()
        {
            return this.clientOrder.DriverName;
        }

        public string GetDriverPhone()
        {
            return this.clientOrder.DriverPhone;
        }

        public string GetOperatorPhone()
        {
            return this.clientOrder.OperatorPhone;
        }

        public void AbortOrder()
        {
            clientService.ServiceProxy.AbortOrder(this.clientOrder.OrderID);
            this.clientOrder.OrderID = -1;
            this.clientOrder.OrderStatus = -1;
        }

        public Tuple<float,float> GetDriverPossition()
        {
            this.clientOrder.DriverPosition = clientService.ServiceProxy.GetDriverPosition(this.clientOrder.OrderID);
            return (this.clientOrder.DriverPosition);
        }
    }
}
