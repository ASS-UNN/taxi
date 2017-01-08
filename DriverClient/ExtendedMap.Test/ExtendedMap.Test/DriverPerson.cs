using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using System.Globalization;

namespace ExtendedMap.Test
{
    public class DriverPerson
    {
        public string driverLogin="";
        public string driverPassword="";
        public double locationX;
        public double locationY;
        public bool isEnd = false;
        public List<Tuple<int, string, string, string, string>> listOfOrder = null;
        public Tuple<double, double> finishPointOfOrder;
        public bool isConnect = false;
        public IScsServiceClient<ITaxiService> driverService { get; set; }
        public string stringOfConnection;
        public DriverPerson(double locationX=0, double locationY=0)
        {
            finishPointOfOrder = new Tuple<double, double>(0,0);
            this.locationX = locationX;
            this.locationY = locationY;
            isConnect = connectToServer("5.164.251.145", 4040);
            if (!isConnect)
            {
                repeatConnect();
                isConnect = true;
            }
            this.stringOfConnection = driverService.CommunicationState.ToString();
        }
        public void updateLocationOfDriver(double newlocationX, double newlocationY)
        {
            this.locationX = newlocationX;
            this.locationY = newlocationY;
        }
        public void enterLogin(string login)
        {
            this.driverLogin = login;
        }
        public void enterPassword(string password)
        {
            this.driverPassword = password;
        }
        public int loginingToServer(string login, string password)
        {
            if ( driverService.ServiceProxy.LogDriverIn(login, password) != -1) // Проверка через scs существования пользователя
            {
                return 1;
            }
            return -1;
        }
        public void putOrder()
        {
            //строка взятия заказа в scs 
        }

        public int registrationNewDriverOnTheServer(string name, string phone, string login, string password)
        {
            int resultOfRegistration = 0;
            resultOfRegistration = driverService.ServiceProxy.RegisterNewDriver(name, phone, login, password);
            return resultOfRegistration;
        }

        public bool connectToServer(string IP, int Port)
        {
            driverService = ScsServiceClientBuilder.CreateClient<ITaxiService>(new ScsTcpEndPoint(IP, Port));
            try
            {
                driverService.Connect();
            }
            catch
            {
                driverService.Disconnect();
                return false;
            }
            return true;
        }
        private void changeConnectStatus()
        {
            if (this.driverService.CommunicationState.ToString() != "Connected")
            {
                isConnect = false;
            }
        }
        public void repeatConnect()
        {
            while (this.driverService.CommunicationState.ToString() != "Connected")
            {
                try
                {
                    driverService.Connect();
                }
                catch
                {
                    driverService.Disconnect();
                }
                // через контроллер вот тут обращаемся к форме и говорим что нет коннекта
            }
        }
        public double getDriverLocationX()
        {
            return this.locationX;
        }
        public double getDriverLocationY()
        {
            return this.locationY;
        }
        public void updateListOfOrder()
        {
            listOfOrder = driverService.ServiceProxy.GetAvailableOrders();
        }
        public bool takeNewOrder(int OrderID)
        {
            return driverService.ServiceProxy.TakeOrder(OrderID);
        }
        public bool endCurrentOrder()
        {
            return driverService.ServiceProxy.MarkOrderComplete();
        }
        public bool haveFinishOrder()
        {
            double endPart = GetDistanceBetweenPoints(locationX, locationY, finishPointOfOrder.Item1, finishPointOfOrder.Item2);
            if (endPart < 500)
            {
                driverService.ServiceProxy.MarkOrderComplete();
                finishPointOfOrder = new Tuple<double, double>(0, 0);
                return true;
            }
            return false;
        }

        public void endOrderAboutButton()
        {
            driverService.ServiceProxy.MarkOrderComplete();
            finishPointOfOrder = new Tuple<double, double>(0, 0);
        }
        public void updateDriverLocation()
        {
            string first = Convert.ToString(locationX, CultureInfo.InvariantCulture);
            string second = Convert.ToString(locationY, CultureInfo.InvariantCulture);
            driverService.ServiceProxy.UpdateDriverPosition(first, second);
        }

        public double GetDistanceBetweenPoints(double lat1, double long1, double lat2, double long2)
        {
            double distance = 0;

            double dLat = (lat2 - lat1) / 180 * Math.PI;
            double dLong = (long2 - long1) / 180 * Math.PI;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(lat1 / 180 * Math.PI) * Math.Cos(lat2 / 180 * Math.PI)
                        * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Calculate radius of earth
            // For this you can assume any of the two points.
            double radiusE = 6378135; // Equatorial radius, in metres
            double radiusP = 6356750; // Polar Radius

            //Numerator part of function
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(lat1 / 180 * Math.PI), 2);
            //Denominator part of the function
            double dr = Math.Pow(radiusE * Math.Cos(lat1 / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(lat1 / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);

            //Calculate distance in meters.
            distance = radius * c;
            return distance; // distance in meters
        }


    }
}
