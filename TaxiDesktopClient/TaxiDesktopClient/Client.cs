using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using YandexAPI.Maps;
using System.Globalization;

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

        public void Close()
        {
            if (clientService!=null)
            {
                clientService.Dispose();
            }
        }

        public bool CreateOrder(string customerName = "", string customerPhone = "", string orderStartAddress = "", string orderStartGeographicalLatitude = "", string orderStartGeographicalLongitude = "",string orderFinishAddress = "", string orderFinishGeographicalLatitude = "", string orderFinishGeographicalLongitude = "", List<int> orderExtraProperty = null)
        {
            int ID = clientService.ServiceProxy.CreateOrder(customerName, customerPhone, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude, orderExtraProperty);
            if (ID != (-1))
            {
                this.clientOrder = new Order(customerName, customerPhone, orderStartAddress, orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishAddress, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude, orderExtraProperty);
                this.clientOrder.OrderID = ID;
                this.clientOrder.OperatorPhone = clientService.ServiceProxy.GetOperatorPhone();
                this.clientOrder.costOfOrder = clientService.ServiceProxy.GetPriceByCoords(orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude);

                return true;
            }
            return false;
        }

        public bool IsTaken()
        {
            if (clientService.ServiceProxy.GetOrderStatus() == 2)
            {
                return true;
            }
            return false;
        }

        public void SetOrderDriverInfo()
        {
            this.clientOrder.DriverName = clientService.ServiceProxy.GetDriverName();
            this.clientOrder.DriverPhone = clientService.ServiceProxy.GetDriverPhone();
        }
        public decimal GetPrice(string orderStartGeographicalLatitude = "", string orderStartGeographicalLongitude = "", string orderFinishGeographicalLatitude = "", string orderFinishGeographicalLongitude = "")
        {
            return clientService.ServiceProxy.GetPriceByCoords(orderStartGeographicalLatitude, orderStartGeographicalLongitude, orderFinishGeographicalLatitude, orderFinishGeographicalLongitude);
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
            clientService.ServiceProxy.AbortOrder();
            this.clientOrder.OrderID = -1;
            this.clientOrder.OrderStatus = -1;
        }

        public string GetDriverPosition()
        {
            Tuple<double, double> Pos = clientService.ServiceProxy.GetDriverPosition();
            this.clientOrder.DriverPosition = Pos.Item2.ToString("G", CultureInfo.InvariantCulture) + "," + Pos.Item1.ToString("G", CultureInfo.InvariantCulture);
            return (this.clientOrder.DriverPosition);
        }

        public string GetStartPosition()
        {
            YandexAPI.Maps.GeoCode geoCode = new GeoCode();
            if (clientOrder.orderStartGeographicalLatitude != "" && clientOrder.orderStartGeographicalLongitude != "")
            {
                //return (clientOrder.orderStartGeographicalLatitude + "," + clientOrder.orderStartGeographicalLongitude);
                return (clientOrder.orderStartGeographicalLongitude + "," + clientOrder.orderStartGeographicalLatitude);
            }
            if (clientOrder.orderStartAddress != "")
            {
                string ResultSearchObject = geoCode.SearchObject(clientOrder.orderStartAddress);
                return (geoCode.GetPoint(ResultSearchObject));
            }
            return ("");
        }
        public string GetFinishPosition()
        {
            YandexAPI.Maps.GeoCode geoCode = new GeoCode();
            if (clientOrder.orderFinishGeographicalLatitude != "" && clientOrder.orderFinishGeographicalLongitude != "")
            {
                //return (clientOrder.orderFinishGeographicalLatitude + "," + clientOrder.orderFinishGeographicalLongitude);
                return (clientOrder.orderFinishGeographicalLongitude + "," + clientOrder.orderFinishGeographicalLatitude);
            }
            if (clientOrder.orderFinishAddress != "")
            {
                string ResultSearchObject = geoCode.SearchObject(clientOrder.orderFinishAddress);
                return (geoCode.GetPoint(ResultSearchObject));
            }
            return ("");
        }
    }
}
